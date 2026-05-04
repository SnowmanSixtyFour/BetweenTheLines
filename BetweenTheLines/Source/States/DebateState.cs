using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using BetweenTheLines;
using BetweenTheLines.Source;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Objects.Level;
using BetweenTheLines.Source.Objects.GUI;
using System.Diagnostics;

namespace BetweenTheLines.Source.States
{
    internal class DebateState : State
    {
        // Dialog
        private DialogBox dialogBox;
        private Portrait portrait;

        // Sprites
        private StaticSprite BG;

        // Sprite Scroll Speeds
        private float
            bgScrollSpeed = 40.0f; // BG Speed

        // 3D Background
        private VertexPositionColor[] mapVertices;
        private VertexBuffer vertexBuffer;
        private _3DCamera bgCam;
        private int amountOfVertices = 5,
            floorWidth = 500, floorY = -100;

        // Interactivity
        private bool
            multipleChoice = false,

            answerSelected = false, // When Answer Dialog Should be Set
            correctAnswerChosen = false,

            selectingCulprit = false;
        private Button
            option1, option2, option3;
        private int
            correctAnswer = 1, // Correct Answer for Multiple Choice (To be changed)

            culprit = 0, // Selected Culprit to Accuse
            maxCulpritAmount = 4; // Amount of Culprits to Choose From (Starts at 0)

        public DebateState()
        {
            // Set BG Sprite
            BG = new StaticSprite(Assets.titleBG, new Rectangle(0, 0, Global.windowWidth, Global.windowHeight), (Color.White * 0.5f), true);

            // Set 3D Background
            bgCam = new _3DCamera(this.graphicsDevice);
            SetWorld();

            // Set Objects
            dialogBox = new DialogBox();
            portrait = new Portrait();

            // Multiple Choice
            option1 = new Button("", new Point(100, 20), 1.0f);
            option2 = new Button("", new Point(350, 20), 1.0f);
            option3 = new Button("", new Point(600, 20), 1.0f);

            // Set State Default Variables
            ResetState();
        }

