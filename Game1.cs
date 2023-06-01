using _Game_.Entities;
using _Game_.Managers;
using Game_;
using GameShooter.Managers;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace _Game_
{
    public enum GameState
    {
        SplashScreen,
        Map1,
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;
        //private Player player;
        public static GameState State;


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            Globals.Bounds = new(1536, 1024);
            graphics.PreferredBackBufferWidth = Globals.Bounds.X;
            graphics.PreferredBackBufferHeight = Globals.Bounds.Y;
            graphics.ApplyChanges();

            Globals.Content = Content;
            Globals.IsPaused = false;            
            State = GameState.SplashScreen;

            PlayerManager.Init();
            BulletManager.Init();
            InterfaceManager.Init();
            SwampManager.Init();
            EnemyManager.Init();
            EnemyManager.AddEnemy();
            HealthManager.Init();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Globals.SpriteBatch = spriteBatch;
            //Player.Load();
            //player = new(Player.PlayerSprite, new(Globals.Bounds.X / 2 - Player.PlayerSprite.Width / 8, Globals.Bounds.Y / 2 - Player.PlayerSprite.Height / 10));
            MenuManager.Load();
            GameOverManager.Load();
            MapManager.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            Globals.gameTime = gameTime;
            Globals.Update();
            InputManager.Update(/*player*/);

            switch (State)
            {
                case GameState.SplashScreen:
                    if (keyboardState.IsKeyDown(Keys.Space))
                        State = GameState.Map1;
                    break;
                case GameState.Map1:
                    if (Globals.IsPaused)
                        break;
                    PlayerManager.Update();
                    BulletManager.Update(EnemyManager.Enemies);
                    SwampManager.Update();
                    //player.Update(EnemyManager.Enemies, SwampManager.Swamps);
                    EnemyManager.Update(/*player*/);
                    HealthManager.Update(/*player*/);
                    if (keyboardState.IsKeyDown(Keys.Enter))
                    {
                        State = GameState.SplashScreen;
                        Restart();
                    }
                    break;
            }

            base.Update(gameTime);
        }

        private void Restart()
        {
            BulletManager.Reset();
            EnemyManager.Reset();
            SwampManager.Reset();
            HealthManager.Reset();
            //player.Reset();
            PlayerManager.Reset();
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            spriteBatch.Begin();
            switch (State)
            {
                case GameState.SplashScreen:
                    MenuManager.Draw(spriteBatch);
                    break;
                case GameState.Map1:
                    MapManager.Draw(spriteBatch);
                    SwampManager.Draw();
                    HealthManager.Draw();
                    BulletManager.Draw();
                    EnemyManager.Draw();
                    //player.Draw();
                    PlayerManager.Draw();
                    InterfaceManager.Draw(/*player*/);
                    if (PlayerManager.player.IsDead)
                    {
                        GameOverManager.Draw();
                    }
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}