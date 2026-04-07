using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BetweenTheLines.Source.Graphics
{
    internal class Text
    {
        protected SpriteFont font;
        protected String text;
        protected Vector2 position, origin;
        protected Color color;
        protected float size = 1.0f;

        /// <summary>
        /// Text to be drawn in a SpriteBatch.
        /// </summary>
        /// <param name="font">The SpriteFont image file of the text.</param>
        /// <param name="text">The text to display.</param>
        /// <param name="position"></param>
        /// <param name="color"></param>
        /// <param name="size">A float that controls the size of the text. (E.g. 1.0f, 2.0f)</param>
        public Text(SpriteFont font, String text, Vector2 position, Color color, float size)
        {
            // Initialize text
            setFont(font);
            setText(text);
            setPosition(position);
            setColor(color);
            setSize(size);

            // Find the center of the string
            origin = (font.MeasureString(text) / 2);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, text, position, color, 0, origin, size, SpriteEffects.None, 0.5f);
        }

        // Getters

        public SpriteFont getFont()
        {
            return this.font;
        }

        public String getText()
        {
            return this.text;
        }

        public Vector2 getPosition()
        {
            return this.position;
        }

        public Color getColor()
        {
            return this.color;
        }

        public float getSize()
        {
            return this.size;
        }

        // Setters

        public void setFont(SpriteFont newFont)
        {
            this.font = newFont;
        }

        public void setText(String newText)
        {
            this.text = newText;
        }

        public void setPosition(Vector2 newPosition)
        {
            this.position = newPosition;
        }

        public void setColor(Color newColor)
        {
            this.color = newColor;
        }

        public void setSize(float newSize)
        {
            this.size = newSize;
        }
    }
}
