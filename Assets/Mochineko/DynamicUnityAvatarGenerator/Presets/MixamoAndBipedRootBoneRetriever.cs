#nullable enable
namespace Mochineko.DynamicUnityAvatarGenerator.Presets
{
    /// <summary>
    /// Preset of root bone retriever for Mixamo and Biped.
    /// </summary>
    public static class MixamoAndBipedRootBoneRetriever
    {
        public static readonly IRootBoneRetriever Preset = new RegularExpressionRootBoneRetriever(
            pattern: @".*(?i)(Hips|Bip01|Pelvis)$"
        );
    }
}
