using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flappy_Final
{
    class Sprite
    {
        public bool Visible { get; set; }
        // The current position of the Sprite
        public Vector2 Position = new Vector2(0, 0);

        public Vector2 Origin;

        // The texture object used when drawing the sprite
        public Texture2D mSpriteTexture;

        // The asset name for the Sprite's Texture
        public string AssetName;

        // The Size of the Sprite (with scale applied)
        public Rectangle Size;

        // The size of each individual frame of the sprite
        // With sprite sheets, this is the portion of the sheet to draw
        public int FrameSize;

        //The amount to increase/decrease the size of the original sprite. 
        private float mScale = 1.0f;

        // Give sprites access to the graphics device
        public GraphicsDevice graphicsDevice;

        // When the scale is modified through the property, the Size of the 
        // sprite is recalculated with the new scale applied.
        public float Scale
        {
            get { return mScale; }
            set
            {
                mScale = value;
                // Recalculate the Size of the Sprite with the new scale
                Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            }
        }

        // Load the texture for the sprite using the Content Pipeline
        public virtual void LoadContent(ContentManager theContentManager, string theAssetName)
        {

            mSpriteTexture = theContentManager.Load<Texture2D>(theAssetName);
            AssetName = theAssetName;
            Size = new Rectangle(0, 0, (int)(mSpriteTexture.Width * Scale), (int)(mSpriteTexture.Height * Scale));
            // Origin of the sprite
            float orX = (float)mSpriteTexture.Width / 2;
            float orY = (float)mSpriteTexture.Height / 2;
            Vector2 Origin = new Vector2(orX, orY);
            Visible = true;
        }

        // Draw the sprite to the screen
        public virtual void Draw(SpriteBatch theSpriteBatch)
        {


            if (Visible)
                theSpriteBatch.Draw(mSpriteTexture, Position,
                    new Rectangle(0, 0, mSpriteTexture.Width, mSpriteTexture.Height),
                    Color.White, 0.0f, Origin, Scale, SpriteEffects.None, 0);
        }

        // Update the Sprite and change its position based on the passed in speed, direction and elapsed time.
        public void Update(GameTime theGameTime, Vector2 theSpeed, Vector2 theDirection)
        {
            // By multiplying by the number of seconds that have passed, we know if we should have only
            // moved a fraction of the distance we intended to move. This bases everything off of their
            // whole values being equated to 1 second, as in if the speed is 10, I expect the player to move
            // 10 units per second, hence a fractional amount will allow me to scale with the computer's
            // performance.
            Position += theDirection * theSpeed * (float)theGameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}