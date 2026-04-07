using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BetweenTheLines.Source
{
    internal class Global
    {
        // Window

        public static string windowName = "Between the Lines";

        public static int
            windowWidth = 844,
            windowHeight = 480;

        public static bool active = true;

        // Settings

        // When Inactive
        public static bool
            renderInactive = true,
            pauseWhenInactive = true;

        // Mouse
        public static float
            mouseDeltaMax = 6,
            mouseSensitivity = 0.1f;

        // Game

        public static string gameVersion = "1.0.0"; // NOTE: Does this really matter in a Game Jam?

        public static bool
            paused = false,
            mouseVisible = false;

        // State
        public enum State
        {
            intro,
            title,
            level
        }

        public static State currentState = State.intro; // Current state of the game - NOTE: Always starts on intro!

        // Cursor Size
        public static int
            cursorWidth = 16, cursorHeight = 20, // Size of Cursor
            cursorResize = 2; // Amount to Resize Cursor by
        public static Point cursorSize = new Point((cursorWidth * cursorResize), (cursorHeight * cursorResize)); // Size as Point

        // Graphics

        public static bool
            shadersEnabled = false,
            menuAnimations = true;

        // Sprites

        // Load Textures
        public static Texture2D
            // --- Global ---
            noImg,
            cursor, cursorHighlight,

            // --- Intro ---
            snowman64,

            // --- Title ---
            logo,

            // --- Level ---
            giovanni;
        public static SpriteFont arial;
        public static Effect crt;

        // Load game assets
        public static void LoadContent(ContentManager content)
        {
            // Images

            Global.noImg = content.Load<Texture2D>("Assets/Images/Global/pixel");

            Global.cursor = content.Load<Texture2D>("Assets/Images/Global/Cursor");
            Global.cursorHighlight = content.Load<Texture2D>("Assets/Images/Global/CursorHighlight");

            Global.snowman64 = content.Load<Texture2D>("Assets/Images/Intro/Snowman64");

            Global.logo = content.Load<Texture2D>("Assets/Images/Title/Logo");

            Global.giovanni = content.Load<Texture2D>("Assets/Images/Level/Giovanni");

            // Fonts

            Global.arial = content.Load<SpriteFont>("Assets/Fonts/Arial");

            // Shaders

            Global.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");
        }
    }
}
