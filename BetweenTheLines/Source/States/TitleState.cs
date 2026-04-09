// The intro animation when the game is first opened.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Objects.GUI;

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
        private Button creditsButton;
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
            creditsButton = new Button("Credits", new Point(cam.Width - xPadding, cam.Height - yPadding));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Button Clicks ---

                // Credits
                if (cursor.HoveringOver(creditsButton.rect) && LeftClicked()) GoToCredits();
            }
        }

        public override void ResetState()
        {
            base.ResetState();
        }

        public void GoToCredits()
        {
            this.changeState = true;
            Global.currentState = Global.State.credits;
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Background
            graphicsDevice.Clear(Color.LightGray);

            // Sprites
            logo.Draw(spriteBatch);
            gameVersion.Draw(spriteBatch);
            gameCredits.Draw(spriteBatch);

            // Buttons
            creditsButton.Draw(spriteBatch);
        }
    }
}
