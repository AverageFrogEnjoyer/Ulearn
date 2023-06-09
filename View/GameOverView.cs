﻿using _Game_.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.Managers
{
    public static class GameOverView
    {
        public static Texture2D Sprite;
        public static void Load()
        {
            Sprite = Globals.Content.Load<Texture2D>("End");
        }
        public static void Draw()
        {
            Globals.SpriteBatch.Draw(
                Sprite,
                new Vector2((Globals.Bounds.X - Sprite.Width) / 2,
                (Globals.Bounds.Y - Sprite.Height) / 2),
                null,
                Color.White * 0.9f,
                0,
                Vector2.Zero,
                1,
                SpriteEffects.None,
                1);
        }
    }
}
