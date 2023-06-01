using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.Managers
{
    public class MenuManager
    {
        public static Texture2D SpriteBack { get; set; }
        public static Texture2D SpriteText { get; set; }
        public static Texture2D Manual { get; set; }

        public static void Load()
        {
            SpriteBack = Globals.Content.Load<Texture2D>("Splash");
            SpriteText = Globals.Content.Load<Texture2D>("StartButton3");
            Manual = Globals.Content.Load<Texture2D>("Rules");

        }

        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                SpriteBack,
                Vector2.Zero,
                Color.White);
            spriteBatch.Draw(
                SpriteText,
                new Vector2((Globals.Bounds.X - SpriteText.Width) / 2,
                (Globals.Bounds.Y - SpriteText.Height) / 2),
                null,
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
            spriteBatch.Draw(
                Manual,
                new Vector2((Globals.Bounds.X - Manual.Width) / 2,
                Globals.Bounds.Y - Manual.Height - 64),
                null,
                Color.White,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1);

        }
    }
}
