// The state of the gameplay loop for Chapter 1.

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
    internal class StoryState : State
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

        // --- Map ---

        public enum Room
        {
            unknown,
            foyer,
            livingRoom,
            mainHall,
            bathroom,
            kitchen,
            closet
        }
        public Room currentRoom = Room.unknown;

        // Map Exploration
        private bool exploring = false;

        // Chapter 1 Progression
        private bool
            seenBathroom = false,
            seenKitchen = false,
            seenCloset = false;

        // Map Triggers
        private StaticSprite
            // Foyer
            foyerToLivingRoom,

            // Living Room
            livingRoomToFoyer, livingRoomToMainHall,

            // Main Hall
            mainHallToLivingRoom, mainHallToBathroom, mainHallToKitchen, mainHallToCloset,

            // Bathroom
            bathroomToMainHall,

            // Kitchen
            kitchenToMainHall,

            // Closet
            closetToMainHall;

        // Trigger Color
        private Color triggerColor = Color.Transparent;

        public StoryState()
        {
            // --- Set Objects ---
            
            // Cinematic and Overlay
            cinematic = new StaticSprite(null, new Rectangle(0, 0, cam.Width, cam.Height), Color.White);

            overlay = new Overlay();
            overlay.cam = cam; // Set Camera for Overlay

            cinematicDoorTrigger = new StaticSprite(null, new Rectangle(new Point((cam.Width / 2) - (doorWidth / 2) + doorPaddingX, (cam.Height - doorHeight) - doorPaddingY), new Point(doorWidth, doorHeight)), Color.Transparent);

            // Door
            dialogBox = new DialogBox();
            portrait = new Portrait(0, 0);

            // Set Default Properties of State
            SetDefaultVariables();

            // --- Set Map Triggers ---

            foyerToLivingRoom = new StaticSprite(null, new Rectangle(700, 145, 122, 270), triggerColor);

            livingRoomToFoyer = new StaticSprite(null, new Rectangle(30, 145, 122, 270), triggerColor);
            livingRoomToMainHall = new StaticSprite(null, new Rectangle(433, 110, 157, 209), triggerColor);

            mainHallToLivingRoom = new StaticSprite(null, new Rectangle(0, 186, 132, 294), triggerColor);
            mainHallToBathroom = new StaticSprite(null, new Rectangle(715, 235, 127, 245), triggerColor);
            mainHallToKitchen = new StaticSprite(null, new Rectangle(526, 111, 108, 250), triggerColor);
            mainHallToCloset = new StaticSprite(null, new Rectangle(273, 56, 72, 259), triggerColor);

            bathroomToMainHall = new StaticSprite(null, new Rectangle(30, 145, 122, 270), triggerColor);

            kitchenToMainHall = new StaticSprite(null, new Rectangle(30, 145, 122, 270), triggerColor);
            
            closetToMainHall = new StaticSprite(null, new Rectangle(620, 155, 122, 270), triggerColor);
        }

        /// <summary>
        /// Sets the properties of many objects in the level. This is to be called either at the start of the state, or during a restart, acting as the gameplay's "Default State".
        /// </summary>
        public void SetDefaultVariables()
        {
            // --- State ---
            cursorVisible = false; // Cursor Hidden by Default

            // --- Game Variables ---

            // Set Real Time Variables
            Global.picklesArrivedTime = DateTime.Now.ToShortTimeString(); // Pickles Arrived upon Chapter 1 Start
            Global.faunArrivedTime = DateTime.Now.AddMinutes(-10).ToShortTimeString(); // Faun Arrived 10 Minutes Earlier than Pickles
            Global.gameStartTime = DateTime.Now.AddMinutes(-5).ToShortTimeString(); // Game Started 5 Minutes Before Pickles Arrived

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
            overlay.chapter.setText("    INTRO"); // Set Chapter Text

            // Map
            currentRoom = Room.unknown; // Set Current Room to Outside
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Graphics ---

            if (!Global.paused)
            {
                // Map Exploration - Change room depending on trigger clicked
                if (exploring)
                {
                    // Room Movement Events

                    if (currentRoom != Room.mainHall)
                    {
                        // Foyer
                        if (currentRoom == Room.foyer)
                        {
                            // Go to Living Room
                            if (cursor.HoveringOver(foyerToLivingRoom.GetDestRect()) && LeftClicked())
                            {
                                currentRoom = Room.livingRoom;
                                SFX.footsteps.Play();
                            }

                            // Inspect
                            if (cursor.HoveringOver(foyerToLivingRoom.GetDestRect())) cursor.Inspect();
                        }

                        // Living Room
                        if (currentRoom == Room.livingRoom)
                        {
                            // Go to Foyer
                            if (cursor.HoveringOver(livingRoomToFoyer.GetDestRect()) && LeftClicked())
                            {
                                currentRoom = Room.foyer;
                                SFX.footsteps.Play();
                            }

                            // Go to Main Hall
                            if (cursor.HoveringOver(livingRoomToMainHall.GetDestRect()) && LeftClicked())
                            {
                                currentRoom = Room.mainHall;
                                SFX.footsteps.Play();
                            }

                            // Inspect
                            if (cursor.HoveringOver(livingRoomToFoyer.GetDestRect())) cursor.Inspect();
                            if (cursor.HoveringOver(livingRoomToMainHall.GetDestRect())) cursor.Inspect();
                        }

                        // Bathroom
                        if (currentRoom == Room.bathroom)
                        {
                            // Go to Main Hall
                            if (cursor.HoveringOver(bathroomToMainHall.GetDestRect()) && LeftClicked())
                            {
                                currentRoom = Room.mainHall;
                                SFX.footsteps.Play();
                            }

                            // Inspect
                            if (cursor.HoveringOver(bathroomToMainHall.GetDestRect())) cursor.Inspect();
                        }

                        // Kitchen
                        if (currentRoom == Room.kitchen)
                        {
                            // Go to Main Hall
                            if (cursor.HoveringOver(kitchenToMainHall.GetDestRect()) && LeftClicked())
                            {
                                currentRoom = Room.mainHall;
                                SFX.footsteps.Play();
                            }

                            // Inspect
                            if (cursor.HoveringOver(kitchenToMainHall.GetDestRect())) cursor.Inspect();
                        }

                        // Closet
                        if (currentRoom == Room.closet)
                        {
                            // Go to Main Hall
                            if (cursor.HoveringOver(closetToMainHall.GetDestRect()) && LeftClicked())
                            {
                                currentRoom = Room.mainHall;
                                SFX.footsteps.Play();
                            }

                            // Inspect
                            if (cursor.HoveringOver(closetToMainHall.GetDestRect())) cursor.Inspect();
                        }
                    }

                    // Main Hall
                    else
                    {
                        // Go to Living Room
                        if (cursor.HoveringOver(mainHallToLivingRoom.GetDestRect()) && LeftClicked())
                        {
                            currentRoom = Room.livingRoom;
                            SFX.footsteps.Play();

                            // Chapter 1 Part 2 Dialog
                            if (dialogBox.dialog != Dialog.chapter1part2
                                && seenBathroom && seenKitchen && seenCloset) StopExploring(Dialog.chapter1part2);
                        }

                        // Go to Bathroom
                        if (cursor.HoveringOver(mainHallToBathroom.GetDestRect()) && LeftClicked())
                        {
                            currentRoom = Room.bathroom;
                            SFX.footsteps.Play();
                        }

                        // Go to Kitchen
                        if (cursor.HoveringOver(mainHallToKitchen.GetDestRect()) && LeftClicked())
                        {
                            currentRoom = Room.kitchen;
                            SFX.footsteps.Play();
                        }

                        // Go to Closet
                        if (cursor.HoveringOver(mainHallToCloset.GetDestRect()) && LeftClicked())
                        {
                            currentRoom = Room.closet;
                            SFX.footsteps.Play();
                        }

                        // Inspect
                        if (cursor.HoveringOver(mainHallToLivingRoom.GetDestRect())) cursor.Inspect();
                        if (cursor.HoveringOver(mainHallToBathroom.GetDestRect())) cursor.Inspect();
                        if (cursor.HoveringOver(mainHallToKitchen.GetDestRect())) cursor.Inspect();
                        if (cursor.HoveringOver(mainHallToCloset.GetDestRect())) cursor.Inspect();
                    }
                }

                // On Left Click
                if (LeftClicked())
                {
                    // --- Chapter 1 Part 1 Dialog ---

                    // Closet Dialog
                    if (currentRoom == Room.closet && !seenCloset)
                    {
                        seenCloset = true;
                        StopExploring(Dialog.chapter1closet);
                    }

                    // Kitchen Dialog
                    if (currentRoom == Room.kitchen && !seenKitchen)
                    {
                        seenKitchen = true;
                        StopExploring(Dialog.chapter1kitchen);
                    }

                    // Bathroom Dialog
                    if (currentRoom == Room.bathroom && !seenBathroom)
                    {
                        seenBathroom = true;
                        StopExploring(Dialog.chapter1bathroom);
                    }

                    // --- Update Cinematic to Match Relevant Room ---

                    // Foyer
                    if (currentRoom == Room.foyer) cinematic.SetTexture(Assets.foyer);

                    // Living Room
                    if (currentRoom == Room.livingRoom) cinematic.SetTexture(Assets.livingRoom);

                    // Main Hall
                    if (currentRoom == Room.mainHall) cinematic.SetTexture(Assets.mainHall);

                    // Bathroom
                    if (currentRoom == Room.bathroom) cinematic.SetTexture(Assets.bathroom);

                    // Kitchen
                    if (currentRoom == Room.kitchen) cinematic.SetTexture(Assets.kitchen);

                    // Closet
                    if (currentRoom == Room.closet) cinematic.SetTexture(Assets.closet);
                }
            }

            // --- Story ---

            if (!Global.paused)
            {
                // If Dialog is not finished
                if (!dialogBox.endOfDialog)
                {
                    // --- Dialog Events ---

                    // Chapter 1 Part 2
                    if (dialogBox.dialog == Dialog.chapter1part2)
                    {
                        if (dialogBox.currentLine == 0)
                        {
                            portrait.SetState(Dialog.smokey, Portrait.State.excited);
                            StopSong();
                        }
                        if (dialogBox.currentLine == 1)
                        {
                            ChangeSong(OST.intense);
                        }
                        if (dialogBox.currentLine == 2) portrait.SetState(Dialog.faun, Portrait.State.worried);
                        if (dialogBox.currentLine == 3) portrait.SetState(Dialog.smokey, Portrait.State.excited);
                    }

                    // Chapter 1 Closet
                    if (dialogBox.dialog == Dialog.chapter1closet)
                    {
                        if (dialogBox.currentLine == 0) portrait.SetState(Dialog.micah, Portrait.State.regular);
                        if (dialogBox.currentLine == 1) portrait.SetState(Dialog.faun, Portrait.State.regular);
                        if (dialogBox.currentLine == 2) portrait.SetState(Dialog.micah, Portrait.State.regular);
                        if (dialogBox.currentLine == 10) portrait.SetState(Dialog.faun, Portrait.State.regular);
                        if (dialogBox.currentLine == 11) portrait.SetState(Dialog.micah, Portrait.State.regular);
                    }

                    // Chapter 1 Kitchen
                    if (dialogBox.dialog == Dialog.chapter1kitchen)
                    {
                    }

                    // Chapter 1 Bathroom
                    if (dialogBox.dialog == Dialog.chapter1bathroom)
                    {
                    }

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
                        if (dialogBox.currentLine == 3)
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

                    // CHAPTER 1 PART 1

                    // Closet
                    if (dialogBox.dialog == Dialog.chapter1closet && seenCloset) ContinueExploring();

                    // Kitchen
                    if (dialogBox.dialog == Dialog.chapter1kitchen && seenKitchen) ContinueExploring();

                    // Bathroom
                    if (dialogBox.dialog == Dialog.chapter1bathroom && seenBathroom) ContinueExploring();

                    // Part 1
                    if (dialogBox.dialog == Dialog.chapter1part1)
                    {
                        // Hide Dialog
                        dialogBox.Hide();
                        portrait.Hide();

                        // Show Cursor
                        cursorVisible = true;

                        // Begin Exploration of Map
                        exploring = true;
                        if (currentRoom == Room.unknown) currentRoom = Room.livingRoom;
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
                        cinematic.SetTexture(Assets.livingRoom); // Living Room

                        // SFX
                        SFX.footsteps.Play();

                        // Change Music
                        ChangeSong(OST.title);
                    }

                    // Intro 2a
                    if (dialogBox.dialog == Dialog.intro2a)
                    {
                        // Dialog
                        dialogBox.setDialog(Dialog.preludeEnd); // Set to Prelude End

                        // Cinematic
                        cinematic.SetTexture(Assets.foyer); // Foyer

                        // Play Footsteps SFX
                        SFX.footsteps.Play();
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

                // --- Update Objects ---

                portrait.Update(gameTime);
                dialogBox.Update(gameTime, dialogSpeed);
                overlay.Update(gameTime, this);
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

        /// <summary>
        /// Stio exploring the map at free will, to play a cutscene.
        /// </summary>
        /// <param name="dialog">The dialog to be displayed in the dialog box upon exploration stopping.</param>
        public void StopExploring(DialogString[] dialog)
        {
            // Show Dialog
            dialogBox.setDialog(dialog); // Set Dialog
            dialogBox.Show();
            portrait.MoveToCenter();

            // Hide Cursor
            cursorVisible = false;

            // Stop Exploring

            exploring = false;
        }

        /// <summary>
        /// Continue exploration of the map at the player's free will.
        /// </summary>
        /// <param name="roomExplored">The bool to set to true, which will prevent the same scene from happening twice.</param>
        public void ContinueExploring()
        {
            // Hide Dialog
            dialogBox.Hide();
            portrait.MoveLeftOffscreen();

            // Show Cursor
            cursorVisible = true;

            // Continue Exploring
            exploring = true;
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

            // Map Triggers
            if (currentRoom == Room.foyer)
            {
                foyerToLivingRoom.Draw(spriteBatch);
            }
            else if (currentRoom == Room.livingRoom)
            {
                livingRoomToFoyer.Draw(spriteBatch);
                livingRoomToMainHall.Draw(spriteBatch);
            }
            else if (currentRoom == Room.mainHall)
            {
                mainHallToLivingRoom.Draw(spriteBatch);
                mainHallToBathroom.Draw(spriteBatch);
                mainHallToKitchen.Draw(spriteBatch);
                mainHallToCloset.Draw(spriteBatch);
            }
            else if (currentRoom == Room.bathroom)
            {
                bathroomToMainHall.Draw(spriteBatch);
            }
            else if (currentRoom == Room.kitchen)
            {
                kitchenToMainHall.Draw(spriteBatch);
            }
            else if (currentRoom == Room.closet)
            {
                closetToMainHall.Draw(spriteBatch);
            }

            // --- Overlay ---

            portrait.Draw(spriteBatch); // Portrait

            dialogBox.Draw(spriteBatch); // Dialog Box

            overlay.Draw(spriteBatch); // Overlay
        }
    }
}
