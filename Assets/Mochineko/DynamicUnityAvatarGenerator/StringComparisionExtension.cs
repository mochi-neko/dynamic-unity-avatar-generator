#nullable enable
using System;

namespace Mochineko.DynamicUnityAvatarGenerator
{
    /// <summary>
    /// Extension methods for StringComparison.
    /// </summary>
    public static class StringComparisionExtension
    {
        /// <summary>
        /// Whether the name matches the rule.
        /// </summary>
        /// <param name="comparison">String comparison mode.</param>
        /// <param name="compared">Compared string.</param>
        /// <param name="keyword">Keyword string.</param>
        /// <param name="caseSensitive">Case sensitive.</param>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public static bool MatchRule(
            this StringComparison comparison,
            string compared,
            string keyword,
            bool caseSensitive)
        {
            switch (comparison)
            {
                case StringComparison.Prefix:
                    return caseSensitive
                        ? compared.StartsWith(keyword)
                        : compared.ToLower().StartsWith(keyword.ToLower());

                case StringComparison.Suffix:
                    return caseSensitive
                        ? compared.EndsWith(keyword)
                        : compared.ToLower().EndsWith(keyword.ToLower());

                case StringComparison.Contains:
                    return caseSensitive
                        ? compared.Contains(keyword)
                        : compared.ToLower().Contains(keyword.ToLower());

                default:
                    throw new ArgumentOutOfRangeException(nameof(comparison));
            }
        }
    }
}
