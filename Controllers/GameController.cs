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
            BulletView.Init();
            InterfaceView.Init();
            SwampView.Init();
            EnemyView.Init();
            HealthView.Init();
            Globals.IsPaused = false;
        }

        public void Load()
        {
            GameOverView.Load();
        }

        public void Update()
        {
            if (Globals.IsPaused)
            {
                return;
            }
            PlayerView.Update();
            BulletView.Update();
            SwampView.Update();
            EnemyView.Update();
            HealthView.Update();
            if (InputController.IsEnterPressed)
            {
                game.State = GameState.Menu;
                Restart();
            }
        }

        public void Draw()
        {
            MapView.Draw();
            SwampView.Draw();
            HealthView.Draw();
            BulletView.Draw();
            EnemyView.Draw();
            PlayerView.Draw();
            InterfaceView.Draw();
            if (PlayerView.player.IsDead)
            {
                GameOverView.Draw();
            }
        }

        private void Restart()
        {
            BulletView.Reset();
            EnemyView.Reset();
            SwampView.Reset();
            HealthView.Reset();
            PlayerView.Reset();
        }
    }
}
