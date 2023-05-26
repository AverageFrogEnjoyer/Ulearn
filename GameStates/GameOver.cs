using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.GameStates
{
    public class GameOver
    {
        public static Texture2D Sprite { get; set; }
        public static void Load()
        {
            Sprite = Globals.Content.Load<Texture2D>("End");
        }
        public static void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(
                Sprite,
                new Vector2((Globals.Bounds.X - Sprite.Width) / 2,
                (Globals.Bounds.Y - Sprite.Height) / 2),
                null,
                Color.White * 0.05f,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
        }
    }
}