        public void SetWorld()
        {
            // Create Floor
            mapVertices = new VertexPositionColor[amountOfVertices];
            mapVertices[0] = new VertexPositionColor(new Vector3(-floorWidth, floorY, -floorWidth), Color.SaddleBrown);
            mapVertices[1] = new VertexPositionColor(new Vector3(floorWidth, floorY, -floorWidth), Color.Black);
            mapVertices[2] = new VertexPositionColor(new Vector3(-floorWidth, floorY, floorWidth), Color.Black);
            mapVertices[3] = new VertexPositionColor(new Vector3(floorWidth, floorY, floorWidth), Color.SaddleBrown);
            mapVertices[4] = new VertexPositionColor(new Vector3(floorWidth, floorY, floorWidth), Color.Black);

            vertexBuffer = new VertexBuffer(MainGame.publicGraphicsDevice, typeof(VertexPositionColor), amountOfVertices, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(mapVertices);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Game Progression ---

                // Selecting a Culprit
                if (selectingCulprit)
                {
                    // --- Controls ---

                    // A & D / Left & Right
                    if (KeyPress(Keys.A) || KeyPress(Keys.Left)) culprit--;
                    if (KeyPress(Keys.D) || KeyPress(Keys.Right)) culprit++;

                    // Cap Selected Culprit
                    if (culprit < 0 || culprit > maxCulpritAmount) culprit = 0;

                    // Select Culprit
                    if (KeyPress(Keys.Enter))
                    {
                        // Set Dialog
                        if (culprit == 0) dialogBox.setDialog(Dialog.chapter1culpritFaun); // Faun
                        if (culprit == 1) dialogBox.setDialog(Dialog.chapter1culpritOtto); // Otto
                        if (culprit == 2) dialogBox.setDialog(Dialog.chapter1culpritAngel); // Angel
                        if (culprit == 3) dialogBox.setDialog(Dialog.chapter1culpritMicah); // Micah
                        if (culprit == 4) dialogBox.setDialog(Dialog.chapter1culpritSmokey); // Smokey

                        // End Selection Event
                        selectingCulprit = false;
                    }

                    // --- Visuals ---

                    // Set Portrait Depending on Selection
                    if (culprit == 0) portrait.SetState(Dialog.faun, Portrait.State.regular); // Faun
                    if (culprit == 1) portrait.SetState(Dialog.otto, Portrait.State.regular); // Otto
                    if (culprit == 2) portrait.SetState(Dialog.angel, Portrait.State.regular); // Angel
                    if (culprit == 3) portrait.SetState(Dialog.micah, Portrait.State.regular); // Micah
                    if (culprit == 4) portrait.SetState(Dialog.smokey, Portrait.State.regular); // Smokey
                }

                // When Dialog Not Finished
                if (!dialogBox.endOfDialog)
                {
                    if (answerSelected) multipleChoice = false;

                    // Proceed Dialog
                    if (KeyPress(Keys.Enter) || LeftClicked()) dialogBox.Proceed();
                }

                // --- Dialog Finished ---

                if (dialogBox.endOfDialog)
                {
                    // Trial Pt. 3

                    // Culprit Selection

                    if (dialogBox.dialog == Dialog.chapter1trial3right // When Multiple Choice Finished
                        
                        // Wrong Culprit Selected
                        || dialogBox.dialog == Dialog.chapter1culpritFaun
                        || dialogBox.dialog == Dialog.chapter1culpritAngel
                        || dialogBox.dialog == Dialog.chapter1culpritMicah
                        || dialogBox.dialog == Dialog.chapter1culpritSmokey)
                    {
                        // Select a Culprit
                        dialogBox.Hide();
                        selectingCulprit = true;
                    }

                    // Question

                    if (dialogBox.dialog == Dialog.chapter1trial3wrong)
                    {
                        // Go Back to Question
                        dialogBox.setDialog(Dialog.chapter1trial3question);
                    }

                    if (dialogBox.dialog == Dialog.chapter1trial3question)
                    {
                        // Multiple Choice

                        // When Answer Selected
                        if (answerSelected)
                        {
                            // Correct and Incorrect Answer Dialog
                            if (correctAnswerChosen) dialogBox.setDialog(Dialog.chapter1trial3right);
                            else dialogBox.setDialog(Dialog.chapter1trial3wrong);

                            // Hide Multiple Choice
                            multipleChoice = false;

                            // End Answer Selected Event
                            answerSelected = false;
                        }
                        // Show Multiple Choice
                        else if (!multipleChoice)
                        {
                            ShowMultipleChoice("Micah", "Otto", "Angel", 1);

                            MultipleChoiceButtonSize(1.0f);
                        }
                    }

                    // Discussion
                    if (dialogBox.dialog == Dialog.chapter1trial3)
                    {
                        dialogBox.setDialog(Dialog.chapter1trial3question);
                    }

                    // Trial Pt. 2

                    // Question

                    if (dialogBox.dialog == Dialog.chapter1trial2right)
                    {
                        // Go to Trial Pt. 3
                        dialogBox.setDialog(Dialog.chapter1trial3);
                    }

                    if (dialogBox.dialog == Dialog.chapter1trial2wrong)
                    {
                        // Go Back to Question
                        dialogBox.setDialog(Dialog.chapter1trial2question);
                    }

                    if (dialogBox.dialog == Dialog.chapter1trial2question)
                    {
                        // Multiple Choice

                        // When Answer Selected
                        if (answerSelected)
                        {
                            // Correct and Incorrect Answer Dialog
                            if (correctAnswerChosen) dialogBox.setDialog(Dialog.chapter1trial2right);
                            else dialogBox.setDialog(Dialog.chapter1trial2wrong);

                            // Hide Multiple Choice
                            multipleChoice = false;

                            // End Answer Selected Event
                            answerSelected = false;
                        }
                        // Show Multiple Choice
                        else if (!multipleChoice)
                        {
                            ShowMultipleChoice("Stab Wound", "Injection", "Broken Neck", 3);

                            MultipleChoiceButtonSize(0.8f);
                        }
                    }

                    // Discussion
                    if (dialogBox.dialog == Dialog.chapter1trial2)
                    {
                        dialogBox.setDialog(Dialog.chapter1trial2question);
                    }

                    // Trial Pt. 1

                    // Question

                    if (dialogBox.dialog == Dialog.chapter1trial1right)
                    {
                        // Go to Trial Pt. 2
                        dialogBox.setDialog(Dialog.chapter1trial2);
                    }

                    if (dialogBox.dialog == Dialog.chapter1trial1wrong)
                    {
                        // Go Back to Question
                        dialogBox.setDialog(Dialog.chapter1trial1question);
                    }

                    if (dialogBox.dialog == Dialog.chapter1trial1question)
                    {
                        // Multiple Choice

                        // When Answer Selected
                        if (answerSelected)
                        {
                            // Correct and Incorrect Answer Dialog
                            if (correctAnswerChosen) dialogBox.setDialog(Dialog.chapter1trial1right);
                            else dialogBox.setDialog(Dialog.chapter1trial1wrong);

                            // Hide Multiple Choice
                            multipleChoice = false;

                            // End Answer Selected Event
                            answerSelected = false;
                        }
                        // Show Multiple Choice
                        else if (!multipleChoice)
                        {
                            ShowMultipleChoice(Global.picklesArrivedTime, Global.faunArrivedTime, Global.arthurKilledTime, 2);

                            MultipleChoiceButtonSize(1.0f);
                        }
                    }

                    // Discussion
                    if (dialogBox.dialog == Dialog.chapter1trial1)
                    {
                        dialogBox.setDialog(Dialog.chapter1trial1question);
                    }
                }

                // --- Scrolling Sprites ---

                // Update Offsets by Speed
                float bgOffset = bgScrollSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Set Sprite Positions to Offset Values
                BG.offset += bgOffset;

                // --- 3D Background ---

                // Rotate Matrix
                Matrix rotationMatrix = Matrix.CreateRotationY(
                                            MathHelper.ToRadians(1f));
                bgCam.position = Vector3.Transform(bgCam.position,
                              rotationMatrix);
            }

            // Update 3D Camera
            bgCam.Update(gameTime);

            // Update Objects
            dialogBox.Update(gameTime, Global.dialogSpeed);
            portrait.Update(gameTime, dialogBox);

            if (multipleChoice)
            {
                option1.Update(gameTime, cursor, LeftClicked());
                option2.Update(gameTime, cursor, LeftClicked());
                option3.Update(gameTime, cursor, LeftClicked());

                // Hide Multiple Choice upon Answering
                if (option1.clicked || option2.clicked || option3.clicked)
                {
                    // Progress with Selected Answer
                    answerSelected = true;

                    // Hide Cursor
                    cursorVisible = false;

                    // Set Cursor to Center (Prevent Clicking the Wrong Option)
                    cursor.X = Global.windowWidth / 2;
                    cursor.Y = Global.windowHeight / 2;
                }

                // Select Correct Answer
                if (correctAnswer == 1 && option1.clicked
                    || correctAnswer == 2 && option2.clicked
                    || correctAnswer == 3 && option3.clicked)
                {
                    correctAnswerChosen = true;
                }
                // Incorrect Answer
                else
                {
                    correctAnswerChosen = false;
                }
            }
        }

