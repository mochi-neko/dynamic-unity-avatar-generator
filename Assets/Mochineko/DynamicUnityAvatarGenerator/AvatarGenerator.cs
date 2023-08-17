#nullable enable
using System.Collections.Generic;
using System.Linq;
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
        public static IResult<(Avatar avatar, IReadOnlyDictionary<HumanBodyBones, Transform> transformMap)>
            GenerateHumanoidAvatar(
                GameObject gameObject,
                IRootBoneRetriever rootBoneRetriever,
                IHumanBoneRetriever[] humanBoneRetrievers,
                HumanDescriptionParameters parameters,
                bool enforceTPose = false)
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
                    return Results.Fail<(Avatar, IReadOnlyDictionary<HumanBodyBones, Transform>)>(
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
            IReadOnlyDictionary<HumanBodyBones, Transform> transformMap;
            switch (constructHumanBonesResult)
            {
                case ISuccessResult<(HumanBone[] bones, IReadOnlyDictionary<HumanBodyBones, Transform> map)>
                    constructHumanBonesSuccess:
                    humanBones = constructHumanBonesSuccess.Result.bones;
                    transformMap = constructHumanBonesSuccess.Result.map;
                    Log.Debug("[AvatarGenerator] Succeeded to construct {0} human bones from {1} skeleton bones.",
                        humanBones.Length, skeletonBones.Length);
                    break;

                case IFailureResult<(HumanBone[], IReadOnlyDictionary<HumanBodyBones, Transform>)>
                    constructHumanBonesFailure:
                    Log.Error("[AvatarGenerator] Failed to construct human bones from {0} skeleton bones.",
                        skeletonBones.Length);
                    return Results.Fail<(Avatar, IReadOnlyDictionary<HumanBodyBones, Transform>)>(
                        $"Failed to construct human bones from {skeletonBones.Length} skeleton bones because -> {constructHumanBonesFailure.Message}");

                default:
                    Log.Fatal("[AvatarGenerator] Unexpected result: {0}.", nameof(constructHumanBonesResult));
                    throw new ResultPatternMatchException(nameof(constructHumanBonesResult));
            }

            if (enforceTPose)
            {
                foreach (var pair in transformMap)
                {
                    pair.Value.localRotation = TPoseLocalRotation(pair.Key);
                }
            }

            var description = new HumanDescription
            {
                human = humanBones,
                skeleton = skeletonBones
                    .Select(pair => pair.skeletonBone)
                    .ToArray(),
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
                return Results.Fail<(Avatar, IReadOnlyDictionary<HumanBodyBones, Transform>)>(
                    $"Avatar is invalid construction for {gameObject.name}.");
            }

            if (!avatar.isHuman)
            {
                Log.Error(
                    "[AvatarGenerator] Avatar is not humanoid construction for {0}.",
                    gameObject.name);
                return Results.Fail<(Avatar, IReadOnlyDictionary<HumanBodyBones, Transform>)>(
                    $"Avatar is not humanoid construction for {gameObject.name}.");
            }

            Log.Info("[AvatarGenerator] Succeeded to generate humanoid avatar for {0}.", gameObject.name);
            return Results.Succeed((avatar, transformMap));
        }

        /// <summary>
        /// Constructs skeleton bones from the root bone.
        /// </summary>
        /// <param name="rootBone"></param>
        /// <returns></returns>
        private static (SkeletonBone skeletonBone, Transform transform)[]
            ConstructSkeletonBones(Transform rootBone)
        {
            var bones = new List<(SkeletonBone, Transform)>();

            CreateSkeletonBoneRecursively(rootBone.parent, bones);

            return bones.ToArray();
        }

        /// <summary>
        /// Creates skeleton bones into children recursively.
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="bones"></param>
        private static void CreateSkeletonBoneRecursively(
            Transform transform,
            ICollection<(SkeletonBone, Transform)> bones)
        {
            Log.Debug("[AvatarGenerator] Create skeleton bone for {0}.", transform.name);

            bones.Add((
                new SkeletonBone
                {
                    name = transform.name,
                    position = transform.localPosition,
                    rotation = transform.localRotation,
                    scale = transform.localScale
                },
                transform
            ));

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
        private static IResult<(HumanBone[] humanBones, IReadOnlyDictionary<HumanBodyBones, Transform> transformMap)>
            ConstructHumanBones(
                IReadOnlyCollection<(SkeletonBone skeletonBone, Transform transform)> skeletonBones,
                IEnumerable<IHumanBoneRetriever> retrievers)
        {
            var humanBones = new List<HumanBone>();
            var map = new Dictionary<HumanBodyBones, Transform>();

            foreach (var retriever in retrievers)
            {
                var (part, result) = retriever.Retrieve(skeletonBones);
                switch (result)
                {
                    case ISuccessResult<(HumanBone bone, Transform transform)> success:
                        if (!map.ContainsKey(part))
                        {
                            Log.Debug("[AvatarGenerator] Succeeded to retrieve human bone {0} as {1}.",
                                success.Result.bone.boneName, success.Result.bone.humanName);
                            humanBones.Add(success.Result.bone);
                            map.Add(part, success.Result.transform);
                            continue;
                        }
                        else
                        {
                            Log.Error(
                                "[AvatarGenerator] The human bone part: {0} is already retrieved but found: {1}.",
                                part, success.Result.bone.boneName);
                            return Results.Fail<(HumanBone[], IReadOnlyDictionary<HumanBodyBones, Transform>)>(
                                $"The human bone part: {part} is already retrieved but found: {success.Result.bone.boneName}.");
                        }

                    case IFailureResult<(HumanBone, Transform)> failure:
                        if (IsHumanoidRequiredPart(part))
                        {
                            Log.Error(
                                "[AvatarGenerator] Failed to retrieve humanoid required human bone: {0} because -> {1}.",
                                part, failure.Message);
                            return Results.Fail<(HumanBone[], IReadOnlyDictionary<HumanBodyBones, Transform>)>(
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

            return Results.Succeed<(HumanBone[], IReadOnlyDictionary<HumanBodyBones, Transform>)>((
                humanBones.ToArray(),
                map
            ));
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

        private static Quaternion TPoseLocalRotation(HumanBodyBones part)
        {
            switch (part)
            {
                case HumanBodyBones.Hips:
                    return Quaternion.Euler(new Vector3(90f, 0f, 0f));
                case HumanBodyBones.LeftUpperLeg:
                    return Quaternion.Euler(new Vector3(0f, 0f, 180f));
                case HumanBodyBones.LeftFoot:
                    return Quaternion.Euler(new Vector3(90f, 0f, 0f));
                case HumanBodyBones.RightUpperLeg:
                    return Quaternion.Euler(new Vector3(0f, 0f, -180f));
                case HumanBodyBones.RightFoot:
                    return Quaternion.Euler(new Vector3(90f, 0f, 0f));
                case HumanBodyBones.LeftShoulder:
                    return Quaternion.Euler(new Vector3(90f, -90f, 0f));
                case HumanBodyBones.RightShoulder:
                    return Quaternion.Euler(new Vector3(90f, 90f, 0f));
                default:
                    return Quaternion.identity;
            }
        }
    }
}
