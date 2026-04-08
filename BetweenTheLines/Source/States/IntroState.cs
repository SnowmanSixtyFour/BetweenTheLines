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
            logoWidth = 78, logoHeight = 8,
            logoResize = 8,
            opacity = 0, fadeTime = 12;

        public IntroState()
        {
            // Set Intro
            cursorVisible = false;
            canPause = false;

            // Set Logo
            snowman64 = new StaticSprite(Global.snowman64, new Rectangle(
                ((cam.Width / 2) - (logoWidth * (logoResize / 2))),
                ((cam.Height / 2) - (logoHeight * logoResize)),
                (logoWidth * logoResize),
                (logoHeight * logoResize)),
                Color.White);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // Keep Logo Variables Update
            snowman64.SetColor(new Color(opacity, opacity, opacity, opacity));

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
        }
    }
}
