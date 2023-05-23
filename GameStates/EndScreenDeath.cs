using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game_.GameStates
{
    public class GameOver
    {
        public static Texture2D Sprite { get; set; }
        public static void Draw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(Sprite, Vector2.Zero, Color.White);
            spriteBatch.Draw(Sprite, new Vector2((Globals.Bounds.X - Sprite.Width) / 2, (Globals.Bounds.Y - Sprite.Height) / 2), null, Color.White * 0.05f, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

        }
    }
}
