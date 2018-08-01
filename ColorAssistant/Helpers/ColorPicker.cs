using System;
using System.Drawing;
using System.Windows.Input;
using System.Windows.Threading;

namespace ColorAssistant.Helpers
{
    class ColorPicker
    {
        //This is static so a new random is not made everytime
        private static Random _random = new Random();

        private DispatcherTimer _pickerThread;

        public Color CurrentColor { get; private set; }
        public event Action<System.Windows.Media.Color> ReadCursorPixelObtained;

        public ColorPicker()
        {
            _pickerThread = new DispatcherTimer();
            _pickerThread.Interval = new TimeSpan(0, 0, 0, 0, 100);
            _pickerThread.Tick += PixelCheck;
        }

        public void ReadCursorPixelStart()
        {
            _pickerThread.Start();
        }

        public void ReadCursorPixelEnd(Color color)
        {
            _pickerThread.Stop();
            CurrentColor = color;
        }

        public void RandomColor()
        {
            var A = 255;
            var R = (byte)_random.Next(0, 255);
            var G = (byte)_random.Next(0, 255);
            var B = (byte)_random.Next(0, 255);

            CurrentColor = Color.FromArgb(A, R, G, B);
        }

        private void PixelCheck(object sender, EventArgs e)
        {
            var color = GetPixel();
            if (Keyboard.GetKeyStates(Key.LeftCtrl).HasFlag(KeyStates.Down) &&
                Keyboard.GetKeyStates(Key.C).HasFlag(KeyStates.Down))
            {
                ReadCursorPixelEnd(color);
            }
            ReadCursorPixelObtained.Invoke(ConvertToMedia(color));
        }

        /// <summary>
        /// Convert drawing color to media color.
        /// </summary>
        /// <param name="color">Drawing color</param>
        /// <returns>Media color</returns>
        private System.Windows.Media.Color ConvertToMedia(Color color)
        {
            System.Windows.Media.Color c = new System.Windows.Media.Color();
            c.A = color.A;
            c.R = color.R;
            c.G = color.G;
            c.B = color.B;
            return c;
        }

        /// <summary>
        /// Obtain pixel at cursor position.
        /// </summary>
        /// <returns>Color at cursor position.</returns>
        private System.Drawing.Color GetPixel()
        {
            var c = new System.Windows.Forms.Cursor(System.Windows.Forms.Cursor.Current.Handle);
            System.Windows.Forms.Cursor.Position = new Point(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            var p = Win32.GetPixelColor(System.Windows.Forms.Cursor.Position.X, System.Windows.Forms.Cursor.Position.Y);
            return p;
        }
    }
}
