
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Models
{
    public class BlockQueue
    {

        private const int Columns = 4;
        private const int Rows = 4;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; private set; }

        public List<AbstractBlock> Blocks { get; set; }

        public BlockQueue(int cellSize = 20)
        {
            Size = new Vector2(cellSize * Columns, cellSize * 25);
        }

       public void DrawCells(SpriteBatch spriteBatch, int cellSize = 10)
        {

            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position, new Rectangle(0, 0, Columns * cellSize, 20 * cellSize), Color.White);
            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position + new Vector2(1,1), new Rectangle(0,0, Columns * cellSize - 2, cellSize * 20 - 2), Color.Black);

            var cellPos = new Vector2();
            for(int i = 0; i < Blocks.Count; i++)
            {
                foreach(var cell in Blocks[i].Cells)
                {
                    cellPos.X = (cell.X + 2) * cellSize + Position.X;
                    cellPos.Y = (cell.Y + 2) * cellSize + Position.Y + i * 4 * cellSize;
                    spriteBatch.Draw(Tetris.Graphics.Textures.Cell, cellPos, new Rectangle(0, 0, cellSize, cellSize), Color.White);
                    spriteBatch.Draw(Tetris.Graphics.Textures.Cell, cellPos, new Rectangle(1, 1, cellSize - 1, cellSize - 1), Blocks[i].Color);
                }
            }

        }
    }
}