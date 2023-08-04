#nullable enable
using System;
using Mochineko.Relent.Result;
using Unity.Logging;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Retriever of the root bone of skeleton bones by string comparison.
    /// </summary>
    public sealed class StringComparisonRootBoneRetriever : IRootBoneRetriever
    {
        private readonly string keyword;
        private readonly StringComparison comparison;
        private readonly bool caseSensitive;

        public StringComparisonRootBoneRetriever(
            string keyword,
            StringComparison comparison,
            bool caseSensitive)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                throw new ArgumentException("Keyword is empty.");
            }

            this.keyword = keyword;
            this.comparison = comparison;
            this.caseSensitive = caseSensitive;
        }

        /// <inheritdoc/>
        IResult<Transform> IRootBoneRetriever.Retrieve(GameObject gameObject)
        {
            return FindChildRecursively(gameObject.transform);
        }

        private IResult<Transform> FindChildRecursively(Transform transform)
        {
            if (comparison.MatchRule(transform.name, keyword, caseSensitive))
            {
                return Results.Succeed(transform);
            }

            foreach (Transform child in transform)
            {
                var result = FindChildRecursively(child);
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

            return Results.Fail<Transform>(
                $"Not found root bone in {transform.name} by keyword: {keyword}, comparision: {comparison}, case sensitive: {caseSensitive}.");
        }
    }
}
