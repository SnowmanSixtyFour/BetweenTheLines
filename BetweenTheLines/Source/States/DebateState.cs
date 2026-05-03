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
        private DialogBox dialogBox;
        private Portrait portrait;

        // 3D Background
        private VertexPositionColor[] triangleVertices;
        private VertexBuffer vertexBuffer;
        private _3DCamera bgCam;

        // Interactivity
        private bool
            multipleChoice = false,

            answerSelected = false, // When Answer Dialog Should be Set
            correctAnswerChosen = false;
        private Button
            option1, option2, option3;
        private int correctAnswer = 1;

        public DebateState()
        {
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
            // Create Triangle
            triangleVertices = new VertexPositionColor[3];
            triangleVertices[0] = new VertexPositionColor(new Vector3(0, 70, 0), Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(-70, -70, 0), Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(70, -70, 0), Color.Blue);

            vertexBuffer = new VertexBuffer(MainGame.publicGraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Game Progression ---

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
                    // Trial Pt. 1

                    // Question

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
                        else if (!multipleChoice) ShowMultipleChoice(Global.picklesArrivedTime, Global.faunArrivedTime, Global.arthurKilledTime, 2);
                    }

                    // Discussion
                    if (dialogBox.dialog == Dialog.chapter1trial1)
                    {
                        dialogBox.setDialog(Dialog.chapter1trial1question);
                    }
                }

                // Rotate 3D Background
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

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw 3D Camera View
            bgCam.Draw(vertexBuffer);

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
