using System;
using System.Windows.Media;
using ColorAssistant.Models;

namespace ColorAssistant.Facades
{
    class TextureFacade : ObservableObject
    {
        private TextureModel _textureModel;

        public TextureModel Model
        {
            get
            {
                return _textureModel;
            }
        }

        public TextureFacade(TextureModel model)
        {
            //Default values to prevent null reference
            _textureModel = model;

            //If no extension is specified, used png for default
            if (string.IsNullOrEmpty(model.Extension))
                Extension = ".PNG";

            RefreshBrush();
        }

        /// <summary>
        /// Brush color value of this texture.
        /// </summary>
        public SolidColorBrush Brush
        {
            get { return _textureModel.Brush; }
            set
            {
                _textureModel.Brush = value;
                OnPropertyChanged("Brush");
            }
        }

        /// <summary>
        /// Color value from ARGB.
        /// </summary>
        public Color Color
        {
            get
            {
                return Color.FromArgb(A, R, G, B);
            }
        }

        /// <summary>
        /// Alpha value of this texture.
        /// </summary>
        public byte A
        {
            get { return _textureModel.A; }
            set
            {
                _textureModel.A = value;
                OnPropertyChanged("A");
                RefreshBrush();
            }
        }

        /// <summary>
        /// Red value of this texture.
        /// </summary>
        public byte R
        {
            get { return _textureModel.R; }
            set
            {
                _textureModel.R = value;
                OnPropertyChanged("R");
                RefreshBrush();
            }
        }

        /// <summary>
        /// Green value fo this texture.
        /// </summary>
        public byte G
        {
            get { return _textureModel.G; }
            set
            {
                _textureModel.G = value;
                OnPropertyChanged("G");
                RefreshBrush();
            }
        }

        /// <summary>
        /// Blue value of this texture.
        /// </summary>
        public byte B
        {
            get { return _textureModel.B; }
            set
            {
                _textureModel.B = value;
                OnPropertyChanged("B");
                RefreshBrush();
            }
        }

        /// <summary>
        /// File name of this texture when it is saved.
        /// </summary>
        public string FileName
        {
            get { return _textureModel.FileName; }
            set
            {
                _textureModel.FileName = value;
                OnPropertyChanged("FileName");
            }
        }

        /// <summary>
        /// The IO path this texture should be exported to.
        /// </summary>
        public string ExportPath
        {
            get { return _textureModel.ExportPath; }
            set
            {
                _textureModel.ExportPath = value;
                OnPropertyChanged("ExportPath");
            }
        }

        /// <summary>
        /// Image extension of this texture when saved, e.g. png, jpeg, etc.
        /// </summary>
        public string Extension
        {
            get { return _textureModel.Extension; }
            set
            {
                _textureModel.Extension = value;
                OnPropertyChanged("Extension");
            }
        }

        /// <summary>
        /// Formated ARGB value of this texture.
        /// </summary>
        public string ARGBString
        {
            get
            {
                return String.Format("[{0}, {1}, {2}, {3}]", A, R, G, B);
            }
        }

        /// <summary>
        /// Refresh the brush data to match ARGB values.
        /// </summary>
        private void RefreshBrush()
        {
            Brush = new SolidColorBrush(Color.FromArgb(A, R, G, B));
            _textureModel.Hex = Brush.ToString();
            OnPropertyChanged("ARGBString");
        }
    }
}
