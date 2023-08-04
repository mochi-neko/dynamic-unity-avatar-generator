#nullable enable
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Mochineko.DynamicUnityAvatarGenerator.Tests
{
    internal sealed class RegularExpressionRootBoneRetrieverTest
    {
        [TestCase(@"(?i)Hips$", "Hips", true)]
        [TestCase(@"(?i)(Hips|Pelvis|Bip01)$", "Hips", true)]
        [RequiresPlayMode(false)]
        public void RetrieveTest(string pattern, string name, bool match)
        {
            IRootBoneRetriever retriever = new RegularExpressionRootBoneRetriever(pattern);

            var gameObject = new GameObject("Root");
            var skeletonParent = new GameObject("SkeletonParent").transform;
            skeletonParent.parent = gameObject.transform;
            var hips = new GameObject("Hips").transform;
            hips.parent = skeletonParent;
            var spine = new GameObject("Spine").transform;
            spine.parent = hips;
            var another = new GameObject("Another").transform;
            another.parent = gameObject.transform;

            retriever.Retrieve(gameObject)
                .Success
                .Should().Be(match);
        }
    }
}
