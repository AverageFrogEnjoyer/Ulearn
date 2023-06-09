﻿using _Game_.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.Managers
{
    public class MapView
    {
        public static Texture2D Sprite { get; set; }
        public static void Load()
        {
            Sprite = Globals.Content.Load<Texture2D>("Grass");
        }
        public static void Draw()
        {
            for (var x = 0; x < Globals.Bounds.X / 32; x++)
            {
                for (var y = 0; y < Globals.Bounds.Y / 32; y++)
                {
                    Globals.SpriteBatch.Draw(
                        Sprite,
                        new Vector2(x * 32, y * 32),
                        null,
                        Color.White,
                        0,
                        Vector2.Zero,
                        1,
                        SpriteEffects.None,
                        0);
                }
            }
        }
    }
}
