// The Dialog Box for gameplay, when someone is speaking.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BetweenTheLines.Source.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
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
        private int textPadding = 20;

        private String typewriterText = "";
        private bool typewriterFinished = false;
        private int currentChar = 0;
        private float timeElapsed = 0;

        // Names
        private String[] names = {
            "???",
            "Detective Pickles"
        };

        // Dialog
        public DialogString[] dialog;
        private int steps = 0; // Current Line of Dialog
        public bool endOfDialog = false;

        // Colors
        private Color
            boxColor = new Color(32, 32, 32, 192),
            textColor = Color.White,
            nameColor = Color.LightYellow,
            invisibleColor = new Color(0, 0, 0, 0);

        public DialogBox()
        {
            this.boxRectangle = new Rectangle(0, height * 2, Global.windowWidth, height);
            this.box = new StaticSprite(null, boxRectangle, boxColor);

            this.name = new Text(Global.arial, "", new Vector2((box.GetDestRect().X + textPadding), (box.GetDestRect().Y + textPadding)), nameColor, 1.0f, false);
            this.text = new Text(Global.arial, "", new Vector2((box.GetDestRect().X + textPadding), (box.GetDestRect().Y + 60)), textColor, 1.0f, false);
        }

        public void Update(GameTime gameTime, float dialogSpeed)
        {
            // If Dialog is not finished
            if (steps < dialog.Length)
            {
                // Update Timer
                timeElapsed += (float)gameTime.ElapsedGameTime.TotalSeconds;

                // Set Name to Array Name
                this.name.setText(names[dialog[steps].name]);

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
                        Global.typewriter.Play();

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
                }
            }
            // When End of Dialog Reached
            else
            {
                endOfDialog = true; // Set Bool to True
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
            // Lower Box
            if (box.GetDestRect().Y <= Global.windowHeight) box.SetDestRect(new Rectangle(boxRectangle.X, boxRectangle.Y += boxLowerAmount, boxRectangle.Width, boxRectangle.Height));

            // Hide all Text
            this.name.setColor(invisibleColor);
            this.text.setColor(invisibleColor);
        }

        // Show Dialog Box
        public void Show()
        {
            // Bring Box back up
            if (box.GetDestRect().Y > boxRectangle.Y) box.SetDestRect(new Rectangle(boxRectangle.X, boxRectangle.Y -= boxLowerAmount, boxRectangle.Width, boxRectangle.Height));
            else box.SetDestRect(boxRectangle); // Reset Position if too high up
            
            // Show all Text
            this.name.setColor(nameColor);
            this.text.setColor(textColor);
        }
    }
}
