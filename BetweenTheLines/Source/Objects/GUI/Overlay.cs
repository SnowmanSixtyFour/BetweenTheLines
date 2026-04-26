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

        // Properties
        private float overlayTransparency = 0.85f;

        public Overlay()
        {
            overlay = new StaticSprite(Assets.overlay, Rectangle.Empty, Color.White * overlayTransparency);

            clock = new StaticSprite(Assets.clock, new Rectangle(38, 26, 64, 64), Color.White);
        }

        public void Update(GameTime gameTime)
        {
            // Set Rect to Size of Camera
            if (overlay.GetDestRect() == Rectangle.Empty) overlay.SetDestRect(new Rectangle(0, 0, cam.Width, cam.Height));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only Draw if Camera is Set (Prevents Errors)
            if (cam != null) overlay.Draw(spriteBatch);

            clock.Draw(spriteBatch);
        }
    }
}
