using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Flappy_Final
{
    class Walls
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public Vector2 texturePos;



        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, texturePos, rectangle,
            Color.White, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 0);

        }
    }


    class ScrollingWalls : Walls
    {


        public ScrollingWalls(Texture2D newTexture, Vector2 newTexturePos, Rectangle newRectangle)
        {
            texture = newTexture;
            texturePos = newTexturePos;
            rectangle = newRectangle;

        }


        public void Update()
        {
            rectangle.X -= 3;
        }

        public Rectangle GetRectangle()
        {
            return rectangle;
        }
    }


}
