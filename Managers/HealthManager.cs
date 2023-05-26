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

namespace _Game_.Managers
{
    public class HealthManager
    {
        private static Texture2D _texture;
        public static List<Health> HealthBoost { get; } = new();
        //private static SpriteFont _font;
        private static Vector2 _position;
        //private static Vector2 _textPosition;
        private static string _playerExp;

        public static void Init()
        {
            _texture = Globals.Content.Load<Texture2D>("Health");
            //_font = Globals.Content.Load<SpriteFont>("font");
            //_position = new(Globals.Bounds.X - (2 * _texture.Width), 0);
        }

        public static void Reset()
        {
            HealthBoost.Clear();
        }

        public static void AddHealth(Vector2 pos)
        {
            var rnd = new Random();
            if (rnd.Next(2) < 1)
                HealthBoost.Add(new(_texture, pos));
        }

        public static void Update(Player player)
        {
            foreach (var health in HealthBoost)
            {
                health.Update();

                if ((health.Position - player.Position - new Vector2(Player.frameWidth / 2, Player.frameHeight / 2)).Length() < 20)
                {
                    health.Collect();
                    //player.GetExperience(1);
                    if (player.Health + 20 > player.MaxHealth)
                        player.Health = player.MaxHealth;
                    else
                        player.Health += 20;
                }
            }

            HealthBoost.RemoveAll((e) => e.Lifespan <= 0);

            //_playerExp = player.Health.ToString();
            //var x = _font.MeasureString(_playerExp).X / 2;
            //_textPosition = new(Globals.Bounds.X - x - 32, 14);
        }

        public static void Draw()
        {
            //Globals.SpriteBatch.Draw(_texture, _position, null, Color.White * 0.75f, 0f, Vector2.Zero, 2f, SpriteEffects.None, 1f);
            //Globals.SpriteBatch.DrawString(_font, _playerExp, _textPosition, Color.White);

            foreach (var e in HealthBoost)
            {
                e.Draw();
            }
        }
    }
}
