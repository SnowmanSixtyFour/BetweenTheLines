// An example of using the State class, to create a level

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Objects.Level;

namespace BetweenTheLines.Source.States
{
    internal class LevelState : State
    {
        // Objects
        private StaticSprite cinematic;

        // Intro Objects
        private StaticSprite cinematicDoorTrigger;
        private int
            doorWidth = 60, doorHeight = 80,
            doorPaddingX = 40, doorPaddingY = 40;

        private DialogBox dialogBox;

        // Dialog Speed
        private float
            defaultDialogSpeed = 0.02f, // Normal - 20 Milliseconds
            fastDialogSpeed = 0.01f; // Fast - 10 Milliseconds

        private float dialogSpeed;

        // Portraits
        Portrait portrait;

        public LevelState()
        {
            // Set Objects
            cinematic = new StaticSprite(null, new Rectangle(0, 0, cam.Width, cam.Height), Color.White);

            cinematicDoorTrigger = new StaticSprite(null, new Rectangle(new Point((cam.Width / 2) - (doorWidth / 2) + doorPaddingX, (cam.Height - doorHeight) - doorPaddingY), new Point(doorWidth, doorHeight)), Color.Transparent);

            dialogBox = new DialogBox();

            portrait = new Portrait(0, 0);

            SetDefaultVariables();

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

        /// <summary>
        /// Sets the properties of many objects in the level. This is to be called either at the start of the state, or during a restart, acting as the gameplay's "Default State".
        /// </summary>
        public void SetDefaultVariables()
        {
            // --- State ---
            cursorVisible = false; // Cursor Hidden by Default

            // --- Objects ---

            // Cinematic BG
            cinematic.SetTexture(Assets.intro1); // Set to Intro 1 by Default
            
            // Portraits
            portrait.Hide(); // Hide Portrait on Start

            // Dialog Box
            dialogBox.ResetText(); // Reset Text (IMPORTANT! To clear any last dialog before reset)
            dialogBox.Show();

            dialogBox.setDialog(Dialog.intro1); // Set to Intro 1
            dialogSpeed = defaultDialogSpeed; // Reset Dialog Speed (Default)
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Story ---

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

                    // Intro 2a
                    if (dialogBox.dialog == Dialog.intro2a)
                    {
                    }

                    // Intro 2
                    if (dialogBox.dialog == Dialog.intro2)
                    {
                        // --- State ---

                        cursorVisible = true; // Show Cursor

                        // --- Objects ---

                        dialogBox.Hide(); // Hide Dialog Box
                        portrait.MoveLeftOffscreen(); // Hide Portrait

                        // Hovering over Door
                        if (cursor.HoveringOver(cinematicDoorTrigger.GetDestRect()))
                        {
                            // Cursor
                            cursor.Inspect();

                            // Cinematic
                            cinematic.SetTexture(Assets.intro2a); // Change to Intro 2A (Hover)
                        }
                        
                        // Not Hovering over Door
                        else
                        {
                            // Cinematic
                            cinematic.SetTexture(Assets.intro2); // Change to Intro 2 (No Hover)
                        }

                        // Clicking Door
                        if (cursor.HoveringOver(cinematicDoorTrigger.GetDestRect()) && LeftClicked())
                        {
                            // Dialog
                            dialogBox.setDialog(Dialog.intro2a); // Set to Intro 2a
                            dialogBox.Show(); // Show Dialog Box
                        }
                    }

                    // Intro 1
                    if (dialogBox.dialog == Dialog.intro1)
                    {
                        // --- Objects ---

                        // Dialog
                        dialogBox.setDialog(Dialog.intro2); // Set to Intro 2
                        portrait.MoveToCenter(); // Move Portrait on-screen

                        // Cinematic
                        cinematic.SetTexture(Assets.intro2); // Intro 2
                    }
                }

                // Update Objects

                portrait.Update(gameTime); // Portrait

                // Dialog Box
                dialogBox.Update(gameTime, dialogSpeed);
            }

            // During Pause
            else
            {
                // Check if R is Pressed (Return to Menu)
                if (KeyPress(Keys.R))
                {
                    // Change Music
                    StopSong();
                    MediaPlayer.Play(OST.title); // Title

                    // Switch State
                    this.changeState = true;
                    Global.currentState = Global.State.title;
                }
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
            */
        }

        public override void ResetState()
        {
            // Reset Objects and Game to Default State
            SetDefaultVariables();

            base.ResetState();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Objects

            cinematic.Draw(spriteBatch); // Cinematic Artwork

            cinematicDoorTrigger.Draw(spriteBatch); // Door Trigger (Invisible)

            portrait.Draw(spriteBatch); // Portrait

            dialogBox.Draw(spriteBatch); // Dialog Box
        }
    }
}
