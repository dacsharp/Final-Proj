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

    class Backgrounds
    {
        public Texture2D texture;
        public Rectangle rectangle;
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, rectangle, Color.White);

        }
    }

    class Sky : Backgrounds
    {
        public Sky(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 3;
        }
    }

    class Trees : Backgrounds
    {
        public Trees(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 4;
        }
    }

    class Stars : Backgrounds
    {
        public Stars(Texture2D newTexture, Rectangle newRectangle)
        {
            texture = newTexture;
            rectangle = newRectangle;
        }

        public void Update()
        {
            rectangle.X -= 1;
            
        }
    }
    
}
