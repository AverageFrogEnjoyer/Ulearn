﻿using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using Color = Microsoft.Xna.Framework.Color;

namespace _Game_.Managers
{
    public class InterfaceManager
    {
        private static Texture2D bulletTexture;
        private static SpriteFont _score;
        private static SpriteFont _bestScore;
        private static int fontY = 10;
        private static int fontX = Globals.Bounds.X / 2 - 50;

        public static void Init()
        {
            bulletTexture = Globals.Content.Load<Texture2D>("Ball");
            _score = Globals.Content.Load<SpriteFont>("Score");
            _bestScore = Globals.Content.Load<SpriteFont>("Score");
        }
        public static void Draw(Player player)
        {
            Color color = player.reloading ? Color.Red : Color.White;
            for (var i = 0; i < player.amo; i++)
            {
                Vector2 pos = new(i * bulletTexture.Width * 0.75f + 32, 32);
                Globals.SpriteBatch.Draw(bulletTexture, pos, null, color * 0.75f, 0, Vector2.Zero, 0.75f, SpriteEffects.None, 1);
            }
            Globals.SpriteBatch.DrawString(_score, $"Score: {Player.Score}", new Vector2(fontX, fontY), Color.White);
            Globals.SpriteBatch.DrawString(_bestScore, $"Best Score: {Player.BestScore}", new Vector2(fontX - 35, fontY + 45), Color.White);
        }
    }
}
