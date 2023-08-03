#nullable enable
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    public interface IRootBoneRetriever
    {
        IResult<Transform> Retrieve(GameObject gameObject);
    }
}
