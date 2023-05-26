using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace _Game_.Managers
{
    public class SwampManager
    {
        public static List<Swamp> Swamps = new();
        private static Texture2D texture;
        private static Random random;

        public static void Init()
        {
            texture = Globals.Content.Load<Texture2D>("Swamp");
            random = new();
        }

        public static void Update()
        {
            if (Swamps.Count < 6)
            {
                Swamps.Add(new(texture, GetRandomPosition()));
            }
        }

        public static void Draw()
        {
            foreach (var swamp in Swamps)
            {
                swamp.Draw();
            }
        }

        private static Vector2 GetRandomPosition()
        {
            var width = Globals.Bounds.X;
            var height = Globals.Bounds.Y;
            Vector2 position = new();
            position.X = random.Next(width - texture.Width) + texture.Width;
            position.Y = random.Next(height - texture.Height) + texture.Height;
            return position;
        }

        public static void Reset()
        {
            Swamps.Clear();
        }
    }
}
