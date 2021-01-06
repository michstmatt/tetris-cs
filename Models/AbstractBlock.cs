using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Models
{
    public abstract class AbstractBlock : Cell
    {
        public List<Cell> Cells { get; protected set; }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public OrientationEnum Orientation { get; protected set; } = OrientationEnum.UP;

        private int lastY;
        public float UnmovedTime { get; private set; }
        public bool IsSet { get; set; }

        public AbstractBlock(int row, int col)
        {
            this.X = col;
            this.Y = row;
            this.lastY = row;
            this.UnmovedTime = 0;
        }

        public virtual void RotateLeft()
        {
            var orient = (int)(this.Orientation);
            orient = this.Orientation == OrientationEnum.DOWN ? ((int)OrientationEnum.RIGHT) : orient + 1;
            this.Orientation = (OrientationEnum)orient;

            foreach (var cell in Cells)
            {
                var xNew = cell.Y;
                var yNew = -1 * cell.X;
                cell.X = xNew;
                cell.Y = yNew;
            }

        }

        public virtual void RotateRight()
        {
            var orient = (int)(this.Orientation);
            orient = this.Orientation == OrientationEnum.RIGHT ? ((int)OrientationEnum.DOWN) : orient - 1;
            this.Orientation = (OrientationEnum)orient;
            foreach (var cell in Cells)
            {
                var xNew = -1 * cell.Y;
                var yNew = cell.X;
                cell.X = xNew;
                cell.Y = yNew;
            }

        }

        public void HorizontalMove(bool right)
        {
            this.X += right ? 1 : -1;
        }

        public void MoveDown(int maxY)
        {
            this.lastY = Y;
            UnmovedTime = 0;
            this.Y += 1;
        }
        public void Update(float time)
        {
            UnmovedTime += this.lastY == Y ? time : 0;
            if(UnmovedTime > 2000)
            {
                IsSet = true;
            }
            this.lastY = Y;
        }
    }
}