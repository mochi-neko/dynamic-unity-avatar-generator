#nullable enable
using System.Collections.Generic;
using FluentAssertions;
using Mochineko.Relent.Result;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Mochineko.DynamicUnityAvatarGenerator.Tests
{
    internal sealed class AvatarGeneratorTest
    {
        [Test]
        [RequiresPlayMode(true)]
        public void GenerateHumanoidAvatarTest()
        {
            var (gameObject, rootBone) = CreateHumanoidHierarchy();
            var retrievers = CreateRetrievers();

            var avatar = AvatarGenerator.GenerateHumanoidAvatar(
                    gameObject,
                    rootBone,
                    retrievers,
                    new HumanDescriptionParameters()
                )
                .Unwrap();

            avatar.isValid.Should().BeTrue();
            avatar.isHuman.Should().BeTrue();
            avatar.humanDescription.skeleton.Length.Should().Be(22);
            avatar.humanDescription.human.Length.Should().Be(21); // Does not contain root bone

            Object.Destroy(avatar);
            Object.Destroy(gameObject);
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

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Spine,
                limit: new HumanLimit(),
                pattern: @".*(?i)Spine$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Chest,
                limit: new HumanLimit(),
                pattern: @".*(?i)Chest$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Neck,
                limit: new HumanLimit(),
                pattern: @".*(?i)Neck$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Head,
                limit: new HumanLimit(),
                pattern: @".*(?i)Head$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftUpperLeg,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftUpperLeg$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftLowerLeg,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftLowerLeg$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftFoot,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftFoot$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftToes,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftToes$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightUpperLeg,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightUpperLeg$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightLowerLeg,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightLowerLeg$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightFoot,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightFoot$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightToes,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightToes$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftShoulder,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftShoulder$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftUpperArm,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftUpperArm$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftLowerArm,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftLowerArm$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftHand,
                limit: new HumanLimit(),
                pattern: @".*(?i)LeftHand$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightShoulder,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightShoulder$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightUpperArm,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightUpperArm$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightLowerArm,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightLowerArm$"));

            retrievers.Add(new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightHand,
                limit: new HumanLimit(),
                pattern: @".*(?i)RightHand$"));

            return retrievers.ToArray();
        }
    }
}
