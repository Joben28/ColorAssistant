using System.Collections.ObjectModel;

namespace ColorAssistant.ViewModels
{
    /// <summary>
    /// The parent of all view models.
    /// </summary>
    class MainViewModel : ObservableObject
    {
        public MenuViewModel Menu { get; set; }
        public ObservableCollection<PresenterViewModel> Presenters { get; set; }

        public MainViewModel()
        {
            Presenters = new ObservableCollection<PresenterViewModel>();
            Menu = new MenuViewModel(Presenters);
        }
    }
}
