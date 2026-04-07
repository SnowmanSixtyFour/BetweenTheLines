using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BetweenTheLines.Source.Graphics;

namespace BetweenTheLines.Source.Objects
{
    internal class Character
    {
        // Public variables
        public int X, Y, Width, Height;

        // Protected variables
        protected int resize = 1, defaultWidth, defaultHeight;
        protected Sprite sprite;

        // Animation variables
        Point sheetSize;
        float elapsed = 0, delay = 200f;
        int frame = 0, maxFrames;

        public List<String> animation;
        protected List<int> startFrame, endFrame;

        // Controls
        public KeyboardState keyboard, previousKeyboard;

        public Character(Texture2D texture, Point location, Point size, Point sheetSize, Color color)
        {
            // Set variables
            this.X = location.X;
            this.Y = location.Y;

            if (texture != null) this.defaultWidth = (size.X / 3);
            else this.defaultWidth = size.X;

            this.defaultHeight = size.Y;
            this.sheetSize = sheetSize;
            UpdateSize();

            animation = new List<String>();
            startFrame = new List<int>();
            endFrame = new List<int>();

            // Create Sprite
            sprite = new Sprite(texture,                // Texture
                new Rectangle(new Point(X, Y),          // Position
                new Point(this.Width, this.Height)),    // Size
                new Rectangle(new Point(0, 0),          // Sprite Sheet Frame Position
                sheetSize),                             // Sprite Sheet Size
                color);                                 // Color
        }

        // Set bounds for Character
        public Rectangle Bounds
        {
            get
            {
                return new Rectangle(new Point(X, Y), new Point(Width, Height));
            }
        }

        public void Update(GameTime gameTime)
        {
            // Controls
            keyboard = Keyboard.GetState();

            // Override Update
            OnUpdate(gameTime);

            previousKeyboard = keyboard;
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Update

            // Sprite
            sprite.SetDestRect(new Rectangle(
                new Point(X, Y),
                new Point(Width * resize, Height * resize)));

            // Current Frame of Sprite Sheet
            sprite.SetSourceRect(new Rectangle(
                new Point((sheetSize.X * frame), 0),
                new Point(sheetSize.X, sheetSize.Y)));

            // Draw

            sprite.Draw(spriteBatch);
        }

        // Animate

        public void CreateAnimation(String name, int startFrame, int endFrame)
        {
            // Add Animation to Name List
            this.animation.Add(name);

            // Start and End Frames
            this.startFrame.Add(startFrame);
            this.endFrame.Add(endFrame);
        }

        public void PlayAnimation(String name)
        {
            int animNumber = -1;

            // Set Animation Number

            // While no current Animation selected
            if (animNumber == -1)
            {
                // For All Animations
                for (int i = 0; i < this.animation.Count; i++)
                {
                    // When User Input matches Animation Name
                    if (name == this.animation[i])
                    {
                        animNumber = i;
                    }
                }
            }

            

            // Play Selected Animation
            PlayAnimation(startFrame[animNumber], endFrame[animNumber]);
        }

        // Private Play Animation
        // This plays the start and end frames of the animation.
        // Not to be publicly used because you only need the name to play an animation.
        private void PlayAnimation(int startFrame, int endFrame)
        {
            // Update animation time using GameTime
            elapsed += (float)MainGame.gameTime.ElapsedGameTime.TotalMilliseconds;

            if (elapsed >= delay)
            {
                // Go back to first frame
                if (frame >= endFrame)
                {
                    frame = startFrame;
                }
                // Update frame
                else
                {
                    frame++;
                }

                // Reset animation time
                elapsed = 0;
            }
        }

        public void SetSprite(Texture2D newTexture)
        {
            this.sprite.SetTexture(newTexture);
        }

        public void SetFrame(int newFrame)
        {
            this.frame = newFrame;
        }

        // Interaction

        public bool CollidesWith(Character collider)
        {
            // Colliding character is intersecting another character
            return this.Bounds.Intersects(collider.Bounds);
        }

        // Setters

        public void SetSize(int newSize)
        {
            this.resize = newSize;

            UpdateSize();
        }

        private void UpdateSize()
        {
            this.Width = (defaultWidth * resize);
            this.Height = (defaultHeight * resize);
        }

        // Controls

        public bool KeyPress(Keys key)
        {
            if (keyboard.IsKeyUp(key) && previousKeyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }
    }
}
