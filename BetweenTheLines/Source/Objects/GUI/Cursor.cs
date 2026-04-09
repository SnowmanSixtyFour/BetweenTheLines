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

namespace BetweenTheLines.Source.Objects.GUI
{
    internal class Cursor
    {
        // Public Variables

        public int X, Y;
        public Rectangle Bounds;

        // Protected Variables

        protected Point size;
        protected StaticSprite sprite;
        public int opacity;

        // States
        protected bool
            highlighted = false;

        // Functions
        protected bool
            fadingOut = false;

        public float timer = 0f;
        protected float timerStart = 0f, timerEnd = 0.05f;

        public Cursor(Point size)
        {
            // Set Cursor

            setSize(size); // Size
            sprite = new StaticSprite(Global.cursor, new Rectangle(new Point(X, Y), size), Color.White); // Sprite
        }

        int previousX;

        public void Update(GameTime gameTime)
        {
            // Update Global Variables
            Bounds = sprite.GetDestRect();

            // Update Timer
            if (timer > timerEnd) timer = timerStart;
            else timer += 0.1f;

            // Update Sprite
            sprite.SetDestRect(new Rectangle(new Point(X, Y), size)); // Rectangle (Position)
            sprite.SetColor(new Color(opacity, opacity, opacity, opacity)); // Color

            // --- Highlight ---

            // --- Fade Out ---

            // When Not Fading Out, Reset Values
            if (!fadingOut)
            {
                // Keep original cursor position stored (for when fade out is finished)
                previousX = X;

                // Set Opacity to Full if game is not paused
                if (!Global.paused) opacity = 255;
            }

            // On Fade Out
            if (timer > timerEnd)
            {
                if (fadingOut)
                {
                    // Update Size
                    size = new Point((int)(size.X * 1.1f), (int)(size.Y * 1.1f));

                    X -= Global.cursorResize / 2; // Move Left to Keep Centered

                    // Lower Opacity
                    if (opacity > 0) opacity -= 30;
                    else opacity = 0;

                    // End Fade Out when Size Limit Reached
                    if (size.X > Global.cursorSize.X * 2)
                    {
                        // End fade
                        fadingOut = false;

                        X = previousX;
                    }
                }
            }

            if (highlighted) sprite.SetTexture(Global.cursorHighlight);
            else sprite.SetTexture(Global.cursor);

            highlighted = false;
        }

        public void ResetValues()
        {
            X = previousX;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            sprite.Draw(spriteBatch);
        }

        // Setters

        public void setSize(Point newSize)
        {
            size = newSize;
        }

        // Transitions

        public void FadeOut()
        {
            fadingOut = true;
        }

        public void ResetSize()
        {
            fadingOut = false;
            size = Global.cursorSize;
        }

        // Unique Behaviours

        public void Highlight(Rectangle rect)
        {
            if (Bounds.Intersects(rect)) highlighted = true;
        }

        public void Highlight(Character collider)
        {
            if (Bounds.Intersects(collider.Bounds)) highlighted = true;
        }

        public void Highlight(Button button)
        {
            if (this.Bounds.Intersects(button.bounds)) highlighted = true;
        }


        public bool HoveringOver(Rectangle rect)
        {
            return Bounds.Intersects(rect);
        }
    }
}
