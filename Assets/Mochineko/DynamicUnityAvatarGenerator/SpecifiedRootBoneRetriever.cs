#nullable enable
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Retriever of the root bone of skeleton bones by specified reference of the root bone.
    /// </summary>
    public sealed class SpecifiedRootBoneRetriever : IRootBoneRetriever
    {
        private readonly Transform rootBone;

        public SpecifiedRootBoneRetriever(Transform rootBone)
        {
            this.rootBone = rootBone;
        }

        /// <inheritdoc/>
        IResult<Transform> IRootBoneRetriever.Retrieve(GameObject gameObject)
        {
            return Results.Succeed(rootBone);
        }
    }
}
