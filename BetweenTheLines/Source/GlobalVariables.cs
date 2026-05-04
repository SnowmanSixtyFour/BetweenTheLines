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

        // Debug
        public static bool debug = true;

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
            // Time Events
            picklesArrivedTime, // Pickles Arrived at House
            faunArrivedTime, // Faun Arrived at House
            gameStartTime, // Game Started Officially
            arthurKilledTime; // Arthur Killed

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
            rescanLineVisible = false,

            menuAnimations = true,
            backculling = false;

        // GUI Colors
        public static Color titleColor = Color.LightGray;

        // Dialog

        // Text Speed
        public static float
            defaultDialogSpeed = 0.02f, // Normal - 20 Milliseconds
            fastDialogSpeed = 0.01f; // Fast - 10 Milliseconds

        public static float dialogSpeed;
    }

    // Store Game Soundtrack in OST
    internal class OST
    {
        public static Song
            title,
            intro,
            chillout,
            intense,
            trialIntro, trial;
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
            corpse,

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

            Assets.corpse = content.Load<Texture2D>("Assets/Images/Level/Cinematic/corpse");

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

            // Music
            OST.title = content.Load<Song>("Assets/Audio/Music/Title");
            OST.intro = content.Load<Song>("Assets/Audio/Music/Intro");
            OST.chillout = content.Load<Song>("Assets/Audio/Music/Chillout");
            OST.intense = content.Load<Song>("Assets/Audio/Music/Intense");

            OST.trialIntro = content.Load<Song>("Assets/Audio/Music/TrialIntro");
            OST.trial = content.Load<Song>("Assets/Audio/Music/Trial");

            // Fonts

            // Default
            Assets.arial = content.Load<SpriteFont>("Assets/Fonts/Arial");

            // Shaders

            // CRT Filter
            Assets.crt = content.Load<Effect>("Assets/Shaders/CRTFilter");
        }
    }

    // Game Dialog
    internal class Dialog
    {
        // Character IDs
        internal static readonly byte
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

        // Portrait Poses
        internal static readonly byte
            regular = 0,
            thinking = 1,
            worried = 2,
            angry = 3,
            excited = 4,
            creepy = 5;

        public static bool picklesVisible = true;

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
            chapter1part2,

            // Investigation
            chapter1investigation, // Intro
            chapter1evidence1, chapter1evidence2, chapter1evidence3, // Evidence
            chapter1investigationEnd, // Buildup to Trial

            // Trial
            chapter1trial1, chapter1trial2, chapter1trial3,

            chapter1trial1question, chapter1trial2question, chapter1trial3question,
            chapter1trial1right, chapter1trial1wrong, chapter1trial2right, chapter1trial2wrong, chapter1trial3right, chapter1trial3wrong,

            chapter1postTrial;

        public static void LoadDialog()
        {
            // NOTE: Lines of dialog are formatted like this:
            // Integer number Name | String Text

            // Intro

            intro1 = new DialogString[]{
                Line(pickles, "Yesterday, I got this letter...", innerThought, hideName: true),
                Line(pickles, "It said \"Congratulations! You have won $1 000 000 dollars!\"", innerThought, hideName: true),
                Line(pickles, "Signed by someone known as \"Novikov.\"", innerThought, hideName: true),
                Line(pickles, "...Yeah, as if.\nHowever, the name sounds familiar...", innerThought, hideName: true),
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
                Line(faun, "Um... Hi...", hideName: true),
                Line(pickles, "Huh?"),
                Line(faun, "So you... got the same letter as us.", hideName: true),
                Line(pickles, "Yeah. I'm guessing I'm not the only one, then."),
                Line(faun, "B-before we go, I should probably introduce myself...", hideName: true),
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
                Line(otto, "'The hell? Who's this tail licker?", hideName: true),
                Line(pickles, "Pickles. Detective P-"),
                Line(otto, "Yeah yeah, whatever. You here for the money?", state: angry, hideName: true),
                Line(pickles, "...Yes. I'm here to uncover the true meaning behind\nthe letter I received in the mail."),
                Line(pickles, "This guy's got some nerve...", innerThought),
                Line(otto, "Look pal, the dough ain't here. Whole thing's a scam.", hideName: true),
                Line(pickles, "I figured so. Do you know more about the situation?"),
                Line(otto, "'The hell do you think I'm gonna tell you that for?\nYou tryna take the money from me?", state: angry, hideName: true),
                Line(faun, "O-Otto... He's just as confused as the rest of us... So...", state: worried),
                Line(otto, "... *sigh* Fine. You're lucky the li'l lady is here.\nOtherwise I woulda-"),
                Line(faun, "Otto...!"),
                Line(otto, "Oh, nevermind..."),
                Line(pickles, "First, I'd like to know why the door was left unlocked."),
                Line(pickles, "It seems... hazardous to leave this seemingly abandoned house just...\nunlocked. For anyone to come inside."),
                Line(angel, "Well, we all thought that it would be a good idea\nto leave the door as we found it.", state: thinking, hideName: true),
                Line(pickles, "Ah, I see. And you are?"),
                Line(angel, "Angel. I may be past my prime, but back in my day...\nI was quite the lady."),
                Line(pickles, "Despite the overlooming feeling of confusion, dread,\nand mystery...", innerThought),
                Line(pickles, "I felt as though we were all becoming closer.", innerThought),
                Line(pickles, "There was a cheerful feeling to the room's atmosphere.", innerThought),
                Line(pickles, "It's nice to meet you, Angel, Faun..."),
                Line(pickles, "Are you the only people here?"),
                Line(faun, "N-No... This house...", state: worried),
                Line(faun, "Has a host.", state: worried),
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
                Line(faun, ".....", state: worried),
                Line(pickles, "She didn't like that...", innerThought),
                Line(pickles, "Hey... Any other rooms to show me?")
            };

            chapter1kitchen = new DialogString[]{
                Line(faun, "T-this is the kitchen... It has everything you'd expect.\nSilverware, appliances, and more..."),
                Line(pickles, "Huh. You'd really expect this place to be more run down..."),
                Line(pickles, "Hey, Faun."),
                Line(faun, "Mm? W-who, me?", state: worried),
                Line(pickles, "Yeah, I've been thinking about the host you mentioned.\nWho is he?"),
                Line(faun, "W-well, he's certainly young looking..."),
                Line(faun, "He's no older than 20..."),
                Line(pickles, "Howcome I haven't seen him yet?"),
                Line(faun, "A-ah, that's because he announced the \"game\" moments before\nyou arrived.", state: worried),
                Line(pickles, "The game...?"),
                Line(faun, "Otto wasn't very happy, but the strange man said\nhe would \"await your arrival...\""),
                Line(pickles, "Who the hell is this host, treating this scam like some kind\nof dinner party?", innerThought)
            };

            chapter1closet = new DialogString[]{
                Line(micah, "Mmm... What time is it...?", hideName: true),
                Line(faun, "M-Micah, you know you shouldn't be sleeping in the closet...", state: worried),
                Line(micah, "Oh d-d-dear! My apologies...!"),
                Line(pickles, "It's fine. I'm still getting adjusted, so-"),
                Line(micah, "Your cortisol levels are rising. Begin the diaphragmatic process."),
                Line(pickles, "...Huh?"),
                Line(pickles, "I didn't catch a word of what she said.", innerThought),
                Line(micah, "O-oh, sorry, I didn't explain properly, did I?"),
                Line(micah, "In case you didn't know, diaphragmatic breathing exercises\ncan help you relax."),
                Line(pickles, "I, uh..."),
                Line(faun, "M-Micah! He has no idea what you're talking about...", state: worried),
                Line(micah, "WHAT?! Ohmygoodness I am SO sorry!"),
                Line(pickles, "Really, it's fine..."),
                Line(micah, "No! It's not fine!"),
                Line(micah, "Allow me to start from the top. *ahem*"),
                Line(pickles, "The sooner I get out of this closet, the better.", innerThought)
            };

            chapter1part2 = new DialogString[]{
                Line(smokey, "I'm glad to see you all get acquainted with one another!", state: excited, hideName: true),
                Line(pickles, "That voice... It doesn't sound close to the nervousness\neveryone else is feeling.", innerThought),
                Line(faun, "That's-!", state: worried),
                Line(smokey, "Meeee!", state: excited, hideName: true),
                Line(pickles, "Who the hell..."),
                Line(otto, "You again?! I thought I told you to fuck off for good, ya douche!"),
                Line(smokey, "Ah, but I just couldn't help myself...!\nI was getting jealous of all your smiles! Your joy!", hideName: true),
                Line(pickles, "Are you the host here..?"),
                Line(smokey, "But of course I am!", state: excited, hideName: true),
                Line(smokey, "Smokeston Novikov, at your service!\nBut you can just call me Smokey. I'd prefer that!"),
                Line(pickles, "Smokeston... Where have I heard that name?", innerThought),
                Line(otto, "If you can't understand my words..."),
                Line(otto, "I'LL JUST HAVE TO BEAT THE SENSE INTO YA!"),
                Line(faun, "No...! OTTO!", state: worried),
                Line(smokey, "Woah woah woah! I wouldn't do that if I were you...!"),
                Line(smokey, "My guts will go allllll over the place if you-", state: excited),
                Line(pickles, "The sound of a sharp punch filled the room before he could finish.", innerThought),
                Line(pickles, "It was as though pure iron was forced through something\nsoft and squishy.", innerThought),
                Line(pickles, "And when my eyes finally adjusted...", innerThought),
                Line(pickles, "There he was. Oozing with blood.", innerThought),
                Line(faun, "KYAAAAAAAAAAAAAAAAAA!", state: worried),
                Line(otto, "How'd ya like that, asshole?! I told you, don't fuck with me!"),
                Line(pickles, "I couldn't believe what I just witnessed.\nOtto, who was completely innocent prior to the argument he started...", innerThought),
                Line(pickles, "Punched a hole through Smokeston.", innerThought),
                Line(smokey, "Oh... This isn't good... Not at all...", state: creepy),
                Line(pickles, "Why hasn't he passed out yet? Losing that much blood should've\nkilled him, let alone the fatal injury he clearly suffers.", innerThought),
                Line(smokey, "Aw, it's all over the furniture... Not cool, Otto!\nI had high hopes for you...", state: creepy),
                Line(otto, "The fuck? Is that not enough for...", state: angry),
                Line(otto, "..."),
                Line(pickles, "Otto suddenly stopped speaking.", innerThought),
                Line(pickles, "I didn't have the courage to ask him why, but...\nWhen I looked over at Smokeston...", innerThought),
                Line(pickles, "...", innerThought),
                Line(pickles, "...", innerThought),
                Line(pickles, "Two unbelievable events happening, back to back.", innerThought),
                Line(pickles, "I should be very careful around either of them. For now,\nFaun and the others seem reliable enough.", innerThought),
                Line(smokey, "Where was I...? Oh, right.", state: creepy),
                Line(smokey, "I'm the host of this game... treat me with a little respect, thank you.", state: creepy),
                Line(otto, "..."),
                Line(faun, "...", state: worried),
                Line(angel, "...", state: thinking),
                Line(micah, "..."),
                Line(pickles, "We were all in shock.", innerThought),
                Line(pickles, "Minutes began to feel like hours.\nNone of us knew what to say about anything we had just witnessed.", innerThought),
                Line(smokey, "... *sigh* So much for a perfect opening night."),
                Line(smokey, "We'll take five.\nThen I want you all to come see the discovery I've made."),
                Line(pickles, "...Discovery...?"),
                Line(smokey, "Well yeah, duh! What kind of game would this be without an objective?", state: excited),
                Line(smokey, "A hero without a princess to save... he's just a nobody, right?"),
                Line(faun, "S-so, y-you were serious about this \"game\" y-you've prepared...", state: worried),
                Line(micah, "According to my calculations, he's not lying..."),
                Line(micah, "When he announced it at " + Global.gameStartTime + ", I checked his pulse.\nHe was dead serious."),
                Line(angel, "I'm afraid I'm just too old for social experiments like this.\nYou younger generations disgust me.", state: thinking),
                Line(otto, "Yo, doctor, how do you know that's not bullshit?\nFor all we know, that pulse you felt could've been fake."),
                Line(otto, "I mean, shit, you saw what happened when I lost control."),
                Line(otto, "I don't have the brains to orchestrate something as beautiful as that.\nTrust me."),
                Line(faun, "*sniff* I w-want to g-get out of here... I-I don't like this...!", state: worried),
                Line(pickles, "...What the fuck?", innerThought),
                Line(pickles, "What... is this?"),
                Line(pickles, "I didn't question it...\nBut none of us had left the old house.", innerThought),
                Line(pickles, "I couldn't tell you why...", innerThought),
                Line(pickles, "But that would soon turn out to be our biggest mistake.", innerThought)
            };

            chapter1investigation = new DialogString[]{
                Line(pickles, "Smokey brought us to the staircase at the end\nof the main hall.", innerThought),
                Line(pickles, "What we found there surprised us all...", innerThought),
                Line(pickles, "Is that...?"),
                Line(pickles, "...", innerThought),
                Line(pickles, "I couldn't believe what I was looking at.", innerThought),
                Line(smokey, "For your first task, I want you to identify the body."),
                Line(smokey, "You should be good at that, right detective?"),
                Line(otto, "That's... Shit..."),
                Line(otto, "Arthur... No fuckin' way...!"),
                Line(faun, "W-w-wha... T-this..."),
                Line(micah, "Oh dear... A-Arthur...!"),
                Line(angel, "..."),
                Line(pickles, "A body...", innerThought),
                Line(pickles, "A real dead body was in front of me.", innerThought),
                Line(smokey, "After enough time has passed, well..."),
                Line(smokey, "I'll fill you all in on the rest afterwards."),
                Line(pickles, "I thought I'd be more prepared... but this was my first time\nhaving to deal with anything like this.", innerThought),
                Line(faun, "N-no... NO!"),
                Line(faun, "W-who is that..?! And W-why is he...!"),
                Line(pickles, "Faun... I can't imagine how she's feeling.", innerThought),
                Line(pickles, "And what the hell does Smokey have planned? I can't leave\nthis place until I figure out his goals...", innerThought),
                Line(pickles, "Get it together, Pickles... Let's investigate.", innerThought),
            };

            chapter1evidence1 = new DialogString[] {
                Line(pickles, "Test 1")
            };

            chapter1evidence2 = new DialogString[] {
                Line(faun, "Test 2")
            };

            chapter1evidence3 = new DialogString[] {
                Line(pickles, "Test 3")
            };

            chapter1investigationEnd = new DialogString[]{
                Line(smokey, "That's all there is to the crime scene, right?"),
                Line(pickles, "W...What?"),
                Line(pickles, "I slowly felt my patience thinning. In that moment...", innerThought),
                Line(pickles, "Smokeston, I came here because of the scam you sent each of us.\nAnd if you don't wipe that cheerful attitude off your face..."),
                Line(smokey, "Ah, you're just as they describe you, Pickles! Always so tense."),
                Line(pickles, "Shut the hell up. I've had it with this.\nThis is a living, breathing person I'm looking at..."),
                Line(pickles, "And you expect me to entertain any more of this?"),
                Line(smokey, "Well... Not breathing any more, but..."),
                Line(smokey, "Won't you all humour me a little while longer?\nThe fun part is about to begin!"),
                Line(faun, "F-fun...? Oh, no..."),
                Line(otto, "I feel you, cat. This shit has gone too far."),
                Line(faun, "*sniff* mm..."),
                Line(angel, "Smokeston. What do you gain from this experience?"),
                Line(smokey, "What do I... gain?"),
                Line(angel, "You are clearly messing with their heads here.\nJust drop the haunted house act."),
                Line(micah, "M-Ms. Angel, this isn't... this isn't fake."),
                Line(angel, "Doctor, I would have expected you to be no more foolish\nthan the rest of the young boys and girls here."),
                Line(pickles, "I'm... not that young..."),
                Line(otto, "The hell, old lady? Have you gone senile!?\nCan't you tell the difference between fiction and reality?!"),
                Line(angel, "And you, Otto. Clearly we are from two different worlds.\nWe simply cannot see eye to eye."),
                Line(otto, "You got that right, you old hag!"),
                Line(micah, "What do you suggest we do now, Ms. Angel?"),
                Line(angel, "There's no need for the authorities, unless our host decides\nto use force on us."),
                Line(angel, "You kids are obsessed with that AI nonsense these days.\nI'm sure Novikov has his ways of fooling us."),
                Line(smokey, "Comparing me to AI... Why that's just...!"),
                Line(smokey, "I'M NOTHING LIKE THAT GENERATIVE FILTH!\nI'm a work of art, man-made!"),
                Line(smokey, "Everything you've seen up until this point is real!\n100% real! As real as it could be!"),
                Line(micah, "I must say, if Ms. Angel is right, Mr. Novikov possesses\ngenerative AI far beyond what we consider advanced."),
                Line(micah, "Could it be...? Is he really, a true artificial intelligence?"),
                Line(micah, "In which case, I wouldn't mind dissecting him...\nSending him to my lab... Seeing what makes him tick...!"),
                Line(otto, "Bahaha! Where's your device hiding at?!\nSpin around, let the cat do a body check!"),
                Line(smokey, "I'm... ..."),
                Line(smokey, "I've never felt more hurt in my life! For the last time,\nI am NOT a machine!!"),
                Line(smokey, "If you all truly believe that this experience is fictional...\nThen you wouldn't mind walking with me."),
                Line(pickles, "..."),
                Line(otto, "..."),
                Line(faun, "..."),
                Line(angel, "...Oh, alright.\nWe shall humour you just a little longer."),
                Line(smokey, "Oh, splendid! Amazing! Right this way!\nOur little game is about to get so much bigger!"),
                Line(pickles, "It's all fake, right?", innerThought),
                Line(pickles, "Right...?", innerThought),
            };

            chapter1trial1 = new DialogString[]{
                Line(smokey, "Okay! Now that we're all here, let me explain how the debate works."),
                Line(smokey, "Each of you in this room will talk amongst yourselves, and me,\nabout who the killer might be!", state: excited),
                Line(pickles, "The killer is... one of us?"),
                Line(smokey, "Well, duh! I wouldn't kill without reason!"),
                Line(pickles, "I'm just not following. He expects us to believe that?", innerThought),
                Line(smokey, "I had a prime opportunity to kill Otto earlier for\nhow he disrespected me...", state: creepy),
                Line(smokey, "But I'm too kind to do that!", state: excited),
                Line(otto, "You gotta be kidding me..."),
                Line(smokey, "Now, let's begin with the debate."),
                Line(smokey, "Detective Pickles... Can you tell us about the body?"),
                Line(pickles, "Sure. Arthur's time of death was at " + Global.arthurKilledTime + "."),
                Line(pickles, "His cause of death was a broken neck,\nafter an impact from quite a decent height up."),
                Line(pickles, "Micah was able to perform an autopsy. And her results were..."),
                Line(micah, "Arthur was definitely killed at " + Global.arthurKilledTime + ". I'm sure of it."),
                Line(micah, "Another detail I noticed was a large bruise on his face,\nspecifically on his left cheek."),
                Line(micah, "It was a puncture wound, albeit not the cause of death...\nDespite how much blood he lost from it."),
                Line(faun, "W-well, now that w-we know the t-time... We can pinpoint the k-k-killer.", state: worried),
                Line(smokey, "Nice observation, Faun! To give swift justice to the killer,\nwe must find out whodunnit!", state: excited),
                Line(pickles, "So the general consensus, judging by the crime scene..."),
                Line(pickles, "Is that Arthur's death was caused by falling from the flight of stairs\nleading to the second floor. Then-"),
                Line(otto, "Sometime before " + Global.arthurKilledTime + ", he got socked in the face!"),
                Line(pickles, "That's... one way you could put it. But yes, that's how it played out."),
                Line(pickles, "Now the question is... Why?"),
                Line(otto, "..."),
                Line(micah, "..."),
                Line(angel, "If I may add to the consensus, detective...", state: thinking),
                Line(pickles, "Go ahead. We need as much information as we can get."),
                Line(angel, "Thank you, darling. I would like to declare that Me and Faun\nare perfectly in the clear."),
                Line(faun, "A-Angel! T-they're not going to believe that...!", state: worried),
                Line(faun, "I-It is true, I-I would never...k-k-kill..someone...\nBut neither would you..."),
                Line(faun, "We have to convince them...!", state: worried),
                Line(angel, "The proof is in the evidence we already know.", state: thinking),
                Line(pickles, "It is..?", innerThought),
                Line(angel, "Me and Faun arrived at the same time.\nWould you happen to remember when that is?"),
                Line(noName, "Angel is asking you a tough question...", tutorial),
                Line(noName, "During debates, you'll have to choose your answer,\nbut think carefully about it.", tutorial),
                Line(noName, "You will be given a single right answer, and multiple wrong ones.", tutorial)
            };

            chapter1trial1question = new DialogString[]{
                Line(pickles, "That's right...\nFaun told me as soon as I arrived...! I should know this..."),
                Line(pickles, "It was 10 minutes before I arrived, which should have been at...")
            };

            chapter1trial1right = new DialogString[]{
                Line(pickles, "That's right. Faun arrived at " + Global.faunArrivedTime + "."),
                Line(angel, "And when Faun arrived, I had arrived at the same time.\nIsn't that right, Faun?"),
                Line(faun, "Y-yes! That's completely true...!"),
                Line(faun, "If A-Arthur really was k-killed at " + Global.arthurKilledTime + "...\nHow could m-me or Angel have done it...?"),
                Line(angel, "We both met up outside the building at " + Global.faunArrivedTime + "."),
                Line(pickles, "Meaning that you two couldn't have possibly killed Arthur..."),
                Line(otto, "Hang on a sec...!"),
                Line(otto, "How do we know that's not bullshit?!", state: angry),
                Line(pickles, "..."),
                Line(otto, "For all we know, one of them... or both of them...\nCould be lying!", state: angry),
                Line(angel, "Otto. Do you, or anyone else remember seeing me,\nor Faun before " + Global.faunArrivedTime + "?", state: angry),
                Line(otto, "Well...no..."),
                Line(otto, "Damn it... You're in the clear for now!", state: angry),
                Line(pickles, "Otto's a real troublemaker... Isn't he?", innerThought)
            };

            chapter1trial1wrong = new DialogString[]{
                Line(faun, "...No...", state: worried),
                Line(angel, "Now that's not right at all.", state: thinking),
                Line(pickles, "I was... wrong?\nI've got to think this through, one more time...", innerThought)
            };

            chapter1trial2 = new DialogString[]{
                Line(smokey, "I believe there's a critical part of this case that none of you have\nthought to mention!", state: excited),
                Line(pickles, "I was trying to avoid it... Because I just can't believe...", innerThought),
                Line(pickles, "...No, that's not it. I can believe it.\nI just don't want to...", innerThought),
                Line(otto, "Oh yeah? And what would that be?"),
                Line(smokey, "The syringe found at the body."),
                Line(otto, "..."),
                Line(faun, "...", state: worried),
                Line(pickles, "The atmosphere got intense.", innerThought),
                Line(pickles, "None of us wanted to accept the reality that Micah,\nour friend... could have killed Arthur.", innerThought),
                Line(pickles, "Micah, I want to hear your thoughts on the matter."),
                Line(micah, "N-now, I know what it looks like...!"),
                Line(faun, "M-Micah... I believe you...", state: worried),
                Line(faun, "Y-You would n-never...never do something like that...!"),
                Line(otto, "Oh, yeah? How do we know for sure?", state: angry),
                Line(angel, "I hate to agree with Otto... But it's too soon to decide\nwhether Micah is safe.", state: thinking),
                Line(micah, "Wait... Nonono, I can explain!"),
                Line(micah, "You guys aren't going to believe this... but..."),
                Line(micah, "I never put that syringe there!"),
                Line(otto, "Oh yeah, you punk?! You expect us to just sit here,\nand listen to your lies because of your PhD?!", state: angry),
                Line(micah, "M-my PhD has nothing to do with this...! You scumbucket!"),
                Line(otto, "What... What the fuck did you call me?", state: angry),
                Line(pickles, "Micah, although I want to believe you..."),
                Line(pickles, "You've got to explain your reasoning. Why would such\na medical device be found by the body?"),
                Line(micah, "Because the killer is trying to frame me..."),
                Line(micah, "Put the medical equipment next to the body... Fill it with anesthetics...\nMake it look like the smart one did it!"),
                Line(pickles, "M-Micah... What was that just now?"),
                Line(micah, "Hm?"),
                Line(pickles, "How would you know the syringe had anesthetics in it?"),
                Line(micah, "Wawawaaa?!"),
                Line(otto, "Oh, shit! Good work scruffy!"),
                Line(pickles, "Listen, Micah... I'm not blaming you immediately."),
                Line(pickles, "I've been hollered at in the workplace for being too soft.\nThat's why I'm giving you the benefit of the doubt."),
                Line(micah, "Good, because Ididn'tdoit!!"),
                Line(pickles, "Slow down..."),
                Line(pickles, "Explain your reasoning.")
            };

            chapter1trial2question = new DialogString[]{
                Line(pickles, "Test")
            };

            chapter1trial2right = new DialogString[]{
                Line(pickles, "Test")
            };

            chapter1trial2wrong = new DialogString[]{
                Line(pickles, "Test")
            };

            chapter1trial3 = new DialogString[]{
                Line(pickles, "Test")
            };

            chapter1trial3question = new DialogString[]{
                Line(pickles, "Test")
            };

            chapter1trial3right = new DialogString[]{
                Line(pickles, "Test")
            };

            chapter1trial3wrong = new DialogString[]{
                Line(pickles, "Test")
            };

            chapter1postTrial = new DialogString[]{
                Line(pickles, "Test")
            };
        }

        // Create Dialog String
        public static DialogString Line(int name, string text, int textColor = 0, int state = 0, bool hideName = false)
        {
            return new DialogString(name, text, textColor, state, hideName);
        }
    }
}
