
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Models
{
    public class HoldBlock
    {

        private const int Columns = 4;
        private const int Rows = 4;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; private set; }

        public AbstractBlock Block { get; set; }

        public HoldBlock(int cellSize = 20)
        {
            Size = new Vector2(cellSize * Columns, cellSize * Rows);
        }

        public void DrawCells(SpriteBatch spriteBatch, int cellSize = 10)
        {

            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position, new Rectangle(0, 0, Columns * cellSize, cellSize * Rows), Color.White);
            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position + new Vector2(1, 1), new Rectangle(0, 0, Columns * cellSize -2, cellSize * Rows - 2), Color.Black);

            var cellPos = new Vector2();
            if(Block == null) return;
            foreach (var cell in Block.Cells)
            {
                cellPos.X = (cell.X + 2) * cellSize + Position.X;
                cellPos.Y = (cell.Y + 2) * cellSize + Position.Y;
                spriteBatch.Draw(Tetris.Graphics.Textures.Cell, cellPos, new Rectangle(0, 0, cellSize, cellSize), Color.White);
                spriteBatch.Draw(Tetris.Graphics.Textures.Cell, cellPos, new Rectangle(1, 1, cellSize - 1, cellSize - 1), Block.Color);
            }


        }
    }
}