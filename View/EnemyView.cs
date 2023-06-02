using _Game_.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _Game_.Managers
{
    public class EnemyView
    {
        public static List<Enemy> Enemies = new();
        private static Texture2D texture;
        private static float spawnCooldown;
        private static float spawnTime;
        private static Random random;
        private static int padding;
        public static int speed;

        public static void Init()
        {
            texture = Globals.Content.Load<Texture2D>("Ghost");
            spawnCooldown = 1f;
            spawnTime = spawnCooldown;
            random = new();
            padding = texture.Width / 2;
            speed = 150;
            
            AddEnemy();
        }
        public static void Reset()
        {
            Enemies.Clear();
            speed = 150;
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
                speed += 3;
                Enemies.Add(new(texture, GetRandomPosition(), speed));
            }
        }

        public static void Update()
        {
            spawnTime -= Globals.TotalSeconds;
            while (spawnTime <= 0)
            {
                spawnTime += spawnCooldown;
                AddEnemy();
            }
            foreach (var enemy in Enemies)
            {
                enemy.Update(PlayerView.player);
            }
            foreach(var enemy in Enemies)
            {
                if (enemy.HP <= 0)
                    PlayerView.player.Score++;
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