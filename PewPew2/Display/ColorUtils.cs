using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using System.Globalization;
using Microsoft.Xna.Framework.Graphics;

namespace PewPew2.Display
{
    public static class ColorUtils
    {
        /// <summary>
        /// Creates an ARGB hex string representation of the <see cref="Color"/> value.
        /// </summary>
        /// <param name="color">The <see cref="Color"/> value to parse.</param>
        /// <param name="includeHash">Determines whether to include the hash mark (#) character in the string.</param>
        /// <returns>A hex string representation of the specified <see cref="Color"/> value.</returns>
        public static string ToHex(this Color color, bool includeHash)
        {
            string[] argb = {
                color.A.ToString("X2"),
                color.R.ToString("X2"),
                color.G.ToString("X2"),
                color.B.ToString("X2"),
            };
            return (includeHash ? "#" : string.Empty) + string.Join(string.Empty, argb);
        }

        /// <summary>
        /// Creates a <see cref="Color"/> value from an ARGB or RGB hex string.  The string may
        /// begin with or without the hash mark (#) character.
        /// </summary>
        /// <param name="hexString">The ARGB hex string to parse.</param>
        /// <returns>
        /// A <see cref="Color"/> value as defined by the ARGB or RGB hex string.
        /// </returns>
        /// <exception cref="InvalidOperationException">Thrown if the string is not a valid ARGB or RGB hex value.</exception>
        public static Color ToColor(this string hexString)
        {
            if (hexString.StartsWith("#"))
                hexString = hexString.Substring(1);
            uint hex = uint.Parse(hexString, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
            Color color = Color.White;
            switch (hexString.Length)
            {
                case 8:
                    color.A = (byte)(hex >> 24);
                    color.R = (byte)(hex >> 16);
                    color.G = (byte)(hex >> 8);
                    color.B = (byte)(hex);
                    break;
                case 6:
                    color.R = (byte)(hex >> 16);
                    color.G = (byte)(hex >> 8);
                    color.B = (byte)(hex);
                    break;
                default:
                    throw new InvalidOperationException("Invald hex representation of an ARGB or RGB color value.");
            }
            return color;
        }

        public static void DrawShadowedString(this SpriteBatch batch, SpriteFont font, string value, Vector2 position, Color color)
        {
            batch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black);
            batch.DrawString(font, value, position, color);
        }

        public static void DrawShadowedString(this SpriteBatch batch, SpriteFont font, string value, Vector2 position, Color color, float rotation, Vector2 origin, float scale, SpriteEffects effects, float layerDepth)
        {
            batch.DrawString(font, value, position + new Vector2(1.0f, 1.0f), Color.Black, rotation, origin, scale, effects, layerDepth);
            batch.DrawString(font, value, position, color, rotation, origin, scale, effects, layerDepth);
        }
    }
}
