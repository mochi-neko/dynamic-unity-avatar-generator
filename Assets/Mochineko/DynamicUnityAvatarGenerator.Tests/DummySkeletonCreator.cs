#nullable enable
using System.Collections.Generic;
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator.Tests
{
    internal static class DummySkeletonCreator
    {
        public static GameObject CreateDummyHumanoidHierarchy()
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

            return gameObject;
        }

        public static IHumanBoneRetriever[] CreateDummyHumanBoneRetrievers()
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

        /// <summary>
        /// See https://docs.readyplayer.me/ready-player-me/api-reference/avatars/full-body-avatars
        /// </summary>
        /// <returns></returns>
        public static GameObject CreateReadyPlayerMeHumanoidHierarchy()
        {
            var gameObject = new GameObject(name: "Armature");
            var root = gameObject.transform;

            var hips = new GameObject(name: "Hips").transform;
            hips.SetParent(root);

            var spine = new GameObject(name: "Spine").transform;
            spine.SetParent(hips);
            var chest = new GameObject(name: "Spine1").transform;
            chest.SetParent(spine);
            var upperChest = new GameObject(name: "Spine2").transform;
            upperChest.SetParent(chest);
            var neck = new GameObject(name: "Neck").transform;
            neck.SetParent(upperChest);
            var head = new GameObject(name: "Head").transform;
            head.SetParent(neck);

            var leftUpperLeg = new GameObject(name: "LeftUpLeg").transform;
            leftUpperLeg.SetParent(hips);
            var leftLowerLeg = new GameObject(name: "LeftLeg").transform;
            leftLowerLeg.SetParent(leftUpperLeg);
            var leftFoot = new GameObject(name: "LeftFoot").transform;
            leftFoot.SetParent(leftLowerLeg);
            var leftToes = new GameObject(name: "LeftToeBase").transform;
            leftToes.SetParent(leftFoot);

            var rightUpperLeg = new GameObject(name: "RightUpLeg").transform;
            rightUpperLeg.SetParent(hips);
            var rightLowerLeg = new GameObject(name: "RightLeg").transform;
            rightLowerLeg.SetParent(rightUpperLeg);
            var rightFoot = new GameObject(name: "RightFoot").transform;
            rightFoot.SetParent(rightLowerLeg);
            var rightToes = new GameObject(name: "RightToeBase").transform;
            rightToes.SetParent(rightFoot);

            var leftShoulder = new GameObject(name: "LeftShoulder").transform;
            leftShoulder.SetParent(chest);
            var leftUpperArm = new GameObject(name: "LeftArm").transform;
            leftUpperArm.SetParent(leftShoulder);
            var leftLowerArm = new GameObject(name: "LeftForeArm").transform;
            leftLowerArm.SetParent(leftUpperArm);
            var leftHand = new GameObject(name: "LeftHand").transform;
            leftHand.SetParent(leftLowerArm);

            var rightShoulder = new GameObject(name: "RightShoulder").transform;
            rightShoulder.SetParent(chest);
            var rightUpperArm = new GameObject(name: "RightArm").transform;
            rightUpperArm.SetParent(rightShoulder);
            var rightLowerArm = new GameObject(name: "RightForeArm").transform;
            rightLowerArm.SetParent(rightUpperArm);
            var rightHand = new GameObject(name: "RightHand").transform;
            rightHand.SetParent(rightLowerArm);

            var leftThumbProximal = new GameObject(name: "LeftHandThumb1").transform;
            leftThumbProximal.SetParent(leftHand);
            var leftIndexProximal = new GameObject(name: "LeftHandIndex1").transform;
            leftIndexProximal.SetParent(leftHand);
            var leftMiddleProximal = new GameObject(name: "LeftHandMiddle1").transform;
            leftMiddleProximal.SetParent(leftHand);
            var leftRingProximal = new GameObject(name: "LeftHandRing1").transform;
            leftRingProximal.SetParent(leftHand);
            var leftLittleProximal = new GameObject(name: "LeftHandPinky1").transform;
            leftLittleProximal.SetParent(leftHand);

            var rightThumbProximal = new GameObject(name: "RightHandThumb1").transform;
            rightThumbProximal.SetParent(rightHand);
            var rightIndexProximal = new GameObject(name: "RightHandIndex1").transform;
            rightIndexProximal.SetParent(rightHand);
            var rightMiddleProximal = new GameObject(name: "RightHandMiddle1").transform;
            rightMiddleProximal.SetParent(rightHand);
            var rightRingProximal = new GameObject(name: "RightHandRing1").transform;
            rightRingProximal.SetParent(rightHand);
            var rightLittleProximal = new GameObject(name: "RightHandPinky1").transform;
            rightLittleProximal.SetParent(rightHand);

            return gameObject;
        }
    }
}
