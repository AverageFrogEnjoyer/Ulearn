using _Game_.Entities;
using Game_;
using GameShooter.Entities;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShooter.Managers
{
    public class BulletManager
    {
        private static Texture2D _texture;
        public static List<Bullet> Bullets { get; } = new();

        public static void Init(Texture2D tex)
        {
            _texture = tex;
        }

        public static void Reset()
        {
            Bullets.Clear();
        }

        public static void AddBullet(BulletData data)
        {
            Bullets.Add(new(_texture, data));
        }

        public static void Update(List<Enemy> enemies)
        {
            foreach (var bullet in Bullets)
            {
                bullet.Update();
                foreach (var enemy in enemies)
                {
                    if (enemy.HP <= 0) continue;
                    if ((bullet.Position - enemy.Position).Length() < 32)
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
