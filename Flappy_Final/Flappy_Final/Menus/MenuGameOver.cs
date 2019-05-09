using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Final
{
    class MenuGameOver
    {
        public GameStates MenuCurrState { get; set; }
        private GameStates MenuPrevState;
        // SCORE
        private int _score;
        private int _lastScore;
        Vector2 _scorePos;

        public bool isVisible { get; set; }

        // Buttons are 
        // 401x170 11edit.png

        // button members
        private Texture2D startButton;
        private Texture2D exitButton;
        private Texture2D pauseButton;
        private Texture2D resumeButton;
        // button positions
        private Vector2 startButtonPosition;
        private Vector2 middleOfStart;
        private Vector2 exitButtonPosition;
        private Vector2 middleOfExit;
        private Vector2 resumeButtonPosition;
        private Vector2 middleOfResume;

        private int buttonWidth;
        private int buttonHeight;

        private float buttonScale = .3f;

        public Rectangle startRect;
        public Rectangle exitRect;

        // Font
        private SpriteFont labelFont;

        // Mouse
        MouseState mouseState;
        MouseState previousMouseState;
        private Rectangle mouseClickRect;

        // graphics
        private SpriteBatch spriteBatch;
        private ContentManager content;
        private GraphicsDeviceManager graphics;

        // Score tracking
        private ScoreManager _scoreManager;

        public void Initialize(SpriteBatch inSpriteBatch, ContentManager content, GraphicsDeviceManager graphics)
        {
            // IsMouseVisible = true;
            this.content = content;
            this.spriteBatch = inSpriteBatch; // pass the sprite batch to the class
            this.graphics = graphics;

            _scorePos = new Vector2(ScreenGlobals.SCREEN_WIDTH/2, ScreenGlobals.SCREEN_HEIGHT - 50);

            startButtonPosition = new Vector2((ScreenGlobals.SCREEN_WIDTH / 2), (ScreenGlobals.SCREEN_HEIGHT / 2) + 50);
            middleOfStart = new Vector2(startButtonPosition.X / 2, startButtonPosition.Y / 2);

            exitButtonPosition = new Vector2((ScreenGlobals.SCREEN_WIDTH / 2), (ScreenGlobals.SCREEN_HEIGHT / 2) - 50);
            middleOfExit = new Vector2(exitButtonPosition.X / 2, exitButtonPosition.Y / 2);

            MenuCurrState = GameStates.GameOver;

            MenuPrevState = GameStates.Playing;

            _lastScore = -1;

        }

        public void LoadContent(ContentManager Content)

        {
            //load the button images into the content pipeline
            startButton = Content.Load<Texture2D>("Png/Shiny/11edit");
            exitButton = Content.Load<Texture2D>("Png/Shiny/11edit");

            // load fonts
            labelFont = Content.Load<SpriteFont>("TimesNewRoman20");

            // load score items
            _scoreManager = ScoreManager.Load();

        }

        public void Update(int inScore)
        {

            _score = inScore;
            if (inScore != _lastScore )
            {
                _scoreManager.Add(new Score { Value = inScore});

                ScoreManager.Save(_scoreManager);
            }

            _lastScore = inScore;

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            buttonHeight = startButton.Height;
            buttonWidth = startButton.Width;
            // (texture, new Vector2(X, Y),soruceRect = null,
            //    color, rotation = 0.0f, origin = new Vector2(Width / 2f, Height / 2f), 
            //    scale = 1.0f, SpriteEffects.None, layer depth = 0)



            spriteBatch.Draw(startButton, startButtonPosition, null,
                Color.White, 0.0f, new Vector2(buttonWidth / 2f, buttonHeight / 2f),
                buttonScale, SpriteEffects.None, 0);


            spriteBatch.Draw(exitButton, exitButtonPosition, null,
                Color.White, 0.0f, new Vector2(buttonWidth / 2f, buttonHeight / 2f),
                buttonScale, SpriteEffects.None, 0);
            
           

            // TESTS FOR RECTS
            //if (mouseClickRect != null)
            //    spriteBatch.Draw(textureFill(), mouseClickRect, Color.White);
            //if (startRect != null)
            //    spriteBatch.Draw(textureFill(), startRect, Color.Blue);
            //if (exitRect != null)
            //    spriteBatch.Draw(textureFill(), exitRect, Color.Red);



            Vector2 startbttnmid = new Vector2(startButton.Height / 2 * buttonScale, startButton.Width / 2 * buttonScale);
            string startMsg = "New Game";
            Vector2 startMsgSize = labelFont.MeasureString(startMsg);
            Vector2 TextMiddlePoint = new Vector2(startMsgSize.X / 2, startMsgSize.Y / 2);
            Vector2 textPositionStart = new Vector2((int)(startButtonPosition.X - startbttnmid.X - startbttnmid.X - 10),
                                                                                                (int)(startButtonPosition.Y - startbttnmid.Y - startbttnmid.Y + 2));
            Vector2 exitbttnmid = new Vector2(startButton.Height / 2 * buttonScale, startButton.Width / 2 * buttonScale);
            string exitMsg = "Exit";
            Vector2 exitMsgSize = labelFont.MeasureString(exitMsg);
            Vector2 exitTextMiddlePoint = new Vector2(exitMsgSize.X / 2, exitMsgSize.Y / 2);
            Vector2 textPositionExit = new Vector2((int)(exitButtonPosition.X - exitbttnmid.X),
                                                                                                (int)(exitButtonPosition.Y + exitbttnmid.Y * 1.4));

            spriteBatch.DrawString(labelFont, startMsg,
                textPositionStart,
                Color.Black);

            spriteBatch.DrawString(labelFont, exitMsg,
                textPositionExit,
                Color.Black);

            //===============================================
            // DRAW SCORES
            // =====================================
            string scoreMsg = "Score: " + _score.ToString("00000");
            Vector2 space = labelFont.MeasureString(scoreMsg);
            spriteBatch.DrawString(labelFont, scoreMsg, new Vector2((ScreenGlobals.SCREEN_WIDTH - space.X) / 2, 40), Color.White);
            int i = 0;
            spriteBatch.DrawString(labelFont, "High Scores\n" + string.Join("\n" , _scoreManager.HighScores.Select(c => c.Value).ToArray())
                , new Vector2(40, 40), Color.White);


        }

        public GameStates MouseClicked(int x, int y)
        {
            //creates a rectangle of 10x10 around the place where the mouse was clicked
            mouseClickRect = new Rectangle(x, y, 10, 10);

            if (MenuPrevState != MenuCurrState) // otherwise the rectangles are already set
            {
                exitRect = new Rectangle(new Point((int)startButtonPosition.X - (int)(startButton.Width / 2 * buttonScale), (int)startButtonPosition.Y - (int)(buttonHeight / 2 * buttonScale) - 1),
                   new Point((int)(startButton.Width * buttonScale), (int)(startButton.Height * buttonScale)));
                startRect = new Rectangle(new Point((int)exitButtonPosition.X - (int)(startButton.Width / 2 * buttonScale), (int)exitButtonPosition.Y - (int)(buttonHeight / 2 * buttonScale) - 1),
                    new Point((int)(exitButton.Width * buttonScale), (int)(exitButton.Height * buttonScale)));
            }

            if (mouseClickRect.Intersects(startRect)) //player clicked start button
            {
                MenuCurrState = GameStates.Menu;
            }
            else if (mouseClickRect.Intersects(exitRect)) //player clicked exit button
            {
                MenuCurrState = GameStates.Exiting;
            }
            else
            {
                MenuCurrState = GameStates.GameOver;
            }
            return MenuCurrState;

        }


        // TEST FILL
        Texture2D textureFill()
        {
            Texture2D texture = new Texture2D(graphics.GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });
            return texture;
        }
    }
}
