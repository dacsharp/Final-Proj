using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
    class Player : Sprite
    {
        const string PLAYER_ASSETNAME = "!$ReBat";
        const string PLAYER_NEXT_ASSETNAME = "!$ReBat_se";
        const string PLAYER_FINAL_ASSETNAME = "!$ReBat_xe";
        const int START_POSITION_X = (ScreenGlobals.SCREEN_WIDTH - 1) / 4;
        const int START_POSITION_Y = (ScreenGlobals.SCREEN_HEIGHT + 1) / 2;
        const int PLAYER_SPEED = ScreenGlobals.GAME_SPEED * 3;
        const int MOVE_UP = -1;
        const int MOVE_DOWN = 1;
        const int MOVE_LEFT = -1;
        const int MOVE_RIGHT = 1;

        const int SECOND_SKIN_SCORE = 10;
        const int THIRD_SKIN_SCORE = 20;

        const bool ICE = false;
        const bool FIRE = true;

        const int FRAME_COUNT = 3;
        TimeSpan FrameLength = TimeSpan.FromSeconds(0.45 / (double)FRAME_COUNT);
        TimeSpan FrameTimer = TimeSpan.Zero;


        // USE FOR GETTING TIME BETWEEN SPACE BAR
        private const float _delay = 3; // seconds
        private float _remainingDelay = _delay;

       
        pState mCurrentState = pState.Flying;
       

       
        Facing mCurrentFacing = Facing.Right;

        bool beginningSpacePressed = false;

        Vector2 mDirection = Vector2.Zero;
        Vector2 mSpeed = Vector2.Zero;


        KeyboardState mPreviousKeyboardState;
        GamePadState mPreviousGamepadState;



        private int FrameNum = 0;

        // rotation of sprite
        private float rotationVelocity = -2.0f;
        private float rotation = 0.0f;

        // PLAYER FRAME SIZE
        const int PLAYER_FRAME_SIZE = 48;



        public List<Bullet> mBullets = new List<Bullet>();
        private sbyte bulletFlip = 1;

        private int score = 0;




        ContentManager content;
        SpriteBatch spriteBatch;
        public Player()
        {
            ;
        }

        public void Initialize(GraphicsDevice gDevice, SpriteBatch inSpritebatch)
        {
            graphicsDevice = gDevice;
            FrameSize = PLAYER_FRAME_SIZE; // the player sprite frames are 48x48
            spriteBatch = inSpritebatch;

        }

        public override void LoadContent(ContentManager theContentManager, string theAssetName)
        {
            content = theContentManager;
            mSpriteTexture = content.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            // Origin of the sprite
            float orX = (float)mSpriteTexture.Width / 2;
            float orY = (float)mSpriteTexture.Height / 2;
            Vector2 Origin = new Vector2(orX, orY);
            Visible = true;
            //Since the player "owns" their bullets, when we update the player,
            //we update all of the bullets, including drawing them
            foreach (Bullet aBullet in mBullets)
            {
                aBullet.LoadContent(theContentManager);
            }

            Position = new Vector2(START_POSITION_X, START_POSITION_Y);
            //LoadFallingBrick();



            base.LoadContent(content, PLAYER_ASSETNAME);


        }
        public void Update(GameTime theGameTime)
        {
            KeyboardState aCurrentKeyboardState = Keyboard.GetState();
            GamePadState aCurrentGamepadState = GamePad.GetState(PlayerIndex.One);

            UpdateMovement(aCurrentKeyboardState);
            UpdateBullet(theGameTime, aCurrentKeyboardState, aCurrentGamepadState);

            mPreviousKeyboardState = aCurrentKeyboardState;
            mPreviousGamepadState = aCurrentGamepadState;

            //CheckBulletBrickHit();

            base.Update(theGameTime, mSpeed, mDirection);

            /* Stop the player from moving off the screen correction */
            // HORIZONTAL PLAYER POSITION DOES NOT CHANGE

            // VERTICAL POSITION - if this is reached player = DEAD
            if (Position.Y >= graphicsDevice.Viewport.Height - Size.Height / 4)
            {
                Position.Y = graphicsDevice.Viewport.Height - Size.Height / 4;
                mCurrentState = pState.Dead;

            }

            if (Position.Y < 0)
            {
                Position.Y = 0;
            }
            /* End player off screen correction */

            //
            FrameTimer += theGameTime.ElapsedGameTime;
            if (FrameTimer >= FrameLength)
            {
                FrameTimer = TimeSpan.Zero;
                FrameNum = (FrameNum + 1) % FRAME_COUNT;
            }




            if (FrameNum >= FRAME_COUNT)
                FrameNum = 0;

            if (score == 10 || score == 20)
            {
                UpdatePlayerSkin(theGameTime);
            }
            //if (mCurrentState == pState.Jumping)
            //{
            //    float timer = (float)theGameTime.ElapsedGameTime.TotalSeconds;

            //    _remainingDelay -= timer;

            //    if (_remainingDelay <= 0)
            //    {
            //        LoadFallingBrick();
            //        _remainingDelay = _delay;
            //    }
            //}




            //// update surroundings
            //if (fallingBrick != null)
            //    fallingBrick.Update(theGameTime);
            //CheckBulletBrickHit();


        }

        //private void CheckBulletBrickHit()
        //{
        //    bool hitBrick = false;

        //    if (hitBrick == false)
        //    {
        //        // check player and brick detection
        //        if (fallingBrick.Visible)
        //        {

        //            // loop through bulltets
        //            if (mBullets.Count >= 1)
        //                foreach (Bullet bullet in mBullets)
        //                {
        //                    if (bullet != null)
        //                        if (fallingBrick.Visible)
        //                        {
        //                            //Brick 
        //                            //Each brick is 16 x 50
        //                            int orY = (int)fallingBrick.Origin.Y;
        //                            int orX = (int)fallingBrick.Origin.X;
        //                            Rectangle BrickRect = new Rectangle(
        //                                (int)fallingBrick.GetX(),
        //                                (int)fallingBrick.GetY() - orY - 1,
        //                                (int)16,
        //                                (int)50
        //                                );
        //                            // bullet
        //                            Rectangle bulletRect = new Rectangle(
        //                                (int)bullet.Position.X,
        //                                (int)bullet.Position.Y,
        //                                (int)bullet.Size.Width, (int)bullet.Size.Height);
        //                            if (HitTest(bulletRect, BrickRect))
        //                            {
        //                                //
        //                                // if both fire
        //                                GameContent gameContent = new GameContent(mContentManager);
        //                                if (bullet.isFire() == true && fallingBrick.fire == true)
        //                                {
        //                                    PlaySound(gameContent.brickSound);
        //                                    fallingBrick.Visible = false;
        //                                    bullet.Visible = false;

        //                                    ++score;

        //                                }
        //                                // if both ice
        //                                else if ((!bullet.isFire() == true) && (!fallingBrick.fire == true))
        //                                {
        //                                    PlaySound(gameContent.brickSound);
        //                                    fallingBrick.Visible = false;
        //                                    bullet.Visible = false;

        //                                    ++score;

        //                                }
        //                                else // bullet hit but was wrong type
        //                                {
        //                                    bullet.Visible = false;
        //                                }
        //                                hitBrick = true;
        //                                break;
        //                            }
        //                        }

        //                }
        //        }
        //    }
        //}

        //private void CheckPlayerBrickCrash()
        //{
        //    this.Origin.X = (float)this.FrameSize / 2.0f;
        //    this.Origin.Y = (float)this.FrameSize / 2.0f;
        //    // Player

        //    Rectangle playerSpriteLocation = new Rectangle(
        //        (int)this.Position.X,
        //        (int)this.Position.Y,
        //        (int)((float)this.Size.Width / 3.0f),
        //        (int)((float)this.Size.Height / 4.0f)
        //        );

        //    if (fallingBrick.Visible)
        //    {
        //        //Brick 
        //        //Each brick is 16 x 50
        //        int orY = (int)fallingBrick.Origin.Y;
        //        int orX = (int)fallingBrick.Origin.X;
        //        Rectangle BrickRect = new Rectangle(
        //            (int)fallingBrick.GetX(),
        //            (int)fallingBrick.GetY() - orY - 1,
        //            (int)16,
        //            (int)50
        //            );

        //        if (HitTest(playerSpriteLocation, BrickRect))
        //        {
        //            mCurrentState = pState.Dead;
        //        }

        //    }
        //}
        //private void LoadFallingBrick()
        //{
        //    GameContent gameContent = new GameContent(mContentManager);
        //    SpriteBatch sB = new SpriteBatch(graphicsDevice);
        //    // 
        //    fallingBrick = new BrickDrop(
        //                    (float)2f * (ScreenGlobals.SCREEN_WIDTH / 3f),
        //                    (float)(ScreenGlobals.SCREEN_HEIGHT / 3f),
        //                    sB, gameContent
        //                    );
        //}

        public void Draw()
        {

            //CheckBulletBrickHit();
            foreach (Bullet aBullet in mBullets)
            {
                aBullet.Draw(spriteBatch);
            }


            // The second param is a "source rectangle", meaning it wants to take a rectangular
            // slice of the source content. The x position is the FrameSize (x width) multiplied by
            // the frame number we currently want to display. The y value is always 0 (for horizontal
            // sprite sheets) and then the x width and y height of the actual frame.

            //if (mSpriteTexture != null && mCurrentState == pState.Flying)
            //spriteBatch.Draw(mSpriteTexture, Position,
            //    new Rectangle(0 + (FrameSize * FrameNum),
            //    (int)mCurrentFacing * FrameSize, FrameSize, mSpriteTexture.Height / 4),
            //    Color.White, rotation, Vector2.Zero, Scale, SpriteEffects.None, 0.0f);

            //if (fallingBrick != null)
            //    fallingBrick.Draw();

            //GameContent gameContent = new GameContent(mContentManager);


            //string scoreMsg = "Score: " + score.ToString("00000");
            //Vector2 space = gameContent.labelFont.MeasureString(scoreMsg);
            //theSpriteBatch.DrawString(gameContent.labelFont, scoreMsg, new Vector2((ScreenGlobals.SCREEN_WIDTH - space.X) / 2, ScreenGlobals.SCREEN_HEIGHT - 40), Color.White);
        }

        

        private void UpdateMovement(KeyboardState aCurrentKeyboardState)
        {


            if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
            {
                beginningSpacePressed = true;
            }


            if ((mCurrentState == pState.Flying || mCurrentState == pState.Jumping) && beginningSpacePressed == true)
            {
                mSpeed = Vector2.Zero;
                mDirection = Vector2.Zero;

                if (aCurrentKeyboardState.IsKeyDown(Keys.Space) == true)
                {


                    mCurrentFacing = Facing.Up;
                    mSpeed.Y = PLAYER_SPEED;
                    mDirection.Y = MOVE_UP;
                    rotation = 0;

                    mCurrentState = pState.Jumping;

                }
                else if (mCurrentState == pState.Dead)
                {
                    mSpeed.Y = 0;
                    mDirection.Y = 0;
                    rotation = 0;
                }

                else
                {
                    mSpeed.Y = PLAYER_SPEED;
                    mDirection.Y = MOVE_DOWN;
                    mCurrentFacing = Facing.Right;
                    if (rotation < .5f && Position.Y != graphicsDevice.Viewport.Height - Size.Height)
                        rotation -= MathHelper.ToRadians(rotationVelocity);
                    else if (Position.Y == graphicsDevice.Viewport.Height - Size.Height)
                    {
                        rotation = 0;
                    }


                }
                // check for environment hit
                // CheckPlayerBrickCrash();




            }


        }

        //public Rectangle getFallingBrickRect()
        //{
        //    if (fallingBrick.Visible)
        //    {
        //        //Brick 
        //        //Each brick is 16 x 50
        //        int orY = (int)fallingBrick.Origin.Y;
        //        int orX = (int)fallingBrick.Origin.X;
        //        Rectangle BrickRect = new Rectangle(
        //            (int)fallingBrick.GetX(),
        //            (int)fallingBrick.GetY() - orY - 1,
        //            (int)16,
        //            (int)50
        //            );
        //        return BrickRect;
        //    }
        //    return new Rectangle();
        //}
        public static bool HitTest(Rectangle r1, Rectangle r2)
        {
            if (Rectangle.Intersect(r1, r2) != Rectangle.Empty)
                return true;
            else
                return false;
        }
        private void UpdatePlayerSkin(GameTime theGameTime)
        {
            if (mCurrentState != pState.Dead)
            {


                if (score == SECOND_SKIN_SCORE)
                {

                    base.LoadContent(content, PLAYER_NEXT_ASSETNAME);

                }
                if (score == THIRD_SKIN_SCORE)
                {
                    base.LoadContent(content, PLAYER_FINAL_ASSETNAME);
                }
                if (mCurrentState == pState.Dead)
                {
                    base.LoadContent(content, "");
                }
            }
        }

        private void UpdateBullet(GameTime theGameTime, KeyboardState aCurrentKeyboardState, GamePadState aCurrentGamepadState)
        {
            foreach (Bullet aBullet in mBullets)
            {
                aBullet.Update(theGameTime);
            }



            if (((aCurrentKeyboardState.IsKeyDown(Keys.F) == true && mPreviousKeyboardState.IsKeyDown(Keys.F) == false)) ||
                (aCurrentGamepadState.Buttons.A == ButtonState.Pressed && mPreviousGamepadState.Buttons.A == ButtonState.Released))
            {

                ShootBullet(FIRE);
            }

            if ((aCurrentKeyboardState.IsKeyDown(Keys.S) == true && mPreviousKeyboardState.IsKeyDown(Keys.S) == false))
            {


                ShootBullet(ICE);
            }
        }

        private void ShootBullet(bool type)
        {
            if (mCurrentState == pState.Flying || mCurrentState == pState.Jumping)
            {
                bool aCreateNew = true;
                for (int i = 0; i < mBullets.Count; i++)
                {
                    if (mBullets[i].Position.Y < 0)
                        mBullets.RemoveAt(i);
                }

                if (aCreateNew == true)
                {
                    Bullet aBullet = new Bullet();
                    aBullet.LoadContent(content);
                    aBullet.Fire(
                        Position + new Vector2(
                            (FrameSize / 2 - aBullet.Size.Width / 2) + (bulletFlip * 12), 3),
                        new Vector2(ScreenGlobals.BULLET_SPEED_X, ScreenGlobals.BULLET_SPEED_Y),
                        new Vector2(1, 0),
                        type
                        );
                    mBullets.Add(aBullet);

                    bulletFlip *= -1;
                }
            }
        }
        public Vector2 getPlayerSpeed()
        {
            return mSpeed;
        }
        public Vector2 getPlayerDir()
        {
            return mDirection;
        }

        public static void PlaySound(SoundEffect sound)
        {
            float volume = 1;
            float pitch = 0.0f;
            float pan = 0.0f;
            sound.Play(volume, pitch, pan);
        }


        public int getCurrState()
        {

            return (int)mCurrentState;
        }
        public int getDead()
        {
            return (int)pState.Dead;
        }
        public int getPause()
        {
            return (int)pState.Pause;
        }
        enum Facing
        {
            Down,
            Left,
            Right,
            Up
        }

       
        enum pState
        {
            Flying,
            Jumping,
            Dead,
            Pause
        }
    }
}

