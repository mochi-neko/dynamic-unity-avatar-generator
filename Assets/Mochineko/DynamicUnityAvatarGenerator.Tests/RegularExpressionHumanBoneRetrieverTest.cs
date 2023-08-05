#nullable enable
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Mochineko.DynamicUnityAvatarGenerator.Tests
{
    internal sealed class RegularExpressionHumanBoneRetrieverTest
    {
        // Ends with "Hips" with case sensitive
        [TestCase(@".*Hips$", "Hips", true)]
        [TestCase(@".*Hips$", "SomeBoneName.Hips", true)]
        [TestCase(@".*Hips$", "Hip", false)]
        [TestCase(@".*Hips$", "HipsX", false)]
        // Ends with "Hips" with case insensitive
        [TestCase(@".*(?i)Hips$", "hips", true)]
        [TestCase(@".*(?i)Hips$", "some_bone_name_hips", true)]
        [TestCase(@".*(?i)Hips$", "HIPS", true)]
        // Starts with "Hips" with case sensitive
        [TestCase(@"^Hips.*", "Hips", true)]
        [TestCase(@"^Hips.*", "Hips.SomeBoneName", true)]
        [TestCase(@"^Hips.*", "Hip.SomeBoneName", false)]
        [TestCase(@"^Hips.*", "XHip", false)]
        // Starts with "Hips" with case insensitive
        [TestCase(@"^(?i)Hips.*", "hips", true)]
        [TestCase(@"^(?i)Hips.*", "hips_some_bone_name", true)]
        // Contains "Hips" with case sensitive
        [TestCase(@".*Hips.*", "Prefix.Hips.Suffix", true)]
        [TestCase(@".*Hips.*", "Prefix.Hips", true)]
        [TestCase(@".*Hips.*", "Hips.Suffix", true)]
        [TestCase(@".*Hips.*", "Prefix.hip.Suffix", false)]
        // Contains "Hips" with case insensitive
        [TestCase(@".*(?i)Hips.*", "prefix_hips_suffix", true)]
        // Ends with "Head" or "Head1" with case insensitive
        [TestCase(@".*(Head|Head1)$", "Head", true)]
        [TestCase(@".*(Head|Head1)$", "Head1", true)]
        [TestCase(@".*(Head|Head1)$", "Head2", false)]
        // Ends with "Head" or plus one number with case insensitive
        [TestCase(@".*Head\d??$", "Head", true)]
        [TestCase(@".*Head\d??$", "Head1", true)]
        [TestCase(@".*Head\d??$", "HeadX", false)]
        [TestCase(@".*Head\d??$", "Head10", false)]
        [RequiresPlayMode(true)]
        public void RetrieveTest(string pattern, string name, bool match)
        {
            IHumanBoneRetriever retriever = new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Hips,
                limit: new HumanLimit(),
                pattern: pattern);

            var skeletonBones = new List<(SkeletonBone, Transform)>
            {
                (
                    new SkeletonBone { name = name },
                    new GameObject().transform
                )
            };

            retriever.Retrieve(skeletonBones)
                .result.Success
                .Should().Be(match);
        }
    }
}
