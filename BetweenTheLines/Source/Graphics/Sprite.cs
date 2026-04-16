using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BetweenTheLines.Source.Graphics
{
    internal class Sprite : StaticSprite
    {
        protected Rectangle sourceRect;
        public bool flipped;

        public Sprite(Texture2D texture, Rectangle destRect, Rectangle sourceRect, Color color) : base(texture, destRect, color)
        {
            // Error check
            if (texture == null) texture = Assets.noImg;

            // Initialize sprite
            SetTexture(texture);
            SetDestRect(destRect);
            SetSourceRect(sourceRect);
            SetColor(color);
        }

        public new void Draw(SpriteBatch spriteBatch)
        {
            // Draw Sprite
            if (!flipped) spriteBatch.Draw(GetTexture(), GetDestRect(), sourceRect, GetColor());
            else spriteBatch.Draw(GetTexture(), GetDestRect(), sourceRect, GetColor(), 0.0f, Vector2.Zero, SpriteEffects.FlipHorizontally, 0.0f);
        }

        // Getters

        public Rectangle GetSourceRect()
        {
            return this.sourceRect;
        }

        // Setters

        public void SetSourceRect(Rectangle newRect)
        {
            this.sourceRect = newRect;
        }
    }
}
