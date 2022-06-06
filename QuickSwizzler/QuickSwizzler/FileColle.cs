/* Spence! 2020 */
/* commonspence.com */

using System.Collections.ObjectModel;
using System.ComponentModel;

namespace QuickSwizzler
{
    /// <summary>
    /// Collection of selected files for batch operations
    /// </summary>
    public class FileColle : INotifyPropertyChanged
    {
        public ObservableCollection<string> SelectedFiles;
        public FileColle()
        {
            SelectedFiles = new ObservableCollection<string>();
        }

        // Export directory
        private string exportPath = "";
        public string ExportPath
        {
            get { return exportPath; }
            set
            {
                if (exportPath != value)
                {
                    exportPath = value;
                    NotifyPropertyChanged("ExportPath");
                }
            }
        }

        /*
        // Overwrite files enabled?
        private bool overwrite = false;
        public bool Overwrite
        {
            get { return overwrite; }
            set
            {
                if (overwrite != value)
                {
                    overwrite = value;
                    NotifyPropertyChanged("Overwrite");
                }
            }
        }

*/
        // handle property change notifications for UI
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string property)
        {
            PropertyChanged?.Invoke(
                this, new PropertyChangedEventArgs(property));
        }
    }
}
