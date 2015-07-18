using ExtensionLibrary.Native;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows;

namespace Education.Application.Helpers
{
    public class ScreenshotHelper : MouseNative
    {
        #region Fields

        private static Bitmap _bitmap;

        #endregion

        #region Properties

        /// <summary>
        /// Gets the primary screen height.
        /// </summary>
        protected static int ScreenHeight
        {
            get
            {
                int height = Convert.ToInt32(SystemParameters.PrimaryScreenHeight);
                return height;
            }
        }

        /// <summary>
        /// Gets the primary screen width.
        /// </summary>
        protected static int ScreenWidth
        {
            get
            {
                int width = Convert.ToInt32(SystemParameters.PrimaryScreenWidth);
                return width;
            }
        }

        #endregion

        #region InitializeBitmap

        /// <summary>
        /// Initializes a new <see cref="System.Drawing.Bitmap"/> instance.
        /// </summary>
        public static void InitializeBitmap()
        {
            _bitmap = new Bitmap(ScreenWidth, ScreenHeight);
        }

        #endregion

        #region Screenshot

        /// <summary>
        /// 
        /// </summary>
        /// <returns>The <see cref="Bitmap"/> instance.</returns>
        public static Bitmap TakeScreenshot()
        {
            using (Graphics graphics = Graphics.FromImage(_bitmap))
            {
                graphics.SmoothingMode = SmoothingMode.HighSpeed;
                graphics.InterpolationMode = InterpolationMode.Low;
                graphics.CompositingQuality = CompositingQuality.HighSpeed;
                graphics.CopyFromScreen(0, 0, 0, 0, _bitmap.Size);

                CURSORINFO pci = new CURSORINFO();
                pci.SetCbSize(Marshal.SizeOf(typeof(CURSORINFO)));

                if (NativeMethods.InvokeGetCursorInfo(out pci))
                {
                    if (pci.GetFlags() == CURSOR_SHOWING)
                    {
                        NativeMethods.InvokeDrawIcon(graphics.GetHdc(), pci.GetScreenPosXCoord(), pci.GetScreenPosYCoord(), pci.GetHCursor());
                        graphics.ReleaseHdc();
                    }
                }
            }

            return _bitmap;
        }

        #endregion
    }
}
