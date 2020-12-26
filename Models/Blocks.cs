using System.Collections.Generic;

using Microsoft.Xna.Framework;
namespace Tetris.Models
{
    class ZBlock : AbstractBlock
    {
        public ZBlock(int row, int col) : base(row, col)
        {
            Width = 3;
            Height = 3;
            Color = Color.Red;
            Cells = new List<Cell>()
            {
                new Cell{Y = -1, X = 1},
                new Cell{Y = -1, X = 0},
                new Cell{Y = 0, X = 0},
                new Cell{Y = 0, X = -1},
            };
        }
    }

    class SBlock : AbstractBlock
    {
        public SBlock(int row, int col) : base(row, col)
        {
            Width = 3;
            Height = 3;
            Color = Color.Green;
            Cells = new List<Cell>()
            {
                new Cell{Y = -1, X = -1},
                new Cell{Y = -1, X = 0},
                new Cell{Y = 0, X = 0},
                new Cell{Y = 0, X = 1},
            };
        }
    }
    class TBlock : AbstractBlock
    {
        public TBlock(int row, int col) : base(row, col)
        {
            Width = 3;
            Height = 3;
            Color = Color.MediumPurple;
            Cells = new List<Cell>()
            {
                new Cell{Y = -1, X = 0},
                new Cell{Y = 0, X = -1},
                new Cell{Y = 0, X = 0},
                new Cell{Y = 0, X = 1},
            };
        }
    }


    class LBlock : AbstractBlock
    {
        public LBlock(int r, int c) : base(r, c)
        {
            Width = 4;
            Height = 4;
            Color = Color.Blue;
            Cells = new List<Cell>()
            {
                new Cell{Y = -1, X = -1},
                new Cell{Y = 0, X = -1},
                new Cell{Y = 1, X = -1},
                new Cell{Y = 1, X = -2},
            };

        }
    }
    class JBlock : AbstractBlock
    {
        public JBlock(int r, int c) : base(r, c)
        {
            Width = 4;
            Height = 4;
            Color = Color.Orange;
            Cells = new List<Cell>()
            {
                new Cell{Y = -1, X = -1},
                new Cell{Y = 0, X = -1},
                new Cell{Y = 1, X = -1},
                new Cell{Y = 1, X = 0},
            };

        }
    }

    // 4 blocks
    class Square : AbstractBlock
    {
        public Square(int r, int c) : base(r, c)
        {
            Width = 4;
            Height = 4;
            Color = Color.Yellow;
            Cells = new List<Cell>()
            {
                new Cell{Y = -1, X = -1},
                new Cell{Y = 0, X = -1},
                new Cell{Y = -1, X = 0},
                new Cell{Y = 0, X =  0},
            };
        }
    }
    
    class Straight : AbstractBlock
    {
        public Straight(int r, int c) : base(r, c)
        {
            Width = 4;
            Height = 4;
            Color = Color.LightBlue;
            Cells = new List<Cell>()
            {
                new Cell{Y = -2, X = -1},
                new Cell{Y = -1, X = -1},
                new Cell{Y = 0, X = -1},
                new Cell{Y = 1, X =  -1},
            };
        }
    }
    
}