using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game_.Entities
{
    public class Swamp : Sprite
    {
        public Swamp(Texture2D tex, Vector2 pos) : base(tex, pos)
        {
            Speed = 0;           
        }

    }
}
