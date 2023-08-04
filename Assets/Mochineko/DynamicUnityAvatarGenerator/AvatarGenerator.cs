#nullable enable
using System.Collections.Generic;
using Mochineko.Relent.Result;
using Unity.Logging;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Generator of <see cref="Avatar"/> at runtime.
    /// </summary>
    public static class AvatarGenerator
    {
        /// <summary>
        /// Generates a humanoid avatar.
        /// </summary>
        /// <param name="gameObject">Root GameObject of the model.</param>
        /// <param name="rootBoneRetriever">Root bone retriever.</param>
        /// <param name="humanBoneRetrievers">Human bone retrievers.</param>
        /// <param name="parameters">Human description parameters.</param>
        /// <returns></returns>
        /// <exception cref="ResultPatternMatchException"></exception>
        public static IResult<Avatar> GenerateHumanoidAvatar(
            GameObject gameObject,
            IRootBoneRetriever rootBoneRetriever,
            IHumanBoneRetriever[] humanBoneRetrievers,
            HumanDescriptionParameters parameters)
        {
            var retrieveRootBoneResult = rootBoneRetriever.Retrieve(gameObject);
            Transform rootBone;
            switch (retrieveRootBoneResult)
            {
                case ISuccessResult<Transform> retrieveRootBoneSuccess:
                    rootBone = retrieveRootBoneSuccess.Result;
                    Log.Debug("[AvatarGenerator] Succeeded to retrieve root bone: {0}.", rootBone.name);
                    break;

                case IFailureResult<Transform> retrieveRootBoneFailure:
                    Log.Error("[AvatarGenerator] Failed to retrieve root bone because -> {0}.",
                        retrieveRootBoneFailure.Message);
                    return Results.Fail<Avatar>(
                        $"Failed to retrieve root bone because -> {retrieveRootBoneFailure.Message}");

                default:
                    Log.Fatal("[AvatarGenerator] Unexpected result: {0}.", nameof(retrieveRootBoneResult));
                    throw new ResultPatternMatchException(nameof(retrieveRootBoneResult));
            }

            var skeletonBones = ConstructSkeletonBones(rootBone);
            Log.Debug("[AvatarGenerator] Finished to construct {0} skeleton bones from root bone: {1}.",
                skeletonBones.Length, rootBone.name);

            var constructHumanBonesResult = ConstructHumanBones(skeletonBones, humanBoneRetrievers);
            HumanBone[] humanBones;
            switch (constructHumanBonesResult)
            {
                case ISuccessResult<HumanBone[]> constructHumanBonesSuccess:
                    humanBones = constructHumanBonesSuccess.Result;
                    Log.Debug("[AvatarGenerator] Succeeded to construct {0} human bones from {1} skeleton bones.",
                        humanBones.Length, skeletonBones.Length);
                    break;

                case IFailureResult<HumanBone[]> constructHumanBonesFailure:
                    Log.Error("[AvatarGenerator] Failed to construct human bones from {0} skeleton bones.",
                        skeletonBones.Length);
                    return Results.Fail<Avatar>(
                        $"Failed to construct human bones from {skeletonBones.Length} skeleton bones because -> {constructHumanBonesFailure.Message}");

                default:
                    Log.Fatal("[AvatarGenerator] Unexpected result: {0}.", nameof(constructHumanBonesResult));
                    throw new ResultPatternMatchException(nameof(constructHumanBonesResult));
            }

            var description = new HumanDescription
            {
                human = humanBones,
                skeleton = skeletonBones,
                upperArmTwist = parameters.upperArmTwist,
                lowerArmTwist = parameters.lowerArmTwist,
                upperLegTwist = parameters.upperLegTwist,
                lowerLegTwist = parameters.lowerLegTwist,
                armStretch = parameters.armStretch,
                legStretch = parameters.legStretch,
                feetSpacing = parameters.feetSpacing,
                hasTranslationDoF = parameters.hasTranslationDoF
            };

            var avatar = AvatarBuilder.BuildHumanAvatar(gameObject, description);
            if (!avatar.isValid)
            {
                Log.Error("[AvatarGenerator] Avatar is invalid construction for {0}.",
                    gameObject.name);
                return Results.Fail<Avatar>(
                    $"Avatar is invalid construction for {gameObject.name}.");
            }

            if (!avatar.isHuman)
            {
                Log.Error(
                    "[AvatarGenerator] Avatar is not humanoid construction for {0}.",
                    gameObject.name);
                return Results.Fail<Avatar>(
                    $"Avatar is not humanoid construction for {gameObject.name}.");
            }

            Log.Info("[AvatarGenerator] Succeeded to generate humanoid avatar for {0}.", gameObject.name);
            return Results.Succeed(avatar);
        }

        /// <summary>
        /// Constructs skeleton bones from the root bone.
        /// </summary>
        /// <param name="rootBone"></param>
        /// <returns></returns>
        private static SkeletonBone[] ConstructSkeletonBones(Transform rootBone)
        {
            var bones = new List<SkeletonBone>();

            CreateSkeletonBoneRecursively(rootBone, bones);

            return bones.ToArray();
        }

        /// <summary>
        /// Creates skeleton bones into children recursively.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="bones"></param>
        private static void CreateSkeletonBoneRecursively(
            Transform transform,
            ICollection<SkeletonBone> bones)
        {
            Log.Debug("[AvatarGenerator] Create skeleton bone for {0}.", transform.name);

            bones.Add(new SkeletonBone
            {
                name = transform.name,
                position = transform.localPosition,
                rotation = transform.localRotation,
                scale = transform.localScale
            });

            foreach (Transform child in transform)
            {
                CreateSkeletonBoneRecursively(child, bones);
            }
        }

        /// <summary>
        /// Constructs human bones from skeleton bones.
        /// </summary>
        /// <param name="skeletonBones"></param>
        /// <param name="retrievers"></param>
        /// <returns></returns>
        /// <exception cref="ResultPatternMatchException"></exception>
        private static IResult<HumanBone[]> ConstructHumanBones(
            IReadOnlyCollection<SkeletonBone> skeletonBones,
            IEnumerable<IHumanBoneRetriever> retrievers)
        {
            var humanBones = new List<HumanBone>();

            foreach (var retriever in retrievers)
            {
                var (part, result) = retriever.Retrieve(skeletonBones);
                switch (result)
                {
                    case ISuccessResult<HumanBone> success:
                        Log.Debug("[AvatarGenerator] Succeeded to retrieve human bone {0} as {1}.",
                            success.Result.boneName, success.Result.humanName);
                        humanBones.Add(success.Result);
                        continue;

                    case IFailureResult<HumanBone> failure:
                        if (IsHumanoidRequiredPart(part))
                        {
                            Log.Error(
                                "[AvatarGenerator] Failed to retrieve humanoid required human bone: {0} because -> {1}.",
                                part, failure.Message);
                            return Results.Fail<HumanBone[]>(
                                $"Failed to retrieve humanoid required human bone: {part} because -> {failure.Message}.");
                        }
                        else // Optional part
                        {
                            Log.Debug(
                                "[AvatarGenerator] Failed to retrieve humanoid optional human bone: {0} because -> {1}.",
                                part, failure.Message);
                            continue;
                        }

                    default:
                        Log.Fatal("[AvatarGenerator] Unexpected result: {0}.", nameof(result));
                        throw new ResultPatternMatchException(nameof(result));
                }
            }

            return Results.Succeed(humanBones.ToArray());
        }

        /// <summary>
        /// Whether the part is humanoid required part.
        /// </summary>
        /// <param name="part"></param>
        /// <returns></returns>
        private static bool IsHumanoidRequiredPart(HumanBodyBones part)
        {
            switch (part)
            {
                // Required
                case HumanBodyBones.Hips:
                case HumanBodyBones.Spine:
                case HumanBodyBones.Head:
                case HumanBodyBones.LeftUpperArm:
                case HumanBodyBones.LeftLowerArm:
                case HumanBodyBones.LeftHand:
                case HumanBodyBones.RightUpperArm:
                case HumanBodyBones.RightLowerArm:
                case HumanBodyBones.RightHand:
                case HumanBodyBones.LeftUpperLeg:
                case HumanBodyBones.LeftLowerLeg:
                case HumanBodyBones.LeftFoot:
                case HumanBodyBones.RightUpperLeg:
                case HumanBodyBones.RightLowerLeg:
                case HumanBodyBones.RightFoot:
                    return true;

                default:
                    return false;
            }
        }
    }
}
