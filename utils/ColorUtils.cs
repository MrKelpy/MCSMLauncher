using System;
using System.Drawing;

namespace mcsm.utils
{
    public class ColorUtils
    {
        /// <summary>
        /// Converts a Color object into its closest value in ConsoleColor.
        /// I'll be straight: I have no idea how this works. This is a modified version of a SO answer.
        /// https://stackoverflow.com/questions/1988833/converting-color-to-consolecolor
        /// </summary>
        /// <param name="color">The color to be converted</param>
        /// <returns>The ConsoleColor value for the Color</returns>
        public static ConsoleColor ClosestConsoleColor(Color color)
        {
            ConsoleColor ret = 0;
            double rr = color.R, gg = color.G, bb = color.B, delta = double.MaxValue;

            foreach (ConsoleColor closestConsoleColor in Enum.GetValues(typeof(ConsoleColor)))
            {
                string colorName = Enum.GetName(typeof(ConsoleColor), closestConsoleColor);
                Color colorEnum = Color.FromName((colorName == "DarkYellow" ? "Orange" : colorName) ?? string.Empty);
                var t = Math.Pow(colorEnum.R - rr, 2.0) + Math.Pow(colorEnum.G - gg, 2.0) + Math.Pow(colorEnum.B - bb, 2.0);
                
                if (t == 0.0) return closestConsoleColor;
                
                if (t < delta)
                {
                    delta = t;
                    ret = closestConsoleColor;
                }
            }
            
            return ret;
        }
    }
}