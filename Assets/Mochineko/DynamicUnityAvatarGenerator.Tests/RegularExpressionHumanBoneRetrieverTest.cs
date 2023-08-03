#nullable enable
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Mochineko.DynamicUnityAvatarGenerator.Tests
{
    internal sealed class AvatarGeneratorTest
    {
        [Test]
        [RequiresPlayMode(true)]
        public void GenerateTest()
        {
            var (gameObject, rootBone) = CreateHumanoidHierarchy();
            var retrievers = CreateRetrievers();

            AvatarGenerator.GenerateHumanoidAvatar(
                    gameObject,
                    rootBone,
                    retrievers,
                    new HumanDescriptionParameters()
                )
                .Success
                .Should().BeTrue();
        }

        private (GameObject gameObject, Transform rootBone) CreateHumanoidHierarchy()
        {
            var gameObject = new GameObject(name: "Root");
            var root = gameObject.transform;

            var hips = new GameObject(name: "bone.Hips");
            hips.transform.SetParent(root);

            var spine = new GameObject(name: "bone.Spine");
            spine.transform.SetParent(hips.transform);
            var chest = new GameObject(name: "bone.Chest");
            chest.transform.SetParent(spine.transform);
            var neck = new GameObject(name: "bone.Neck");
            neck.transform.SetParent(chest.transform);
            var head = new GameObject(name: "bone.Head");
            head.transform.SetParent(neck.transform);

            var leftUpperLeg = new GameObject(name: "bone.LeftUpperLeg");
            leftUpperLeg.transform.SetParent(hips.transform);
            var leftLowerLeg = new GameObject(name: "bone.LeftLowerLeg");
            leftLowerLeg.transform.SetParent(leftUpperLeg.transform);
            var leftFoot = new GameObject(name: "bone.LeftFoot");
            leftFoot.transform.SetParent(leftLowerLeg.transform);
            var leftToes = new GameObject(name: "bone.LeftToes");
            leftToes.transform.SetParent(leftFoot.transform);

            var rightUpperLeg = new GameObject(name: "bone.RightUpperLeg");
            rightUpperLeg.transform.SetParent(hips.transform);
            var rightLowerLeg = new GameObject(name: "bone.RightLowerLeg");
            rightLowerLeg.transform.SetParent(rightUpperLeg.transform);
            var rightFoot = new GameObject(name: "bone.RightFoot");
            rightFoot.transform.SetParent(rightLowerLeg.transform);
            var rightToes = new GameObject(name: "bone.RightToes");
            rightToes.transform.SetParent(rightFoot.transform);

            var leftShoulder = new GameObject(name: "bone.LeftShoulder");
            leftShoulder.transform.SetParent(chest.transform);
            var leftUpperArm = new GameObject(name: "bone.LeftUpperArm");
            leftUpperArm.transform.SetParent(leftShoulder.transform);
            var leftLowerArm = new GameObject(name: "bone.LeftLowerArm");
            leftLowerArm.transform.SetParent(leftUpperArm.transform);
            var leftHand = new GameObject(name: "bone.LeftHand");
            leftHand.transform.SetParent(leftLowerArm.transform);

            var rightShoulder = new GameObject(name: "bone.RightShoulder");
            rightShoulder.transform.SetParent(chest.transform);
            var rightUpperArm = new GameObject(name: "bone.RightUpperArm");
            rightUpperArm.transform.SetParent(rightShoulder.transform);
            var rightLowerArm = new GameObject(name: "bone.RightLowerArm");
            rightLowerArm.transform.SetParent(rightUpperArm.transform);
            var rightHand = new GameObject(name: "bone.RightHand");
            rightHand.transform.SetParent(rightLowerArm.transform);

            return (gameObject, root);
        }

        private IHumanBoneRetriever[] CreateRetrievers()
        {
            var retrievers = new List<IHumanBoneRetriever>();
            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Hips,
                limit: new HumanLimit(),
                pattern: @".*(?i)Hips$"));

            return retrievers.ToArray();
        }
    }

    internal sealed class RegularExpressionHumanBoneRetrieverTest
    {
        [TestCase("Hips", @".*Hips$")] // Ends with "Hips" with case sensitive
        [TestCase("SomeBoneName.Hips", @".*Hips$")]
        [TestCase("hips", @".*(?i)Hips$")] // Ends with "Hips" with case insensitive
        [TestCase("some_bone_name_hips", @".*(?i)Hips$")]
        [TestCase("Hips", @"^Hips.*")] // Starts with "Hips" with case sensitive
        [TestCase("Hips.SomeBoneName", @"^Hips.*")]
        [TestCase("hips", @"^(?i)Hips.*")] // Starts with "Hips" with case insensitive
        [TestCase("hips_some_bone_name", @"^(?i)Hips.*")]
        [TestCase("Prefix.Hips.Suffix", @".*Hips.*")] // Contains "Hips" with case sensitive
        [TestCase("prefix_hips_suffix", @".*(?i)Hips.*")] // Contains "Hips" with case insensitive
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
