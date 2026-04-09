// The main state of the game.

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;

namespace BetweenTheLines.Source.States
{
    internal class Main : State
    {
        private State currentState; // Current State of Game

        // States
        private IntroState intro;
        private TitleState title;
        private OptionsState options;
        private LevelState level;
        private CreditsState credits;

        public Main()
        {
            // Set Main
            cursorVisible = false;
            canPause = false;

            // Set States
            intro = new IntroState(); // Intro
            title = new TitleState(); // Title
            options = new OptionsState(); // Options
            level = new LevelState(); // Level
            credits = new CreditsState(); // Credits
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Current State ---

            // Set State
            if (Global.currentState == Global.State.intro) currentState = intro; // Intro
            if (Global.currentState == Global.State.title) currentState = title; // Title
            if (Global.currentState == Global.State.options) currentState = options; // Options
            if (Global.currentState == Global.State.level) currentState = level; // Level
            if (Global.currentState == Global.State.credits) currentState = credits; // Credits

            // When State Switched
            if (currentState.changeState)
            {
                // Reset the Current State
                currentState.ResetState();

                // After State is Switched
                currentState.changeState = false;
            }

            // Update State
            currentState.Update(gameTime);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Current State
            currentState.Draw(spriteBatch);
        }
    }
}
