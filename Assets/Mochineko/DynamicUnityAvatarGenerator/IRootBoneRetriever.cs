#nullable enable
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Retriever of the root bone of skeleton bones.
    /// </summary>
    public interface IRootBoneRetriever
    {
        /// <summary>
        /// Retrieves the root bone of skeleton bones.
        /// </summary>
        /// <param name="gameObject">The root of GameObject.</param>
        /// <returns></returns>
        IResult<Transform> Retrieve(GameObject gameObject);
    }
}
