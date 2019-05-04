using Microsoft.Xna.Framework;
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
        // button members
        private Texture2D startButton;
        private Texture2D exitButton;
        private Texture2D pauseButton;
        private Texture2D resumeButton;
        // button positions
        private Vector2 startButtonPosition;
        private Vector2 exitButtonPosition;
        private Vector2 resumeButtonPosition;

        MouseState mouseState;
        MouseState previousMouseState;

        private GameStates menuCurrState;

        protected void Initialize()
        {
           // IsMouseVisible = true;
            startButtonPosition = new Vector2((ScreenGlobals.SCREEN_WIDTH /2) - 50, (ScreenGlobals.SCREEN_HEIGHT/2 ) + 50);
            exitButtonPosition = new Vector2((ScreenGlobals.SCREEN_WIDTH / 2) - 50 , (ScreenGlobals.SCREEN_HEIGHT / 2 ) - 50);

            menuCurrState = GameStates.Menu;
           
        }


    }
}
