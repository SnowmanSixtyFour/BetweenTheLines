using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using BetweenTheLines.Source.Graphics;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

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

        public void Update(GameTime gameTime)
        {
            if (steps < dialog.Length)
            {
                // Set Text to Current Line of Dialog
                this.text.setText(dialog[steps].text);

                // Set Name to Array Name
                this.name.setText(names[dialog[steps].name]);
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

        // Set Dialog
        public void setDialog(DialogString[] newDialog)
        {
            this.steps = 0; // Reset Steps
            this.endOfDialog = false; // Set Bool to False
            this.dialog = newDialog; // Set New Dialog
        }

        // Continue Dialog
        public void Proceed()
        {
            steps++;
        }

        int boxLowerAmount = 5;

        // Hide Dialog Box
        public void Hide()
        {
            // Lower Box
            if (box.GetDestRect().Y <= Global.windowHeight) box.SetDestRect(new Rectangle(boxRectangle.X, boxRectangle.Y += boxLowerAmount, boxRectangle.Width, boxRectangle.Height));

            this.name.setColor(invisibleColor);
            this.text.setColor(invisibleColor);
        }

        // Show Dialog Box
        public void Show()
        {
            // Bring Box back up
            if (box.GetDestRect().Y > boxRectangle.Y) box.SetDestRect(new Rectangle(boxRectangle.X, boxRectangle.Y -= boxLowerAmount, boxRectangle.Width, boxRectangle.Height));
            else box.SetDestRect(boxRectangle); // Reset Position if too high up
            
            this.name.setColor(nameColor);
            this.text.setColor(textColor);
        }
    }
}
