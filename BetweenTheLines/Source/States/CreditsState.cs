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

        private StaticSprite
            picklesIrl, angelIrl, smokeyIrl;

        // Buttons
        private Button backButton; // Back
        private int backButtonPadding = 20;

        // Text
        private Text
            versionNum, // Version Number under Logo
            creditsText, // Credits
            pickles, angel, smokey; // Cats
        
        // Position
        private Vector2 creditsPosition;
        private int
            verNumOffset = 25, // Version Number Vertical Offset from Logo

            // Y Positions
            logoYStart = 700,
            creditsXPadding = 375,
            creditsYStart = 800,
            creditsYEnd = -1550, // Credits End (Go back to Title)

            // Cat Positions
            picklesAngelPos = 1650, // Pickles and Angel Y Positions
            angelOffset = 350, // Angel X Offset
            smokeyPosX = 160, smokeyPosY = 2000, // Smokey Position

            catPicOffset = 50; // Picture Y Offset
        private Point catPicSize = new Point(250, 250); // Picture Size

        private int amountToMove = 1;

        // String - Try to keep credits in-bounds of about here -> ||
        private String creditsString = """
            Created by Snowman64

            Developed from April 7, 2026 - TBA
            Made for the Mystery Game Jam 2026 on itch.io.
            https://itch.io/jam/mystery-game-jam-2026



            Special Thanks

            CRT Scanline Shaders (Public Domain) - Timothy Lottes



            Copyright (c) 2026 Snowman64

            Licensed under the GNU General Public License v3.0.
            https://www.gnu.org/licenses/gpl-3.0.html



            Freesound.org Sounds

            Open button 2 - kickhat
            https://freesound.org/people/kickhat/sounds/264447/

            Typewriter Button SLIDE 1A.wav - mincedbeats
            https://freesound.org/people/mincedbeats/sounds/433600/

            paper5.wav - florian_reinke
            https://freesound.org/people/florian_reinke/sounds/63514/

            footsteps.wav - TampaJoey
            https://freesound.org/people/TampaJoey/sounds/588501/

            All Freesound.org sounds used are licensed
            under the Creative Commons 0 License.
            https://creativecommons.org/publicdomain/zero/1.0/
            """;

        public CreditsState()
        {
            // Set Variables

            // --- Sprites ---

            // Game Logo
            logo = new StaticSprite(Assets.logo, new Rectangle(new Point((cam.Width / 2) - (logoSize.X / 2), logoYStart - logoSize.Y), logoSize), Color.White);
            versionNum = new Text(Assets.arial, Global.gameVersion, new Vector2(logo.GetDestRect().Center.X, logo.GetDestRect().Bottom + verNumOffset), Color.White, 1.0f, true);

            // --- Credits ---

            // Position
            creditsPosition = new Vector2(cam.Width / 2 - creditsXPadding, creditsYStart);

            // Set Text
            creditsText = new Text(Assets.arial, creditsString, creditsPosition, Color.White, 1.0f, false);

            pickles = new Text(Assets.arial, "           Pickles", Vector2.Zero, Color.White, 1.0f, false);
            angel = new Text(Assets.arial, "              Angel", Vector2.Zero, Color.White, 1.0f, false);
            smokey = new Text(Assets.arial, "  Dedicated to Smokey", Vector2.Zero, Color.White, 1.0f, false);

            // Cats
            picklesIrl = new StaticSprite(Assets.picklesIrl, Rectangle.Empty, Color.White);
            angelIrl = new StaticSprite(Assets.angelIrl, Rectangle.Empty, Color.White);
            smokeyIrl = new StaticSprite(Assets.smokeyIrl, Rectangle.Empty, Color.White);

            // Set Buttons
            backButton = new Button("Back", Point.Zero);
            backButton.SetPosition(new Point(
                (cam.Width - backButton.Width) - backButtonPadding, // X
                (cam.Height - backButton.Height) - backButtonPadding) // Y
                );

            // Set Object Positions
            ResetState();
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

                    pickles.setPosition(new Vector2(pickles.getPosition().X, pickles.getPosition().Y - amountToMove));
                    picklesIrl.SetY(picklesIrl.GetY() - amountToMove);

                    angel.setPosition(new Vector2(angel.getPosition().X, angel.getPosition().Y - amountToMove));
                    angelIrl.SetY(angelIrl.GetY() - amountToMove);

                    smokey.setPosition(new Vector2(smokey.getPosition().X, smokey.getPosition().Y - amountToMove));
                    smokeyIrl.SetY(smokeyIrl.GetY() - amountToMove);

                    // Keep Text Updated
                    versionNum.setPosition(new Vector2(logo.GetDestRect().Center.X, logo.GetDestRect().Bottom + verNumOffset));
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

            versionNum.setPosition(new Vector2(logo.GetDestRect().Center.X, logo.GetDestRect().Bottom + verNumOffset)); // Version Number

            creditsPosition.Y = creditsYStart; // Credits

            // Cats
            pickles.setPosition(new Vector2(creditsPosition.X, picklesAngelPos));
            angel.setPosition(new Vector2(creditsPosition.X + angelOffset, picklesAngelPos));
            smokey.setPosition(new Vector2(creditsPosition.X + smokeyPosX, smokeyPosY));

            // Cats
            picklesIrl.SetDestRect(new Rectangle((int)creditsPosition.X, picklesAngelPos + catPicOffset, catPicSize.X, catPicSize.Y));
            angelIrl.SetDestRect(new Rectangle((int)creditsPosition.X + angelOffset, picklesAngelPos + catPicOffset, catPicSize.X, catPicSize.Y));
            smokeyIrl.SetDestRect(new Rectangle((int)creditsPosition.X + smokeyPosX, smokeyPosY + catPicOffset, catPicSize.X, catPicSize.Y));

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

            picklesIrl.Draw(spriteBatch);
            angelIrl.Draw(spriteBatch);
            smokeyIrl.Draw(spriteBatch);

            // Text
            versionNum.Draw(spriteBatch);
            creditsText.Draw(spriteBatch);

            pickles.Draw(spriteBatch);
            angel.Draw(spriteBatch);
            smokey.Draw(spriteBatch);

            // Buttons
            if (Global.viewingCreditsFromTitle) backButton.Draw(spriteBatch);
        }
    }
}
