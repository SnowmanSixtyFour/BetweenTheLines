// The Dialog Box for gameplay, when someone is speaking.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using BetweenTheLines.Source.Graphics;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace BetweenTheLines.Source.Objects.Level
{
    internal class DialogBox
    {
        // Sprite
        private StaticSprite box;
        private int height = (Global.windowHeight / 3);
        private Rectangle boxRectangle;

        // Text
        private Text
            text, name;
        private int
            textPadding = 30,
            xOffset = 25, // Text Indent
            yOffset = 70; // Text Vertical Offset

        private String typewriterText = "";
        private bool typewriterFinished = false;
        private int currentChar = 0;
        private float timeElapsed = 0;

        // Names
        private String[] names = {
            "???",
            "Pickles",
            "Faun",
            "Otto",
            "Angel",
            "Micah",
            "Smokey"
        };

        // Dialog
        public DialogString[] dialog;
        private int steps = 0; // Current Line of Dialog
        public bool
            endOfDialog = false,

            // Show / Hide Bools
            lowering = false,
            heightening = false;
        public int currentLine = 0;

        // Box Transparency
        private float transparency = 0.85f;

        // Colors
        private Color
            // Box
            boxColor = Color.Gray,

            // Text
            textColor = Color.White,

            textDefaultColor = Color.White,
            textThoughtColor = Color.LightBlue,
            textTooltipColor = Color.LightGreen,

            // Name
            nameColor = Color.LightYellow,

            nameDefaultColor = Color.LightYellow,
            namePicklesColor = new Color(179, 196, 213),
            nameFaunColor = Color.LightSalmon,
            nameOttoColor = new Color(250, 80, 120),
            nameAngelColor = Color.LightGray,
            nameMicahColor = Color.Wheat,
            nameSmokeyColor = Color.MediumPurple,
            
            // Other
            invisibleColor = new Color(0, 0, 0, 0);

        public DialogBox()
        {
            this.boxRectangle = new Rectangle(0, height * 2, Global.windowWidth, height);
            this.box = new StaticSprite(Assets.dialogBox, boxRectangle, (boxColor * transparency));

            this.name = new Text(Assets.arial, "", new Vector2((box.GetDestRect().X + textPadding), (box.GetDestRect().Y + textPadding)), nameColor, 1.0f, false);
            this.text = new Text(Assets.arial, "", new Vector2((box.GetDestRect().X + textPadding + xOffset), (box.GetDestRect().Y + yOffset)), textColor, 1.0f, false);
        }

        public void Update(GameTime gameTime, float dialogSpeed)
        {
            // --- Dialog Text ---

            // Update Current Line
            currentLine = steps;

            // Only update Dialog Box if position is not changing
            if (!lowering && !heightening)
            {
                // If Dialog is not finished
                if (steps < dialog.Length)
                {
                    // Update Timer
                    timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                    // Set Name to Blank if -1 or Lower
                    if (dialog[steps].name <= -1)
                    {
                        this.name.setText("");
                    }
                    else
                    {
                        // Set Name to Array Name
                        this.name.setText(names[dialog[steps].name]);
                    }

                    // Set Text to Current Line in Typewriter Text
                    if (!typewriterFinished)
                    {
                        // When Timer Reaches Dialog Speed
                        if (timeElapsed >= dialogSpeed)
                        {
                            // Update Text to Match Current Character
                            typewriterText = dialog[steps].text.Substring(0, currentChar);

                            // Update Current Character of Dialog
                            currentChar++;

                            // Play Sound Effect
                            SFX.typewriter.Play();

                            // Display Text
                            this.text.setText(typewriterText);

                            // Reset Timer
                            timeElapsed = 0f;

                            // When End of Dialog is Reached
                            if (currentChar >= (dialog[steps].text.Length + 1))
                            {
                                typewriterFinished = true;
                            }
                        }

                        // --- Set Text Colour ---

                        if (dialog[steps].textColor != 0)
                        {
                            // Thought Text (1)
                            if (dialog[steps].textColor == 1)
                            {
                                this.textColor = textThoughtColor;
                            }
                            // Tooltip Text (2)
                            if (dialog[steps].textColor == 2)
                            {
                                this.textColor = textTooltipColor;
                            }
                        }
                        // Default Text
                        else
                        {
                            this.textColor = textDefaultColor;
                        }

                        // Set Colour
                        this.text.setColor(textColor);

                        // --- Set Name Colour ---

                        if (dialog[steps].name == Dialog.pickles) this.nameColor = namePicklesColor;
                        else if (dialog[steps].name == Dialog.faun) this.nameColor = nameFaunColor;
                        else if (dialog[steps].name == Dialog.otto) this.nameColor = nameOttoColor;
                        else if (dialog[steps].name == Dialog.angel) this.nameColor = nameAngelColor;
                        else if (dialog[steps].name == Dialog.micah) this.nameColor = nameMicahColor;
                        else if (dialog[steps].name == Dialog.smokey) this.nameColor = nameSmokeyColor;
                        else this.nameColor = nameDefaultColor;

                        // Set Colour
                        this.name.setColor(nameColor);
                    }
                }
                // When End of Dialog Reached
                else
                {
                    endOfDialog = true; // Set Bool to True
                }
            }

            // Lowering and Heightening - Position when Show(); / Hide(); is used.

            // Hide
            if (lowering)
            {
                // Lower Box
                if (box.GetDestRect().Y <= Global.windowHeight)
                {
                    // Heighten by specified amount
                    box.SetDestRect(new Rectangle(boxRectangle.X, boxRectangle.Y += boxLowerAmount, boxRectangle.Width, boxRectangle.Height));
                }

                else
                {
                    // Set position when too far down
                    box.SetDestRect(new Rectangle(boxRectangle.X, Global.windowHeight + 1, boxRectangle.Width, boxRectangle.Height));

                    // End Lowering
                    lowering = false;
                }
            }

            // Show
            if (heightening)
            {
                // Bring Box back up
                if (box.GetDestRect().Y > (height * 2))
                {
                    // Lower by specified amount
                    box.SetDestRect(new Rectangle(boxRectangle.X, boxRectangle.Y -= boxLowerAmount, boxRectangle.Width, boxRectangle.Height));
                }
                else
                {
                    box.SetDestRect(boxRectangle); // Reset Position if too high up

                    // Show all Text (with Colour)
                    ResetText();
                    this.name.setColor(nameColor);
                    this.text.setColor(textColor);

                    // End Heightening
                    heightening = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.box.Draw(spriteBatch);

            this.name.Draw(spriteBatch);
            this.text.Draw(spriteBatch);
        }

        // --- Unique Behaviours ---

        // Reset Typewriter Variables for Reuse
        public void ResetTypewriter()
        {
            this.currentChar = 0; // Reset Character

            // Bool to Loop Typewriter Effect
            typewriterFinished = false;

            // Reset Text
            typewriterText = "";

            // Reset Timer
            timeElapsed = 0f;
        }

        // Set Dialog
        public void setDialog(DialogString[] newDialog)
        {
            ResetTypewriter();

            this.steps = 0; // Reset Steps
            this.currentLine = 0; // Reset Current Line

            this.endOfDialog = false; // Set Bool to False

            this.dialog = newDialog; // Set New Dialog
        }

        // Continue Dialog
        public void Proceed()
        {
            // Go To Next Line
            if (currentChar >= (dialog[steps].text.Length + 1))
            {
                ResetTypewriter();
                steps++;

                SFX.dialogContinue.Play(); // Play Click Sound
            }
            // Skip Dialog
            else
            {
                currentChar = dialog[steps].text.Length;
            }
        }

        int boxLowerAmount = 5;

        // Hide Dialog Box
        public void Hide()
        {
            // Set Variables
            lowering = true;
            heightening = false;

            // Hide all Text (with Colour)
            this.name.setColor(invisibleColor);
            this.text.setColor(invisibleColor);
        }

        // Show Dialog Box
        public void Show()
        {
            // Set Variables
            heightening = true;
            lowering = false;
        }

        /// <summary>
        /// Reset all text values to be completely blank. Only to be used in State Resets and other similar situations.
        /// </summary>
        public void ResetText()
        {
            this.name.setText("");
            this.text.setText("");
        }
    }
}
