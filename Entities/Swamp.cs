using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
