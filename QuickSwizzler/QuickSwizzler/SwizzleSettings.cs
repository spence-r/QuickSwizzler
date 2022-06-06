/* Spence! 2020 */
/* commonspence.com */

namespace QuickSwizzler
{
    /// <summary>
    /// Global swizzler settings
    /// </summary>
    public static class SwizzleSettings
    {
        // Channel pack settings
        public static Channel RedChannelTarget = Channel.Red;
        public static Channel GreenChannelTarget = Channel.Green;
        public static Channel BlueChannelTarget = Channel.Blue;

        // The image files to manipulate
        public static FileColle Files = new FileColle();
    }
}
