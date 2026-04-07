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
        private StaticSprite logo;
        private Point logoSize = new Point(364, 214);
        private int logoPadding = 30;

        public TitleState()
        {
            // Set Title
            cursorVisible = true;

            // --- Graphics ---

            // Logo
            logo = new StaticSprite(Global.logo, new Rectangle(new Point((cam.Width / 2) - (logoSize.X / 2), logoPadding), logoSize), Color.White);
        }

        public override void OnUpdate(GameTime gameTime)
        {
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // BG Color
            graphicsDevice.Clear(Color.LightGray);

            // Logo
            logo.Draw(spriteBatch);
        }
    }
}
