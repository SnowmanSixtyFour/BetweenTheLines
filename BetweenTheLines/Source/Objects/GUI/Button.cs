using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BetweenTheLines.Source.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using BetweenTheLines.Source.Graphics;

namespace BetweenTheLines.Source.Objects.GUI
{
    internal class Button
    {
        // Variables
        private StaticSprite sprite;
        private Color color;
        private Text text;
        private float textSize;

        // Public Variables
        public int
            X = 0,
            Y = 0,
            Width = 128,
            Height = 64;

        public Rectangle bounds;

        private Color
            defaultColor = Color.White,
            highlightedColor = Color.LightGoldenrodYellow;

        private bool highlighted = false;
        public bool clicked { get; private set; }  = false;

        public Button(String text, Point position, float textSize = 1.0f)
        {
            // Set Sprite
            this.sprite = new StaticSprite(Assets.button, Rectangle.Empty, defaultColor);

            // Set Text
            this.textSize = textSize;
            this.text = new Text(Assets.arial, text, Vector2.Zero, Color.Black, this.textSize, false);

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
            this.text.setPosition(new Vector2(
                newPosition.X + (this.Width / 2) - (this.text.getText().Length * (this.textSize * 5) + 4), // X
                newPosition.Y + (this.Height / 3) // Y
                ));
        }

        public void SetText(String newText)
        {
            // Set New Text
            this.text.setText(newText);

            // Reposition Text on Button
            SetPosition(this.bounds.Location);
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
            cursor.Highlight(this);

            // When Clicked
            if (this.clicked)
            {
                // Play Click SFX
                SFX.button.Play();

                // End Click Event
                this.clicked = false;
            }

            // Get Cursor Hover
            if (cursor.HoveringOver(this.bounds))
            {
                // Highlight
                this.highlighted = true;

                // When Clicked
                if (clicked) this.clicked = true;

                // End Click Event
                else this.clicked = false;
            }
            // Cursor is Not Hovering over Button
            else
            {
                // Unhighlight
                this.highlighted = false;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
            this.text.Draw(spriteBatch);
        }
    }
}
