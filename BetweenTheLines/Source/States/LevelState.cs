// The Main State of the Gameplay Loop

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Objects.GUI;
using BetweenTheLines.Source.Objects.Level;

namespace BetweenTheLines.Source.States
{
    internal class LevelState : State
    {
        // Objects
        private StaticSprite cinematic;
        private Overlay overlay;

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

            overlay = new Overlay();
            overlay.cam = cam; // Set Camera for Overlay

            cinematicDoorTrigger = new StaticSprite(null, new Rectangle(new Point((cam.Width / 2) - (doorWidth / 2) + doorPaddingX, (cam.Height - doorHeight) - doorPaddingY), new Point(doorWidth, doorHeight)), Color.Transparent);

            dialogBox = new DialogBox();

            portrait = new Portrait(0, 0);

            SetDefaultVariables();
        }

        /// <summary>
        /// Sets the properties of many objects in the level. This is to be called either at the start of the state, or during a restart, acting as the gameplay's "Default State".
        /// </summary>
        public void SetDefaultVariables()
        {
            // --- State ---
            cursorVisible = false; // Cursor Hidden by Default

            // --- Game Variables ---

            // Set Time Faun Arrived
            Global.faunArrivedTime = (DateTime.Now.Hour + ":" + (DateTime.Now.Minute - 10));

            // Load Dialog after Variables Set
            Dialog.LoadDialog();

            // --- Objects ---

            // Cinematic BG
            cinematic.SetTexture(Assets.intro1); // Set to Intro 1 by Default
            
            // Portraits
            portrait.Hide(); // Hide Portrait on Start
            portrait.SetState(Dialog.pickles, Portrait.State.regular); // Set to Pickles

            // Dialog Box
            dialogBox.ResetText(); // Reset Text (IMPORTANT! To clear any last dialog before reset)
            dialogBox.Show();

            dialogBox.setDialog(Dialog.intro1); // Set to Intro 1
            dialogSpeed = defaultDialogSpeed; // Reset Dialog Speed (Default)

            // Overlay
            overlay.chapter.setText(" PRELUDE"); // Set Chapter Text
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Story ---

            if (!Global.paused)
            {
                // If Dialog is not finished
                if (!dialogBox.endOfDialog)
                {
                    // --- Dialog Events ---

                    // Chapter 1 Part 1
                    if (dialogBox.dialog == Dialog.chapter1part1)
                    {
                        if (dialogBox.currentLine == 0) portrait.SetState(Dialog.otto, Portrait.State.regular);
                        if (dialogBox.currentLine == 2) portrait.SetState(Dialog.otto, Portrait.State.angry);
                        if (dialogBox.currentLine == 5) portrait.SetState(Dialog.otto, Portrait.State.regular);
                        if (dialogBox.currentLine == 7) portrait.SetState(Dialog.otto, Portrait.State.angry);
                        if (dialogBox.currentLine == 8) portrait.SetState(Dialog.faun, Portrait.State.worried);
                        if (dialogBox.currentLine == 9) portrait.SetState(Dialog.otto, Portrait.State.regular);
                        if (dialogBox.currentLine == 10) portrait.SetState(Dialog.faun, Portrait.State.worried);
                        if (dialogBox.currentLine == 11) portrait.SetState(Dialog.otto, Portrait.State.regular);
                        if (dialogBox.currentLine == 14) portrait.SetState(Dialog.angel, Portrait.State.regular);
                        if (dialogBox.currentLine == 16) portrait.SetState(Dialog.angel, Portrait.State.thinking);
                        if (dialogBox.currentLine == 17) portrait.SetState(Dialog.angel, Portrait.State.regular);
                        if (dialogBox.currentLine == 18) portrait.SetState(Dialog.faun, Portrait.State.regular);
                        if (dialogBox.currentLine == 19) portrait.SetState(Dialog.otto, Portrait.State.regular);
                        if (dialogBox.currentLine == 22) portrait.SetState(Dialog.faun, Portrait.State.worried);
                        if (dialogBox.currentLine == 23) StopSong();
                        if (dialogBox.currentLine == 24) ChangeSong(OST.intense);
                        if (dialogBox.currentLine == 29)
                        {
                            portrait.SetState(Dialog.faun, Portrait.State.regular);
                            ChangeSong(OST.intro);
                        }
                    }

                    // Prelude 1
                    if (dialogBox.dialog == Dialog.preludeEnd)
                    {
                        // Line 1
                        if (dialogBox.currentLine == 1)
                        {
                            // Dialog Portraits
                            portrait.SetState(Dialog.faun, Portrait.State.regular);

                            // Portrait Position
                            portrait.MoveToCenter();
                        }
                    }

                    // Intro 2
                    if (dialogBox.dialog == Dialog.intro2)
                    {
                        // Line 3
                        if (dialogBox.currentLine == 3)
                        {
                            portrait.MoveLeftOffscreen(); // Hide Portrait
                        }
                    }

                    // --- Game Progression ---

                    // Proceed Dialog
                    if (KeyPress(Keys.Enter) || LeftClicked()) dialogBox.Proceed();
                }

                // When Dialog is finished
                else
                {
                    // NOTE: Functions trigger after chosen dialog is finished.
                    // Dialog must be ordered from last to first, or else triggers will conflict!

                    // --- Dialog End Events ---

                    // CHAPTER 1

                    // Part 1
                    if (dialogBox.dialog == Dialog.chapter1part1)
                    {
                    }

                    // PRELUDE

                    // Prelude End
                    if (dialogBox.dialog == Dialog.preludeEnd)
                    {
                        // Dialog
                        dialogBox.setDialog(Dialog.chapter1part1); // Set to Chapter 1

                        // Set Overlay to Display Chapter 1
                        overlay.chapter.setText("CHAPTER 1");

                        // Cinematic
                        cinematic.SetTexture(Assets.livingRoom); // Change to Foyer

                        // Change Music
                        ChangeSong(OST.title);
                    }

                    // Intro 2a
                    if (dialogBox.dialog == Dialog.intro2a)
                    {
                        // Dialog
                        dialogBox.setDialog(Dialog.preludeEnd); // Set to Prelude End

                        // Cinematic
                        cinematic.SetTexture(Assets.foyer); // Change to Foyer
                    }

                    // Intro 2
                    if (dialogBox.dialog == Dialog.intro2)
                    {
                        // --- State ---

                        cursorVisible = true; // Show Cursor

                        // --- Objects ---

                        dialogBox.Hide(); // Hide Dialog Box

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

                            cursorVisible = false; // Hide Cursor
                            SFX.button.Play(); // Play Click Sound
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

                portrait.Update(gameTime);
                dialogBox.Update(gameTime, dialogSpeed);
                overlay.Update(gameTime);
            }

            // During Pause
            else
            {
                // Check if R is Pressed (Return to Menu)
                if (KeyPress(Keys.R))
                {
                    ChangeSong(OST.title);

                    // Switch State
                    this.changeState = true;
                    Global.currentState = Global.State.title;
                }
            }
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

            // --- Scene ---

            cinematic.Draw(spriteBatch); // Cinematic Artwork

            cinematicDoorTrigger.Draw(spriteBatch); // Door Trigger (Invisible)

            // --- Overlay ---

            portrait.Draw(spriteBatch); // Portrait

            dialogBox.Draw(spriteBatch); // Dialog Box

            overlay.Draw(spriteBatch); // Overlay
        }
    }
}
