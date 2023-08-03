#nullable enable
using System.Collections.Generic;
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    public interface IHumanBoneRetriever
    {
        (HumanBodyBones part, IResult<HumanBone> result) Retrieve(IEnumerable<SkeletonBone> skeletonBones);
    }
}
