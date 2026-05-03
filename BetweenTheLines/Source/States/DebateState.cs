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
            triangleVertices[0] = new VertexPositionColor(new Vector3(0, 70, 0), Color.Red);
            triangleVertices[1] = new VertexPositionColor(new Vector3(-70, -70, 0), Color.Green);
            triangleVertices[2] = new VertexPositionColor(new Vector3(70, -70, 0), Color.Blue);

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

            // Rotate 3D Background
            Matrix rotationMatrix = Matrix.CreateRotationY(
                                        MathHelper.ToRadians(1f));
            bgCam.position = Vector3.Transform(bgCam.position,
                          rotationMatrix);

            // Update 3D Camera
            bgCam.Update(gameTime);

            // Update Objects
            dialogBox.Update(gameTime, Global.dialogSpeed);
            portrait.Update(gameTime, dialogBox);
        }

        public override void OnDraw(SpriteBatch spriteBatch)
        {
            // Draw 3D Camera View
            bgCam.Draw(vertexBuffer);

            // Draw Objects

            // Dialog
            portrait.Draw(spriteBatch);
            dialogBox.Draw(spriteBatch);
        }

        public override void ResetState()
        {
            // Set Dialog
            dialogBox.Show();
            dialogBox.setDialog(Dialog.chapter1trial1);

            Dialog.picklesVisible = true; // Show Pickles

            base.ResetState();
        }
    }
}
