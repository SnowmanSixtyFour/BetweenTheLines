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
        private Button backButton; // Back
        private int backButtonPadding = 20;

        public OptionsState()
        {
            // Set Options
            cursorVisible = true;

            // --- Set Objects ---

            // Text
            optionsLabel = new Text(Global.arial, "Options", new Vector2(cam.Width / 2, labelPadding), Color.Black, 1.5f, true);

            // Checkboxes
            music = new Checkbox(new Point(40, 60), "Music");
            fullscreen = new Checkbox(new Point(40, 130), "Fullscreen");

            crtFilter = new Checkbox(new Point(620, 60), "CRT Filter");
            // crtFilter = new Checkbox(new Point(40, 320), "CRT Filter");

            SetCheckboxStatus();

            // Buttons
            backButton = new Button("Save & Exit", Point.Zero);
            backButton.SetPosition(new Point(
                backButtonPadding, // X
                (cam.Height - backButton.Height) - backButtonPadding) // Y
                );
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
                    SetCheckboxStatus();

                    // End Click Event
                    music.clicked = false;
                }

                if (fullscreen.clicked)
                {
                    // Toggle Fullscreen
                    Global.fullscreen = !Global.fullscreen;
                    Global.fullscreenChanged = true;

                    // Update Status
                    SetCheckboxStatus();

                    // End Click Event
                    fullscreen.clicked = false;
                }

                if (crtFilter.clicked)
                {
                    // Toggle CRT Filter
                    Global.crtFilter = !Global.crtFilter;

                    // Update Status
                    SetCheckboxStatus();

                    // End Click Event
                    crtFilter.clicked = false;
                }    

                // --- Button Highlights ---

                cursor.Highlight(backButton);

                // --- Button Clicks ---

                // Back
                if (cursor.HoveringOver(backButton.bounds) && LeftClicked()) GoToTitle();

                // --- Object Updates ---

                music.Update(gameTime, cursor, LeftClicked());
                fullscreen.Update(gameTime, cursor, LeftClicked());

                crtFilter.Update(gameTime, cursor, LeftClicked());
            }
        }

        /// <summary>
        /// Set the active status of each checkbox to their respective variables.
        /// </summary>
        public void SetCheckboxStatus()
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
            base.ResetState();
        }

        public void GoToTitle()
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

            backButton.Draw(spriteBatch);
        }
    }
}
