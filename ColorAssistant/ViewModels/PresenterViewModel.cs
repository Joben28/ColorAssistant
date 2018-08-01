using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using ColorAssistant.Models;
using ColorAssistant.Facades;
using ColorAssistant.Helpers;

namespace ColorAssistant.ViewModels
{
    /// <summary>
    /// Presenter view model for textures.
    /// </summary>
    class PresenterViewModel : ObservableObject
    {
        public ICommand SaveColorCommand { get; set; }
        public ICommand OpenFileDialogCommand { get; set; }
        public ICommand RandomColorCommand { get; set; }
        public ICommand GetColorCommand { get; set; }

        private ColorPicker _picker;

        private TextureFacade _texture;
        public TextureFacade Texture
        {
            get { return _texture; }
            set
            {
                _texture = value;
                OnPropertyChanged("Texture");
            }
        }


        public PresenterViewModel()
        {
            _picker = new ColorPicker();
            _picker.ReadCursorPixelObtained += PixelObtained;

            SaveColorCommand = new RelayCommand<object>(SaveImage);
            OpenFileDialogCommand = new RelayCommand<object>(CreateFilePath);
            RandomColorCommand = new RelayCommand<object>(RandomColor);
            GetColorCommand = new RelayCommand<object>(GetColor);
        }

        public PresenterViewModel(TextureModel model) : this()
        {
            Texture = new TextureFacade(model);
        }

        private void CreateFilePath(object param)
        {
            Texture.ExportPath = FileManager.RetrieveFolderPath();
        }

        private void SaveImage(object param)
        {
            SaveImageTo(Texture.ExportPath);
        }

        private void GetColor(object obj)
        {
            _picker.ReadCursorPixelStart();
        }

        private void RandomColor(object obj)
        {
            _picker.RandomColor();

            _texture.A = _picker.CurrentColor.A;
            _texture.R = _picker.CurrentColor.R;
            _texture.G = _picker.CurrentColor.G;
            _texture.B = _picker.CurrentColor.B;
        }

        private void PixelObtained(Color obj)
        {
            _texture.A = obj.A;
            _texture.R = obj.R;
            _texture.G = obj.G;
            _texture.B = obj.B;
        }

        public void SaveImageTo(string exportPath)
        {
            var finalPath = exportPath + "/" + Texture.FileName + Texture.Extension;
            var bitmapEncoder = new BitmapCreator<PngBitmapEncoder>();
            bitmapEncoder.AddBitmapFrame(Texture.Color);

            FileManager.SaveTextureFromEncoder(bitmapEncoder.Encoder, finalPath);
        }
    }
}
