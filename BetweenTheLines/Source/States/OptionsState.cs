using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects.GUI;

namespace BetweenTheLines.Source.States
{
    internal class OptionsState : State
    {
        // Variables

        // Stored Previous Settings
        private bool
            previousMusic,
            previousFullscreen,
            previousCRT;

        // Text
        private Text
            optionsLabel;
        private int labelPadding = 30;

        // Checkboxes
        private Checkbox
            music,
            fullscreen,
            crtFilter;

        // Buttons
        private Button
            backSave, // Save Changes and Exit
            backDont; // Exit without Saving Changes

        private int backButtonPadding = 20;

        public OptionsState()
        {
            // Set Options
            cursorVisible = true;

            // Set Previous Options (For Exitting without Saving Changes)
            SetPreviousOptions();

            // --- Set Objects ---

            // Text
            optionsLabel = new Text(Global.arial, "Options", new Vector2(cam.Width / 2, labelPadding), Color.Black, 1.5f, true);

            // Checkboxes
            music = new Checkbox(new Point(40, 60), "Music");
            fullscreen = new Checkbox(new Point(40, 150), "Fullscreen");

            crtFilter = new Checkbox(new Point(620, 60), "CRT Filter");
            // crtFilter = new Checkbox(new Point(40, 320), "CRT Filter");

            UpdateSprites();

            // --- Set Buttons ---

            // Save Changes
            backSave = new Button("Save and Exit", Point.Zero, 0.77f);
            backSave.SetPosition(new Point(
                backButtonPadding, // X
                (cam.Height - backSave.Height) - backButtonPadding) // Y
                );

            // Exit without Saving Changes
            backDont = new Button("Exit w/o Saving", Point.Zero, 0.7f);
            backDont.SetPosition(new Point(
                (int)(backDont.Width * 1.5f) + (backButtonPadding * 2), // X
                (cam.Height - backDont.Height) - backButtonPadding) // Y
                );
        }

        public void SetPreviousOptions()
        {
            previousMusic = Global.musicEnabled; // Music

            previousFullscreen = Global.fullscreen; // Fullscreen Mode

            previousCRT = Global.crtFilter; // CRT Filter
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Interactive Options ---

                if (music.clicked)
                {
                    // Toggle Music
                    Global.musicEnabled = !Global.musicEnabled;
                    Global.musicToggled = true;

                    // Update Status
                    UpdateSprites();

                    // End Click Event
                    music.clicked = false;
                }

                if (fullscreen.clicked)
                {
                    // Toggle Fullscreen
                    Global.fullscreen = !Global.fullscreen;
                    Global.fullscreenChanged = true;

                    // Update Status
                    UpdateSprites();

                    // End Click Event
                    fullscreen.clicked = false;
                }

                if (crtFilter.clicked)
                {
                    // Toggle CRT Filter
                    Global.crtFilter = !Global.crtFilter;

                    // Update Status
                    UpdateSprites();

                    // End Click Event
                    crtFilter.clicked = false;
                }

                // Buttons
                if (backSave.clicked) SaveAndExit();
                if (backDont.clicked) ExitWithoutSaving();

                // --- Object Updates ---

                music.Update(gameTime, cursor, LeftClicked());
                fullscreen.Update(gameTime, cursor, LeftClicked());

                crtFilter.Update(gameTime, cursor, LeftClicked());

                backDont.Update(gameTime, cursor, LeftClicked());
                backSave.Update(gameTime, cursor, LeftClicked());
            }
        }

        /// <summary>
        /// Set the active status of each checkbox to their respective variables.
        /// </summary>
        public void UpdateSprites()
        {
            // Music
            music.active = Global.musicEnabled;

            // Fullscreen
            fullscreen.active = Global.fullscreen;

            // CRT Filter
            crtFilter.active = Global.crtFilter;
        }

        public override void ResetState()
        {
            SetPreviousOptions();

            base.ResetState();
        }

        public void SaveAndExit()
        {
            // --- Write to Settings.xml ---

            if (File.Exists("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/Settings.xml"))
            {
                XDocument settingsDoc = XDocument.Load("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/Settings.xml");

                settingsDoc.Descendants("Fullscreen").First().Value = Convert.ToString(Global.fullscreen);
                settingsDoc.Descendants("CRTFilter").First().Value = Convert.ToString(Global.crtFilter);
                settingsDoc.Descendants("Music").First().Value = Convert.ToString(Global.musicEnabled);

                settingsDoc.Save("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/Settings.xml", SaveOptions.None);

                Debug.Print("Saved to Settings.xml.");
            }
            else // If Settings.xml does not exist
            {
                Global.checkAndCreateSettings = true;
            }

            // --- Exit State ---

            GoToTitle();
        }

        public void ExitWithoutSaving()
        {
            // --- Settings ---

            // Alter Fullscreen
            if (Global.fullscreen != previousFullscreen) Global.fullscreenChanged = true;

            // Reset Options to their Previous State
            Global.musicEnabled = previousMusic;
            Global.fullscreen = previousFullscreen;
            Global.crtFilter = previousCRT;

            // --- Exit State ---

            GoToTitle();
        }

        public void GoToTitle()
        {
            // Update Checkboxes
            UpdateSprites();

            // --- Change State ---
            this.changeState = true;
            Global.currentState = Global.State.title;
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Global.titleColor);

            // Text
            optionsLabel.Draw(spriteBatch);

            // Interactive Options
            music.Draw(spriteBatch);
            fullscreen.Draw(spriteBatch);

            crtFilter.Draw(spriteBatch);

            backSave.Draw(spriteBatch);
            backDont.Draw(spriteBatch);
        }
    }
}
