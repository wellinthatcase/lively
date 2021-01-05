// =============================================== //
//		    © 2021 - All rights reserved.          // 
// =============================================== //

using System.Windows;
using System.Windows.Media;

/**
 * Namespace: StaticExtensions
 *
 * Container for custom, static extensions used by this Software, in particular, the Editor.
 */

namespace StaticExtensions
{
    // wf; WinForm
    using wfFont = System.Drawing.Font;
    using wfBitmap = System.Drawing.Bitmap;
    using wfGraphics = System.Drawing.Graphics;

    /**
     * Class: Converters
     *
     * String extensions used to convert the string into another arbitrary object.
     *
     * Author: wellinthatcase
     *
     * Date: 12/29/2020
     */

    public static class Converters
    {
        /**
         * Function: AsBrush
         *
         * A string extension method that converts a str to a brush.
         *
         * Author: wellinthatcase
         *
         * Date: 12/29/2020
         *
         * Parameters:
         * str -  The str to act on.
         *
         * Returns: A Brush.
         */

        public static Brush AsBrush(this string str)
        {
            return (Brush)new BrushConverter().ConvertFromString(str);
        }

        /**
         * Function: AsSolidColorBrush
         *
         * A string extension method that converts a str to a solid color brush.
         *
         * Author: wellinthatcase
         *
         * Date: 12/29/2020
         *
         * Parameters:
         * str -  The str to act on.
         *
         * Returns: A SolidColorBrush.
         */

        public static SolidColorBrush AsSolidColorBrush(this string str)
        {
            return (SolidColorBrush)AsBrush(str);
        }

        /**
         * Function: IntoFontSize
         *
         * A string extension method that converts the str into a font size.
         *
         * Author: wellinthatcase
         *
         * Date: 12/29/2020
         *
         * Parameters:
         * str -         The str to act on.
         * defaultVal -  (Optional) The default value.
         *
         * Returns: A double.
         */

        public static double IntoFontSize(this string str, double? defaultVal = 15F)
        {
            return double.TryParse(str, out double size) ? size : defaultVal ?? 15F;
        }

        /**
         * Function: IntoFontStyle
         *
         * A string extension method that converts the str into a font style.
         *
         * Author: wellinthatcase
         *
         * Date: 12/29/2020
         *
         * Parameters:
         * str -  The str to act on.
         *
         * Returns: A FontStyle.
         */

        public static FontStyle IntoFontStyle(this string str)
        {
            return (FontStyle)new FontStyleConverter().ConvertFromString(str);
        }

        /**
         * Function: IntoFontFamiy
         *
         * A string extension method that converts the str into a font famiy.
         *
         * Author: wellinthatcase
         *
         * Date: 12/29/2020
         *
         * Parameters:
         * str -  The str to act on.
         *
         * Returns: A FontFamily.
         */

        public static FontFamily IntoFontFamily(this string str)
        {
            return (FontFamily)new FontFamilyConverter().ConvertFromString(str);
        }

        /**
         * Function: IntoFontWeight
         *
         * A string extension method that converts the str into a font weight.
         *
         * Author: wellinthatcase
         *
         * Date: 12/29/2020
         *
         * Parameters:
         * str -  The str to act on.
         *
         * Returns: A FontWeight.
         */

        public static FontWeight IntoFontWeight(this string str)
        {
            return (FontWeight)new FontWeightConverter().ConvertFromString(str);
        }
    }

    /**
     * Class: Measurement
     *
     * String extensions used to measure the string relatively in different units.
     *
     * Author: wellinthatcase
     *
     * Date: 12/29/2020
     */

    public static class Measurement
    {
        /**
         * Function: WidthInPixels
         *
         * Measures the string's length in pixels.
         *
         * Author: wellinthatcase
         *
         * Date: 12/29/2020
         *
         * Parameters:
         * str -       The str to act on.
         * font -      The font.
         *
         * Returns: An integer representing how many pixels the string is in length.
         */

        public static int WidthInPixels(this string str, wfFont font)
        {
            using (wfGraphics gfx = wfGraphics.FromImage(new wfBitmap(1, 1)))
            {
                return (int)gfx.MeasureString(str, font).Width;
            }
        }
    }
}
