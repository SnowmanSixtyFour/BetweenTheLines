using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source.Graphics;

namespace BetweenTheLines.Source.Objects.GUI
{
    internal class Button
    {
        // Variables
        private StaticSprite sprite;
        private Text text;

        // Public Variables
        public int
            X = 0,
            Y = 0,
            Width = 128,
            Height = 64;

        public Rectangle bounds;

        private Color
            defaultColor = Color.White,
            highlightColor = Color.Yellow;
        private bool highlighted = false;

        public Button(String text, Point position)
        {
            // Set Sprite
            this.sprite = new StaticSprite(Global.button, Rectangle.Empty, defaultColor);

            // Set Text
            this.text = new Text(Global.arial, text, Vector2.Zero, Color.Black, 1.0f, false);

            // Update Position
            SetPosition(position);
        }

        public void SetPosition(Point newPosition)
        {
            // --- Update Variables ---

            // Position
            this.X = newPosition.X;
            this.Y = newPosition.Y;

            // Rectangle
            this.bounds = new Rectangle(X, Y, Width, Height);

            // --- Reposition Variables ---

            // Sprite Position
            this.sprite.SetDestRect(new Rectangle(X, Y, Width, Height));

            // Text Position
            this.text.setPosition(new Vector2(newPosition.X + (this.Width / 2) - (this.Width / 4), // X
                newPosition.Y + (this.Height / 3)) // Y
                );

            // Highlight
            if (this.highlighted)
            {
                this.sprite.SetColor(highlightColor);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
            this.text.Draw(spriteBatch);
        }

        public void Highlight()
        {
            this.highlighted = true;
        }
    }
}