        /// <summary>
        /// Shows a multiple choice option during the debate, with 3 interactable buttons.
        /// The strings will be used for each button's text.
        /// </summary>
        /// <param name="correctAnswer">The integer number of the correct answer, from 1 to 3.</param>
        public void ShowMultipleChoice(String option1, String option2, String option3, int correctAnswer)
        {
            // Valid Choice Handling
            if (correctAnswer < 0 || correctAnswer > 3)
            {
                this.correctAnswer = 1;

                Debug.Print("The answer assigned to the multiple choice question was invalid! Defaulting to option 1...");
            }
            // Set Correct Answer to Input
            else this.correctAnswer = correctAnswer;

            // Show Cursor
            cursorVisible = true;

            // Set Options
            this.option1.SetText(option1);
            this.option2.SetText(option2);
            this.option3.SetText(option3);

            // Show Multiple Choice Options
            multipleChoice = true;
        }

        /// <summary>
        /// Sets the size of the text of the multiple choice buttons.
        /// </summary>
        /// <param name="newSize">The float size of the button text. (E.g. 1.0f, 0.75f)</param>
        public void MultipleChoiceButtonSize(float newSize)
        {
            // Set Size of all Text for Buttons
            this.option1.SetTextSize(newSize);
            this.option2.SetTextSize(newSize);
            this.option3.SetTextSize(newSize);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Background
            graphicsDevice.Clear(Color.SaddleBrown * 0.5f); // Skybox Colour
            BG.Draw(spriteBatch); // Skybox Sprite
            bgCam.Draw(vertexBuffer, amountOfVertices); // 3D Camera View

            // Draw Objects

            // Dialog
            portrait.Draw(spriteBatch);
            dialogBox.Draw(spriteBatch);

            // Interactivity
            if (multipleChoice)
            {
                option1.Draw(spriteBatch);
                option2.Draw(spriteBatch);
                option3.Draw(spriteBatch);
            }
        }

        public override void ResetState()
        {
            // Set Dialog
            dialogBox.Show();
            dialogBox.setDialog(Dialog.chapter1trial1);

            Dialog.picklesVisible = true; // Show Pickles

            base.ResetState();
        }
    }
}
