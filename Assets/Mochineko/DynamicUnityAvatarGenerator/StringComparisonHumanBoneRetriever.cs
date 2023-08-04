#nullable enable
using System;
using System.Collections.Generic;
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Retriever of human bones from skeleton bones by string comparison.
    /// </summary>
    public sealed class StringComparisonHumanBoneRetriever : IHumanBoneRetriever
    {
        private readonly HumanBodyBones target;
        private readonly HumanLimit limit;
        private readonly string keyword;
        private readonly StringComparison comparison;
        private readonly bool caseSensitive;

        public StringComparisonHumanBoneRetriever(
            HumanBodyBones target,
            HumanLimit limit,
            string keyword,
            StringComparison comparison,
            bool caseSensitive)
        {
            if (string.IsNullOrEmpty(keyword))
            {
                throw new ArgumentException("Pattern is empty.");
            }

            this.target = target;
            this.limit = limit;
            this.keyword = keyword;
            this.comparison = comparison;
            this.caseSensitive = caseSensitive;
        }

        /// <inheritdoc/>
        (HumanBodyBones part, IResult<HumanBone> result) IHumanBoneRetriever.Retrieve(
            IEnumerable<SkeletonBone> skeletonBones)
        {
            foreach (var bone in skeletonBones)
            {
                if (comparison.MatchRule(bone.name, keyword, caseSensitive))
                {
                    return (target, Results.Succeed(new HumanBone
                    {
                        boneName = bone.name,
                        humanName = target.ToString(), // TODO: Check
                        limit = limit
                    }));
                }
            }

            return (
                target,
                Results.Fail<HumanBone>($"Not found {target} human bone in skeleton bones.")
            );
        }
    }
}
