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
        Paused,
        GameOver,
        Exiting
    }
    

    public class Game1 : Game
    {
        public GraphicsDeviceManager graphics { get; }
        SpriteBatch spriteBatch;


        //=====================================================================
        // MENU AND STATES
        //=====================================================================
        private GameStates _gameState;

        private MenuMain _menuMain;
        private MenuGameOver _menuGameOver;
        private bool started { get; set; }
        //=====================================================================
        private MouseState mouseState;
        private MouseState previousMouseState;
        private KeyboardState keyboardState;
        private KeyboardState previousKeyboardState;

        private Player _player;
        //======================================================================





   
        //====================================================================
        //BACKGROUND VARIABLES
        //====================================================================
        Sky Sky1;
        Sky Sky2;

        Trees Trees1;
        Trees Trees2;

        Stars Stars1;
        Stars Stars2;
        //===========================================================================


        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            // Screen Size
            graphics.PreferredBackBufferWidth = ScreenGlobals.SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = ScreenGlobals.SCREEN_HEIGHT;

            // INITIAL GAME STATE - MENUS ARE CREATED
            _gameState = GameStates.Menu;
            _menuMain = new MenuMain();
            _menuGameOver = new MenuGameOver();
            started = false;

            // mouse states
            IsMouseVisible = true;
            mouseState = Mouse.GetState();
            previousMouseState = mouseState;
            // keyboard states
            keyboardState = Keyboard.GetState();
            previousKeyboardState = keyboardState;

            _player = new Player(spriteBatch);

            

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
            _menuMain.Initialize(spriteBatch, Content, graphics);
            _player.Initialize(graphics.GraphicsDevice);
            _menuGameOver.Initialize(spriteBatch, Content, graphics);


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
            _menuMain.LoadContent(Content);
            _player.LoadContent(Content, ScreenGlobals.PLAYER_ASSETNAME);
            _menuGameOver.LoadContent(Content);



        
            //=======================================================================================================
            //BACKGROUND CONTENT
            //=======================================================================================================
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            //Sky
            Sky1 = new Sky(Content.Load<Texture2D>("Sky"), new Rectangle(0, 0, ScreenGlobals.SCREEN_WIDTH, ScreenGlobals.SCREEN_HEIGHT));
            Sky2 = new Sky(Content.Load<Texture2D>("Sky"), new Rectangle(ScreenGlobals.SCREEN_WIDTH, 0, ScreenGlobals.SCREEN_WIDTH, ScreenGlobals.SCREEN_HEIGHT));
            //Trees
            Trees1 = new Trees(Content.Load<Texture2D>("Trees"), new Rectangle(0, 0, ScreenGlobals.SCREEN_WIDTH, ScreenGlobals.SCREEN_HEIGHT));
            Trees2 = new Trees(Content.Load<Texture2D>("Trees"), new Rectangle(ScreenGlobals.SCREEN_WIDTH, 0, ScreenGlobals.SCREEN_WIDTH, ScreenGlobals.SCREEN_HEIGHT));

            //Stars
            Stars1 = new Stars(Content.Load<Texture2D>("Stars"), new Rectangle(0, 0, ScreenGlobals.SCREEN_WIDTH, ScreenGlobals.SCREEN_HEIGHT));
            Stars2 = new Stars(Content.Load<Texture2D>("Stars"), new Rectangle(ScreenGlobals.SCREEN_WIDTH, 0, ScreenGlobals.SCREEN_WIDTH, ScreenGlobals.SCREEN_HEIGHT));

            //=======================================================================================================





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

            //========================================================================
            //BACKGROUND LOGIC
            //========================================================================
            //Sky
            if (Sky1.rectangle.X + Sky1.texture.Width <= 0)
                Sky1.rectangle.X = Sky2.rectangle.X + Sky2.texture.Width;

            if (Sky2.rectangle.X + Sky2.texture.Width <= 0)
                Sky2.rectangle.X = Sky1.rectangle.X + Sky1.texture.Width;

            //Trees
            if (Trees1.rectangle.X + Trees1.texture.Width <= 0)
                Trees1.rectangle.X = Trees2.rectangle.X + Trees2.texture.Width;

            if (Trees2.rectangle.X + Trees2.texture.Width <= 0)
                Trees2.rectangle.X = Trees1.rectangle.X + Trees1.texture.Width;

            //Stars
            if (Stars1.rectangle.X + Stars1.texture.Width <= 0)
                Stars1.rectangle.X = Stars2.rectangle.X + Stars2.texture.Width;

            if (Stars2.rectangle.X + Stars2.texture.Width <= 0)
                Stars2.rectangle.X = Stars1.rectangle.X + Stars1.texture.Width;
            //===================================================================================


            // TODO: Add your update logic here
            mouseState = Mouse.GetState();
            keyboardState = Keyboard.GetState();



            //  Update method for menus here when menus exist

            if (_gameState == GameStates.Menu)
            {
                IsMouseVisible = true;
                // clicked to start
                 if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    
                    _gameState = _menuMain.MouseClicked(mouseState.X, mouseState.Y);
                    if (_gameState == GameStates.Playing)
                    {
                        started = true;
                        _player.ResetPlayer();
                    }
                }
                 // space to start
                 else if (keyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    _gameState = GameStates.Playing;
                    _player.ResetPlayer();

                }
                 if (_gameState == GameStates.Playing)
                {
                    _menuMain.MenuCurrState = GameStates.Playing;
                    _player.ResetPlayer();
                }
            }
            else if (_gameState == GameStates.Playing)
            {
                IsMouseVisible = false;

                // play game logic here
                _player.Update(gameTime);

              if( (int)Player.pState.Dead == _player.getCurrState())
                {
                   _gameState = GameStates.GameOver;
                }

            }
            else if (_gameState == GameStates.Paused)
            {
                ;
            }
            else if (_gameState == GameStates.GameOver)
            {
                IsMouseVisible = true;
                // clicked to start
                if (previousMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {

                    _gameState = _menuGameOver.MouseClicked(mouseState.X, mouseState.Y);
                    if (_gameState == GameStates.Menu)
                    {
                        started = false;
                    }
                }
                // space to start
                else if (keyboardState.IsKeyDown(Keys.Space) && !previousKeyboardState.IsKeyDown(Keys.Space))
                {
                    _gameState = GameStates.Menu;

                }
                if (_gameState == GameStates.Menu)
                {
                    _menuGameOver.MenuCurrState = GameStates.Menu;
                }
            }
            if (_gameState == GameStates.Exiting)
            {
                Exit();
            }
            else
            {
                ;
                
            }
            previousMouseState = mouseState;


            //=========================
            Sky1.Update();
            Sky2.Update();
            Trees1.Update();
            Trees2.Update();
            Stars1.Update();
            Stars2.Update();
            //===============================



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            if (_gameState == GameStates.Menu)
            {
                //===================================
                Sky1.Draw(spriteBatch);
                Sky2.Draw(spriteBatch);
                Trees1.Draw(spriteBatch);
                Trees2.Draw(spriteBatch);
                Stars1.Draw(spriteBatch);
                Stars2.Draw(spriteBatch);
                //====================================

                _menuMain.Draw(spriteBatch);
            }
            else if (_gameState == GameStates.Playing)
            {
                //Draw the game
                //base.Draw(gameTime);



                //==========================================
                Sky1.Draw(spriteBatch);
                Sky2.Draw(spriteBatch);
                Trees1.Draw(spriteBatch);
                Trees2.Draw(spriteBatch);
                Stars1.Draw(spriteBatch);
                Stars2.Draw(spriteBatch);
                //=========================================


                _player.Draw(spriteBatch);

            }
            else if (_gameState == GameStates.GameOver)
            {
                _menuGameOver.Draw(spriteBatch);
            }
            spriteBatch.End();
           
        }
    }
}
