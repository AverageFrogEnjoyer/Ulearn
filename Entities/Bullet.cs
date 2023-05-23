using _Game_;
using _Game_.Managers;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShooter.Entities
{
    public class Bullet : Sprite
    {
        public Vector2 Direction { get; set; }
        public float Lifespan { get; private set; }
        public int Damage { get; }

        public Bullet(Texture2D tex, BulletData data) : base(tex, data.Position)
        {
            Speed = data.Speed;
            var toMouse = InputManager.MousePosition - Position;
            Rotation = (float)Math.Atan2(toMouse.Y, toMouse.X);
            Direction = new((float)Math.Cos(Rotation), (float)Math.Sin(Rotation));
            Lifespan = data.Lifespan;
            Damage = data.Damage;
        }

        public void Destroy()
        {
            Lifespan = 0;
        }

        public void Update()
        {
            Position += Direction * Speed * Globals.TotalSeconds;
            Lifespan -= Globals.TotalSeconds;            
        }
    }
}
