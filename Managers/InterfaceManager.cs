using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Color = Microsoft.Xna.Framework.Color;

namespace _Game_.Managers
{
    public class InterfaceManager
    {
        private static Texture2D bulletTexture;
        public static void Init(Texture2D tex)
        {
            bulletTexture = tex;
        }
        public static void Draw(Player player)
        {
            Color color = player.reloading ? Color.Red : Color.White;
            //Globals.SpriteBatch.Draw(bulletTexture, new(0, 0), null, color * 0.75f, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            //Globals.SpriteBatch.Draw(bulletTexture, new(bulletTexture.Width + 10, 0), null, color * 0.75f, 0, Vector2.Zero, 1, SpriteEffects.None, 1);
            for (var i = 0; i < player.amo; i++)
            {
                Vector2 pos = new(i * bulletTexture.Width * 0.75f + 32, 32);
                Globals.SpriteBatch.Draw(bulletTexture, pos, null, color * 0.75f, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 1);

            }
        }
    }
}
