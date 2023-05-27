using _Game_.Managers;
using Game_;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace _Game_.Entities
{
    public class Enemy : Sprite
    {
        public int HP { get; set; }
        public Enemy(Texture2D tex, Vector2 pos, int speed) : base(tex, pos)
        {
            Speed = 100;
            HP = 3;
        }

        public void GetDamage(int damage)
        {
            HP -= damage;
            if (HP <= 0)
            {
                HealthManager.AddHealth(Position);
            }
        }

        public void Update(Player player)
        {
            var toPlayer = player.Position - Position + new Vector2(player.frameWidth / 2, player.frameHeight / 2);
            if (HP == 1)
            {
                Speed = 250;
                texture = Globals.Content.Load<Texture2D>("GhosAngryt");
            }
            if (toPlayer.Length() > 4)
            {
                var dir = Vector2.Normalize(toPlayer);
                Position += dir * Speed * Globals.TotalSeconds;
            }
        }
    }
}
