// --- Between the Lines ---

// Created by Snowman64.
// Credits can be found in-game or in CreditsState.cs.

// -------------------------

using BetweenTheLines.Source;
using BetweenTheLines.Source.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Xml.Linq;
using Windows.System;

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

            // Load Settings.xml File
            if (File.Exists("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/Settings.xml"))
            {
                Debug.Print("Loading Settings.xml.");

                // Load File
                XDocument settingsDoc = XDocument.Load("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/Settings.xml");

                // Set Settings to File Properties
                Global.fullscreen = Convert.ToBoolean(settingsDoc.Descendants("Fullscreen").First().Value);
                Global.crtFilter = Convert.ToBoolean(settingsDoc.Descendants("CRTFilter").First().Value);
                Global.musicEnabled = Convert.ToBoolean(settingsDoc.Descendants("Music").First().Value);

                Debug.Print("Settings.xml loaded!");
            }
            else // If Settings.xml does not exist
            {
                // Create Settings File
                CheckAndCreateSettingsFile();
            }

            // Set game state
            game = new Main();

            // Set CRT Shader
            Assets.crt.Parameters["brightboost"].SetValue(0.92f);

            var texSize = new Vector2(Global.windowWidth, Global.windowHeight);
            Assets.crt.Parameters["textureSize"]?.SetValue(texSize);
            Assets.crt.Parameters["videoSize"]?.SetValue(texSize);
            var outSize = new Vector2(MainGame.publicGraphics.PreferredBackBufferWidth, MainGame.publicGraphics.PreferredBackBufferHeight);
            Assets.crt.Parameters["outputSize"]?.SetValue(outSize);

            // Set Cam
            ChangeWindowSize(Global.windowWidth, Global.windowHeight);

            // Update Window Size

            // Set Windowed Size
            this.windowedWidth = Global.windowWidth;
            this.windowedHeight = Global.windowHeight;
            
            UpdateFullscreen(); // Fullscreen Mode
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

            // Toggle Music
            Global.musicToggled = true;
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

        private void CheckAndCreateSettingsFile()
        {
            Debug.Print("Settings.xml does not exist in the current directory. Creating a new file...");

            if (Directory.Exists("C:/Users/" + Environment.UserName + "/Documents/"))
            {
                if (Directory.Exists("C:/Users/" + Environment.UserName + "/Documents/My Games/"))
                {
                    if (Directory.Exists("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/"))
                    {
                        Debug.Print("Directory has been found!"); //Write to console
                    }
                    else // If Game Directory does not exist
                    {
                        Directory.CreateDirectory("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/"); //Create the directory
                        Debug.Print("Creating new directory for C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/"); //Write to console
                    }
                }
                else // If "My Games" does not exist
                {
                    Directory.CreateDirectory("C:/Users/" + Environment.UserName + "/Documents/My Games/"); //Create the directory
                    Debug.Print("Creating new directory for C:/Users/" + Environment.UserName + "/Documents/My Games/"); //Write to console
                }
            }
            else // If Documents does not exist
            {
                Directory.CreateDirectory("C:/Users/" + Environment.UserName + "/Documents/"); //Create the directory
                Debug.Print("Creating new directory for C:/Users/" + Environment.UserName + "/Documents/"); //Write to console
            }

            // If File Already Exists
            if (Directory.Exists("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/"))
            {
                CreateSettingsFile();
            }
            else
            {
                CheckAndCreateSettingsFile();
            }
        }

        private void CreateSettingsFile()
        {
            // Create Settings.xml File
            var settingsDoc = new XDocument(new XElement("Settings",
                new XElement("Fullscreen", new XElement("Value", graphics.IsFullScreen)),
                new XElement("CRTFilter", new XElement("Value", Global.crtFilter)),
                new XElement("Music", new XElement("Value", Global.musicEnabled))
                ));
            
            // Save File
            settingsDoc.Save("C:/Users/" + Environment.UserName + "/Documents/My Games/Between the Lines/Settings.xml", SaveOptions.None);

            Debug.Print("Created Settings.xml.");
        }

        protected override void Update(GameTime gameTime)
        {
            /*
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            */

            // When Create Settings Called
            if (Global.checkAndCreateSettings)
            {
                // Run Create Settings.xml File
                CheckAndCreateSettingsFile();

                // End Event
                Global.checkAndCreateSettings = false;
            }

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
