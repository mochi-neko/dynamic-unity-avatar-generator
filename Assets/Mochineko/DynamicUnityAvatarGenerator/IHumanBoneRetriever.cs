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
        /// <param name="skeletonBones">Skeleton bones.</param>
        /// <returns></returns>
        (HumanBodyBones part, IResult<HumanBone> result) Retrieve(IEnumerable<SkeletonBone> skeletonBones);
    }
}
