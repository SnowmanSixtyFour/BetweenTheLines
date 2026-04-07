// An example of using the State class, to create a level

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;

namespace BetweenTheLines.Source.States
{
    internal class LevelState : State
    {
        Character giovanni;
        Text debug;

        public LevelState()
        {
            // Set Level
            cursorVisible = true;

            // Test Character
            giovanni = new Character(Global.giovanni, new Point(10, 200), new Point(48, 29), new Point(16, 29), Color.White);
            giovanni.SetSize(2);

            // Test Create Animations
            giovanni.CreateAnimation("idle", 0, 0);
            giovanni.CreateAnimation("walk", 1, 2);

            debug = new Text(Global.arial, "", new Vector2(10, 10), Color.White, 1.0f);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Text
            debug.setText("X: " + giovanni.X
                + "\nY: " + giovanni.Y
                + "\nWidth: " + giovanni.Width
                + "\nHeight: " + giovanni.Height);

            // Char Movement
            if (KeyDown(Keys.A))
            {
                giovanni.X -= 2;
            }
            if (KeyDown(Keys.D))
            {
                giovanni.X += 2;
            }
            if (KeyDown(Keys.W))
            {
                giovanni.Y -= 2;
            }
            if (KeyDown(Keys.S))
            {
                giovanni.Y += 2;
            }

            // Char Animations
            if (!KeyDown(Keys.A) // Idle
                && !KeyDown(Keys.D)
                && !KeyDown(Keys.W)
                && !KeyDown(Keys.S))
            {
                giovanni.PlayAnimation("idle");
            }
            else // Walking
            {
                giovanni.PlayAnimation("walk");
            }

            // Toggle Filter
            if (KeyPress(Keys.Enter))
            {
                Global.shadersEnabled = !Global.shadersEnabled;
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.CornflowerBlue);

            debug.Draw(spriteBatch);
            giovanni.Draw(spriteBatch);
        }
    }
}
