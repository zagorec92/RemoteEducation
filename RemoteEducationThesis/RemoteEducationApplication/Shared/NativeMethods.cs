using System;
using System.Runtime.InteropServices;

namespace RemoteEducationApplication.Shared
{
    public abstract class NativeMethods
    {
        /// <summary>
        /// CURSORINFO
        /// </summary>
        internal struct CURSORINFO
        {
            public int _cbSize;
            public int _flags;
            private IntPtr _hCursor;
            public POINTAPI _ptScreenPos;

            /// <summary>
            /// Gets the <see cref="IntPtr"/> hCursor.
            /// </summary>
            /// <returns></returns>
            public IntPtr GetHCursor()
            {
                return _hCursor;
            }
        }

        /// <summary>
        /// POINTAPI
        /// </summary>
        internal struct POINTAPI
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
        internal static extern bool GetCursorInfo(out CURSORINFO pci);

        /// <summary>
        /// DrawIcon
        /// </summary>
        /// <param name="hDC"></param>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <param name="hIcon"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        internal static extern bool DrawIcon(IntPtr hDC, int X, int Y, IntPtr hIcon);
    }
}
