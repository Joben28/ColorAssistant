using System.Collections.ObjectModel;

namespace ColorAssistant.ViewModels
{
    /// <summary>
    /// Context menu view model of the application.
    /// </summary>
    class MenuViewModel : ObservableObject
    {
        public FileViewModel File { get; set; }
        public EditViewModel Edit { get; set; }

        public MenuViewModel(ObservableCollection<PresenterViewModel> collection)
        {
            File = new FileViewModel(collection);
            Edit = new EditViewModel(collection);
        }
    }
}
