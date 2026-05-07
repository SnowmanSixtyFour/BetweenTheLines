// The intro animation when the game is first opened.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source.Graphics;
using Microsoft.Xna.Framework.Media;
using BetweenTheLines.Source.Objects;

namespace BetweenTheLines.Source.States
{
    internal class IntroState : State
    {
        // Timer
        protected float
            introTime = 0f,

            timerEnd = 22f,
            increment = 0.1f,

            // Timer Events
            logoAppear = 4f,
            logoDisappear = 18f;

        // Graphics
        protected StaticSprite snowman64;
        protected int
            logoWidth = 81, logoHeight = 8,
            logoResize = 8,
            logoYOffset = 80;

        protected StaticSprite gameJam;
        protected int
            jamWidth = 125, jamHeight = 72,
            jamResize = 2,
            jamYOffset = 140;

        protected int opacity = 0, fadeTime = 12;

        protected Text disclaimer;

        public IntroState()
        {
            // --- State ---

            // Set Intro
            cursorVisible = false;

            // --- Audio ---

            // Play Title Music
            MediaPlayer.Play(OST.title);

            // --- Graphics ---

            // Set Logos
            snowman64 = new StaticSprite(Assets.snowman64, new Rectangle(
                ((cam.Width / 2) - (logoWidth * (logoResize / 2))), // X
                ((cam.Height / 2) - (logoHeight * logoResize) - logoYOffset), // Y
                (logoWidth * logoResize), // Width
                (logoHeight * logoResize)), // Height
                Color.White);

            gameJam = new StaticSprite(Assets.gameJamLogo, new Rectangle(
                ((cam.Width / 2) - (jamWidth * (jamResize / 2))), // X
                ((cam.Height / 2) - (jamHeight * jamResize) + jamYOffset), // Y
                (jamWidth * jamResize), // Width
                (jamHeight * jamResize)), // Height
                Color.White);

            // Set Text
            disclaimer = new Text(Assets.arial, "This game contains foul language, slight blood and gore, and other mature themes.", new Vector2(17, Global.windowHeight - 35), Color.White, 0.93f, false);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Keep Opacity of Graphics Updated (Fadein and Fadeout)
            Color opacityColor = new Color(opacity, opacity, opacity, opacity);
            snowman64.SetColor(opacityColor);
            gameJam.SetColor(opacityColor);
            disclaimer.setColor(opacityColor);

            // Run Timer for Scene
            if (introTime < timerEnd) introTime += increment;
            
            // When Intro is Finished
            else
            {
                // Switch State to Title
                Global.currentState = Global.State.title;
            }

            // Fade In
            if (introTime > logoAppear && introTime < logoDisappear)
            {
                if (opacity < 255)
                {
                    opacity += fadeTime;
                }
                else opacity = 255;
            }

            // Fade Out
            if (introTime > logoDisappear)
            {
                if (opacity > 0)
                {
                    opacity -= fadeTime;
                }
                else opacity = 0;
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            graphicsDevice.Clear(Color.Black);

            snowman64.Draw(spriteBatch);
            gameJam.Draw(spriteBatch);

            disclaimer.Draw(spriteBatch);
        }
    }
}
