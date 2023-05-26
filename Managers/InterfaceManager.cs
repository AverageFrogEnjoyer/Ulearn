using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace _Game_.Managers
{
    public class InterfaceManager
    {
        private static Texture2D bulletTexture;
        private static Texture2D healthTexture;
        private static SpriteFont _score;
        private static SpriteFont _bestScore;
        private static int fontY = 10 ;
        private static int fontX = Globals.Bounds.X;

        public static void Init()
        {
            bulletTexture = Globals.Content.Load<Texture2D>("Ball");
            _score = Globals.Content.Load<SpriteFont>("Score");
            _bestScore = Globals.Content.Load<SpriteFont>("Score");
            healthTexture = Globals.Content.Load<Texture2D>("HealthBar");

        }
        public static void Draw(Player player)
        {
            Globals.SpriteBatch.DrawString(_score, $"Score: {player.Score}", new Vector2(fontX - 140, fontY), Color.White);
            Globals.SpriteBatch.DrawString(_bestScore, $"Best Score: {player.BestScore}", new Vector2(fontX - 205, fontY + 45), Color.White);

            if (player.IsDead)
                return;
            Color color = player.reloading ? Color.Red : Color.White;
            for (var i = 0; i < player.amo; i++)
            {
                Vector2 pos = new(i * bulletTexture.Width * 0.75f + 32, 96);
                Globals.SpriteBatch.Draw(bulletTexture, pos, null, color * 0.75f, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 1);
            }
            for (var i = 0; i < player.Health; i++)
            {
                Vector2 pos = new(i * healthTexture.Width * 0.2f + 32, 32);
                Globals.SpriteBatch.Draw(healthTexture, pos, null, Color.White * 0.75f, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 1);
            }
        }
    }
}
