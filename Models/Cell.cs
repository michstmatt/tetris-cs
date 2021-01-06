using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Models
{
    public class Cell
    {
        public int X {get; set;}
        public int Y {get; set;}

        public Color Color {get; set;}

        public static void DrawCell(SpriteBatch spriteBatch, Vector2 truePosition, int size, Color color, Color? borderColor = null)
        {
            int borderSize = size / 10;
            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, truePosition, new Rectangle(0, 0, size, size), borderColor ?? Color.White);
            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, truePosition + new Vector2(borderSize, borderSize), new Rectangle(0, 0, size - 2*borderSize, size - 2*borderSize), color);
        }
    }
}