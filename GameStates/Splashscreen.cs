using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.GameStates
{
    public class Splashscreen
    {
        public static Texture2D SpriteBack { get; set; }
        public static Texture2D SpriteText1 { get; set; }
        public static Texture2D SpriteText2 { get; set; }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(SpriteBack, Vector2.Zero, Color.White);
            spriteBatch.Draw(SpriteText2, new Vector2((Globals.Bounds.X - SpriteText2.Width) / 2, (Globals.Bounds.Y - SpriteText2.Height) / 2), null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 1);

        }
    }
}
