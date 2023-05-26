using _Game_.Managers;
using Game_;
using GameShooter.Managers;
using GameShooter;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System;

namespace _Game_.Entities
{
    public class Player : Sprite
    {
        public static Texture2D PlayerSprite { get; set; }
        public static Texture2D DeathSprite { get; set; }
        public int Health;

        private static Point currentFrame;
        private static Point spriteSize;
        public static int frameWidth = 158;
        public static int frameHeight = 169;

        private float cooldown;
        private float cooldownLeft;
        private int maxAmo;
        public int amo { get; set; }
        private float reloadTime;
        public bool reloading { get; set; }
        public static bool IsDead;
        public static bool HasUlta;
        public static int Score;
        public static int BestScore;

        public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Reset();
        }

        public void Reset()
        {
            Position = new(Globals.Bounds.X / 2 - Player.PlayerSprite.Width / 8, Globals.Bounds.Y / 2 - Player.PlayerSprite.Height / 10);
            currentFrame = new Point(0, 0);
            spriteSize = new Point(4, 5);
            cooldown = 0.25f;
            cooldownLeft = 0f;
            maxAmo = 12;
            amo = maxAmo;
            reloadTime = 2f;
            reloading = false;
            IsDead = false;
            HasUlta = false;
            BestScore = Score;
            Score = 0;            
            Speed = 450;
            Health = 200;
        }

        private void Reload()
        {
            if (reloading)
                return;
            cooldownLeft = reloadTime;
            reloading = true;
            amo = maxAmo;
        }

        public void Update(List<Enemy> enemies, List<Swamp> swamps)
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
                var direction = Vector2.Normalize(InputManager.Direction);
                Position += direction * Speed * Globals.TotalSeconds;
            }
            if (InputManager.MouseLeftDown)
            {
                Shoot();
            }
            if (InputManager.MouseRightClicked)
            {
                Reload();
            }
            CheckDrown(swamps);
            CheckDeath(enemies);
            
        }

        public static void ChangePositionAndFrame(int row)
        {
            currentFrame.Y = row;
            ++currentFrame.X;
            if (currentFrame.X >= spriteSize.X)
                currentFrame.X = 1;
        }

        public void Draw()
        {
            if (IsDead)
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
                    Speed = 600,
                    Damage = 1
                };
                BulletManager.AddBullet(bulletData);
            
        }

        private void CheckDeath(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if (enemy.HP <= 0)
                {
                    GetExperience(1);
                    continue;
                }
                if ((Position - enemy.Position + new Vector2(frameWidth / 2, frameHeight / 2)).Length() < 70)
                {
                    IsDead = true;
                    break;
                }
            }
        }

        private void CheckDrown(List<Swamp> swamps)
        {
            //foreach (var swamp in swamps)
            //{
            //    if ((Position - swamp.Position + new Vector2(frameWidth / 2, frameHeight / 2)).Length() < 50)
            //    {
            //        Health -= 1;
            //        Speed = 300;
            //        if (Health <= 0)
            //        {
            //            IsDead = true;
            //            break;
            //        }
            //    }

            //}
            if (IsInSwamp(swamps))
            {
                Health -= 1;
                Speed = 150;
            }
            else
            {
                Speed = 450;
            }
        }

        private bool IsInSwamp(List<Swamp> swamps)
        {
            var result = false;
            foreach (var swamp in swamps)
            {
                if ((Position - swamp.Position + new Vector2(frameWidth / 2, frameHeight / 2)).Length() < 60)
                {
                    result = true;
                }
            }
            return result;
        }

        public void GetExperience(int exp)
        {
            Score += exp;
        }
    }
}
