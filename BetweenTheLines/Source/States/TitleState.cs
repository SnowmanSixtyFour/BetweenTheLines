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
        private StaticSprite
            BG,

            pickles, smokey;
        private int charSize = 300;
        private Color charColor = (Color.Gray);

        // Sprite Scroll Speeds
        private float
            bgScrollSpeed = 20.0f; // BG Speed

        private StaticSprite logo;
        private Point logoSize = new Point(364, 214);
        private int logoPadding = 20;

        // Text
        private Text
            gameJam,
            gameVersion,
            gameCredits,
            chapter;
        private int
            chapterOffsetY = 35;

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

            // BG
            BG = new StaticSprite(Assets.titleBG, new Rectangle(0, 0, Global.windowWidth, Global.windowHeight), Color.DarkGray, true);

            pickles = new StaticSprite(Dialog.picklesRegular, new Rectangle(Global.windowWidth - (charSize / 2), 0, charSize, Global.windowHeight), charColor);
            smokey = new StaticSprite(Dialog.smokeyRegular, new Rectangle(-charSize / 2, 0, charSize, Global.windowHeight), charColor);

            // Logo
            logo = new StaticSprite(Assets.logo, new Rectangle(new Point((cam.Width / 2) - (logoSize.X / 2), logoPadding), logoSize), Color.White);

            // Text
            gameJam = new Text(Assets.arial, "Mystery Game Jam 2026 Edition", new Vector2((logo.GetDestRect().X + 5), (logo.GetDestRect().Bottom + 15)), Color.White, 1.0f, false);

            gameVersion = new Text(Assets.arial, (Global.gameVersion), new Vector2(10, (cam.Height - 30)), Color.White, 1.0f, false);

            gameCredits = new Text(Assets.arial, "(c) 2026 Snowman64", new Vector2((cam.Width - 230), (cam.Height - 30)), Color.White, 1.0f, false);

            chapter = new Text(Assets.arial, "Chapter 1", Vector2.Zero, Color.White, 1.0f, false);

            // Buttons
            startButton = new Button("Start", new Point(xPadding / 2, cam.Height - yPadding));
            optionsButton = new Button("Options", new Point(xPadding + 80, cam.Height - yPadding));
            creditsButton = new Button("Credits", new Point(cam.Width - (xPadding * 2) + 20, cam.Height - yPadding));
            exitButton = new Button("Quit", new Point(cam.Width - xPadding, cam.Height - yPadding));

            // Set Position of Objects that have not already been set
            chapter.setPosition(new Vector2((startButton.X + chapter.getText().Length), startButton.Y - chapterOffsetY));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Scrolling Sprites ---

                // Update Offsets by Speed
                float bgOffset = bgScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Set Sprite Positions to Offset Values
                BG.offset += bgOffset;

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
            BG.Draw(spriteBatch);

            pickles.Draw(spriteBatch);
            smokey.Draw(spriteBatch);

            logo.Draw(spriteBatch);
            if (Global.gameJam) gameJam.Draw(spriteBatch);

            gameVersion.Draw(spriteBatch);
            gameCredits.Draw(spriteBatch);

            if (cursor.HoveringOver(startButton.bounds)) chapter.Draw(spriteBatch);

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
            ChangeSong(OST.intro);

            // Switch State
            this.changeState = true;
            Global.currentState = Global.State.story;
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
