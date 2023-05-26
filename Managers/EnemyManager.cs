using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game_.Managers
{
    public class EnemyManager
    {
        public static List<Enemy> Enemies = new();
        private static Texture2D texture;
        private static float spawnCooldown;
        private static float spawnTime;
        private static Random random;
        private static int padding;
        public static int speed = 150;

        public static void Init()
        {
            texture = Globals.Content.Load<Texture2D>("Ghost");
            spawnCooldown = 1f;
            spawnTime = spawnCooldown;
            random = new();
            padding = texture.Width / 2;
        }
        public static void Reset()
        {
            Enemies.Clear();
            spawnTime = spawnCooldown;
        }

        private static Vector2 GetRandomPosition()
        {
            float width = Globals.Bounds.X;
            float height = Globals.Bounds.Y;
            Vector2 pos = new();
            if (random.NextDouble() < width / (width + height))
            {
                pos.X = (int)(random.NextDouble() * width - width / 2);
                pos.Y = (int)(random.NextDouble() < 0.5 ? -padding : height + padding);
            }
            else
            {
                pos.Y = (int)(random.NextDouble() * height - height / 2);
                pos.X = (int)(random.NextDouble() < 0.5 ? -padding : width + padding);
            }
            return pos;
        }
        public static void AddEnemy()
        {
            if (Enemies.Count < 4)
            {
                speed += 4;
                Enemies.Add(new(texture, GetRandomPosition(), speed));
            }
        }

        public static void Update(Player player)
        {
            spawnTime -= Globals.TotalSeconds;
            while (spawnTime <= 0)
            {
                spawnTime += spawnCooldown;
                AddEnemy();
            }
            foreach (var enemy in Enemies)
            {
                enemy.Update(player);
            }
            Enemies.RemoveAll((z) => z.HP <= 0);
        }

        public static void Draw()
        {
            foreach (var enemy in Enemies)
            {
                enemy.Draw();
            }
        }
    }
}