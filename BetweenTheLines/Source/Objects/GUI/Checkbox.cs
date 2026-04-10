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
        private Color color;
        private Text text;
        private float textSize;
        private int textPadding = 20;

        // Public Variables
        public int
            X = 0,
            Y = 0,
            Width = 40,
            Height = 40;

        public Rectangle bounds;

        private Color
            defaultColor = Color.White,
            highlightedColor = Color.LightGoldenrodYellow;

        public bool
            highlighted = false, // Highlight (Hovering over with Cursor)
            clicked = false, // Click Event
            active = false; // Active Sprite

        public Checkbox(Point position, String text = "", float textSize = 1.0f)
        {
            // Set Checkbox
            this.sprite = new StaticSprite(Global.checkboxInactive, Rectangle.Empty, defaultColor);
            this.textSize = textSize;
            this.text = new Text(Global.arial, text, Vector2.Zero, Color.Black, this.textSize, false);

            SetPosition(position);
        }

        public void Update(GameTime gameTime, Cursor cursor, bool clicked)
        {
            // --- Update Variables ---

            // Highlighted Color
            if (this.highlighted) this.color = this.highlightedColor;
            else this.color = this.defaultColor;

            // --- Update Sprite ---

            // Color
            this.sprite.SetColor(color);

            // --- Cursor Properties ---

            // Highlight Cursor
            cursor.Highlight(this.bounds);

            // Get Cursor Hover
            if (cursor.HoveringOver(this.bounds))
            {
                // Highlight
                this.highlighted = true;

                // Click Event
                if (clicked) this.clicked = true;
            }
            // Cursor is Not Hovering over Button
            else
            {
                // Unhighlight
                this.highlighted = false;
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
