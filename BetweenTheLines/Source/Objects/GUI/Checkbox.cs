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
        private Text text;
        private int textPadding = 20;
        private Color color = Color.White;

        // Public Variables
        public int
            X = 0,
            Y = 0,
            Width = 40,
            Height = 40;

        public Rectangle bounds;

        public bool
            clicked = false, // Click Event
            active = false; // Active Sprite

        public Checkbox(Point position, String text = "")
        {
            // Set Checkbox
            this.sprite = new StaticSprite(Global.checkboxInactive, Rectangle.Empty, color);
            this.text = new Text(Global.arial, text, Vector2.Zero, Color.White, 1.0f, false);

            SetPosition(position);
        }

        public void Update(GameTime gameTime, Cursor cursor, bool clicked)
        {
            // --- Cursor Properties ---

            // Highlight Cursor
            cursor.Highlight(this.bounds);

            // Get Cursor Click
            if (cursor.HoveringOver(this.bounds) && clicked)
            {
                this.clicked = true;
            }

            // --- Checkbox Properties ---

            // Sprite
            if (this.active) this.sprite.SetTexture(Global.checkboxActive);
            else this.sprite.SetTexture(Global.checkboxInactive);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
            this.text.Draw(spriteBatch);
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

            // Sprite
            this.sprite.SetDestRect(this.bounds);

            // Text
            this.text.setPosition(new Vector2(X + (Width + textPadding), Y + (Height / 4)));
        }
    }
}
