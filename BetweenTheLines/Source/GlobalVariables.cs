using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using BetweenTheLines.Source.Objects.Level;

namespace BetweenTheLines.Source
{
    // Global Variables
    internal class Global
    {
        // Quit Game
        public static bool quit = false;

        // Window

        public static string windowName = "Between the Lines";

        public static int
            windowWidth = 844,
            windowHeight = 480;

        public static bool
            active = true,
            fullscreen = false, fullscreenChanged = false;

        // Settings

        public static bool
            checkAndCreateSettings = false; // Create Settings

        // When Inactive
        public static bool
            renderInactive = true,
            pauseWhenInactive = true;

        // Mouse
        public static float
            mouseDeltaMax = 6,
            mouseSensitivity = 0.1f;

        // Game

        // Version (start with "v")
        public static string gameVersion = "v1.0.0"; // NOTE: Does this really matter in a Game Jam?

        public static bool
            paused = false,
            mouseVisible = false,

            musicEnabled = true, musicToggled = false;

        // State
        public enum State
        {
            intro,
            title,
            options,
            level,
            credits
        }

        public static bool viewingCreditsFromTitle = false;
        public static State currentState = State.intro; // Current state of the game - NOTE: Always starts on intro!

        // Cursor Size
        public static int
            cursorWidth = 16, cursorHeight = 20, // Size of Cursor
            cursorResize = 2; // Amount to Resize Cursor by
        public static Point cursorSize = new Point((cursorWidth * cursorResize), (cursorHeight * cursorResize)); // Size as Point

        // Graphics

        public static bool
            crtFilter = false,
            menuAnimations = true;

        // GUI Colors
        public static Color titleColor = Color.LightGray;
    }

    // Store Game Soundtrack in OST
    internal class OST
    {
        public static Song
            title,
            intro,
            intense;
    }

    // Sound Effects
    internal class SFX
    {
        public static SoundEffect
            // --- GUI ---
            button,

            // --- Dialog ---
            typewriter;
    }

    // Game Assets
    internal class Assets
    {
        // Set Assets

        // --- Sprites ---

        // Load Textures
        public static Texture2D
            // --- Global ---
            noImg, crtVignette,

            // --- GUI ---
            cursor, cursorHighlight, cursorInspect,
            button,
            checkboxInactive, checkboxActive,

            // --- Intro ---
            snowman64, gameJamLogo,

            // --- Title ---
            logo,
            titleBG,

            // --- Level ---

            // Intro
            intro1,
            intro2, intro2a;

            // Gameplay
            // TBA

        // --- Fonts ---
        public static SpriteFont arial;

        // --- Shaders ---
        public static Effect crt;

        // Load Assets from Image Files
        public static void LoadContent(ContentManager content)
        {
            // Images

            // --- Utilities ---
            Assets.noImg = content.Load<Texture2D>("Assets/Images/Global/pixel");
            Assets.crtVignette = content.Load<Texture2D>("Assets/Images/Global/CRTVignette");

            // --- GUI ---
            Assets.cursor = content.Load<Texture2D>("Assets/Images/Global/Cursor");
            Assets.cursorHighlight = content.Load<Texture2D>("Assets/Images/Global/CursorHighlight");
            Assets.cursorInspect = content.Load<Texture2D>("Assets/Images/Global/CursorInspect");

            Assets.button = content.Load<Texture2D>("Assets/Images/Global/Button");

            Assets.checkboxInactive = content.Load<Texture2D>("Assets/Images/Global/CheckboxInactive");
            Assets.checkboxActive = content.Load<Texture2D>("Assets/Images/Global/CheckboxActive");

            // --- Intro ---
            Assets.snowman64 = content.Load<Texture2D>("Assets/Images/Intro/Snowman64");
            Assets.gameJamLogo = content.Load<Texture2D>("Assets/Images/Intro/GameJam");

            // --- Title ---
            Assets.titleBG = content.Load<Texture2D>("Assets/Images/Title/Diamonds");
            Assets.logo = content.Load<Texture2D>("Assets/Images/Title/Logo");

            // --- Gameplay ---
            // TBA

            // Intro
            Assets.intro1 = content.Load<Texture2D>("Assets/Images/Level/Intro/intro1");
            Assets.intro2 = content.Load<Texture2D>("Assets/Images/Level/Intro/intro2");
            Assets.intro2a = content.Load<Texture2D>("Assets/Images/Level/Intro/intro2door");

            // --- Portraits ---

            // Pickles
            Dialog.picklesRegular = content.Load<Texture2D>("Assets/Images/Level/Portrait/picklesRegular");
            Dialog.picklesThinking = content.Load<Texture2D>("Assets/Images/Level/Portrait/picklesThinking");

            // Audio

            // SFX
            SFX.button = content.Load<SoundEffect>("Assets/Audio/SFX/button");
            SFX.typewriter = content.Load<SoundEffect>("Assets/Audio/SFX/typewriter");

            // NOTE: Music is ran in try and catch to prevent errors while copyrighted placeholder music is left out of the source code.

            // Music
            try
            {
                OST.title = content.Load<Song>("Assets/Audio/Music/Title");
                OST.intro = content.Load<Song>("Assets/Audio/Music/Intro");
                OST.intense = content.Load<Song>("Assets/Audio/Music/Intense");
            }
            catch (Exception e)
            {
                Debug.Print("Error! " + e);
            }

            // Fonts

            Assets.arial = content.Load<SpriteFont>("Assets/Fonts/Arial");

            // Shaders

            Assets.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");
        }
    }
    
    // Game Dialog
    internal class Dialog
    {
        // Dialog Portraits

        public static Texture2D
            // --- Pickles ---
            picklesRegular,
            picklesThinking;
        
        // NOTE: Lines of dialog are formatted like this:
        // Integer number Name | String Text

        // Intro

        public static DialogString[] intro1 = {
            Line(1, "Last night, I got this letter..."),
            Line(1, "It said \"Congratulations! You have won $1 000 000 dollars!\""),
            Line(1, "...Yeah, as if."),
            Line(1, "In my line of work... I make a decent amount of money."),
            Line(1, "But this?..."),
            Line(1, "I'm gonna get to the bottom of this."),
            Line(1, "Besides... Who would fall for a scheme like this?")
        };

        public static DialogString[] intro2 = {
            Line(1, "Well, here I am."),
            Line(1, "This place's address was at the bottom of the letter."),
            Line(1, "Let's hope this wasn't a waste of my time.")
        };

        // Create Dialog String
        public static DialogString Line(int name, string text)
        {
            return new DialogString(name, text);
        }
    }
}
