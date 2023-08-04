#nullable enable
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Mochineko.Relent.Result;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Retriever of human bones from skeleton bones by regular expression.
    /// </summary>
    public sealed class RegularExpressionHumanBoneRetriever : IHumanBoneRetriever
    {
        private readonly HumanBodyBones target;
        private readonly HumanLimit limit;
        private readonly string pattern;

        public RegularExpressionHumanBoneRetriever(
            HumanBodyBones target,
            HumanLimit limit,
            string pattern)
        {
            if (string.IsNullOrEmpty(pattern))
            {
                throw new ArgumentException("Pattern is empty.");
            }

            this.target = target;
            this.limit = limit;
            this.pattern = pattern;
        }

        /// <inheritdoc/>
        (HumanBodyBones part, IResult<HumanBone> result) IHumanBoneRetriever.Retrieve(
            IEnumerable<SkeletonBone> skeletonBones)
        {
            var regex = new Regex(pattern);

            foreach (var bone in skeletonBones)
            {
                if (regex.IsMatch(bone.name))
                {
                    return (target, Results.Succeed(new HumanBone
                    {
                        boneName = bone.name,
                        humanName = target.ToString(),
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
