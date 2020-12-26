using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Tetris.Graphics
{
   public static class Textures
   {
       public static Texture2D Cell {get; private set;}

       public static void Init(GraphicsDevice device)
       {
           Cell = new Texture2D(device, 1, 1);
           Cell.SetData(new Color[]{Color.White});
       }
   } 
}