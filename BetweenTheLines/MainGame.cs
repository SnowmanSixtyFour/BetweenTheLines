// --- Between the Lines ---

// Created by Snowman64.
// Credits can be found in-game or in CreditsState.cs.

// -------------------------

using System;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BetweenTheLines.Source;
using BetweenTheLines.Source.States;
using System.Diagnostics;

namespace BetweenTheLines
{
    public class MainGame : Game
    {
        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        public static GraphicsDeviceManager publicGraphics;
        public static GraphicsDevice publicGraphicsDevice;
        public static GameTime gameTime;

        private int
            windowedWidth, windowedHeight;

        private Main game;

        public MainGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            IsFixedTimeStep = false;
        }

        protected override void Initialize()
        {
            // Initialize variables
            publicGraphics = this.graphics;
            publicGraphicsDevice = this.GraphicsDevice;

            // Set window
            this.Window.Title = Global.windowName;
            this.Window.AllowUserResizing = true;
            this.Window.ClientSizeChanged += WindowSizeChanged;

            base.Initialize();
        }

        // When window is resized
        private void WindowSizeChanged(object sender, EventArgs e)
        {
            // Update game camera
            game.cam.SetDestRect();
        }

        private void ChangeWindowSize(int windowWidth, int windowHeight)
        {
            // Window graphics
            graphics.PreferredBackBufferWidth = windowWidth;
            graphics.PreferredBackBufferHeight = windowHeight;

            graphics.ApplyChanges();

            // Game camera
            game.cam.SetDestRect();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Load Content
            Assets.LoadContent(Content);

            // Set game state
            game = new Main();

            // Set CRT Shader
            Global.crt.Parameters["brightboost"].SetValue(0.92f);

            var texSize = new Vector2(Global.windowWidth, Global.windowHeight);
            Global.crt.Parameters["textureSize"]?.SetValue(texSize);
            Global.crt.Parameters["videoSize"]?.SetValue(texSize);
            var outSize = new Vector2(MainGame.publicGraphics.PreferredBackBufferWidth, MainGame.publicGraphics.PreferredBackBufferHeight);
            Global.crt.Parameters["outputSize"]?.SetValue(outSize);

            // Set Cam
            ChangeWindowSize(Global.windowWidth, Global.windowHeight);
        }

        // Update Global Variables
        private void UpdateGlobal()
        {
            // Set Window
            IsMouseVisible = Global.mouseVisible;

            // Set Global Variables
            Global.active = this.IsActive;

            // --- Fullscreen ---

            if (!graphics.IsFullScreen)
            {
                this.windowedWidth = this.GraphicsDevice.Viewport.Width;
                this.windowedHeight = this.GraphicsDevice.Viewport.Height;
            }

            // Update Variables when Fullscreen Toggled
            if (Global.fullscreenChanged) UpdateFullscreen();
        }

        /// <summary>
        /// Keeps the size of the window update, depending on fullscreen or windowed mode.
        /// </summary>
        public void UpdateFullscreen()
        {
            // Set Bool to False (so method only runs once)
            Global.fullscreenChanged = false;

            // Change Screen Size depending on Fullscreen Status
            if (Global.fullscreen) // Fullscreen Mode
            {
                // Set Window to Monitor Size
                graphics.PreferredBackBufferWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
                graphics.PreferredBackBufferHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;

                // Apply Changes before Fullscreen Status is Set
                // NOTE: This is to prevent any lag when the window is set to fullscreen.
                graphics.ApplyChanges();
            }
            if (!Global.fullscreen) // Windowed Mode
            {
                // Set Window to Previous Windowed Size
                graphics.PreferredBackBufferWidth = this.windowedWidth;
                graphics.PreferredBackBufferHeight = this.windowedHeight;
            }

            // Update Fullscreen Mode for Window
            this.graphics.IsFullScreen = Global.fullscreen;

            // Apply Changes to Window
            graphics.ApplyChanges();
        }

        protected override void Update(GameTime gameTime)
        {
            /*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            */

            // Quit Game on Global Trigger
            if (Global.quit) Exit();

            // Update game variables
            UpdateGlobal();

            // Update public game time
            MainGame.gameTime = gameTime;

            // Update game
            game.Update(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);

            // Draw Game whether renderInactive is true, if false then only while user is tabbed in
            if (this.IsActive
                || Global.renderInactive && !this.IsActive)
            {
                game.cam.Activate();

                // SpriteBatch
                spriteBatch.Begin(SpriteSortMode.Deferred,
                BlendState.AlphaBlend,
                SamplerState.PointClamp,
                DepthStencilState.None,
                RasterizerState.CullNone
                );

                // Draw Game
                game.Draw(spriteBatch);

                // End SpriteBatch
                spriteBatch.End();

                // Draw Game Camera
                game.cam.Draw(spriteBatch);
            }

            base.Draw(gameTime);
        }
    }
}
