using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using BetweenTheLines.Source.Objects.Level;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BetweenTheLines.Source
{
    // Global Variables
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

            // Intro
            intro1, intro2,

            giovanni;
        public static SpriteFont arial;
        public static Effect crt;
    }

    // Game Assets
    internal class Assets
    {
        // Load Assets from Image Files
        public static void LoadContent(ContentManager content)
        {
            // Images

            // --- Utilities ---
            Global.noImg = content.Load<Texture2D>("Assets/Images/Global/pixel");

            Global.cursor = content.Load<Texture2D>("Assets/Images/Global/Cursor");
            Global.cursorHighlight = content.Load<Texture2D>("Assets/Images/Global/CursorHighlight");

            // --- Intro ---
            Global.snowman64 = content.Load<Texture2D>("Assets/Images/Intro/Snowman64");

            // --- Title ---
            Global.logo = content.Load<Texture2D>("Assets/Images/Title/Logo");

            // --- Gameplay ---
            Global.giovanni = content.Load<Texture2D>("Assets/Images/Level/Giovanni");

            // Intro
            Global.intro1 = content.Load<Texture2D>("Assets/Images/Level/Intro/intro1");
            Global.intro2 = content.Load<Texture2D>("Assets/Images/Level/Intro/intro2");

            // Fonts

            Global.arial = content.Load<SpriteFont>("Assets/Fonts/Arial");

            // Shaders

            Global.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");
        }
    }
    
    // Game Dialog
    internal class Dialog
    {
        // Intro

        public static DialogString[] intro1 = {
            new DialogString(1, "Last night, I got this letter..."),
            new DialogString(1, "It said \"Congratulations! You have won $1 000 000 dollars!\""),
            new DialogString(1, "...Yeah, as if."),
            new DialogString(1, "In my line of work... I make a decent amount of money."),
            new DialogString(1, "But this?..."),
            new DialogString(1, "I'm gonna get to the bottom of this."),
            new DialogString(1, "Besides... Who would fall for a scheme like this?")
        };

        public static DialogString[] intro2 = {
            new DialogString(1, "Well, here I am."),
            new DialogString(1, "This place's address was at the bottom of the letter."),
            new DialogString(1, "Let's hope this wasn't a waste of my time.")
        };
    }
}
