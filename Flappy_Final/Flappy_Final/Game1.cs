using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System;

namespace Flappy_Final
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    /// 
    //http://www.xnadevelopment.com/tutorials/thestateofthings/thestateofthings.shtml
    // https://www.reddit.com/r/monogame/comments/35crx8/how_to_add_menus/
    public enum GameStates
    {
        Menu,
        Playing,
        Paused
    }

    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        private GameStates _gameState;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Screen Size
            graphics.PreferredBackBufferWidth = ScreenGlobals.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = ScreenGlobals.SCREEN_HEIGHT;

            // INITIAL GAME STATE UNTIL MENUS ARE CREATED
            _gameState = GameStates.Playing;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here

            //  Update method for menus here when menus exist

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            // TODO: Add your drawing code here

            if (_gameState == GameStates.Menu)
            {
                //Draw the menu
            }
            else if (_gameState == GameStates.Playing)
            {
                //Draw the game
                base.Draw(gameTime);
            }

           
        }
    }
}
