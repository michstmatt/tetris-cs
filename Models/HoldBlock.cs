
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

        public void Draw(SpriteBatch spriteBatch, string title, int cellSize = 10)
        {
            this.DrawCells(spriteBatch, cellSize);
            var pos = this.Position + new Vector2(0, cellSize*4);
            spriteBatch.DrawString(Tetris.Graphics.Textures.Font, title, pos, Color.White);
        }

        private void DrawCells(SpriteBatch spriteBatch, int cellSize)
        {

            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position, new Rectangle(0, 0, Columns * cellSize, cellSize * Rows), Color.White);
            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position + new Vector2(1, 1), new Rectangle(0, 0, Columns * cellSize -2, cellSize * Rows - 2), Color.Black);

            var cellPos = new Vector2();
            if(Block == null) return;
            foreach (var cell in Block.Cells)
            {
                cellPos.X = (cell.X + 2) * cellSize + Position.X;
                cellPos.Y = (cell.Y + 2) * cellSize + Position.Y;

                Cell.DrawCell(spriteBatch, cellPos, cellSize, Block.Color);
            }


        }
    }
}