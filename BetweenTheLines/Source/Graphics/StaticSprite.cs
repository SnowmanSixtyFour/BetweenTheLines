using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BetweenTheLines.Source.Graphics
{
    internal class StaticSprite
    {
        protected Texture2D texture;
        protected Rectangle destRect;
        protected Color color;

        public StaticSprite(Texture2D texture, Rectangle rect, Color color)
        {
            // Error check
            if (texture == null) texture = Global.noImg;

            // Initialize sprite
            SetTexture(texture);
            SetDestRect(rect);
            SetColor(color);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, destRect, color);
        }

        // --- Modifiers ---

        // Getters

        public Texture2D GetTexture()
        {
            return this.texture;
        }

        public Rectangle GetDestRect()
        {
            return this.destRect;
        }

        public Color GetColor()
        {
            return this.color;
        }

        // Position and Size

        public int GetX()
        {
            return this.destRect.X;
        }

        public int GetY()
        {
            return this.destRect.Y;
        }

        public int GetWidth()
        {
            return this.destRect.Width;
        }

        public int GetHeight()
        {
            return this.destRect.Height;
        }

        // Setters

        public void SetTexture(Texture2D newTexture)
        {
            this.texture = newTexture;
        }

        public void SetDestRect(Rectangle newRect)
        {
            this.destRect = newRect;
        }

        public void SetColor(Color newColor)
        {
            this.color = newColor;
        }

        // Position and Size

        public void SetX(int newX)
        {
            this.destRect.X = newX;
        }

        public void SetY(int newY)
        {
            this.destRect.Y = newY;
        }

        public void SetWidth(int newWidth)
        {
            this.destRect.Width = newWidth;
        }

        public void SetHeight(int newHeight)
        {
            this.destRect.Height = newHeight;
        }
    }
}
