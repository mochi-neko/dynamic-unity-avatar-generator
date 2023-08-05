#nullable enable
using System.Collections.Generic;
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Retriever of human bones from skeleton bones.
    /// </summary>
    public interface IHumanBoneRetriever
    {
        /// <summary>
        /// Retrieves human bone from skeleton bones.
        /// </summary>
        /// <param name="skeletonBones"></param>
        /// <returns></returns>
        (HumanBodyBones part, IResult<(HumanBone humanBone, Transform transform)> result)
            Retrieve(IEnumerable<(SkeletonBone skeletonBone, Transform transform)> skeletonBones);
    }
}
