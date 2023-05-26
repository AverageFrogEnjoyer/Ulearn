using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game_.Managers
{
    public class SwampManager
    {
        public static List<Swamp> Swamps = new();
        private static Texture2D texture;
        //private static float spawnCooldown;
        //private static float spawnTime;
        private static Random random;
        //private static int padding;
        public static void Init()
        {
            texture = Globals.Content.Load<Texture2D>("Grass-1");
            //spawnCooldown = 0.01f;
            //spawnTime = spawnCooldown;
            random = new();
            //padding = texture.Width / 2;
        }
        public static void Update()
        {
            //spawnTime -= Globals.TotalSeconds;
            //while (spawnTime <= 0)
            //{
            //    spawnTime += spawnCooldown;
                //AddSwamp();
            //}

            //foreach (var swamp in Swamps)
            //{
            //    swamp.Update(player);
            //}
            //Enemies.RemoveAll((z) => z.HP <= 0);
            if (Swamps.Count < 6)
            {
                Swamps.Add(new(texture, GetRandomPosition()));
            }
        }
        //public static void AddSwamp()
        //{
            
        //}
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
            return position/*new(100, 100)*/;
        }


        public static void Reset()
        {
            Swamps.Clear();
            //spawnTime = spawnCooldown;
        }
    }
}
