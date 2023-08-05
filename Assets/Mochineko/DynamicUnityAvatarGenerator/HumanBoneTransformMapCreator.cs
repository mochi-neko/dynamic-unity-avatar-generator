#nullable enable
using System;
using System.Collections.Generic;
using Mochineko.Relent.Result;
using Unity.Logging;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Human bone transform map creator from <see cref="Avatar"/>.
    /// </summary>
    public static class HumanBoneTransformMapCreator
    {
        /// <summary>
        /// Creates human bone transform map from <see cref="Avatar"/>.
        /// </summary>
        /// <param name="avatar">Target avatar.</param>
        /// <param name="gameObject">Target object.</param>
        /// <returns></returns>
        public static IResult<IReadOnlyDictionary<HumanBodyBones, Transform>>
            MapFromAvatar(Avatar avatar, GameObject gameObject)
        {
            var map = new Dictionary<HumanBodyBones, Transform>();

            foreach (var humanBone in avatar.humanDescription.human)
            {
                var result = FindBoneRecursively(gameObject.transform, humanBone);
                switch (result)
                {
                    case ISuccessResult<(HumanBodyBones part, Transform transform)> success:
                        Log.Debug("[AvatarGenerator] Found human bone transform of {0}.", humanBone.humanName);
                        map.Add(success.Result.part, success.Result.transform);
                        continue;

                    case IFailureResult<(HumanBodyBones, Transform)> failure:
                        Log.Error("[AvatarGenerator] Not found human bone of {0} because -> {1}",
                            humanBone.humanName, failure.Message);
                        return Results.Fail<IReadOnlyDictionary<HumanBodyBones, Transform>>(
                            $"Not found human bone of {humanBone.humanName} because -> {failure.Message}.");
                }
            }

            Log.Info("[AvatarGenerator] Succeeded to created HumanBoneTransformMap.");
            return Results.Succeed<IReadOnlyDictionary<HumanBodyBones, Transform>>(map);
        }

        private static IResult<(HumanBodyBones, Transform)> FindBoneRecursively(
            Transform transform,
            HumanBone humanBone)
        {
            if (transform.name == humanBone.boneName)
            {
                if (Enum.TryParse<HumanBodyBones>(humanBone.humanName, out var part))
                {
                    return Results.Succeed((part, transform));
                }
                else
                {
                    Log.Error("[AvatarGenerator] Failed to parse HumanBodyBones from {0}.", humanBone.humanName);
                    return Results.Fail<(HumanBodyBones, Transform)>(
                        $"Failed to parse HumanBodyBones from {humanBone.humanName}.");
                }
            }

            foreach (Transform child in transform)
            {
                var result = FindBoneRecursively(child, humanBone);
                if (result.Success)
                {
                    return result;
                }
            }

            return Results.Fail<(HumanBodyBones, Transform)>(
                $"Not found {humanBone.boneName}.");
        }
    }
}
