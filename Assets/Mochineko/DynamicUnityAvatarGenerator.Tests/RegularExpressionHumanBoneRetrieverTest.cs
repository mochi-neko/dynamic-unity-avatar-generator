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
        [TestCase(".Hips", @".*Hips$")] // Ends with "Hips" with case sensitive
        [TestCase("SomeBoneName.Hips", @".*Hips$")]
        [TestCase(".hips", @".*(?i)Hips$")] // Ends with "Hips" with case insensitive
        [TestCase("some_bone_name_hips", @".*(?i)Hips$")]
        [TestCase("Hips.", @"^Hips.*")] // Starts with "Hips" with case sensitive
        [TestCase("Hips.SomeBoneName", @"^Hips.*")]
        [TestCase("hips.", @"^(?i)Hips.*")] // Starts with "Hips" with case insensitive
        [TestCase("hips_some_bone_name", @"^(?i)Hips.*")]
        [TestCase("Prefix.Hips.Suffix", @".*Hips.*")] // Contains "Hips" with case sensitive
        [TestCase("prefix_hips_suffix", @".*(?i)Hips.*")] // Contains "Hips" with case insensitive
        [TestCase(".Head", @".*(?i)(Head|Head1)$")] // Ends with "Head" or "Head1" with case insensitive
        [TestCase(".Head1", @".*(?i)(Head|Head1)$")]
        [TestCase(".Head", @".*(?i)Head\d??$")] // Ends with "Head" or plus one number with case insensitive
        [TestCase(".Head1", @".*(?i)Head\d??$")]
        [RequiresPlayMode(false)]
        public void RetrieveTest(string name, string pattern)
        {
            IHumanBoneRetriever retriever = new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Hips,
                limit: new HumanLimit(),
                pattern: pattern);

            var skeletonBones = new List<SkeletonBone>();
            skeletonBones.Add(new SkeletonBone()
            {
                name = name
            });

            retriever.Retrieve(skeletonBones)
                .result.Success
                .Should().BeTrue();
        }
    }
}
