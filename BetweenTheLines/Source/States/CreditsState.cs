// Display credits for the game.

// Full credits can be found in-game, or via the creditsString variable below.

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
    internal class CreditsState : State
    {
        // Variables
        
        // Buttons
        private Button backButton; // Back
        private int backButtonPadding = 20;

        // Text
        private Text creditsText; // Credits
        
        // Position
        private Vector2 creditsPosition;
        private int
            yStart = 700,
            yEnd = -200;

        // String
        private String creditsString = """
            Between the Lines

            Created by Snowman64

            Developed from April 7, 2026 - TBA
            Made for the Mystery Game Jam 2026 on itch.io.
            https://itch.io/jam/mystery-game-jam-2026

            Special Thanks:
            CRT Scanline Shaders (Public Domain) - Timothy Lottes

            Freesound.org Sounds

            Typewriter Button SLIDE 1A.wav - mincedbeats
            https://freesound.org/people/mincedbeats/sounds/433600/
            """;

        public CreditsState()
        {
            // Set State
            cursorVisible = true;

            // Set Variables

            // Position
            creditsPosition = new Vector2(cam.Width / 2, yStart);

            // Set Text
            creditsText = new Text(Global.arial, creditsString, creditsPosition, Color.White, 1.0f, true);

            // Set Buttons
            backButton = new Button("Back", Point.Zero);
            backButton.SetPosition(new Point(
                (cam.Width - backButton.Width) - backButtonPadding, // X
                (cam.Height - backButton.Height) - backButtonPadding) // Y
                );
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // If Credits are on-screen
                if (creditsPosition.Y > yEnd)
                {
                    // Move Y Position Up
                    creditsPosition.Y--;

                    // Keep Credits Updated
                    creditsText.setPosition(creditsPosition);
                }

                // When Credits are off-screen
                else
                {
                    GoToTitle();
                }

                // --- Button Highlights ---

                cursor.Highlight(backButton);

                // --- Button Clicks ---

                // Back
                if (cursor.HoveringOver(backButton.bounds) && LeftClicked()) GoToTitle();
            }
        }

        public override void ResetState()
        {
            // Reset Credits Position
            creditsPosition.Y = yStart;

            base.ResetState();
        }

        public void GoToTitle()
        {
            this.changeState = true;
            Global.currentState = Global.State.title;
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Variables

            // Text
            creditsText.Draw(spriteBatch);

            // Buttons
            backButton.Draw(spriteBatch);
        }
    }
}
