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
using BetweenTheLines.Source.States;

namespace BetweenTheLines.Source.Objects.GUI
{
    internal class Overlay
    {
        // Game Camera
        public Camera cam { get; set; }

        // Sprites
        private StaticSprite
            // Overlay
            overlay,
            clock,
            
            // Map
            map;
        private Point
            mapOffset = new Point(15, 10),
            mapSize = new Point(256, 172);
        private float mapTransparency = 0.9f;

        // Text
        private Text
            time;
        private int
            timeOffsetX = 15, timeOffsetY = 70;

        public Text
            chapter;
        private int
            chapterOffsetY = 40;

        public Text
            room;
        private int
            roomOffsetX = 50, roomOffsetY = 25;
        private StoryState.Room currentRoom; // Current Room from Chapter (to call anywhere)

        // Properties
        private float overlayTransparency = 0.85f;

        public Overlay()
        {
            // --- Overlay ---

            overlay = new StaticSprite(Assets.overlay, Rectangle.Empty, Color.White * overlayTransparency);

            clock = new StaticSprite(Assets.clock, new Rectangle(38, 26, 64, 64), Color.White);
            time = new Text(Assets.arial, "00:00 AM", new Vector2(clock.GetX() - timeOffsetX, clock.GetY() + timeOffsetY), Color.White, 1.0f, false);

            chapter = new Text(Assets.arial, "CHAPTER 1", new Vector2(clock.GetDestRect().Center.X, clock.GetY() + timeOffsetY + chapterOffsetY), Color.White, 0.8f, true);

            // --- Map ---

            map = new StaticSprite(Assets.map, Rectangle.Empty, Color.White * mapTransparency);
            room = new Text(Assets.arial, "", new Vector2(chapter.getPosition().X - roomOffsetX, chapter.getPosition().Y + roomOffsetY), Color.White, 0.65f, false);
        }

        public void Update(GameTime gameTime, StoryState level)
        {
            // --- Update Overlay ---

            // Set Rect to Size of Camera
            if (overlay.GetDestRect() == Rectangle.Empty) overlay.SetDestRect(new Rectangle(0, 0, cam.Width, cam.Height));

            // Set Time
            if (time.getText() != DateTime.Now.ToShortTimeString()) time.setText(DateTime.Now.ToShortTimeString());

            // --- Update Map ---

            // Set Current Room Variable
            if (this.currentRoom != level.currentRoom) this.currentRoom = level.currentRoom;

            // Position and Size
            map.SetDestRect(new Rectangle((cam.Width - mapSize.X) - mapOffset.X, mapOffset.Y, mapSize.X, mapSize.Y));

            // Current Room
            if (level.currentRoom == StoryState.Room.foyer)
            {
                map.SetTexture(Assets.mapFoyer);

                room.setText("     FOYER");
            }
            else if (level.currentRoom == StoryState.Room.livingRoom)
            {
                map.SetTexture(Assets.mapLivingRoom);

                room.setText("LIVING ROOM");
            }
            else if (level.currentRoom == StoryState.Room.mainHall)
            {
                map.SetTexture(Assets.mapMainHall);

                room.setText(" MAIN HALL");
            }
            else if (level.currentRoom == StoryState.Room.bathroom)
            {
                map.SetTexture(Assets.mapBathroom);

                room.setText("BATHROOM");
            }
            else if (level.currentRoom == StoryState.Room.kitchen)
            {
                map.SetTexture(Assets.mapKitchen);

                room.setText("  KITCHEN");
            }
            else if (level.currentRoom == StoryState.Room.closet)
            {
                map.SetTexture(Assets.mapCloset);

                room.setText("    CLOSET");
            }
            else
            {
                map.SetTexture(Assets.map);

                room.setText("       ???");
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            // Only Draw if Camera is Set (Prevents Errors)
            if (cam != null) overlay.Draw(spriteBatch);

            clock.Draw(spriteBatch);
            time.Draw(spriteBatch);

            chapter.Draw(spriteBatch);

            // Draw Map if not Outside
            if (currentRoom != StoryState.Room.unknown) map.Draw(spriteBatch);

            room.Draw(spriteBatch);
        }
    }
}
