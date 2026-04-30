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

            // Cinematics
            lessonLearned1, lessonLearned2, lessonLearned3, lessonLearned4,

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

            // Dialog Cinematics
            Assets.lessonLearned1 = content.Load<Texture2D>("Assets/Images/Level/Cinematic/lessonLearned1");
            Assets.lessonLearned2 = content.Load<Texture2D>("Assets/Images/Level/Cinematic/lessonLearned2");
            Assets.lessonLearned3 = content.Load<Texture2D>("Assets/Images/Level/Cinematic/lessonLearned3");
            Assets.lessonLearned4 = content.Load<Texture2D>("Assets/Images/Level/Cinematic/lessonLearned4");

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
        // Character IDs
        internal static readonly byte
            unknown = 0,
            pickles = 1,
            faun = 2,
            otto = 3,
            angel = 4,
            micah = 5,
            smokey = 6;

        // Text IDs
        internal static readonly int
            noName = -1;
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
                Line(unknown, "Last night, I got this letter...", innerThought),
                Line(unknown, "It said \"Congratulations! You have won $1 000 000 dollars!\"", innerThought),
                Line(unknown, "...Yeah, as if.", innerThought),
                Line(pickles, "My name is Pickles. I'm a renowned detective.", innerThought),
                Line(pickles, "In my line of work... I make a decent amount of money.", innerThought),
                Line(pickles, "But this?...", innerThought),
                Line(pickles, "I'm gonna get to the bottom of this.", innerThought),
                Line(pickles, "Besides... Who would fall for a scheme like this?\nI've never seen something so outrageous.", innerThought)
            };

            intro2 = new DialogString[]{
                Line(pickles, "Well, here I am."),
                Line(pickles, "This place's address was at the bottom of the letter.", innerThought),
                Line(pickles, "Let's hope this wasn't a waste of my time."),
                Line(noName, "In this game, you'll have to take full advantage of your surroundings.", tutorial),
                Line(noName, "That means using the mouse to interact with objects\nthat are full of mystery, or might contain clues.", tutorial),
                Line(noName, "Go on, try moving your mouse to interact with the door!", tutorial)
            };

            intro2a = new DialogString[]{
                Line(pickles, "Huh... That's weird."),
                Line(pickles, "The door's unlocked."),
                Line(pickles, "I guess I'll just have to head inside.\nI'll be sure to keep my guard up...")
            };

            preludeEnd = new DialogString[]{
                Line(pickles, "When I stepped foot inside... The house didn't look unkept.", innerThought),
                Line(pickles, "Despite it's shoddy look from outside...\nIt was as though someone really was waiting for me to arrive.", innerThought),
                Line(pickles, "Without hesitation, I called out into the large foyer,\nand waited for a response.", innerThought),
                Line(pickles, "Anyone home?"),
                Line(unknown, "Um... Hi..."),
                Line(pickles, "Huh?"),
                Line(unknown, "So you... got the same letter as us."),
                Line(pickles, "Yeah. I'm guessing I'm not the only one, then."),
                Line(unknown, "B-before we go, I should probably introduce myself..."),
                Line(faun, "I'm Faun. I got here first. I-It's nice to meet you..."),
                Line(pickles, "The name's Pickles. Detective Pickles.\nI'm here on behalf of my work investigating this..."),
                Line(faun, "...Weirdly obvious scam?"),
                Line(pickles, "Yeah. Any info about that?"),
                Line(faun, "W-well, I got here at about " + Global.faunArrivedTime + ".."),
                Line(pickles, "Since I arrived at " + Global.picklesArrivedTime + ", that would mean you got here\n10 minutes before me."),
                Line(faun, "Come with me... the others are waiting.\nI'll explain everything in the living room."),
                Line(pickles, "The others...", innerThought),
                Line(pickles, "Alright.")
            };

            chapter1part1 = new DialogString[]{
                Line(unknown, "'The hell? Who's this tail licker?"),
                Line(pickles, "Pickles. Detective P-"),
                Line(unknown, "Yeah yeah, whatever. You here for the money?"),
                Line(pickles, "...Yes. I'm here to uncover the true meaning behind\nthe letter I received in the mail."),
                Line(pickles, "This guy's got some nerve...", innerThought),
                Line(unknown, "Look pal, the dough ain't here. Whole thing's a scam."),
                Line(pickles, "I figured so. Do you know more about the situation?"),
                Line(unknown, "'The hell do you think I'm gonna tell you that for?\nYou tryna take the money from me?"),
                Line(faun, "O-Otto... He's just as confused as the rest of us... So..."),
                Line(otto, "... *sigh* Fine. You're lucky the li'l lady is here.\nOtherwise I woulda-"),
                Line(faun, "Otto...!"),
                Line(otto, "Oh, nevermind..."),
                Line(pickles, "First, I'd like to know why the door was left unlocked."),
                Line(pickles, "It seems... hazardous to leave this seemingly abandoned house just...\nunlocked. For anyone to come inside."),
                Line(unknown, "Well, we all thought that it would be a good idea\nto leave the door as we found it."),
                Line(pickles, "Ah, I see. And you are?"),
                Line(angel, "Angel. I may be past my prime, but back in my day...\nI was quite the lady."),
                Line(pickles, "Despite the overlooming feeling of confusion, dread,\nand mystery...", innerThought),
                Line(pickles, "I felt as though we were all becoming closer.", innerThought),
                Line(pickles, "There was a cheerful feeling to the room's atmosphere.", innerThought),
                Line(pickles, "It's nice to meet you, Angel, Faun..."),
                Line(pickles, "Are you the only people here?"),
                Line(faun, "N-No... This house..."),
                Line(faun, "Has a host."),
                Line(pickles, "A host?", innerThought),
                Line(pickles, "What... does she mean by that?", innerThought),
                Line(pickles, "What do you mean, host?"),
                Line(pickles, "The room went silent.\nOtto looked incredibly frustrated.", innerThought),
                Line(pickles, "And just like that... That cheerfulness was gone.", innerThought),
                Line(faun, "Detective, I-I can show you around, if... if it's not too much trouble..."),
                Line(pickles, "Sure. That would be nice."),
                Line(noName, "Faun wants to show you around? How kind of her!", tutorial),
                Line(noName, "This is where the game really opens up.\nExplore the world- *ahem* house... to your heart's content.", tutorial),
                Line(noName, "Keep an eye on the map in the top right corner.\nIt'll help you keep track of where you are.", tutorial)
            };

            chapter1bathroom = new DialogString[]{
                Line(pickles, "This bathroom is really nice, actually."),
                Line(pickles, "I wouldn't mind taking a shit in here... Hehehe."),
                Line(faun, "....."),
                Line(pickles, "She didn't like that...", innerThought),
                Line(pickles, "Hey... Any other rooms to show me?")
            };

            chapter1kitchen = new DialogString[]{
                Line(faun, "T-this is the kitchen... It has everything you'd expect.\nSilverware, appliances, and more..."),
                Line(pickles, "Huh. You'd really expect this place to be more run down..."),
                Line(pickles, "Hey, Faun."),
                Line(faun, "Mm? W-who, me?"),
                Line(pickles, "Yeah, I've been thinking about the host you mentioned.\nWho is he?"),
                Line(faun, "W-well, he's certainly young looking..."),
                Line(faun, "He's no older than 20..."),
                Line(pickles, "Howcome I haven't seen him yet?"),
                Line(faun, "A-ah, that's because he announced the \"game\" moments before\nyou arrived."),
                Line(pickles, "The game...?"),
                Line(faun, "Otto wasn't very happy, but the strange man said\nhe would \"await your arrival...\""),
                Line(pickles, "Who the hell is this host, treating this scam like some kind\nof dinner party?", innerThought)
            };

            chapter1closet = new DialogString[]{
                Line(unknown, "Mmm... What time is it...?"),
                Line(faun, "M-Micah, you know you shouldn't be sleeping in the closet..."),
                Line(micah, "Oh d-d-dear! My apologies...!"),
                Line(pickles, "It's fine. I'm still getting adjusted, so-"),
                Line(micah, "Cortisol levels rising. Begin the diaphragmatic process."),
                Line(pickles, "...Huh?"),
                Line(pickles, "I didn't catch a word of what she said.", innerThought),
                Line(micah, "O-oh, sorry, I didn't explain properly, did I?"),
                Line(micah, "In case you didn't know, diaphragmatic breathing exercises\ncan help you relax."),
                Line(pickles, "I, uh..."),
                Line(faun, "M-Micah! He has no idea what you're talking about..."),
                Line(micah, "WHAT?! Ohmygoodness I am SO sorry!"),
                Line(pickles, "Really, it's fine..."),
                Line(micah, "No! It's not fine!"),
                Line(micah, "Allow me to start from the top. *ahem*"),
                Line(pickles, "The sooner I get out of this closet, the better.", innerThought)
            };

            chapter1part2 = new DialogString[]{
                Line(unknown, "I'm glad to see you all get acquainted with one another!"),
                Line(pickles, "That voice... It doesn't sound close to the nervousness\neveryone else is feeling.", innerThought),
                Line(faun, "That's-!"),
                Line(unknown, "Meeee!"),
                Line(pickles, "Who the hell..."),
                Line(otto, "You again?! I thought I told you to fuck off for good, ya douche!"), // Holy edgy
                Line(unknown, "Ah, but I just couldn't help myself...!\nI was getting jealous of all your smiles! Your joy!"),
                Line(pickles, "Are you the host here..?"),
                Line(unknown, "But of course I am!"),
                Line(smokey, "Smokeston Novikov, at your service!\nBut you can just call me Smokey. I'd prefer that!"),
                Line(pickles, "Smokeston... Where have I heard that name?", innerThought),
                Line(otto, "If you can't understand my words..."),
                Line(otto, "I'LL JUST HAVE TO BEAT THE SENSE INTO YA!"),
                Line(faun, "No...! OTTO!"),
                Line(smokey, "Woah woah woah! I wouldn't do that if I were you...!"),
                Line(smokey, "My guts will go allllll over the place if you-"),
                Line(pickles, "The sound of a sharp punch filled the room before he could finish.", innerThought),
                Line(pickles, "It was as though pure iron was forced through something\nsoft and squishy.", innerThought),
                Line(pickles, "And when my eyes finally adjusted...", innerThought),
                Line(pickles, "There he was. Oozing with blood.", innerThought),
                Line(faun, "KYAAAAAAAAAAAAAAAAAA!"),
                Line(otto, "How'd ya like that, asshole?! I told you, don't fuck with me!"),
                Line(pickles, "I couldn't believe what I just witnessed.\nOtto, who was completely innocent prior to the argument he started...", innerThought),
                Line(pickles, "Punched a hole through Smokeston.", innerThought),
                Line(smokey, "Oh... This isn't good... Not at all..."),
                Line(pickles, "Why hasn't he passed out yet? Losing that much blood should've\nkilled him, let alone the fatal injury he clearly suffers.", innerThought),
                Line(smokey, "Aw, it's all over the furniture... Not cool, Otto!\nI had high hopes for you..."),
                Line(otto, "The fuck? Is that not enough for..."),
                Line(otto, "..."),
                Line(pickles, "Otto suddenly stopped speaking.", innerThought),
                Line(pickles, "I didn't have the courage to ask him why, but...\nWhen I looked over at Smokeston...", innerThought),
                Line(pickles, "...", innerThought),
                Line(pickles, "...", innerThought),
                Line(pickles, "Two unbelievable events happening, back to back.", innerThought),
                Line(pickles, "I should be very careful around either of them. For now,\nFaun and the others seem reliable enough.", innerThought),
                Line(smokey, "Where was I...? Oh, right."),
                Line(smokey, "I'm the host of this game... treat me with a little respect, thank you."),
                Line(otto, "..."),
                Line(faun, "..."),
                Line(angel, "..."),
                Line(micah, "..."),
                Line(pickles, "We were all in shock.", innerThought),
                Line(pickles, "Minutes began to feel like hours.\nNone of us knew what to say about anything we had just witnessed.", innerThought),
                Line(smokey, "... *sigh* So much for a perfect opening night."),
                Line(smokey, "We'll take five.\nThen I want you all to come see the discovery I've made."),
                Line(pickles, "...Discovery...?"),
                Line(smokey, "Well yeah, duh! What kind of game would this be without an objective?"),
                Line(smokey, "A hero without a princess to save... he's just a nobody, right?"),
                Line(faun, "S-so, y-you were serious about this \"game\" y-you've prepared..."),
                Line(micah, "According to my calculations, he's not lying..."),
                Line(micah, "When he announced it at " + Global.gameStartTime + ", I checked his pulse.\nHe was dead serious."),
                Line(angel, "I'm afraid I'm just too old for social experiments like this.\nYou younger generations disgust me."),
                Line(otto, "Yo, doctor, how do you know that's not bullshit?\nFor all we know, that pulse you felt could've been fake."),
                Line(otto, "I mean, shit, you saw what happened when I lost control."),
                Line(otto, "I don't have the brains to orchestrate something as beautiful as that.\nTrust me."),
                Line(faun, "*sniff* I w-want to g-get out of here... I-I don't like this...!"),
                Line(pickles, "...What the fuck?", innerThought),
                Line(pickles, "What... is this?"),
                Line(pickles, "I didn't question it...\nBut none of us had left the old house.", innerThought),
                Line(pickles, "I couldn't tell you why...", innerThought),
                Line(pickles, "But that would soon turn out to be our biggest mistake.", innerThought)
            };
        }

        // Create Dialog String
        public static DialogString Line(int name, string text, int textColor = 0)
        {
            return new DialogString(name, text, textColor);
        }
    }
}
