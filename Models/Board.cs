using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Models
{
    public class Board
    {
        public readonly int Columns;
        public readonly int Rows;
        public readonly int ViewableRows;

        public Vector2 Position { get; set; }
        public Vector2 Size { get; private set; }

        public List<Color?[]> Cells { get; private set; }

        public Board(int col = 10, int row = 40, int viewable = 20, int cellSize = 20)
        {
            Columns = col;
            Rows = row;
            ViewableRows = viewable;
            Cells = new List<Color?[]>();
            for (int i = 0; i < row; i++)
            {
                Cells.Add(new Color?[col]);
            }
            Size = new Vector2(cellSize * col, cellSize * viewable);
        }

        public void ClearBlock(AbstractBlock block)
        {
            foreach (Cell c in block.Cells)
            {
                var y = block.Y + c.Y + ViewableRows;
                var x = c.X + block.X;
                this.Cells[y][x] = null;
            }
        }

        public bool BlockCanMove(AbstractBlock block, int mX, int mY)
        {
            bool allowed = true;
            foreach (Cell c in block.Cells)
            {
                var y = block.Y + c.Y + ViewableRows + mY;
                var x = c.X + block.X + mX;
                allowed &= x >= 0 && x < Columns && y >= 0 && y < Rows && this.Cells[y][x] == null;
            }
            return allowed;
        }

        public bool BlockCanRotate(AbstractBlock block, bool right)
        {
            return true;
        }

        public void BlockPreview(AbstractBlock block)
        {
            var minY = 0;
            var selectedX = 0;
            foreach (Cell c in block.Cells)
            {
                var y = c.Y;
                if (y > minY)
                {
                    selectedX = c.X;
                }
            }

            var bottom = 0;
            for(int r = block.Y + minY; r < Rows; r++)
            {
                bottom = r-1;
                if(this.Cells[r][selectedX + block.X] != null)
                {
                    break;
                }
            }

            foreach (Cell c in block.Cells)
            {
                var y = bottom + c.Y + ViewableRows;
                var x = c.X + block.X;
                this.Cells[y][x] = Color.LightGray;
            }

        }
        public void UpdateBlock(AbstractBlock block)
        {
            UpdateInBounds(block);
            foreach (Cell c in block.Cells)
            {
                var y = block.Y + c.Y + ViewableRows;
                var x = c.X + block.X;
                this.Cells[y][x] = block.Color;
            }
        }
        private void UpdateInBounds(AbstractBlock block)
        {
            foreach(var c in block.Cells)
            {
                var y = block.Y + c.Y + ViewableRows;
                var x = c.X + block.X;
                // too far left, move right
                if (x < 0 || this.Cells[y][x] != null)
                {
                    block.X -= c.X;
                    break;
                }
                if (x >= this.Columns || this.Cells[y][x] != null)
                {
                    block.X -= c.X ;
                    break;
                }
            }
        }

        public bool DidLose()
        {
            for (int c = 0; c < Columns; c++)
            {
                if (this.Cells[ViewableRows - 1][c] != null)
                {
                    return true;
                }
            }
            return false;
        }

        public int Update()
        {
            int rowsCleared = 0;
            for (int r = Rows - 1; r >= ViewableRows; r--)
            {
                bool fullRow = true;
                bool emptyRow = true;
                for (int c = 0; c < Columns; c++)
                {
                    fullRow &= this.Cells[r][c] != null;
                    emptyRow &= this.Cells[r][c] == null;
                }

                if (fullRow)
                {
                    this.Cells.RemoveAt(r);
                    this.Cells.Insert(0, new Color?[Columns]);
                    r++;
                    rowsCleared ++;
                }
                else if(emptyRow)
                {
                    break;
                }
            }
            return rowsCleared;
        }
        public void DrawCells(SpriteBatch spriteBatch, int cellSize = 10)
        {

            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position, new Rectangle(0, 0, Columns * cellSize, ViewableRows * cellSize), Color.White);
            spriteBatch.Draw(Tetris.Graphics.Textures.Cell, Position + new Vector2(1,1), new Rectangle(1, 1, Columns * cellSize - 2, ViewableRows * cellSize - 2), Color.Black);

            for (int row = ViewableRows; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    if (Cells[row][col] != null)
                    {
                        var adjustedRow = row - ViewableRows;
                        var cellPos = Position + new Vector2(col * cellSize, adjustedRow * cellSize);
                        Cell.DrawCell(spriteBatch, cellPos, cellSize, Cells[row][col].Value);
                    }
                }
            }

        }
    }
}