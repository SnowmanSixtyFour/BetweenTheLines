// The Cursor to transform mouse input relative to the game window.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BetweenTheLines.Source.Graphics;

namespace BetweenTheLines.Source.Objects
{
    internal class Cursor
    {
        // Public Variables

        public int X, Y;

        // Protected Variables

        protected Point size;
        protected StaticSprite sprite;

        public Cursor(Point size)
        {
            // Set Cursor

            setSize(size); // Size
            sprite = new StaticSprite(Global.cursor, new Rectangle(new Point(X, Y), size), Color.White); // Sprite
        }

        public void Update(GameTime gameTime)
        {
            sprite.SetDestRect(new Rectangle(new Point(X, Y), size)); // Position
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        // Setters

        public void setSize(Point newSize)
        {
            this.size = newSize;
        }
    }
}
