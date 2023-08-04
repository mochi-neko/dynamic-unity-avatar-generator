#nullable enable
using UnityEngine;

namespace Mochineko.DynamicUnityAvatarGenerator.Presets
{
    /// <summary>
    /// Preset of human bone retrievers for Mixamo and Biped.
    /// </summary>
    public static class MixamoAndBipedHumanBoneRetrievers
    {
        public static readonly IHumanBoneRetriever[] Preset =
        {
            // Spines
            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Hips,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(Hips|Bip01|Pelvis)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Spine,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(Spine)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Chest,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(Spine1|Chest)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.UpperChest,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(Spine2|Spine3|Spine4|UpperChest)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Neck,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(Neck|Neck1)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.Head,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(Head|Head1)$"),

            // Left Legs
            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftUpperLeg,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftUpLeg|L\sThigh|L_Thigh|LeftUpperLeg)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftLowerLeg,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftLeg|L\sCalf|L_Calf|LeftLowerLeg)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftFoot,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftFoot|L\sFoot|L_Foot)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftToes,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftToeBase|L\sToe0|L_Toe0|LeftToes)$"),

            // Right Legs
            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightUpperLeg,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightUpLeg|R\sThigh|R_Thigh|RightUpperLeg)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightLowerLeg,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightLeg|R\sCalf|R_Calf|RightLowerLeg)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightFoot,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightFoot|R\sFoot|R_Foot)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightToes,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightToeBase|R\sToe0|R_Toe0|RightToes)$"),

            // Left Arms
            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftShoulder,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftShoulder|L\sClavicle|L_Clavicle)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftUpperArm,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftArm|L\sUpperArm|L_UpperArm|LeftUpperArm)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftLowerArm,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftForeArm|L\sForeArm|L_ForeArm|LeftLowerArm)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftHand,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHand|L\sHand|L_Hand|LeftWrist)$"),

            // Right Arms
            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightShoulder,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightShoulder|R\sClavicle|R_Clavicle)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightUpperArm,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightArm|R\sUpperArm|R_UpperArm|RightUpperArm)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightLowerArm,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightForeArm|R\sForeArm|R_ForeArm|RightLowerArm)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightHand,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHand|R\sHand|R_Hand|RightWrist)$"),

            // Left Fingers
            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftThumbProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandThumb1|L\sFinger0|L_Finger0|LeftThumbProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftThumbIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandThumb2|L\sFinger01|L_Finger01|LeftThumbIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftThumbDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandThumb3|L\sFinger02|L_Finger02|LeftThumbDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftIndexProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandIndex1|L\sFinger1|L_Finger1|LeftIndexProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftIndexIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandIndex2|L\sFinger11|L_Finger11|LeftIndexIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftIndexDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandIndex3|L\sFinger12|L_Finger12|LeftIndexDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftMiddleProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandMiddle1|L\sFinger2|L_Finger2|LeftMiddleProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftMiddleIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandMiddle2|L\sFinger21|L_Finger21|LeftMiddleIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftMiddleDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandMiddle3|L\sFinger22|L_Finger22|LeftMiddleDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftRingProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandRing1|L\sFinger3|L_Finger3|LeftRingProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftRingIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandRing2|L\sFinger31|L_Finger31|LeftRingIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftRingDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandRing3|L\sFinger32|L_Finger32|LeftRingDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftLittleProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandPinky1|L\sFinger4|L_Finger4|LeftLittleProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftLittleIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandPinky2|L\sFinger41|L_Finger41|LeftLittleIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.LeftLittleDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(LeftHandPinky3|L\sFinger42|L_Finger42|LeftLittleDistal)$"),

            // Right Fingers
            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightThumbProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandThumb1|R\sFinger0|R_Finger0|RightThumbProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightThumbIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandThumb2|R\sFinger01|R_Finger01|RightThumbIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightThumbDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandThumb3|R\sFinger02|R_Finger02|RightThumbDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightIndexProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandIndex1|R\sFinger1|R_Finger1|RightIndexProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightIndexIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandIndex2|R\sFinger11|R_Finger11|RightIndexIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightIndexDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandIndex3|R\sFinger12|R_Finger12|RightIndexDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightMiddleProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandMiddle1|R\sFinger2|R_Finger2|RightMiddleProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightMiddleIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandMiddle2|R\sFinger21|R_Finger21|RightMiddleIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightMiddleDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandMiddle3|R\sFinger22|R_Finger22|RightMiddleDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightRingProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandRing1|R\sFinger3|R_Finger3|RightRingProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightRingIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandRing2|R\sFinger31|R_Finger31|RightRingIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightRingDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandRing3|R\sFinger32|R_Finger32|RightRingDistal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightLittleProximal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandPinky1|R\sFinger4|R_Finger4|RightLittleProximal)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightLittleIntermediate,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandPinky2|R\sFinger41|R_Finger41|RightLittleIntermediate)$"),

            new RegularExpressionHumanBoneRetriever(
                target: HumanBodyBones.RightLittleDistal,
                limit: new HumanLimit { useDefaultValues = true },
                pattern: @".*(?i)(RightHandPinky3|R\sFinger42|R_Finger42|RightLittleDistal)$"),
        };
    }
}
