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
    internal class Checkbox
    {
        // Private Variables
        private StaticSprite sprite;
        private Color color = Color.White;

        // Public Variables
        public int
            X = 0,
            Y = 0,
            Width = 40,
            Height = 40;

        public Rectangle bounds;

        public Checkbox(Point position)
        {
            // Set Checkbox
            this.sprite = new StaticSprite(Global.checkboxInactive, Rectangle.Empty, color);

            SetPosition(position);
        }

        public void Update(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
        }

        public void SetPosition(Point newPosition)
        {
            // --- Set Variables ---

            // Position
            this.X = newPosition.X;
            this.Y = newPosition.Y;

            // Rectangle
            this.bounds = new Rectangle(X, Y, Width, Height);

            // --- Reposition Variables ---

            // Set Sprite Position

            this.sprite.SetDestRect(this.bounds);
        }
    }
}
