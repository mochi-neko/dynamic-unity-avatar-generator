#nullable enable
using System.Text.RegularExpressions;
using Mochineko.Relent.Result;
using Unity.Logging;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Retriever of the root bone of skeleton bones by regular expression.
    /// </summary>
    public sealed class RegularExpressionRootBoneRetriever : IRootBoneRetriever
    {
        private readonly string pattern;

        public RegularExpressionRootBoneRetriever(string pattern)
        {
            this.pattern = pattern;
        }

        /// <inheritdoc/>
        IResult<Transform> IRootBoneRetriever.Retrieve(GameObject gameObject)
        {
            return FindChildRecursively(gameObject.transform, pattern);
        }

        private static IResult<Transform> FindChildRecursively(Transform transform, string pattern)
        {
            if (Regex.IsMatch(transform.name, pattern))
            {
                return Results.Succeed(transform);
            }

            foreach (Transform child in transform)
            {
                var result = FindChildRecursively(child, pattern);
                switch (result)
                {
                    case ISuccessResult<Transform> success:
                        return success;

                    case IFailureResult<Transform>:
                        continue;

                    default:
                        Log.Fatal("[AvatarGenerator] Unexpected result: {0}.", nameof(result));
                        throw new ResultPatternMatchException(nameof(result));
                }
            }

            return Results.Fail<Transform>($"Not found {pattern} pattern root bone in {transform.name}.");
        }
    }
}
