using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ColorAssistant.Helpers
{
    class BitmapCreator<T> where T : BitmapEncoder
    {
        public T Encoder { get; private set; }

        public BitmapCreator()
        {
            Encoder = Activator.CreateInstance<T>();
        }

        public void AddBitmapFrame(Color color)
        {
            var bitmapSource = CreateBitmapSource(color);
            Encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        }

        /// <summary>
        /// Create a bitmap source.
        /// </summary>
        /// <param name="color">Color to fill source.</param>
        /// <returns>Bitmap source for provided color.</returns>
        private BitmapSource CreateBitmapSource(System.Windows.Media.Color color)
        {
            //Stole this from: http://stackoverflow.com/questions/10637064/create-bitmapimage-and-apply-to-it-a-specific-color
            int width = 128;
            int height = width;
            int stride = width / 8;
            byte[] pixels = new byte[height * stride];

            List<System.Windows.Media.Color> colors = new List<System.Windows.Media.Color>();
            colors.Add(color);
            BitmapPalette myPalette = new BitmapPalette(colors);

            BitmapSource image = BitmapSource.Create(
                width,
                height,
                96,
                96,
                PixelFormats.Indexed1,
                myPalette,
                pixels,
                stride);

            return image;
        }
    }
}
