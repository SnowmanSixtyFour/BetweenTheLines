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
                // --- Button Highlights ---

                cursor.Highlight(backButton);

                // --- Button Clicks ---

                // Back
                if (cursor.HoveringOver(backButton.bounds) && LeftClicked()) GoToTitle();
            }
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

            // Buttons
            backButton.Draw(spriteBatch);
        }
    }
}
