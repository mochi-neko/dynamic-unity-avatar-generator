#nullable enable
namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// String comparison mode.
    /// </summary>
    public enum StringComparison
    {
        /// <summary>
        /// Matches the pattern prefix of string.
        /// </summary>
        Prefix,
        /// <summary>
        /// Matches the pattern suffix of string.
        /// </summary>
        Suffix,
        /// <summary>
        /// Contains the pattern in string.
        /// </summary>
        Contains,
    }
}
