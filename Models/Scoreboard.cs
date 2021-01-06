using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Models
{
    public class Scoreboard
    {


        public Vector2 Position { get; set; }
        public Vector2 Size { get; private set; }


        public Scoreboard()
        {
        }

        public void Draw(SpriteBatch spriteBatch, double time, float scores)
        {
            var pos = this.Position;
            var offset = new Vector2(0,20);

            spriteBatch.DrawString(Tetris.Graphics.Textures.Font, "Time", pos, Color.White);
            pos += offset;
            spriteBatch.DrawString(Tetris.Graphics.Textures.Font, time.ToString(), pos, Color.White);

            pos += offset;
            spriteBatch.DrawString(Tetris.Graphics.Textures.Font, "Score", pos, Color.White);
            pos += offset;
            spriteBatch.DrawString(Tetris.Graphics.Textures.Font, scores.ToString(), pos, Color.White);
        }
    }
}