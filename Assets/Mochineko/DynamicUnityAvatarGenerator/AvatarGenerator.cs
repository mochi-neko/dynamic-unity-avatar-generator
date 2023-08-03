#nullable enable
using System.Collections.Generic;
using Mochineko.Relent.Result;
using Unity.Logging;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    public static class AvatarGenerator
    {
        public static IResult<Avatar> GenerateHumanoidAvatar(
            GameObject gameObject,
            Transform rootBone,
            IHumanBoneRetriever[] retrievers,
            HumanDescriptionParameters parameters)
        {
            var skeletonBones = ConstructSkeletonBones(rootBone);
            Log.Debug("[AvatarGenerator] Finished to construct {0} skeleton bones from root bone: {1}.",
                skeletonBones.Length, rootBone.name);

            var constructHumanBonesResult = ConstructHumanBones(skeletonBones, retrievers);
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
                        $"Failed to generate avatar because -> {constructHumanBonesFailure.Message}");

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
                Log.Error("[AvatarGenerator] Failed to generate avatar because avatar is invalid construction for {0}.",
                    gameObject.name);
                return Results.Fail<Avatar>(
                    $"Failed to generate avatar because avatar is invalid construction for {gameObject.name}.");
            }

            if (!avatar.isHuman)
            {
                Log.Error(
                    "[AvatarGenerator] Failed to generate avatar because avatar is not humanoid construction for {0}.",
                    gameObject.name);
                return Results.Fail<Avatar>(
                    $"Failed to generate avatar because avatar is not humanoid construction for {gameObject.name}.");
            }

            Log.Info("[AvatarGenerator] Succeeded to generate humanoid avatar for {0}.", gameObject.name);
            return Results.Succeed(avatar);
        }

        private static SkeletonBone[] ConstructSkeletonBones(Transform rootBone)
        {
            var bones = new List<SkeletonBone>();

            CreateSkeletonBoneRecursively(rootBone, bones);

            return bones.ToArray();
        }

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

        private static bool IsHumanoidRequiredPart(HumanBodyBones part)
        {
            switch (part)
            {
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
