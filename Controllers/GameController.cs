using _Game_.Entities;
using _Game_.Managers;
using GameShooter.Managers;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.Direct2D1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _Game_.Controllers
{
    public class GameController
    {
        private Game1 game;
        private Texture2D Sprite { get; set; }
       
        public GameController(Game1 game)
        {
            this.game = game;
            //PlayerManager.Init();
            BulletManager.Init();
            InterfaceManager.Init();
            SwampManager.Init();
            EnemyManager.Init();
            HealthManager.Init();
            Globals.IsPaused = false;
        }

        public void Load()
        {
            Sprite = Globals.Content.Load<Texture2D>("Grass");
            GameOverManager.Load();
        }

        public void Update()
        {
            if (Globals.IsPaused)
            {
                return;
            }
            PlayerManager.Update();
            BulletManager.Update();
            SwampManager.Update();
            EnemyManager.Update();
            HealthManager.Update();
            if (InputManager.IsEnterPressed)
            {
                game.State = GameState.Menu;
                Restart();
            }
            //if (PlayerManager.player.IsDead)
            //{
            //    //game.State = GameState.End;
            //    //game.GameOverController.Draw();
            //    GameOverManager.Draw();
            //}
        }

        public void Draw()
        {
            MapManager.Draw(/*spriteBatch*/);
            SwampManager.Draw();
            HealthManager.Draw();
            BulletManager.Draw();
            EnemyManager.Draw();
            PlayerManager.Draw();
            InterfaceManager.Draw();
            if (PlayerManager.player.IsDead)
            {
                GameOverManager.Draw();
            }
        }

        private void Restart()
        {
            BulletManager.Reset();
            EnemyManager.Reset();
            SwampManager.Reset();
            HealthManager.Reset();
            PlayerManager.Reset();
        }
    }
}
