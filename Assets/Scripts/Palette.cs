using UnityEngine;

namespace Tetris
{
    public class Palette
    {
        public static Color Cyan = new Color(0f, 240f / 255f, 240f / 255f);
        public static Color Blue = new Color(0f, 0f, 240f / 255f);
        public static Color Orange = new Color(240f / 255f, 160f / 255f, 0f);
        public static Color Yellow = new Color(241f / 255f, 240f / 255f, 0f);
        public static Color Green = new Color(1f / 255f, 239f / 255f, 0f);
        public static Color Purple = new Color(157f / 255f, 1f / 255f, 235 / 255f);
        public static Color Red = new Color(241f / 255f, 0f, 0f);
        public static Color White = new Color(1f, 1f, 1f);

        public static Color GetColor(PaletteColor colorEnum)
        {
            return colorEnum switch
            {
                PaletteColor.Cyan => Cyan,
                PaletteColor.Blue => Blue,
                PaletteColor.Orange => Orange,
                PaletteColor.Yellow => Yellow,
                PaletteColor.Green => Green,
                PaletteColor.Purple => Purple,
                PaletteColor.Red => Red,
                PaletteColor.White => White,
                _ => Color.white
            };
        }
    }
    public enum PaletteColor
    {
        Cyan, Blue, Orange, Yellow, Green, Purple, Red, White
    }
}
