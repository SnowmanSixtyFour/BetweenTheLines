using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source.Graphics;
using BetweenTheLines.Source.Objects;
using BetweenTheLines.Source.Objects.Level;

namespace BetweenTheLines.Source.Objects.GUI
{
    internal class Overlay
    {
        // Game Camera
        public Camera cam { get; set; }

        // Sprites
        private StaticSprite
            overlay,
            clock;

        // Text
        private Text
            time;
        private int
            timeOffsetX = 15,
            timeOffsetY = 70;

        public Text
            chapter;
        private int
            chapterOffsetY = 40;

        // Properties
        private float overlayTransparency = 0.85f;

        public Overlay()
        {
            overlay = new StaticSprite(Assets.overlay, Rectangle.Empty, Color.White * overlayTransparency);

            clock = new StaticSprite(Assets.clock, new Rectangle(38, 26, 64, 64), Color.White);
            time = new Text(Assets.arial, "00:00 AM", new Vector2(clock.GetX() - timeOffsetX, clock.GetY() + timeOffsetY), Color.White, 1.0f, false);

            chapter = new Text(Assets.arial, "CHAPTER 1", new Vector2(clock.GetDestRect().Center.X, clock.GetY() + timeOffsetY + chapterOffsetY), Color.White, 0.8f, true);
        }

        public void Update(GameTime gameTime)
        {
            // Set Rect to Size of Camera
            if (overlay.GetDestRect() == Rectangle.Empty) overlay.SetDestRect(new Rectangle(0, 0, cam.Width, cam.Height));

            // Set Time
            if (time.getText() != DateTime.Now.ToShortTimeString()) time.setText(DateTime.Now.ToShortTimeString());
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only Draw if Camera is Set (Prevents Errors)
            if (cam != null) overlay.Draw(spriteBatch);

            clock.Draw(spriteBatch);
            time.Draw(spriteBatch);

            chapter.Draw(spriteBatch);
        }
    }
}
