﻿using _Game_.Entities;
using _Game_.Managers;
using GameShooter.Entities;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace GameShooter.Managers
{
    public class BulletManager
    {
        private static Texture2D _texture;
        public static List<Bullet> Bullets { get; } = new();

        public static void Init()
        {
            _texture = Globals.Content.Load<Texture2D>("Ball");
        }

        public static void Reset()
        {
            Bullets.Clear();
        }

        public static void AddBullet(BulletData data)
        {
            Bullets.Add(new(_texture, data));
        }

        public static void Update()
        {
            var enemies = EnemyManager.Enemies;
            foreach (var bullet in Bullets)
            {
                bullet.Update();
                foreach (var enemy in enemies)
                {
                    if (enemy.HP <= 0)
                    {
                        continue;
                    }
                    if ((bullet.Position - enemy.Position).Length() < 50)
                    {
                        enemy.GetDamage(bullet.Damage);
                        bullet.Destroy();
                        break;
                    }
                }
            }
            Bullets.RemoveAll((p) => p.Lifespan <= 0);
        }

        public static void Draw()
        {
            foreach (var p in Bullets)
            {
                p.Draw();
            }
        }
    }
}
