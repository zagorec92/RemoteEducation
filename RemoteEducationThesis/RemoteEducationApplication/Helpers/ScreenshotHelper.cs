using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows;

namespace RemoteEducationApplication.Helpers
{
    public class ScreenshotHelper
    {
        #region Fields
        
        private static Bitmap _bitmap;

        #endregion

        #region MousePointer

        private const Int32 CURSOR_SHOWING = 0x00000001;

        /// <summary>
        /// CURSORINFO
        /// </summary>
        struct CURSORINFO
        {
            public Int32 cbSize;
            public Int32 flags;
            public IntPtr hCursor;
            public POINTAPI ptScreenPos;
        }

        /// <summary>
        /// POINTAPI
        /// </summary>
        struct POINTAPI
        {
            public int x;
            public int y;
        }

        /// <summary>
        /// GetCursorInfo
        /// </summary>
        /// <param name="pci"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool GetCursorInfo(out CURSORINFO pci);

        /// <summary>
        /// DrawIcon
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="hIcon"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);

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
        /// <returns>The <see cref="System.Drawing.Bitmap"/> instance.</returns>
        public static Bitmap TakeScreenshot()
        {
            using (Graphics graphics = Graphics.FromImage(_bitmap))
            {
                graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;
                graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                graphics.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighSpeed;
                graphics.CopyFromScreen(0, 0, 0, 0, _bitmap.Size);

                CURSORINFO pci;
                pci.cbSize = Marshal.SizeOf(typeof(CURSORINFO));

                if (GetCursorInfo(out pci))
                {
                    if (pci.flags == CURSOR_SHOWING)
                    {
                        DrawIcon(graphics.GetHdc(), pci.ptScreenPos.x, pci.ptScreenPos.y, pci.hCursor);
                        graphics.ReleaseHdc();
                    }
                }
            }

            return _bitmap;
        }

        #endregion
    }
}
