using System.Collections.Generic;
using System;
using System.Windows;
using System.Dynamic;

namespace RemoteDesktopThesisServer.Helpers
{
    public static class StyleHelper
    {
        #region Enum

        /// <summary>
        /// Predefined font sizes.
        /// </summary>
        public enum FontSize
        {
            /// <summary>
            /// Font size 6.
            /// </summary>
            ExtraTiny = 6,

            /// <summary>
            /// Font size 8.
            /// </summary>
            Tiny = 8,

            /// <summary>
            /// Font size 12.
            /// </summary>
            Small = 12,

            /// <summary>
            /// Font size 16.
            /// </summary>
            Medium = 16,

            /// <summary>
            /// Font size 18.
            /// </summary>
            Large = 18,

            /// <summary>
            /// Font size 22.
            /// </summary>
            Larger = 22,

            /// <summary>
            /// Font size 28.
            /// </summary>
            ExtraLarge = 28
        }

        #endregion

        #region Struct

        /// <summary>
        /// 
        /// </summary>
        public struct BrushesResourceKeys
        {
            public static string BetterWhiteBrush = "BetterWhiteBrush";
            public static string ApplicationMenuHoverBrush = "ApplicationMenuHoverBrush";
        }

        #endregion
    }
}
