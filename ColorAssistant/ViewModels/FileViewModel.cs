using ColorAssistant.Models;
using ColorAssistant.Helpers;
using ColorAssistant.Serializers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ColorAssistant.ViewModels
{
    class FileViewModel : ObservableObject
    {
        private ObservableCollection<PresenterViewModel> _collection;
        private ISerializer<List<TextureModel>> _manager;

        public string CollectionPath { get; set; }

        public ICommand ExportCollectionCommand { get; set; }
        public ICommand SaveCollectionAsCommand { get; set; }
        public ICommand SaveCollectionCommand { get; set; }
        public ICommand LoadCollectionCommand { get; set; }
        public ICommand NewCollectionCommand { get; set; }
        public ICommand ExitCommand { get; set; }


        public FileViewModel(ObservableCollection<PresenterViewModel> collection)
        {
            _collection    = collection;
            _manager       = new XMLSerializer<List<TextureModel>>();
            CollectionPath = string.Empty;

            ExportCollectionCommand = new RelayCommand<object>(ExportCollection);
            SaveCollectionAsCommand = new RelayCommand<object>(SaveCollectionAs);
            SaveCollectionCommand   = new RelayCommand<object>(SaveCollection, x => CollectionPath != string.Empty);
            LoadCollectionCommand   = new RelayCommand<object>(LoadCollection);
            NewCollectionCommand    = new RelayCommand<object>(NewCollection);
            ExitCommand             = new RelayCommand<object>(ExitApplication);
        }

        private void ExitApplication(object obj)
        {
            App.Current.MainWindow.Close();
        }

        private void NewCollection(object obj)
        {
            _collection.Clear();
            CollectionPath = string.Empty;
        }

        private void SaveCollection(object obj)
        {
            var models = GetModelCollection();
            _manager.Serialize(models, CollectionPath);
        }

        private void SaveCollectionAs(object param)
        {
            var models = GetModelCollection();
            var path = FileManager.SerializeAndSaveCollection(_manager, models);
            CollectionPath = path;
        }

        private void ExportCollection(object param)
        {
            var folderPath = FileManager.RetrieveFolderPath();

            if (folderPath == null)
                return;

            foreach (var p in _collection)
            {
                p.SaveImageTo(folderPath);
            }
        }

        private void LoadCollection(object param)
        {
            var responseTuple = FileManager.LoadAndDeserializeCollection(_manager);

            if (responseTuple == null)
                return;

            var path = responseTuple.Item1;
            var textures = responseTuple.Item2;

            foreach (var t in textures)
            {
                _collection.Add(new PresenterViewModel(t));
            }

            CollectionPath = path;
        }

        private List<TextureModel> GetModelCollection()
        {
            var textures = new List<TextureModel>();
            foreach (var content in _collection)
            {
                textures.Add(content.Texture.Model);
            }
            return textures;
        }
    }
}
