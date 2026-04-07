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
        private State currentState;

        private IntroState introState;
        private TitleState titleState;
        private LevelState levelState;

        public Main()
        {
            // Set Main
            cursorVisible = false;
            canPause = false;

            // Set States
            introState = new IntroState(); // Intro
            titleState = new TitleState(); // Title
            levelState = new LevelState(); // Level

            // Set Current State
            currentState = introState; // NOTE: Always start on intro! Will be different for debug purposes.
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- State Behaviour ---

            // Intro
            if (Global.currentState == Global.State.intro)
            {
                // Go to Title when Intro Finishes
                if (introState.introFinished) Global.currentState = Global.State.title;
            }

            // --- Current State ---

            // Set State
            if (Global.currentState == Global.State.intro) currentState = introState; // Intro
            if (Global.currentState == Global.State.title) currentState = titleState; // Title
            if (Global.currentState == Global.State.level) currentState = levelState; // Level

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
