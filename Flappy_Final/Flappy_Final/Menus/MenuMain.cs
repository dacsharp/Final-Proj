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
    class MenuMain
    {
        private GameStates menuCurrState;

        // Buttons are 480x480
        // 401x170 11 edit

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


        // Font
        private SpriteFont labelFont;

            // Mouse
        MouseState mouseState;
        MouseState previousMouseState;

        // graphics
        private SpriteBatch spriteBatch;
        private ContentManager content;
        

        public void Initialize(SpriteBatch inSpriteBatch, ContentManager content)
        {
            // IsMouseVisible = true;
            this.content = content;
            this.spriteBatch = inSpriteBatch; // pass the sprite batch to the class

            startButtonPosition = new Vector2((ScreenGlobals.SCREEN_WIDTH /2), (ScreenGlobals.SCREEN_HEIGHT/2 ) + 50);
            middleOfStart = new Vector2(startButtonPosition.X / 2, startButtonPosition.Y / 2);

            exitButtonPosition = new Vector2((ScreenGlobals.SCREEN_WIDTH / 2), (ScreenGlobals.SCREEN_HEIGHT / 2 ) - 50);
            middleOfExit = new Vector2(exitButtonPosition.X/2, exitButtonPosition.Y/2);
            


            menuCurrState = GameStates.Menu;

           

        }

          public void LoadContent(ContentManager Content)

          { 
            //load the button images into the content pipeline
            startButton = Content.Load<Texture2D>("Png/Shiny/11edit");
             exitButton = Content.Load<Texture2D>("Png/Shiny/11edit");

            // load fonts
            labelFont = Content.Load<SpriteFont>("TimesNewRoman20");

        }
        public void Draw(SpriteBatch spriteBatch)
        {
            buttonHeight = startButton.Height;
            buttonWidth = startButton.Width;
            // (texture, new Vector2(X, Y),soruceRect = null,
            //    color, rotation = 0.0f, origin = new Vector2(Width / 2f, Height / 2f), 
            //    scale = 1.0f, SpriteEffects.None, layer depth = 0)

            

            spriteBatch.Draw(startButton, startButtonPosition,null,
                Color.White, 0.0f, new Vector2(buttonWidth / 2f, buttonHeight / 2f), 
                0.3f, SpriteEffects.None, 0);
            spriteBatch.Draw(exitButton, exitButtonPosition, null,
                Color.White, 0.0f, new Vector2(buttonWidth / 2f, buttonHeight / 2f),
                0.3f, SpriteEffects.None, 0);

            Vector2 startbttnmid = new Vector2(startButton.Height / 2 * .3f, startButton.Width / 2 * .3f);
            string startMsg = "Start";
            Vector2 startMsgSize = labelFont.MeasureString(startMsg);
            Vector2 TextMiddlePoint = new Vector2(startMsgSize.X / 2, startMsgSize.Y / 2);
            Vector2 textPositionStart = new Vector2((int)(startButtonPosition.X - startbttnmid.X),
                                                                                                (int)(startButtonPosition.Y - startbttnmid.Y - startbttnmid.Y + 2));

            spriteBatch.DrawString(labelFont, startMsg, 
                textPositionStart, 
                Color.Black);

        }

    }
}
