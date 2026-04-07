// The intro animation when the game is first opened.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;

namespace BetweenTheLines.Source.States
{
    internal class TitleState : State
    {
        // Variables

        // Sprites
        private StaticSprite logo;
        private Point logoSize = new Point(364, 214);
        private int logoPadding = 20;

        private Text
            gameVersion,
            gameCredits;

        public TitleState()
        {
            // Set Title
            cursorVisible = true;

            // --- Graphics ---

            // Logo
            logo = new StaticSprite(Global.logo, new Rectangle(new Point((cam.Width / 2) - (logoSize.X / 2), logoPadding), logoSize), Color.White);

            // Text
            gameVersion = new Text(Global.arial, (Global.gameVersion), new Vector2(10, (cam.Height - 30)), Color.Black, 1.0f, false);

            gameCredits = new Text(Global.arial, "Created by Snowman64.", new Vector2((cam.Width - 265), (cam.Height - 30)), Color.Black, 1.0f, false);
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Background
            graphicsDevice.Clear(Color.LightGray);

            // Sprites
            logo.Draw(spriteBatch);
            gameVersion.Draw(spriteBatch);
            gameCredits.Draw(spriteBatch);
        }
    }
}
