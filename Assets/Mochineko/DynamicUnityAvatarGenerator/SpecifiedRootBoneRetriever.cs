#nullable enable
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    public sealed class SpecifiedRootBoneRetriever : IRootBoneRetriever
    {
        private readonly Transform rootBone;

        public SpecifiedRootBoneRetriever(Transform rootBone)
        {
            this.rootBone = rootBone;
        }

        IResult<Transform> IRootBoneRetriever.Retrieve(GameObject gameObject)
        {
            return Results.Succeed(rootBone);
        }
    }
}
