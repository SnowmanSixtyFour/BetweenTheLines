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

        public DebateState()
        {
            // Set 3D Background
            bgCam = new _3DCamera(this.graphicsDevice);
            SetWorld();

            // Set Objects
            dialogBox = new DialogBox();
            portrait = new Portrait();

            // Set State Default Variables
            ResetState();
        }

        public void SetWorld()
        {
            // Create Triangle
            triangleVertices = new VertexPositionColor[3];
            triangleVertices[0] = new VertexPositionColor(new Vector3(0, 20, 0), Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(-20, -20, 0), Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(20, -20, 0), Color.Blue);

            vertexBuffer = new VertexBuffer(MainGame.publicGraphicsDevice, typeof(VertexPositionColor), 3, BufferUsage.WriteOnly);
            vertexBuffer.SetData<VertexPositionColor>(triangleVertices);
        }

        public override void OnUpdate(GameTime gameTime)
        {
            if (!Global.paused)
            {
                // --- Game Progression ---

                // Proceed Dialog
                if (KeyPress(Keys.Enter) || LeftClicked()) dialogBox.Proceed();
            }

            // Update 3D Background
            bgCam.Update(gameTime);

            // Update Objects
            dialogBox.Update(gameTime, Global.dialogSpeed);
            portrait.Update(gameTime, dialogBox);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw 3D Background
            bgCam.Draw(vertexBuffer);

            // Draw Objects

            // Dialog
            portrait.Draw(spriteBatch);
            dialogBox.Draw(spriteBatch);
        }

        public override void ResetState()
        {
            // Set Music
            ChangeSong(OST.trial);

            // Set Dialog
            dialogBox.Show();
            dialogBox.setDialog(Dialog.chapter1trial1);

            Dialog.picklesVisible = true; // Show Pickles

            base.ResetState();
        }
    }
}
