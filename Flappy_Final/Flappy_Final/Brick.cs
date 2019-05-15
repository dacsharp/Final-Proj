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

        private Color color;

        public bool isFire { get; set; }

        private int fallSpeed;

       public enum State
        {
            Falling,
            OffScreen,
            Destroyed
        }

        
        public State currState { get; set; }
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

        private Texture2D imgFireBrick; // cached image of brick
        private Texture2D imgIceBrick;
        private SpriteBatch spriteBatch; // allows us to write on backbuffer when we need to draw self
                                         //        private ContentManager content;
        private Rectangle brickRect;
        public Rectangle getBrickRect()
        {
            return brickRect;
        }

        public void Initialize(GraphicsDevice gDevice)
        {
            graphicsDevice =  (gDevice);

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
            int blueRed = GenRandInt();
            
            if (blueRed == 1)
            {
                color = Color.Firebrick;
                isFire = true;
            }
            else if (blueRed == 0)
            {
                color = Color.Blue;
                isFire = false;
            }
            else
            {
                color = Color.Firebrick;
                isFire = true;
            }

        }
        public void LoadContent(ContentManager content)
        {
            this.AssetName = "FireBrickedit";

            imgFireBrick = content.Load<Texture2D>(AssetName);
            imgIceBrick = content.Load<Texture2D>("iceBrickEdit");
            Size = new Rectangle(0, 0, (int)(imgFireBrick.Width * Scale), (int)(imgFireBrick.Height * Scale));
            // Origin of the sprite
            float orX = (float)imgFireBrick.Width / 2;
            float orY = (float)imgFireBrick.Height / 2;
            Origin = new Vector2(orX, orY);
            Visible = true;


            Position = new Vector2(GenRandX(), GenRandY());
            brickRect = new Rectangle((int)this.Position.X - FrameSize,
                    (int)(Position.Y - Size.Height / 4.0f + 10),
                    (int)((float)Size.Width),
                    (int)((float)Size.Height));

        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            

            if (currState == State.Falling && Visible)
            {
                if (isFire == true)
                {
                    spriteBatch.Draw(imgFireBrick, new Vector2(X, Y),
                      null, Color.White, 0.0f,
                      new Vector2(Width / 2f, Height / 2f), 1.0f, SpriteEffects.None, 0);
                }
                else
                {
                    spriteBatch.Draw(imgIceBrick, new Vector2(X, Y),
                      null, Color.White, 0.0f,
                      new Vector2(Width / 2f, Height / 2f), 1.0f, SpriteEffects.None, 0);
                }

                //spriteBatch.Draw(textureFillRect(), brickRect, Color.Green);
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
            if (currState == State.Destroyed)
            {
                Visible = false;
            }

            brickRect = new Rectangle((int)(X - Size.Width/2.0f),
                   (int)(Y - Size.Height/2.0f),
                   (int)((float)Size.Width),
                   (int)((float)Size.Height));
        }
        public Texture2D textureFillRect()
        {
            Texture2D texture = new Texture2D(graphicsDevice, 1, 1, false, SurfaceFormat.Color);
            texture.SetData<Color>(new Color[] { Color.White });
            return texture;
        }
    }
}
