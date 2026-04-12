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
using Microsoft.Xna.Framework.Media;

namespace BetweenTheLines.Source.States
{
    internal class CreditsState : State
    {
        // Variables

        // Sprites
        private StaticSprite logo;
        private Point logoSize = new Point(364, 214);

        // Buttons
        private Button backButton; // Back
        private int backButtonPadding = 20;

        // Text
        private Text creditsText; // Credits
        
        // Position
        private Vector2 creditsPosition;
        private int
            logoYStart = 700,
            creditsYStart = 900,
            creditsYEnd = -200;

        private int amountToMove = 1;

        // String
        private String creditsString = """
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
            // Set Variables

            // --- Sprites ---

            // Game Logo
            logo = new StaticSprite(Global.logo, new Rectangle(new Point((cam.Width / 2) - (logoSize.X / 2), logoYStart - logoSize.Y), logoSize), Color.White);

            // --- Credits ---

            // Position
            creditsPosition = new Vector2(cam.Width / 2, creditsYStart);

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
            // When Game is Unpaused
            if (!Global.paused)
            {
                // If Credits are on-screen
                if (creditsPosition.Y > creditsYEnd)
                {
                    // Move Y Position Up
                    logo.SetDestRect(new Rectangle(logo.GetDestRect().X, logo.GetDestRect().Y - amountToMove, logo.GetDestRect().Width, logo.GetDestRect().Height));
                    creditsPosition.Y -= amountToMove;

                    // Keep Credits Updated
                    creditsText.setPosition(creditsPosition);
                }

                // When Credits are off-screen
                else
                {
                    if (!backButton.clicked) GoToTitle();
                }

                // --- Object Updates ---

                // If Viewing Credits on Title
                if (Global.viewingCreditsFromTitle)
                {
                    // --- State ---
                    cursorVisible = true;

                    // --- Cursor ---

                    // Make Translucent When Not Hovering over Back
                    if (cursor.HoveringOver(backButton.bounds)) cursor.setTransparency(255);
                    else cursor.setTransparency(128);

                    // --- Button Clicks ---

                    // Back
                    if (backButton.clicked) GoToTitle();

                    // --- Button Updates ---

                    // Back
                    backButton.Update(gameTime, cursor, LeftClicked());
                }

                // If Viewing Credits after Beaten Game
                else
                {
                    // --- State ---
                    cursorVisible = false;
                }
            }
        }

        public override void ResetState()
        {
            // Play Title Music
            if (MediaPlayer.State == MediaState.Stopped) // If Music is Not Playing
            {
                MediaPlayer.Play(OST.title);
            }

            // Reset Positions
            
            // Logo
            logo.SetDestRect(new Rectangle
                (logo.GetDestRect().X,
                (logoYStart - logoSize.Y), // Y
                logo.GetDestRect().Width,
                logo.GetDestRect().Height));

            creditsPosition.Y = creditsYStart; // Credits

            base.ResetState();
        }

        public void GoToTitle()
        {
            // Reset Credits Version
            Global.viewingCreditsFromTitle = false;

            // Switch State
            this.changeState = true;
            Global.currentState = Global.State.title;
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Variables

            // Sprites
            logo.Draw(spriteBatch);

            // Text
            creditsText.Draw(spriteBatch);

            // Buttons
            if (Global.viewingCreditsFromTitle) backButton.Draw(spriteBatch);
        }
    }
}
