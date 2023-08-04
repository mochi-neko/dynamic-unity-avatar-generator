#nullable enable
namespace Mochineko.DynamicUnityAvatarGenerator.Presets
{
    /// <summary>
    /// Preset of human description parameters.
    /// </summary>
    public static class HumanDescriptionParametersPreset
    {
        public static readonly HumanDescriptionParameters Preset = new(
            upperArmTwist: 0.5f,
            lowerArmTwist: 0.5f,
            upperLegTwist: 0.5f,
            lowerLegTwist: 0.5f,
            armStretch: 0.05f,
            legStretch: 0.05f,
            feetSpacing: 0,
            hasTranslationDoF: false
        );
    }
}
