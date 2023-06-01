using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _Game_.Managers
{
    public class HealthManager
    {
        private static Texture2D _texture;
        public static List<Health> HealthBoost { get; } = new();

        public static void Init()
        {
            _texture = Globals.Content.Load<Texture2D>("Health");
        }

        public static void Reset()
        {
            HealthBoost.Clear();
        }

        public static void AddHealth(Vector2 pos)
        {
            var rnd = new Random();
            if (rnd.Next(2) < 1)
            {
                HealthBoost.Add(new(_texture, pos));
            }
        }

        public static void Update(/*Player player*/)
        {
            var player = PlayerManager.player;
            foreach (var health in HealthBoost)
            {
                health.Update();
                if ((health.Position - player.Position - new Vector2(player.frameWidth / 2, player.frameHeight / 2)).Length() < 40)
                {
                    health.Collect();
                    if (player.Health + 30 > player.MaxHealth)
                    {
                        player.Health = player.MaxHealth;

                    }
                    else
                    {
                        player.Health += 30;
                    }
                }
            }
            HealthBoost.RemoveAll((e) => e.Lifespan <= 0);
        }

        public static void Draw()
        {
            foreach (var e in HealthBoost)
            {
                e.Draw();
            }
        }
    }
}
