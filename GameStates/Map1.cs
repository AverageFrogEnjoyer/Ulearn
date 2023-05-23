using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace _Game_.GameStates
{
    public class Map1
    {
        public static Texture2D Sprite { get; set; }
        public static void Draw(SpriteBatch spriteBatch)
        {
            for (var x = 0; x < Globals.Bounds.X / 32; x++)
            {
                for (var y = 0; y < Globals.Bounds.Y / 32; y++)
                {
                    Globals.SpriteBatch.Draw(Sprite, new Vector2(x * 32, y * 32), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
                }
            }
        }


    }
}
