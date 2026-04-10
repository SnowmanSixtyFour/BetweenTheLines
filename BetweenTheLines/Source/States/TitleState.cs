// The intro animation when the game is first opened.

using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Objects.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetweenTheLines.Source.States
{
    internal class TitleState : State
    {
        // Variables

        // Sprites
        private StaticSprite logo;
        private Point logoSize = new Point(364, 214);
        private int logoPadding = 20;

        // Text
        private Text
            gameVersion,
            gameCredits;

        // Buttons
        private Button
            startButton,
            optionsButton,
            creditsButton,
            exitButton;
        private int
            xPadding = 196,
            yPadding = 144;

        public TitleState()
        {
            // Set Title
            cursorVisible = true;

            // --- Graphics ---

            // Logo
            logo = new StaticSprite(Global.logo, new Rectangle(new Point((cam.Width / 2) - (logoSize.X / 2), logoPadding), logoSize), Color.White);

            // Text
            gameVersion = new Text(Global.arial, (Global.gameVersion), new Vector2(10, (cam.Height - 30)), Color.Black, 1.0f, false);

            gameCredits = new Text(Global.arial, "2026 Snowman64", new Vector2((cam.Width - 195), (cam.Height - 30)), Color.Black, 1.0f, false);

            // Buttons
            startButton = new Button("Start", new Point(xPadding / 2, cam.Height - yPadding));
            optionsButton = new Button("Options", new Point(xPadding + 80, cam.Height - yPadding));
            creditsButton = new Button("Credits", new Point(cam.Width - (xPadding * 2) + 20, cam.Height - yPadding));
            exitButton = new Button("Quit", new Point(cam.Width - xPadding, cam.Height - yPadding));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Button Clicks ---

                // Start
                if (startButton.clicked) GoToLevel();

                // Options
                if (optionsButton.clicked) GoToOptions();

                // Credits
                if (creditsButton.clicked) GoToCredits();

                // Exit
                if (exitButton.clicked) Global.quit = true;

                // --- Object Updates ---

                startButton.Update(gameTime, cursor, LeftClicked());
                optionsButton.Update(gameTime, cursor, LeftClicked());
                creditsButton.Update(gameTime, cursor, LeftClicked());
                exitButton.Update(gameTime, cursor, LeftClicked());
            }
        }

        public override void ResetState()
        {
            base.ResetState();
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Background
            graphicsDevice.Clear(Global.titleColor);

            // Sprites
            logo.Draw(spriteBatch);
            gameVersion.Draw(spriteBatch);
            gameCredits.Draw(spriteBatch);

            // Buttons
            startButton.Draw(spriteBatch);
            optionsButton.Draw(spriteBatch);
            creditsButton.Draw(spriteBatch);
            exitButton.Draw(spriteBatch);
        }

        // State Switches

        public void GoToLevel()
        {
            // Change Music
            StopSong();
            MediaPlayer.Play(OST.intro); // Intro Music

            // Switch State
            this.changeState = true;
            Global.currentState = Global.State.level;
        }

        public void GoToOptions()
        {
            this.changeState = true;
            Global.currentState = Global.State.options;
        }

        public void GoToCredits()
        {
            // Set Credits to Title Version
            Global.viewingCreditsFromTitle = true;

            // Switch State
            this.changeState = true;
            Global.currentState = Global.State.credits;
        }
    }
}
