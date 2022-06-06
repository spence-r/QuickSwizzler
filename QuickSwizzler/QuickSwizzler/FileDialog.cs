/* Spence! 2020 */
/* commonspence.com */

using System.IO;

namespace QuickSwizzler
{
    /// <summary>
    /// A class to handle shared file dialog operations
    /// </summary>
    public static class FileDialog
    {
        /// <summary>
        /// The shared OFD instance
        /// </summary>
        public static Microsoft.Win32.OpenFileDialog OFD { get; }
            = new Microsoft.Win32.OpenFileDialog();

        /// <summary>
        /// Show an open file dialog
        /// </summary>
        /// <param name="filter">The file type filter</param>
        /// <param name="multiSelect">Able to select multiple files?</param>
        /// <param name="title">The title of the dialog</param>
        /// <returns></returns>
        public static bool? ShowOFD(string filter = "All|*.*",
            bool multiSelect = false, string title = "Select file")
        {
            OFD.Filter = filter;
            OFD.Multiselect = multiSelect;
            OFD.Title = title;

            return OFD.ShowDialog();
        }

        /// <summary>
        /// Show file selection dialog for the export path
        /// </summary>
        public static void SelectExportPath()
        {
            System.Windows.Forms.FolderBrowserDialog dialog =
                new System.Windows.Forms.FolderBrowserDialog();

            System.Windows.Forms.DialogResult result = dialog.ShowDialog();

            SwizzleSettings.Files.ExportPath = dialog.SelectedPath;
        }

        /// <summary>
        /// Select multiple files and store them to the files list
        /// </summary>
        public static void SelectMultiFiles()
        {
            bool? dialogResult = ShowOFD(
                "BMP|*.bmp|TIFF|*.tiff|PNG|*.png|JPG|*.jpg|JPEG|*.jpeg",
                true, "Select image files");

            if (dialogResult == true)
            {
                SwizzleSettings.Files.SelectedFiles.Clear();

                foreach (string filename in OFD.FileNames)
                {
                    SwizzleSettings.Files.SelectedFiles.Add(Path.GetFullPath(filename));
                }

            }
        }

    }

}
