using System.Collections.ObjectModel;
using System.Windows.Input;
using ColorAssistant.Models;

namespace ColorAssistant.ViewModels
{
    class EditViewModel : ObservableObject
    {
        private ObservableCollection<PresenterViewModel> _collection;

        public ICommand AddPresenterCommand { get; set; }
        public ICommand RemovePresenterCommand { get; set; }

        public EditViewModel(ObservableCollection<PresenterViewModel> collection)
        {
            _collection = collection;
            AddPresenterCommand    = new RelayCommand<object>(AddPresenter);
            RemovePresenterCommand = new RelayCommand<PresenterViewModel>(RemovePresenter);
        }

        private void RemovePresenter(PresenterViewModel obj)
        {
            _collection.Remove(obj);
        }

        public void AddPresenter(object param)
        {
            var newPanel = new PresenterViewModel(new TextureModel());
            newPanel.Texture.FileName = "Color_" + _collection.Count;
            _collection.Add(newPanel);
        }
    }
}
