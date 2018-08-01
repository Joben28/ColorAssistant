using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using ColorAssistant.Serializers;

namespace ColorAssistant.Helpers
{
    class FileManager
    {
        public static string RetrieveFolderPath()
        {
            //The folder path selected
            var filePath = string.Empty;

            //Create new folder dialog (This requires the System.Windows.Forms assembly in project references)
            var folderDialog = new FolderBrowserDialog();
            //Allow users to create a new folder in this dialog, if they desire.
            folderDialog.ShowNewFolderButton = true;

            //Get the result with the path selected
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
                filePath = folderDialog.SelectedPath;
            else
                return string.Empty;

            return filePath;
        }

        public static string SerializeAndSaveCollection<T>(ISerializer<T> serializer, T collection)
        {
            //The folder path selected
            var filePath = string.Empty;

            //Create new folder dialog (This requires the System.Windows.Forms assembly in project references)
            var folderDialog = new SaveFileDialog();
            folderDialog.Filter = "XML|*.xml";

            //Get the result with the path selected
            DialogResult result = folderDialog.ShowDialog();
            if (result == DialogResult.OK)
                filePath = folderDialog.FileName;
            else
                return string.Empty;

            return SaveCollection<T>(serializer, collection, filePath);
        }

        public static string SaveCollection<T>(ISerializer<T> serializer, T collection, string path)
        {
            serializer.Serialize(collection, path);
            return path;
        }

        public static Tuple<string, T> LoadAndDeserializeCollection<T>(ISerializer<T> serializer)
        {
            var collection = Activator.CreateInstance<T>();
            //The folder path selected
            var filePath = string.Empty;

            //Create new folder dialog (This requires the System.Windows.Forms assembly in project references)
            var openFileDialog = new OpenFileDialog();

            //Get the result with the path selected
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
                filePath = openFileDialog.FileName;
            else
                return null;

            collection = serializer.Deserialize(filePath);

            return new Tuple<string, T>(filePath, collection);
        }

        public static void SaveTextureFromEncoder<T>(T encoder, string path) where T : BitmapEncoder
        {
            //Use png encoder to save bitmap to path
            try
            {
                using (var fs = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    encoder.Save(fs);
                    encoder.Frames.Clear();
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

    }
}

