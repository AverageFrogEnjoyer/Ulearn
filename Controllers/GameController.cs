using _Game_.Entities;
using _Game_.Managers;
using GameShooter.Managers;

namespace _Game_.Controllers
{
    public class GameController
    {
        private Game1 game;
       
        public GameController(Game1 game)
        {
            this.game = game;
            BulletManager.Init();
            SwampManager.Init();
            EnemyManager.Init();
            HealthManager.Init();
            Globals.IsPaused = false;
        }

        public void Load()
        {
            GameOverView.Load();
            InterfaceView.Load();
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
            if (InputController.IsEnterPressed)
            {
                game.State = GameState.Menu;
                Restart();
            }
        }

        public void Draw()
        {
            MapView.Draw();
            SwampManager.Draw();
            HealthManager.Draw();
            BulletManager.Draw();
            EnemyManager.Draw();
            PlayerManager.Draw();
            InterfaceView.Draw();
            if (PlayerManager.player.IsDead)
            {
                GameOverView.Draw();
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
