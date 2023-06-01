using _Game_.Entities;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace _Game_.Managers
{
    public class PlayerManager
    {
        public static Player player;
        public static Texture2D PlayerSprite { get; set; }
        public  static Texture2D DeathSprite { get; set; }
        private static int currentTime = 0;
        private static int period = 18;
        public static Point currentFrame = new Point(0, 0);
        private static Point spriteSize = new Point(4, 5);
        public static void Init()
        {
            PlayerSprite = Globals.Content.Load<Texture2D>("Player_new");
            DeathSprite = Globals.Content.Load<Texture2D>("PlayerDead");
            player = new(PlayerSprite, new(Globals.Bounds.X / 2 - PlayerSprite.Width / 8, Globals.Bounds.Y / 2 - PlayerSprite.Height / 10));
            //player.Reset();
        }
        public static void Reset()
        {
            currentFrame = new Point(0, 0);
            spriteSize = new Point(4, 5);
            player.Reset();
        }

        public static void Update()
        {
            player.Update();
        }

        public static void ChangePositionAndFrame(int row)
        {
            if (Globals.IsPaused)
                return;
            currentTime += Globals.gameTime.ElapsedGameTime.Milliseconds;
            if (currentTime > period)
            {
                currentTime -= period;
                currentFrame.Y = row;
                ++currentFrame.X;
                if (currentFrame.X >= spriteSize.X)
                {
                    currentFrame.X = 1;
                }
            }
        }

        public static void Draw()
        {
            if (player.IsDead)
            {
                Globals.SpriteBatch.Draw(DeathSprite, player.Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            else
            {
                Globals.SpriteBatch.Draw(PlayerSprite, player.Position,
                new Rectangle(currentFrame.X * player.frameWidth,
                    currentFrame.Y * player.frameHeight,
                    player.frameWidth, player.frameHeight),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            }
        }

    }
}
