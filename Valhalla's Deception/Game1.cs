using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace VallhalasDeception
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Point screenSize;
        SceneManager sceneManager;
        public static Game1 instance;
        public Camera camera;
        public Game1()
        {
            screenSize = new Point(1280,720);
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            _graphics.PreferredBackBufferWidth = screenSize.X;
            _graphics.PreferredBackBufferHeight = screenSize.Y;
            _graphics.ApplyChanges();
            IsMouseVisible = true;
            instance = this;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            sceneManager = new SceneManager();
            camera = new Camera(GraphicsDevice.Viewport);
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            sceneManager.LoadAllScenes(Content);
            // TODO: use this.Content to load your game content here
        }

        protected override void Update(GameTime _gameTime)
        {
            // TODO: Add your update logic here
            sceneManager.Update(_gameTime);
            //pos = player.GetRect().Location.ToVector2();
            //camera.Update(pos, _gameTime);
            base.Update(_gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkGray);
            sceneManager.Draw(_spriteBatch);
        }

        public Point GetScreenSize()
        {
            return screenSize;
        }
    }
}
