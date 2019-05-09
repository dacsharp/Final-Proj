using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using System;

namespace Flappy_Final
{
    class Brick : Sprite
    {
        public float X { get; set; } // x position of brick on screen
        public float Y { get; set; } // y position of brick on screen
        public float Width { get; set; } // width of brick
        public float Height { get; set; } // height of brick
        public bool Visible { get; set; } // does brick still exist
        private Color color;

        public bool isFire { get; set; }

        private int fallSpeed;

       public enum State
        {
            Falling,
            OffScreen,
            Destroyed
        }

        
        State currState { get; set; }
        public State GetCurrState()
        {
            return currState;
        }

        private int GenRandInt()
        {
            Random r = new Random();
            int nextValue = r.Next(0, 2); // returns random from 0 - 1
            return nextValue;
        }
        private int GenRandX()
        {
            Random r = new Random();
            int nextValue = r.Next((int)ScreenGlobals.SCREEN_WIDTH / 2, ScreenGlobals.SCREEN_WIDTH); // returns random from 0 - 1
            return nextValue;
        }
        private int GenRandY() // generate somewhere in the top 2 thirds
        {
            Random r = new Random();
            int nextValue = r.Next((int)ScreenGlobals.SCREEN_HEIGHT / 2, ScreenGlobals.SCREEN_HEIGHT); // returns random from 0 - 1
            return nextValue;
        }

        private Texture2D imgBrick; // cached image of brick
        private SpriteBatch spriteBatch; // allows us to write on backbuffer when we need to draw self
//        private ContentManager content;


        public void Initialize(GraphicsDevice gDevice)
        {
            graphicsDevice = gDevice;

        }

        public Brick()
        {
            fallSpeed = 1;

            FrameSize = Size.Width;

            currState = State.Falling;

            X = GenRandX();
            Y = 0;



            Width = 16;
            Height = 50;
            Origin = new Vector2(Width / 2, Height / 2);
            Visible = true;

            Color colorIn = new Color(178, 34, 34, 255);
            if (colorIn == Color.Firebrick)
            {
                this.color = Color.Firebrick;
                isFire = true;
            }
            else if (colorIn == Color.Blue)
            {
                this.color = Color.Blue;
                isFire = false;
            }
            else
            {
                this.color = Color.Firebrick;
                isFire = true;
            }

        }
        public void LoadContent(ContentManager content)
        {
            this.AssetName = "brick";
            imgBrick = content.Load<Texture2D>(AssetName);
            Size = new Rectangle(0, 0, (int)(imgBrick.Width * Scale), (int)(imgBrick.Height * Scale));
            // Origin of the sprite
            float orX = (float)imgBrick.Width / 2;
            float orY = (float)imgBrick.Height / 2;
            Origin = new Vector2(orX, orY);
            Visible = true;


            Position = new Vector2(GenRandX(), GenRandY());


        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            if (currState == State.Falling && Visible)
            {
                spriteBatch.Draw(imgBrick, new Vector2(X, Y),
                    null, color, 0.0f,
                    new Vector2(Width / 2f, Height / 2f), 1.0f, SpriteEffects.None, 0);
            }
          
        }

        public void Update(GameTime gameTime)
        {
            // Move the brick down by the fall speed given to us in the constructor.
            Y += (int)fallSpeed * 3;
            X -= (int)fallSpeed * 3;

            if (Y >= ScreenGlobals.SCREEN_HEIGHT)
            {
                currState = State.OffScreen;
                Visible = false;

            }


        }

    }
}
