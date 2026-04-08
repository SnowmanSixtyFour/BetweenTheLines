// An example of using the State class, to create a level

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Objects.Level;

namespace BetweenTheLines.Source.States
{
    internal class LevelState : State
    {
        // Objects
        private StaticSprite cinematic;

        private DialogBox dialogBox;

        // Dialog Speed
        private float
            defaultDialogSpeed = 0.02f, // Normal - 20 Milliseconds
            fastDialogSpeed = 0.01f; // Fast - 10 Milliseconds

        private float dialogSpeed;

        public LevelState()
        {
            // Set Level
            cursorVisible = true;

            // Set Objects
            cinematic = new StaticSprite(null, new Rectangle(0, 0, cam.Width, cam.Height), Color.White);

            dialogBox = new DialogBox();
            dialogBox.setDialog(Dialog.intro1);

            // Set Dialog Speed
            dialogSpeed = defaultDialogSpeed;

            // --- IGNORE ---
            /*
            // Test Character
            giovanni = new Character(Global.giovanni, new Point(10, 200), new Point(48, 29), new Point(16, 29), Color.White);
            giovanni.SetSize(2);

            // Test Create Animations
            giovanni.CreateAnimation("idle", 0, 0);
            giovanni.CreateAnimation("walk", 1, 2);

            debug = new Text(Global.arial, "", new Vector2(10, 10), Color.White, 1.0f, false);
            */
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Cinematic

            if (dialogBox.dialog == Dialog.intro1) cinematic.SetTexture(Global.intro1); // Intro 1
            if (dialogBox.dialog == Dialog.intro2) cinematic.SetTexture(Global.intro2); // Intro 2

            // Dialog Box

            if (!Global.paused)
            {
                // If Dialog is not finished
                if (!dialogBox.endOfDialog)
                {
                    // Proceed Dialog
                    if (KeyPress(Keys.Enter) || LeftClicked()) dialogBox.Proceed();
                }
                // When Dialog is finished
                else
                {
                    // NOTE: Functions trigger after chosen dialog is finished.
                    // Dialog must be ordered from last to first, or else triggers will conflict!

                    // Intro 2
                    if (dialogBox.dialog == Dialog.intro2) dialogBox.Hide(); // Hide

                    // Intro 1
                    if (dialogBox.dialog == Dialog.intro1) dialogBox.setDialog(Dialog.intro2); // Set to Intro 2
                }

                // Update Objects

                // Dialog Box
                dialogBox.Update(gameTime, dialogSpeed);
            }

            // --- IGNORE ---
            /*
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
            */
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Objects

            cinematic.Draw(spriteBatch); // Cinematic Artwork
            dialogBox.Draw(spriteBatch); // Dialog Box
        }
    }
}
