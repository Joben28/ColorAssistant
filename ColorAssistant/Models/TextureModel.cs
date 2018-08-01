using System.Windows.Media;
using System.Xml.Serialization;

namespace ColorAssistant.Models
{
    public class TextureModel
    {
        [XmlIgnore]
        public SolidColorBrush Brush;

        public string FileName;
        public string ExportPath;
        public string Extension;

        public string Hex;

        public byte A;
        public byte R;
        public byte G;
        public byte B;
    }
}
