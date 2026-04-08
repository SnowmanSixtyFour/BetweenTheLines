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
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects.GUI;

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

        public State()
        {
            // --- Set State ---

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
            pauseOverlay = new StaticSprite(null, new Rectangle(0, 0, cam.Width, cam.Height), new Color(0, 0, 0, 128));

            // Text
            pauseText = new Text(Global.arial, "Paused", new Vector2((cam.Width / 2), (cam.Height / 2) - pauseTextPadding), Color.White, 2.0f, true);
            pauseTooltip = new Text(Global.arial, "Press ESCAPE to resume focus.", new Vector2((cam.Width / 2), (cam.Height / 2) + pauseTextPadding), Color.LightGray, 1.0f, true);
        }

        public void Update(GameTime gameTime)
        {
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

            // Update Controls

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
                if (Global.active) // If Window is Open
                {
                    // On Mouse Move
                    if (MouseMoved())
                    {
                        // Update Cursor Position
                        cursor.X += (mouse.X - screenWidth / 2);    // X
                        cursor.Y += (mouse.Y - screenHeight / 2);   // Y
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
        }

        /// <summary>
        /// The method to override when drawing in the state.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch used in Draw.</param>
        public virtual void OnDraw(SpriteBatch spriteBatch)
        {
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
