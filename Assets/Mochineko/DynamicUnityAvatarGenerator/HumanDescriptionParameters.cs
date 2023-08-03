#nullable enable
namespace Mochineko.DynamicUnityAvatarGenerator
{
    public readonly struct HumanDescriptionParameters
    {
        public readonly float upperArmTwist;
        public readonly float lowerArmTwist;
        public readonly float upperLegTwist;
        public readonly float lowerLegTwist;
        public readonly float armStretch;
        public readonly float legStretch;
        public readonly float feetSpacing;
        public readonly bool hasTranslationDoF;

        public HumanDescriptionParameters(
            float upperArmTwist,
            float lowerArmTwist,
            float upperLegTwist,
            float lowerLegTwist,
            float armStretch,
            float legStretch,
            float feetSpacing,
            bool hasTranslationDoF)
        {
            this.upperArmTwist = upperArmTwist;
            this.lowerArmTwist = lowerArmTwist;
            this.upperLegTwist = upperLegTwist;
            this.lowerLegTwist = lowerLegTwist;
            this.armStretch = armStretch;
            this.legStretch = legStretch;
            this.feetSpacing = feetSpacing;
            this.hasTranslationDoF = hasTranslationDoF;
        }
    }
}
