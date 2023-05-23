using _Game_.Managers;
using Game_;
using GameShooter.Managers;
using GameShooter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _Game_.Entities
{
    public class Player : Sprite
    {
        public static Texture2D PlayerSprite { get; set; }
        public static Texture2D DeathSprite { get; set; }
        private static Point currentFrame = new Point(0, 0);
        private static Point spriteSize = new Point(4, 5);
        public static int frameWidth = 158;
        public static int frameHeight = 169;
        public static Vector2 velocity = new(8f, 8f);

        private float cooldown;
        private float cooldownLeft;
        public int maxAmo;
        public int amo { get; set; }
        public float reloadTime;
        public bool reloading { get; set; }
        public static bool Dead { get; /*private*/ set; }
        public int Experience { get; private set; }
        public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Speed = 450;
            maxAmo = 12;
            amo = maxAmo;
            reloadTime = 2f;
            reloading = false;
            cooldown = 0.25f;
            cooldownLeft = 0f;
            Dead = false;
        }
        
        public void Reset()
        {
            Dead = false;
            Position = new(Globals.Bounds.X / 2 - Player.PlayerSprite.Width / 8, Globals.Bounds.Y / 2 - Player.PlayerSprite.Height / 10);
            amo = 12;
        }

        private void Reload()
        {
            if (reloading)
                return;
            cooldownLeft = reloadTime;
            reloading = true;
            amo = maxAmo;
        }

        public void Update(List<Enemy> enemies)
        {
            if (cooldownLeft > 0)
            {
                cooldownLeft -= Globals.TotalSeconds;
            }
            else if (reloading)
            {
                reloading = false;
            }
            if (InputManager.Direction != Vector2.Zero)
            {
                var dir = Vector2.Normalize(InputManager.Direction);
                Position += dir * Speed * Globals.TotalSeconds;
                //new(
                //    MathHelper.Clamp(Position.X + (dir.X * Speed * Globals.TotalSeconds), 0, Globals.Bounds.X),
                //    MathHelper.Clamp(Position.Y + (dir.Y * Speed * Globals.TotalSeconds), 0, Globals.Bounds.Y)
                //    );
            }

            if (InputManager.MouseLeftDown)
            {
                Shoot();
            }
            if (InputManager.MouseRightClicked)
            {
                Reload();
            }
            CheckDeath(enemies);
            //if (Dead)
            //{
            //    Game1.State = GameState.EndScreenDeath;
            //}
            //var toMouse = InputManager.MousePosition - Position;
            //Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);
        }

        //public /*static*/ void Update()
        //{
        //    KeyboardState keyboardState = Keyboard.GetState();
        //    if (InputManager.MouseClicked)
        //    {
        //        Shoot();
        //    }
        //    if (InputManager.Direction != Vector2.Zero)
        //    {
        //        var dir = Vector2.Normalize(InputManager.Direction);
        //        Position += dir * Speed * Globals.TotalSeconds;
        //    }
        //    //if (keyboardState.IsKeyDown(Keys.Left) ||
        //    //    keyboardState.IsKeyDown(Keys.A))
        //    //{
        //    //    ChangePositionAndFrame(1);
        //    //}
        //    //if (keyboardState.IsKeyDown(Keys.Right) ||
        //    //    keyboardState.IsKeyDown(Keys.D))
        //    //{
        //    //    ChangePositionAndFrame(2);
        //    //}
        //    //if (keyboardState.IsKeyDown(Keys.Up) ||
        //    //    keyboardState.IsKeyDown(Keys.W))
        //    //{
        //    //    ChangePositionAndFrame(3);
        //    //}
        //    //if (keyboardState.IsKeyDown(Keys.Down) ||
        //    //    keyboardState.IsKeyDown(Keys.S))
        //    //{
        //    //    ChangePositionAndFrame(0);
        //    //}
        //}

        public static void ChangePositionAndFrame(int row)
        {
            currentFrame.Y = row;
            ++currentFrame.X;
            if (currentFrame.X >= spriteSize.X)
                currentFrame.X = 1;
        }

        public /*static*/ void Draw()
        {
            if (Dead)
            {
                Globals.SpriteBatch.Draw(DeathSprite, Position, null, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            }
            else
            {
                Globals.SpriteBatch.Draw(PlayerSprite, Position,
                new Rectangle(currentFrame.X * frameWidth,
                    currentFrame.Y * frameHeight,
                    frameWidth, frameHeight),
                Color.White, 0, Vector2.Zero,
                1, SpriteEffects.None, 0);
            }
        }

        private void Shoot()
        {
            if (cooldownLeft > 0 || reloading)
                return;
            amo--;
            if (amo > 0)
            {
                cooldownLeft = cooldown;
            }
            else
            {
                Reload();
            }

            BulletData bulletData = new()
            {
                Position = Position + new Vector2(frameWidth / 2, frameHeight / 2),
                Rotation = Rotation,
                Lifespan = 1,
                Speed = 600
            };
            BulletManager.AddBullet(bulletData);
        }

        private void CheckDeath(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.HP <= 0) continue;
                if ((Position - enemy.Position + new Vector2(frameWidth / 2, frameHeight / 2)).Length() < 50)
                {
                    Dead = true;
                    break;
                }
            }
        }

        public void GetExperience(int exp)
        {
            Experience += exp;
        }
    }
}
