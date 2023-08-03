#nullable enable
using System;
using System.Collections.Generic;
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    public sealed class StringComparisonHumanBoneRetriever : IHumanBoneRetriever
    {
        private readonly HumanBodyBones target;
        private readonly HumanLimit limit;
        private readonly string keyword;
        public ComparisonMode comparison;
        private readonly bool caseSensitive;

        public enum ComparisonMode
        {
            Prefix,
            Suffix,
            Contains,
        }

        public StringComparisonHumanBoneRetriever(
            HumanBodyBones target,
            HumanLimit limit,
            string keyword,
            ComparisonMode comparison,
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

        (HumanBodyBones part, IResult<HumanBone> result) IHumanBoneRetriever.Retrieve(
            IEnumerable<SkeletonBone> skeletonBones)
        {
            foreach (var bone in skeletonBones)
            {
                if (CheckPattern(bone.name))
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

        private bool CheckPattern(string name)
        {
            switch (comparison)
            {
                case ComparisonMode.Prefix:
                    return caseSensitive
                        ? name.StartsWith(keyword)
                        : name.ToLower().StartsWith(keyword.ToLower());

                case ComparisonMode.Suffix:
                    return caseSensitive
                        ? name.EndsWith(keyword)
                        : name.ToLower().EndsWith(keyword.ToLower());

                case ComparisonMode.Contains:
                    return caseSensitive
                        ? name.Contains(keyword)
                        : name.ToLower().Contains(keyword.ToLower());

                default:
                    throw new ArgumentOutOfRangeException(nameof(comparison));
            }
        }
    }
}
