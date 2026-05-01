using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source.Graphics;
using System.Diagnostics;

namespace BetweenTheLines.Source.Objects.Level
{
    internal class Portrait
    {
        public StaticSprite sprite { get; private set; }

        public enum State
        {
            regular,
            thinking,
            worried,
            angry,
            excited,
            creepy
        }

        private Point size = new Point(300, 480);

        public State portraitState = State.regular;

        private int
            positionX,
            increment = 12;

        // NOTE: The portrait will always move from the right edge of the screen to the center when created.

        // Move Events
        private bool
            // On-screen
            fromLeftToCenter = false, // From Left
            fromRightToCenter = true, // From Right

            // Off-screen
            moveToLeftOffscreen = false, // To Left
            moveToRightOffscreen = false; // To Right

        public Portrait(byte state = 0)
        {
            // --- Portrait ---

            // Set Sprite

            this.positionX = ((Global.windowWidth / 2) - (size.X / 2));

            this.sprite = new StaticSprite(null, new Rectangle(Global.windowWidth, 0, size.X, size.Y), Color.White);

            // --- State ---

            SetState(state, this.portraitState);
        }

        public void Update(GameTime gameTime, DialogBox dialog)
        {
            // --- Graphics ---

            // Set Sprite
            SetState((byte)dialog.character, (State)dialog.state);

            // --- Move Events ---

            if (fromLeftToCenter) // Center, from Left Edge of Screen
            {
                // Move to Center
                if (this.sprite.GetX() < positionX)
                {
                    this.sprite.SetX(sprite.GetX() + increment);
                }

                // When Reached Center
                else
                {
                    // Reposition Sprite to Center
                    this.sprite.SetX(positionX);

                    // End Event
                    fromLeftToCenter = false;
                }
            }

            if (fromRightToCenter) // Center, from Right Edge of Screen
            {
                // Move to Center
                if (this.sprite.GetX() > positionX)
                {
                    this.sprite.SetX(sprite.GetX() - increment);
                }

                // When Reached Center
                else
                {
                    // Reposition Sprite to Center
                    this.sprite.SetX(positionX);

                    // End Event
                    fromRightToCenter = false;
                }
            }

            if (moveToLeftOffscreen) // Left Edge of Screen, from Center
            {
                // Move to Center
                if (this.sprite.GetX() > (0 - size.X))
                {
                    this.sprite.SetX(sprite.GetX() - increment);
                }

                // When Reached Center
                else
                {
                    // Reposition Sprite to Center
                    this.sprite.SetX((0 - size.X));

                    // End Event
                    // moveToLeftOffscreen = false;
                }
            }

            if (moveToRightOffscreen) // Right Edge of Screen, from Center
            {
                // Move to Center
                if (this.sprite.GetX() > (0 - size.X))
                {
                    this.sprite.SetX(sprite.GetX() + increment);
                }

                // When Reached Center
                else
                {
                    // Reposition Sprite to Center
                    this.sprite.SetX((0 - size.X));

                    // End Event
                    // moveToRightOffscreen = false;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            this.sprite.Draw(spriteBatch);
        }

        // Setters

        public void SetState(byte state, State newPortraitState)
        {
            // Set Properties
            portraitState = newPortraitState;

            try
            {
                // Pickles
                if (state == Dialog.pickles && Dialog.picklesVisible) // Pickles has a special condition whether he should be visible or not
                {
                    // Regular
                    if (portraitState == State.regular) this.sprite.SetTexture(Dialog.picklesRegular);

                    // Thinking
                    if (portraitState == State.thinking) this.sprite.SetTexture(Dialog.picklesThinking);
                }

                // Faun
                if (state == Dialog.faun)
                {
                    // Regular
                    if (portraitState == State.regular) this.sprite.SetTexture(Dialog.faunRegular);

                    // Worried
                    if (portraitState == State.worried) this.sprite.SetTexture(Dialog.faunWorried);
                }

                // Otto
                if (state == Dialog.otto)
                {
                    // Regular
                    if (portraitState == State.regular) this.sprite.SetTexture(Dialog.ottoRegular);

                    // Angry
                    if (portraitState == State.angry) this.sprite.SetTexture(Dialog.ottoAngry);
                }

                // Angel
                if (state == Dialog.angel)
                {
                    // Regular
                    if (portraitState == State.regular) this.sprite.SetTexture(Dialog.angelRegular);

                    // Thinking
                    if (portraitState == State.thinking) this.sprite.SetTexture(Dialog.angelThinking);
                }

                // Micah
                if (state == Dialog.micah)
                {
                    // Regular
                    if (portraitState == State.regular) this.sprite.SetTexture(Dialog.micahRegular);
                }

                // Smokey
                if (state == Dialog.smokey)
                {
                    // Regular
                    if (portraitState == State.regular) this.sprite.SetTexture(Dialog.smokeyRegular);

                    // Excited
                    if (portraitState == State.excited) this.sprite.SetTexture(Dialog.smokeyExcited);

                    // Creepy
                    if (portraitState == State.creepy) this.sprite.SetTexture(Dialog.smokeyCreepy);
                }
            }
            // If Portrait does not exist
            catch (Exception e)
            {
                Debug.Print(e.ToString());
            }
        }

        // Unique Behaviours

        public void Hide()
        {
            // Set X Back to Right Edge of Screen
            this.sprite.SetDestRect(new Rectangle(Global.windowWidth, sprite.GetDestRect().Y, sprite.GetDestRect().Width, sprite.GetDestRect().Height));

            // Disable all Move Events
            this.fromRightToCenter = false;
            this.moveToLeftOffscreen = false;
        }

        public void MoveToCenter()
        {
            Hide(); // Reset Position to Right Edge of Screen
            this.fromRightToCenter = true; // Enable Move to Center, from Left Edge of Screen
        }

        public void MoveLeftOffscreen()
        {
            // Enable Move Left Event
            this.moveToLeftOffscreen = true;

            // Disable other Move Events
            this.moveToRightOffscreen = false;
        }

        public void MoveRightOffscreen()
        {
            // Enable Move Right Event
            this.moveToRightOffscreen = true;

            // Disable other Move Events
            this.moveToLeftOffscreen = false;
        }
    }
}
