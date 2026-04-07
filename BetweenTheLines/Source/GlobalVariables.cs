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

        // Cursor Size
        public static int
            cursorWidth = 16, cursorHeight = 20, // Size of Cursor
            cursorResize = 1; // Amount to Resize Cursor by
        public static Point cursorSize = new Point((cursorWidth * cursorResize), (cursorHeight * cursorResize)); // Size as Point

        // Graphics

        public static bool shadersEnabled = false;

        // Sprites

        public static Texture2D
            noImg,
            cursor, cursorHighlight,
            giovanni;
        public static SpriteFont arial;
        public static Effect crt;

        // Load game assets
        public static void LoadContent(ContentManager content)
        {
            // Images

            Global.noImg = content.Load<Texture2D>("Assets/Images/pixel");

            Global.cursor = content.Load<Texture2D>("Assets/Images/Cursor");
            Global.cursorHighlight = content.Load<Texture2D>("Assets/Images/CursorHighlight");

            Global.giovanni = content.Load<Texture2D>("Assets/Images/Giovanni");

            // Fonts

            Global.arial = content.Load<SpriteFont>("Assets/Fonts/Arial");

            // Shaders

            Global.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");
        }
    }
}
