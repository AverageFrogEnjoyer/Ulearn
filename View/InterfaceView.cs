using _Game_.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Color = Microsoft.Xna.Framework.Color;

namespace _Game_.Managers
{
    public class InterfaceView
    {
        private static Texture2D bulletTexture;
        private static Texture2D healthTexture;
        private static SpriteFont _score;
        private static SpriteFont _bestScore;

        public static void Init()
        {
            bulletTexture = Globals.Content.Load<Texture2D>("Ball");
            _score = Globals.Content.Load<SpriteFont>("Score");
            _bestScore = Globals.Content.Load<SpriteFont>("Score");
            healthTexture = Globals.Content.Load<Texture2D>("HealthBar");

        }
        public static void Draw()
        {
            var player = PlayerView.player;
            Globals.SpriteBatch.DrawString(_score, $"Score: {player.Score}", new Vector2(Globals.Bounds.X / 2 - 40, 32), Color.White);
            Globals.SpriteBatch.DrawString(_bestScore, $"Best Score: {player.BestScore}", new Vector2(Globals.Bounds.X / 2 - 70, 74), Color.White);

            Color color = player.reloading ? Color.Red : Color.White;
            for (var i = 0; i < player.bulletsCount; i++)
            {
                Vector2 pos = new(i * bulletTexture.Width * 0.75f + 32, 96);
                Globals.SpriteBatch.Draw(
                    bulletTexture,
                    pos,
                    null,
                    color * 0.75f,
                    0,
                    Vector2.Zero,
                    0.75f,
                    SpriteEffects.None,
                    1);
            }
            for (var i = 0; i < player.Health; i++)
            {
                Vector2 pos = new(i * healthTexture.Width * 0.2f + 32, 32);
                Globals.SpriteBatch.Draw(
                    healthTexture,
                    pos,
                    null,
                    Color.White * 0.75f,
                    0,
                    Vector2.Zero,
                    0.75f,
                    SpriteEffects.None,
                    1);
            }
        }
    }
}
