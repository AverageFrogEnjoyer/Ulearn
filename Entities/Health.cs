using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game_.Entities
{
    public class Health : Sprite
    {
        public float Lifespan;

        public Health(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Lifespan = 10f;
        }

        public void Update()
        {
            Lifespan -= Globals.TotalSeconds;
        }

        public void Collect()
        {
            Lifespan = 0;
        }
    }
}
