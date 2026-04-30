using BetweenTheLines.Source.Objects.Level;
using BetweenTheLines.Source.States;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Media;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

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
        public static string gameVersion = "Mystery Game Jam 2026 Edition"; // NOTE: Version does not have a real number for game jam release!

        public static bool
            paused = false,
            mouseVisible = false,

            musicEnabled = true, musicToggled = false;

        // Gameplay Variables
        public static string
            picklesArrivedTime, // Time Pickles Arrived at House
            faunArrivedTime, // Time Faun Arrived at House
            gameStartTime; // Time Game Started Officially

        // State
        public enum State
        {
            intro,
            title,
            options,
            story,
            debate,
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
            typewriter, dialogContinue,

            // --- Actions ---
            footsteps;
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
            pauseOverlay,
            dialogBox, overlay, clock,
            button,
            checkboxInactive, checkboxActive,

            // Map
            map,
            mapFoyer, mapLivingRoom, mapMainHall, mapBathroom, mapKitchen, mapCloset,

            // --- Intro ---
            snowman64, gameJamLogo,

            // --- Title ---
            logo,
            titleBG,

            // --- Level ---

            // Intro
            intro1,
            intro2, intro2a,

            // Gameplay
            foyer, livingRoom, mainHall, bathroom, kitchen, closet,

            // --- Credits ---

            // Cats
            picklesIrl, angelIrl, smokeyIrl;

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

            Assets.pauseOverlay = content.Load<Texture2D>("Assets/Images/Global/PauseOverlay");
            Assets.dialogBox = content.Load<Texture2D>("Assets/Images/Global/DialogBox");
            Assets.overlay = content.Load<Texture2D>("Assets/Images/Global/OverlayTimeCorner");
            Assets.clock = content.Load<Texture2D>("Assets/Images/Global/Clock");

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

            // Intro
            Assets.intro1 = content.Load<Texture2D>("Assets/Images/Level/Intro/intro1");
            Assets.intro2 = content.Load<Texture2D>("Assets/Images/Level/Intro/intro2");
            Assets.intro2a = content.Load<Texture2D>("Assets/Images/Level/Intro/intro2door");

            // House
            Assets.foyer = content.Load<Texture2D>("Assets/Images/Level/House/Foyer");
            Assets.livingRoom = content.Load<Texture2D>("Assets/Images/Level/House/LivingRoom");
            Assets.mainHall = content.Load<Texture2D>("Assets/Images/Level/House/MainHall");
            Assets.bathroom = content.Load<Texture2D>("Assets/Images/Level/House/Bathroom");
            Assets.kitchen = content.Load<Texture2D>("Assets/Images/Level/House/Kitchen");
            Assets.closet = content.Load<Texture2D>("Assets/Images/Level/House/Closet");

            // Map
            Assets.map = content.Load<Texture2D>("Assets/Images/Level/House/Map/MapDefault");
            Assets.mapFoyer = content.Load<Texture2D>("Assets/Images/Level/House/Map/MapFoyer");
            Assets.mapLivingRoom = content.Load<Texture2D>("Assets/Images/Level/House/Map/MapLivingRoom");
            Assets.mapMainHall = content.Load<Texture2D>("Assets/Images/Level/House/Map/MapMainHall");
            Assets.mapBathroom = content.Load<Texture2D>("Assets/Images/Level/House/Map/MapBathroom");
            Assets.mapKitchen = content.Load<Texture2D>("Assets/Images/Level/House/Map/MapKitchen");
            Assets.mapCloset = content.Load<Texture2D>("Assets/Images/Level/House/Map/MapCloset");

            // --- Portraits ---

            // Pickles
            Dialog.picklesRegular = content.Load<Texture2D>("Assets/Images/Level/Portrait/picklesRegular");
            Dialog.picklesThinking = content.Load<Texture2D>("Assets/Images/Level/Portrait/picklesThinking");

            // Faun
            Dialog.faunRegular = content.Load<Texture2D>("Assets/Images/Level/Portrait/faunRegular");
            Dialog.faunWorried = content.Load<Texture2D>("Assets/Images/Level/Portrait/faunWorried");

            // Otto
            Dialog.ottoRegular = content.Load<Texture2D>("Assets/Images/Level/Portrait/ottoRegular");
            Dialog.ottoAngry = content.Load<Texture2D>("Assets/Images/Level/Portrait/ottoAngry");

            // Angel
            Dialog.angelRegular = content.Load<Texture2D>("Assets/Images/Level/Portrait/angelRegular");
            Dialog.angelThinking = content.Load<Texture2D>("Assets/Images/Level/Portrait/angelThinking");

            // Micah
            Dialog.micahRegular = content.Load<Texture2D>("Assets/Images/Level/Portrait/micahRegular");

            // Smokey
            Dialog.smokeyRegular = content.Load<Texture2D>("Assets/Images/Level/Portrait/smokeyRegular");
            Dialog.smokeyExcited = content.Load<Texture2D>("Assets/Images/Level/Portrait/smokeyExcited");
            Dialog.smokeyCreepy = content.Load<Texture2D>("Assets/Images/Level/Portrait/smokeyCreepy");

            // --- Credits ---

            // Cats
            Assets.picklesIrl = content.Load<Texture2D>("Assets/Images/Credits/picklesIrl");
            Assets.angelIrl = content.Load<Texture2D>("Assets/Images/Credits/angelIrl");
            Assets.smokeyIrl = content.Load<Texture2D>("Assets/Images/Credits/smokeyIrl");

            // Audio

            // SFX
            SFX.button = content.Load<SoundEffect>("Assets/Audio/SFX/button");
            SFX.typewriter = content.Load<SoundEffect>("Assets/Audio/SFX/typewriter");
            SFX.dialogContinue = content.Load<SoundEffect>("Assets/Audio/SFX/dialogContinue");

            SFX.footsteps = content.Load<SoundEffect>("Assets/Audio/SFX/footsteps");

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
        // Portrait IDs
        internal static readonly byte
            pickles = 1,
            faun = 2,
            otto = 3,
            angel = 4,
            micah = 5,
            smokey = 6;

        // Text IDs
        internal static readonly byte
            innerThought = 1,
            tutorial = 2;

        // Dialog Portraits

        public static Texture2D
            // --- Pickles ---
            picklesRegular, picklesThinking,

            // --- Faun ---
            faunRegular, faunWorried,

            // --- Otto ---
            ottoRegular, ottoAngry,

            // --- Angel ---
            angelRegular, angelThinking,

            // --- Micah ---
            micahRegular,

            // --- Smokey ---
            smokeyRegular, smokeyExcited, smokeyCreepy;

        public static DialogString[]
            // Intro
            intro1,
            intro2, intro2a, // Tutorial
            preludeEnd,
            
            // Chapter 1
            chapter1part1,
            chapter1bathroom, chapter1kitchen, chapter1closet, // Exploration
            chapter1part2;

        public static void LoadDialog()
        {
            // NOTE: Lines of dialog are formatted like this:
            // Integer number Name | String Text

            // Intro

            intro1 = new DialogString[]{
                Line(0, "Last night, I got this letter...", innerThought),
                Line(0, "It said \"Congratulations! You have won $1 000 000 dollars!\"", innerThought),
                Line(0, "...Yeah, as if.", innerThought),
                Line(1, "My name is Pickles. I'm a renowned detective.", innerThought),
                Line(1, "In my line of work... I make a decent amount of money.", innerThought),
                Line(1, "But this?...", innerThought),
                Line(1, "I'm gonna get to the bottom of this.", innerThought),
                Line(1, "Besides... Who would fall for a scheme like this?\nI've never seen something so outrageous.", innerThought)
            };

            intro2 = new DialogString[]{
                Line(1, "Well, here I am."),
                Line(1, "This place's address was at the bottom of the letter.", innerThought),
                Line(1, "Let's hope this wasn't a waste of my time."),
                Line(-1, "In this game, you'll have to take full advantage of your surroundings.", tutorial),
                Line(-1, "That means using the mouse to interact with objects\nthat are full of mystery, or might contain clues.", tutorial),
                Line(-1, "Go on, try moving your mouse to interact with the door!", tutorial)
            };

            intro2a = new DialogString[]{
                Line(1, "Huh... That's weird."),
                Line(1, "The door's unlocked."),
                Line(1, "I guess I'll just have to head inside.\nI'll be sure to keep my guard up...")
            };

            preludeEnd = new DialogString[]{
                Line(1, "When I stepped foot inside... The house didn't look unkept.", innerThought),
                Line(1, "Despite it's shoddy look from outside...\nIt was as though someone really was waiting for me to arrive.", innerThought),
                Line(1, "Without hesitation, I called out into the large foyer,\nand waited for a response.", innerThought),
                Line(1, "Anyone home?"),
                Line(0, "Um... Hi..."),
                Line(1, "Huh?"),
                Line(0, "So you... got the same letter as us."),
                Line(1, "Yeah. I'm guessing I'm not the only one, then."),
                Line(0, "B-before we go, I should probably introduce myself..."),
                Line(2, "I'm Faun. I got here first. I-It's nice to meet you..."),
                Line(1, "The name's Pickles. Detective Pickles.\nI'm here on behalf of my work investigating this..."),
                Line(2, "...Weirdly obvious scam?"),
                Line(1, "Yeah. Any info about that?"),
                Line(2, "W-well, I got here at about " + Global.faunArrivedTime + ".."),
                Line(1, "Since I arrived at " + Global.picklesArrivedTime + ", that would mean you got here\n10 minutes before me."),
                Line(2, "Come with me... the others are waiting.\nI'll explain everything in the living room."),
                Line(1, "The others...", innerThought),
                Line(1, "Alright.")
            };

            chapter1part1 = new DialogString[]{
                Line(0, "'The hell? Who's this tail licker?"),
                Line(1, "Pickles. Detective P-"),
                Line(0, "Yeah yeah, whatever. You here for the money?"),
                Line(1, "...Yes. I'm here to uncover the true meaning behind\nthe letter I received in the mail."),
                Line(1, "This guy's got some nerve...", innerThought),
                Line(0, "Look pal, the dough ain't here. Whole thing's a scam."),
                Line(1, "I figured so. Do you know more about the situation?"),
                Line(0, "'The hell do you think I'm gonna tell you that for?\nYou tryna take the money from me?"),
                Line(2, "O-Otto... He's just as confused as the rest of us... So..."),
                Line(3, "... *sigh* Fine. You're lucky the li'l lady is here.\nOtherwise I woulda-"),
                Line(2, "Otto...!"),
                Line(3, "Oh, nevermind..."),
                Line(1, "First, I'd like to know why the door was left unlocked."),
                Line(1, "It seems... hazardous to leave this seemingly abandoned house just...\nunlocked. For anyone to come inside."),
                Line(0, "Well, we all thought that it would be a good idea\nto leave the door as we found it."),
                Line(1, "Ah, I see. And you are?"),
                Line(4, "Angel. I may be past my prime, but back in my day...\nI was quite the lady."),
                Line(1, "Despite the overlooming feeling of confusion, dread,\nand mystery...", innerThought),
                Line(1, "I felt as though we were all becoming closer.", innerThought),
                Line(1, "There was a cheerful feeling to the room's atmosphere.", innerThought),
                Line(1, "It's nice to meet you, Angel. Faun, Otto..."),
                Line(1, "Are you the only people here?"),
                Line(2, "N-No... This house..."),
                Line(2, "Has a host."),
                Line(1, "A host?", innerThought),
                Line(1, "What... does she mean by that?", innerThought),
                Line(1, "What do you mean, host?"),
                Line(1, "The room went silent.", innerThought),
                Line(1, "And just like that... That cheerfulness was gone.", innerThought),
                Line(2, "Detective, I-I can show you around, if... if it's not too much trouble..."),
                Line(1, "Sure. That would be nice."),
                Line(-1, "Faun wants to show you around? How kind of her!", tutorial),
                Line(-1, "This is where the game really opens up.\nExplore the world- *ahem* house... to your heart's content.", tutorial),
                Line(-1, "Keep an eye on the map in the top right corner.\nIt'll help you keep track of where you are.", tutorial)
            };

            chapter1bathroom = new DialogString[]{
                Line(1, "Test")
            };

            chapter1kitchen = new DialogString[]{
                Line(1, "Test")
            };

            chapter1closet = new DialogString[]{
                Line(0, "Mmm... What time is it...?"),
                Line(2, "M-Micah, you know you shouldn't be sleeping in the closet..."),
                Line(5, "Oh d-d-dear! My apologies...!"),
                Line(1, "It's fine. I'm still getting adjusted, so-"),
                Line(5, "Cortisol levels rising. Begin the diaphragmatic process."),
                Line(1, "...Huh?"),
                Line(1, "I didn't catch a word of what she said.", innerThought),
                Line(5, "O-oh, sorry, I didn't explain properly, did I?"),
                Line(5, "In case you didn't know, diaphragmatic breathing exercises\ncan help you relax."),
                Line(1, "I, uh..."),
                Line(2, "M-Micah! He has no idea what you're talking about..."),
                Line(5, "WHAT?! Ohmygoodness I am SO sorry!"),
                Line(1, "Really, it's fine..."),
                Line(5, "No! It's not fine!"),
                Line(5, "Allow me to start from the top. *ahem*"),
                Line(1, "The sooner I get out of this closet, the better.", innerThought)
            };

            chapter1part2 = new DialogString[]{
                Line(0, "I'm glad to see you all get acquainted with one another!"),
                Line(1, "That voice...", innerThought),
                Line(2, "That's-!"),
                Line(0, "Meeee!"),
                Line(1, "Who the hell..."),
                Line(3, "You again?! I thought I told you to scram for good, ya douche!"),
                Line(0, "Ah, but I just couldn't help myself...!\nI was getting jealous of all your smiles! Your joy!"),
                Line(1, "Are you the host here..?"),
                Line(0, "But of course I am!"),
                Line(6, "Smokeston Novikov, at your service!\nBut you can just call me Smokey. I'd prefer that!"),
                Line(1, "Smokeston... Where have I heard that name?", innerThought),
                Line(3, "If you can't understand my words..."),
                Line(3, "I'LL JUST HAVE TO BEAT THE SENSE INTO YA!"),
                Line(6, "Woah woah woah! I wouldn't do that if I were you...!"),
                Line(6, "My guts will go allllll over the place if you-"),
                Line(1, "The sound of a sharp punch filled the room before he could finish.", innerThought),
                Line(1, "It was as though pure iron was forced through something\nsoft and squishy.", innerThought),
                Line(1, "And when my eyes finally adjusted...", innerThought),
                Line(1, "There he was. Oozing with blood.", innerThought),
                Line(2, "KYAAAAAAAAAAAAAAAAAA!"),
                Line(3, "How'd ya like that, asshole?! I told you, don't fuck with me!"),
                Line(1, "I couldn't believe what I just witnessed.\nOtto, who was completely innocent prior to the argument he started...", innerThought),
                Line(1, "Punched a hole through Smokeston.", innerThought),
                Line(6, "Oh... This isn't good... Not at all..."),
                Line(1, "Why hasn't he passed out yet? Losing that much blood should've\nkilled him, let alone the fatal injury he clearly suffers.", innerThought),
                Line(6, "Aw, it's all over the furniture... Not cool, Otto!\nI had high hopes for you..."),
                Line(3, "The fuck? Is that not enough for..."),
                Line(3, "..."),
                Line(1, "Otto suddenly stopped speaking.", innerThought),
                Line(1, "I didn't have the courage to ask him why, but...\nWhen I looked over at Smokeston...", innerThought),
                Line(1, "...", innerThought),
                Line(1, "...", innerThought),
                Line(1, "Two unbelievable events happening, back to back.", innerThought),
                Line(1, "I should be very careful around either of them. For now,\nFaun and the others seem reliable enough.", innerThought),
                Line(6, "Where was I...? Oh, right."),
                Line(6, "I'm the host of this game... treat me with a little respect, thank you."),
                Line(3, "..."),
                Line(2, "..."),
                Line(4, "..."),
                Line(5, "..."),
                Line(1, "We were all in shock.", innerThought),
                Line(1, "Minutes began to feel like hours.\nNone of us knew what to say about anything we had just witnessed.", innerThought),
                Line(6, "... *sigh* So much for a perfect opening night."),
                Line(6, "We'll take five.\nThen I want you all to come see the discovery I've made."),
                Line(1, "...Discovery...?"),
                Line(6, "Well yeah, duh! What kind of game would this be without an objective?"),
                Line(6, "A hero without a princess to save... he's just a nobody, right?"),
                Line(2, "S-so, y-you were serious about this \"game\" y-you've prepared..."),
                Line(5, "According to my calculations, he's not lying..."),
                Line(5, "When he announced it at " + Global.gameStartTime + ", I checked his pulse.\nHe was dead serious."),
                Line(4, "I'm afraid I'm just too old for social experiments like this.\nYou younger generations disgust me."),
                Line(3, "Yo, doctor, how do you know that's not bullshit?\nFor all we know, that pulse you felt could've been fake."),
                Line(3, "I mean, shit, you saw what happened when I lost control."),
                Line(3, "I don't have the brains to orchestrate something as beautiful as that.\nTrust me."),
                Line(2, "*sniff* I w-want to g-get out of here... I-I don't like this...!"),
                Line(1, "...What the fuck?", innerThought),
                Line(1, "What... is this?"),
                Line(1, "I didn't question it...\nBut none of us had left the old house.", innerThought),
                Line(1, "I couldn't tell you why...", innerThought),
                Line(1, "But that would soon turn out to be our biggest mistake.", innerThought)
            };
        }

        // Create Dialog String
        public static DialogString Line(int name, string text, int textColor = 0)
        {
            return new DialogString(name, text, textColor);
        }
    }
}
