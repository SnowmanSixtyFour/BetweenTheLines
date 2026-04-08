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

        public Button(String text, Point position)
        {
            // Update Variables
            this.X = position.X;
            this.Y = position.Y;

            // Set Sprite
            this.sprite = new StaticSprite(null, Rectangle.Empty, Color.Gray);

            // Set Text
            this.text = new Text(Global.arial, text, Vector2.Zero, Color.White, 1.0f, false);

            // Update Position
            SetPosition(position);
        }

        public void SetPosition(Point newPosition)
        {
            // --- Update Variables ---

            // Position
            this.X = newPosition.X;
            this.Y = newPosition.Y;

            // --- Reposition Variables ---

            // Sprite Position
            this.sprite.SetDestRect(new Rectangle(X, Y, Width, Height));

            // Text Position
            this.text.setPosition(new Vector2(newPosition.X + (this.Width / 2) - (this.text.getText().Length * 4), // X
                newPosition.Y + (this.Height / 3)) // Y
                );
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
            this.text.Draw(spriteBatch);
        }
    }
}
