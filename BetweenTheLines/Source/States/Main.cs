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
        private StoryState story;
        private DebateState debate;
        private CreditsState credits;

        // CRT Filter
        public StaticSprite
            rescanLine;
        private int
            rescanLineHeight = 100,
            rescanLineIncrement = 1;

        public Main()
        {
            // Set Main
            cursorVisible = false;
            canPause = false;

            // Set States
            intro = new IntroState(); // Intro
            title = new TitleState(); // Title
            options = new OptionsState(); // Options
            story = new StoryState(); // Story
            debate = new DebateState(); // Debate
            credits = new CreditsState(); // Credits

            currentState = new State(); // Current State (Blank)

            // CRT Filter
            rescanLine = new StaticSprite(null, new Rectangle(0, 0, cam.Width, -rescanLineHeight), (Color.Black * 0.15f));
        }

        public override void OnUpdate(GameTime gameTime)
        {
            // --- Current State ---

            // Set State
            if (Global.currentState == Global.State.intro) currentState = intro; // Intro
            if (Global.currentState == Global.State.title) currentState = title; // Title
            if (Global.currentState == Global.State.options) currentState = options; // Options
            if (Global.currentState == Global.State.story) currentState = story; // Story
            if (Global.currentState == Global.State.debate) currentState = debate; // Debate
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

            // CRT Filter
            if (Global.crtFilter)
            {
                // Rescan Line
                if (Global.rescanLineVisible)
                {
                    // When Rescan Line Reaches Bottom of Screen
                    if (rescanLine.GetY() > (cam.Height + rescanLineHeight)) rescanLine.SetY(-rescanLineHeight);

                    // Add to Rescan Line Y
                    else rescanLine.SetY(rescanLine.GetY() + rescanLineIncrement);
                }
            }

            // While Game Paused
            if (Global.paused)
            {
                // During Gameplay (Story, Debate)
                if (Global.currentState == Global.State.story
                    || Global.currentState == Global.State.debate)
                {
                    // Check if R is Pressed (Return to Menu)
                    if (KeyPress(Keys.R))
                    {
                        ChangeSong(OST.title);

                        // Switch State
                        this.changeState = true;
                        Global.currentState = Global.State.title;
                    }
                }
            }
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw Current State
            currentState.Draw(spriteBatch);

            // CRT Filter
            if (Global.crtFilter)
            {
                if (Global.rescanLineVisible) rescanLine.Draw(spriteBatch); // Rescan Line
            }
        }
    }
}
