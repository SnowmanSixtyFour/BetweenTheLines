// The template class for a state in the game.
// A state is a different scene.

/*
 * Templates:
 * 
 * To Update:
 * public override void Update(GameTime gameTime)
 * 
 * To Draw:
 * public override void OnDraw(SpriteBatch spriteBatch)
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects.GUI;
using Microsoft.Xna.Framework.Media;
using BetweenTheLines.Source.Objects.Level;

namespace BetweenTheLines.Source.States
{
    internal class State
    {
        // Public variables from MainGame
        public GraphicsDeviceManager graphics = MainGame.publicGraphics;
        public GraphicsDevice graphicsDevice = MainGame.publicGraphicsDevice;

        // State variables
        public KeyboardState keyboard, previousKeyboard;
        public MouseState mouse, previousMouse;
        public Vector2 mouseDelta;
        protected float MAXDELTA = Global.mouseDeltaMax;

        // Camera
        public int screenWidth, screenHeight;
        public Camera cam;

        // Cursor
        public Cursor cursor;
        public bool cursorVisible = false;

        // Pausing
        public bool canPause = true;
        private bool justPaused = false;

        private StaticSprite pauseOverlay;
        private Text pauseText, pauseTooltip;
        private int pauseTextPadding = 30;

        // Pause Strings
        private String
            pauseTooltipMenu = "Press ESCAPE to resume focus.",
            pauseTooltipGame = "Press ESCAPE to resume focus.\n\nR to exit to main menu.";

        // State Changed
        public bool changeState = false;

        // Audio
        public bool musicLoop = true;

        // CRT Filter
        private StaticSprite crtVignette;

        public State()
        {
            // --- Set State ---

            // Audio
            MediaPlayer.IsRepeating = musicLoop;

            // Camera
            cam = new Camera(this.graphicsDevice, Global.windowWidth, Global.windowHeight);

            // Cursor
            cursor = new Cursor(Global.cursorSize);

            // Center Cursor
            cursor.X = cam.Width / 2;
            cursor.Y = cam.Height / 2;

            // --- Set Sprites ---

            // Pause Overlay

            // Background
            pauseOverlay = new StaticSprite(Assets.pauseOverlay, new Rectangle(0, 0, cam.Width, cam.Height), (Color.White * 0.75f));

            // Text
            pauseText = new Text(Assets.arial, "Paused", new Vector2((cam.Width / 2), (cam.Height / 2) - pauseTextPadding), Color.White, 2.0f, true);
            pauseTooltip = new Text(Assets.arial, pauseTooltipMenu, new Vector2((cam.Width / 2) - 160, (cam.Height / 2) + pauseTextPadding), Color.Gray, 1.0f, false);

            // CRT Filter
            crtVignette = new StaticSprite(Assets.crtVignette, new Rectangle(0, 0, cam.Width, cam.Height), Color.White);
        }

        /// <summary>
        /// Stop the current song playing in MediaPlayer.
        /// </summary>
        public void StopSong()
        {
            MediaPlayer.Stop();
        }

        /// <summary>
        /// Start playing a specific song in MediaPlayer.
        /// </summary>
        /// <param name="music">The Song variable to play.</param>
        public void ChangeSong(Song music)
        {
            // Start New Song if Not Already Playing
            if (MediaPlayer.Queue.ActiveSong != music) MediaPlayer.Play(music);
        }

        public void Update(GameTime gameTime)
        {
            // --- Audio ---

            if (MediaPlayer.Queue.ActiveSong == OST.trialIntro)
            {
                MediaPlayer.IsRepeating = false;
            }
            else
            {
                MediaPlayer.IsRepeating = true;
            }

            // Loop Music
            if (MediaPlayer.Queue.ActiveSong == OST.trialIntro && MediaPlayer.State == MediaState.Stopped)
            {
                // Trial
                MediaPlayer.Play(OST.trial);
            }

            // Music Toggled
            if (Global.musicToggled)
            {
                // Music Disabled
                if (!Global.musicEnabled)
                {
                    // Lower the volume to 0
                    MediaPlayer.Volume = 0.0f;
                }
                // Music Enabled
                else
                {
                    // Set the volume back to default
                    MediaPlayer.Volume = 1.0f;
                }

                // End Event
                Global.musicToggled = false;
            }

            // --- State ---

            // Update state variables
            screenWidth = graphicsDevice.PresentationParameters.Bounds.Width;
            screenHeight = graphicsDevice.PresentationParameters.Bounds.Height;

            // Set Controls
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();

            // Override Update
            OnUpdate(gameTime);

            // On Pause / UnPause
            if (canPause)
            {
                // Toggle Pause
                if (KeyPress(Keys.Escape))
                {
                    Global.paused = !Global.paused;

                    // Toggle Pause Tooltip Depending on State

                    // During Gameplay (Story, Debate)
                    if (Global.currentState == Global.State.story
                        || Global.currentState == Global.State.debate)
                    {
                        this.pauseTooltip.setText(pauseTooltipGame);
                    }

                    // Elsewhere (Menus)
                    else
                    {
                        this.pauseTooltip.setText(pauseTooltipMenu);
                    }
                }

                // Mouse Visibility
                Global.mouseVisible = Global.paused;

                // When Paused
                if (KeyPress(Keys.Escape) && Global.paused ||
                    justPaused)
                {
                    // If Just Paused is True
                    Global.paused = true; // Set Game to Paused

                    // Transition Objects
                    if (Global.menuAnimations) // If Animations are Enabled
                    {
                        // Cursor
                        cursor.FadeOut();
                    }

                    // Update Just Paused to False
                    justPaused = false;
                }

                // When Unpaused
                if (KeyPress(Keys.Escape) && !Global.paused)
                {
                    // Set Game after Unpause
                    justPaused = false;

                    // Reset Objects
                    cursor.ResetSize();
                    cursor.ResetValues();
                }
            }

            // --- Controls ---

            // Mouse Delta
            mouseDelta = new Vector2(Math.Min(MAXDELTA, (previousMouse.X - mouse.X)), Math.Min(MAXDELTA, (previousMouse.Y - mouse.Y)));

            previousKeyboard = keyboard;
            previousMouse = mouse;

            // --- Cursor ---

            // Pause Game when Inactive
            if (Global.pauseWhenInactive && !Global.active) justPaused = true; // Just Paused

            // Cursor Movement

            // If Game is not Paused
            if (!Global.paused)
            {
                // Fix Cursor Size
                if (cursor.Bounds.Width != Global.cursorSize.X || cursor.Bounds.Height != Global.cursorSize.Y)
                {
                    cursor.ResetSize();
                }

                if (Global.active) // If Window is Open
                {
                    // On Mouse Move
                    if (MouseMoved())
                    {
                        // Update Cursor Position
                        if (cursorVisible)
                        {
                            cursor.X += (mouse.X - screenWidth / 2);    // X
                            cursor.Y += (mouse.Y - screenHeight / 2);   // Y
                        }
                    }

                    // Center Mouse
                    Mouse.SetPosition(screenWidth / 2, screenHeight / 2);

                    // Cursor Bounds
                    if (cursor.X < 0) cursor.X = 0; // Left
                    if (cursor.X > cam.Width - Global.cursorSize.X) cursor.X = cam.Width - Global.cursorSize.X;   // Right
                    if (cursor.Y < 0) cursor.Y = 0; // Top
                    if (cursor.Y > cam.Height - Global.cursorSize.Y) cursor.Y = cam.Height - Global.cursorSize.Y; // Bottom
                }
            }

            // Update Cursor
            cursor.Update(gameTime);
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            OnDraw(spriteBatch);

            // --- Global Variables ---

            // Cursor
            if (cursorVisible) cursor.Draw(spriteBatch);

            // Pause Overlay
            if (Global.paused)
            {
                // NOTE: Only draw the overlay if the game is not running the intro!
                if (Global.currentState != Global.State.intro)
                {
                    // Background
                    pauseOverlay.Draw(spriteBatch);

                    // Text
                    pauseText.Draw(spriteBatch);
                    pauseTooltip.Draw(spriteBatch);
                }
            }

            // CRT Filter
            if (Global.crtFilter)
            {
                crtVignette.Draw(spriteBatch); // Vignette
            }
        }

        /// <summary>
        /// The method to override when drawing in the state.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch used in Draw.</param>
        public virtual void OnDraw(SpriteBatch spriteBatch)
        {
        }

        /// <summary>
        /// Resets the current state.
        /// </summary>
        public virtual void ResetState()
        {
            // State Properties
            cursor.ResetValues(); // Reset Cursor Position and Size

            // Unpause State
            if (Global.active && Global.paused) Global.paused = false;

            // Set Cursor Position to Center of Screen
            cursor.X = (Global.windowWidth / 2);    // X
            cursor.Y = (Global.windowHeight / 2);   // Y
        }

        // --- Controls ---

        // Keyboard

        public bool KeyPress(Keys key)
        {
            if (keyboard.IsKeyUp(key) && previousKeyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool KeyDown(Keys key)
        {
            if (keyboard.IsKeyDown(key))
            {
                return true;
            }
            else return false;
        }

        public bool MouseMoved()
        {
            if (mouseDelta.X != 0 || mouseDelta.Y != 0)
            {
                return true;
            }
            else return false;
        }

        // Mouse

        public bool LeftClicked()
        {
            if (mouse.LeftButton == ButtonState.Released && previousMouse.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }

        public bool RightClicked()
        {
            if (mouse.RightButton == ButtonState.Released && previousMouse.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }

        public bool LeftHold()
        {
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }

        public bool RightHold()
        {
            if (mouse.RightButton == ButtonState.Pressed)
            {
                return true;
            }
            else return false;
        }
    }
}
