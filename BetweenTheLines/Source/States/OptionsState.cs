using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            fullscreen;

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
            fullscreen = new Checkbox(new Point(40, 120), "Fullscreen");

            SetCheckboxStatus();

            // Buttons
            backButton = new Button("Back", Point.Zero);
            backButton.SetPosition(new Point(
                backButtonPadding, // X
                (cam.Height - backButton.Height) - backButtonPadding) // Y
                );
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Checkbox Clicks ---

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

                // --- Button Highlights ---

                cursor.Highlight(backButton);

                // --- Button Clicks ---

                // Back
                if (cursor.HoveringOver(backButton.bounds) && LeftClicked()) GoToTitle();

                // --- Object Updates ---

                music.Update(gameTime, cursor, LeftClicked());
                fullscreen.Update(gameTime, cursor, LeftClicked());
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
        }

        public override void ResetState()
        {
            base.ResetState();
        }

        public void GoToTitle()
        {
            this.changeState = true;
            Global.currentState = Global.State.title;
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Global.titleColor);

            // Text
            optionsLabel.Draw(spriteBatch);

            // Checkboxes
            music.Draw(spriteBatch);
            fullscreen.Draw(spriteBatch);

            // Buttons
            backButton.Draw(spriteBatch);
        }
    }
}
