using _Game_.Managers;
using GameShooter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace _Game_.Entities
{
    public class Player : Sprite
    {
        public int MaxHealth;
        public int Health;

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

        public Player(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Reset();
        }

        public void Reset()
        {
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

        public void Update()
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
            CheckDrown(SwampManager.Swamps);
            CheckDeath(EnemyManager.Enemies);
            
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
                Speed = 650,
                Damage = 1
            };
            BulletManager.AddBullet(bulletData);            
        }

        private void CheckDeath(List<Enemy> enemies)
        {
            foreach (var enemy in enemies)
            {
                if ((Position - enemy.Position + new Vector2(frameWidth / 2, frameHeight / 2)).Length() < 75)
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
    }
}
