using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace QuickSwizzler
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // set data contexts
            FilesListbox.DataContext = SwizzleSettings.Files.SelectedFiles;
            ExportPathBox.DataContext = SwizzleSettings.Files;
        }

        /// <summary>
        /// Save as swizzled copy behaviour
        /// </summary>
        private void SaveCopyButtonClick(object sender, RoutedEventArgs e)
        {
            var selection = FilesListbox.SelectedItem;
            if (selection != null)
            {
                if (QuickSwizzle.ValidateExportPath())
                {
                    // confirmed swizzle-able
                    QuickSwizzle.SwizzleAndSaveImage(
                        FilesListbox.SelectedItem as string, true);

                    MessageBox.Show("Finished!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                }
            }
            else // no selected item
            {
                MessageBox.Show("Select a file from the files list first.",
                    "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Save as swizzled behaviour
        /// </summary>
        private void SaveSwizzledButtonClick(object sender, RoutedEventArgs e)
        {
            var selection = FilesListbox.SelectedItem;
            if(selection != null)
            {
                if (QuickSwizzle.ValidateExportPath())
                {
                    if (QuickSwizzle.ConfirmSwizzle() == MessageBoxResult.Yes)
                    {
                        // confirmed swizzle-able
                        QuickSwizzle.SwizzleAndSaveImage(
                            FilesListbox.SelectedItem as string);

                        MessageBox.Show("Finished!", "", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                }
            }
            else // no selected item
            {
                MessageBox.Show("Select a file from the files list first.", 
                    "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Batch swizzle behaviour
        /// </summary>
        private void BatchSwizzleClick(object sender, RoutedEventArgs e)
        {
            if(SwizzleSettings.Files.SelectedFiles.Count <= 0)
            {
                MessageBox.Show("Select files first.", 
                    "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else // 1+ file selected
            {
                if (QuickSwizzle.ValidateExportPath())
                {
                    if (QuickSwizzle.ConfirmBatchSwizzle() == MessageBoxResult.Yes)
                    {
                        foreach (string file in SwizzleSettings.Files.SelectedFiles)
                        {
                            QuickSwizzle.SwizzleAndSaveImage(file);
                        }

                        MessageBox.Show("Finished batch!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
        }

        /// <summary>
        /// Batch swizzle as copy behaviour
        /// </summary>
        private void BatchCopyClick(object sender, RoutedEventArgs e)
        {
            if (SwizzleSettings.Files.SelectedFiles.Count <= 0)
            {
                MessageBox.Show("Select files first.",
                    "", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else // 1+ file selected
            {
                if (QuickSwizzle.ValidateExportPath())
                {
                    foreach (string file in SwizzleSettings.Files.SelectedFiles)
                    {
                        QuickSwizzle.SwizzleAndSaveImage(file, true);
                    }

                    MessageBox.Show("Finished batch!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        /// <summary>
        /// Select file button behaviour
        /// </summary>
        private void PickFileButtonClick(object sender, RoutedEventArgs e)
        {
            FileDialog.SelectMultiFiles();
        }

        /// <summary>
        /// Clear file button behaviour
        /// </summary>
        private void ClearFileButtonClick(object sender, RoutedEventArgs e)
        {
            SwizzleSettings.Files.SelectedFiles.Clear();
        }

        /// <summary>
        /// Select output path button behaviour
        /// </summary>
        private void PickOutputButtonClick(object sender, RoutedEventArgs e)
        {
            FileDialog.SelectExportPath();
        }

        /// <summary>
        /// Clear output path button behaviour
        /// </summary>
        private void ClearOutputButtonClick(object sender, RoutedEventArgs e)
        {
            SwizzleSettings.Files.ExportPath = "";
        }

        /// <summary>
        /// Radio button selection behaviour
        /// </summary>
        private void RadioButtonChecked(object sender, RoutedEventArgs e)
        {
            RadioButton rb = sender as RadioButton;
            switch (rb.Name) // set the output targets based on the buttons' names
            {
                case "RC_R":
                    SwizzleSettings.RedChannelTarget = Channel.Red;
                    break;
                case "RC_G":
                    SwizzleSettings.RedChannelTarget = Channel.Green;
                    break;
                case "RC_B":
                    SwizzleSettings.RedChannelTarget = Channel.Blue;
                    break;
                case "GC_G":
                    SwizzleSettings.GreenChannelTarget = Channel.Green;
                    break;
                case "GC_B":
                    SwizzleSettings.GreenChannelTarget = Channel.Blue;
                    break;
                case "GC_R":
                    SwizzleSettings.GreenChannelTarget = Channel.Red;
                    break;
                case "BC_B":
                    SwizzleSettings.BlueChannelTarget = Channel.Blue;
                    break;
                case "BC_R":
                    SwizzleSettings.BlueChannelTarget = Channel.Red;
                    break;
                case "BC_G":
                    SwizzleSettings.BlueChannelTarget = Channel.Green;
                    break;
            }

            return;
        }
    }
}
