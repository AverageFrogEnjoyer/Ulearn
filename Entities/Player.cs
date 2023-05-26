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
        private static Texture2D DeathSprite { get; set; }
        public int MaxHealth;
        public int Health;

        private static Point currentFrame;
        private static Point spriteSize;
        public int frameWidth;
        public int frameHeight;

        private float cooldown;
        private float cooldownLeft;
        private int maxBulletsCount;
        public int bulletsCount { get; set; }
        private float reloadTime;
        public bool reloading { get; set; }
        public bool IsDead;
        public int Score;
        public int BestScore;

        private Vector2 minPos;
        private Vector2 maxPos;

        private static int currentTime = 0;
        private static int period = 25;

        public static void Load() 
        {
            PlayerSprite = Globals.Content.Load<Texture2D>("Player_new");
            DeathSprite = Globals.Content.Load<Texture2D>("PlayerDead");
        }
        public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Reset();
        }

        public void Reset()
        {
            Position = new(Globals.Bounds.X / 2 - PlayerSprite.Width / 8, Globals.Bounds.Y / 2 - PlayerSprite.Height / 10);
            currentFrame = new Point(0, 0);
            spriteSize = new Point(4, 5);
            frameWidth = 89;
            frameHeight = 138;
            cooldown = 0.25f;
            cooldownLeft = 0f;
            maxBulletsCount = 14;
            bulletsCount = maxBulletsCount;
            reloadTime = 2f;
            reloading = false;
            IsDead = false;
            BestScore = Score;
            Score = 0;
            minPos = new(0, 0);
            maxPos = new(Globals.Bounds.X - 89, Globals.Bounds.Y - 138);
            Speed = 450;
            MaxHealth = 150;
            Health = MaxHealth;
        }

        private void Reload()
        {
            if (reloading)
            {
                return;
            }
            cooldownLeft = reloadTime;
            reloading = true;
            bulletsCount = maxBulletsCount;
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
                Position = Vector2.Clamp(Position, minPos, maxPos);
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

        public override void Draw()
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
            {
                return;
            }
            bulletsCount--;
            if (bulletsCount > 0)
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
                if ((Position - enemy.Position + new Vector2(frameWidth / 2, frameHeight / 2)).Length() < 85)
                {
                    IsDead = true;
                    break;
                }
            }
        }

        private void CheckDrown(List<Swamp> swamps)
        {
            if (IsInSwamp(swamps))
            {
                Health -= 1;
                if (Health <= 0)
                {
                    IsDead = true;
                }
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
