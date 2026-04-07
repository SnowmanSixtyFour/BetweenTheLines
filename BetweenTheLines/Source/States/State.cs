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

namespace BetweenTheLines.Source.States
{
    internal class State
    {
        public GraphicsDeviceManager graphics = MainGame.publicGraphics;
        public GraphicsDevice graphicsDevice = MainGame.publicGraphicsDevice;

        // State variables
        public KeyboardState keyboard, previousKeyboard;
        public MouseState mouse, previousMouse;
        public Vector2 mouseDelta;
        protected float MAXDELTA = Global.mouseDeltaMax;

        public int screenWidth, screenHeight;
        public Camera cam;

        public Cursor cursor;
        public bool cursorVisible;

        // Pausing
        public bool canPause = true;

        public State()
        {
            // Set Camera
            cam = new Camera(this.graphicsDevice, Global.windowWidth, Global.windowHeight);

            // Set Cursor
            cursor = new Cursor(Global.cursorSize);
            cursorVisible = true;
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

            // Update Controls

            // Mouse Delta
            mouseDelta = new Vector2(Math.Min(MAXDELTA, (previousMouse.X - mouse.X)), Math.Min(MAXDELTA, (previousMouse.Y - mouse.Y)));

            previousKeyboard = keyboard;
            previousMouse = mouse;
        }

        public virtual void OnUpdate(GameTime gameTime)
        {
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            OnDraw(spriteBatch);

            // Draw Global State Objects

            // Cursor
            if (!Global.mouseVisible) // If mouse is not visible
            {
                if (cursorVisible) // If cursor can be seen
                {
                    cursor.Draw(spriteBatch);
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

        // Controls

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
    }
}
