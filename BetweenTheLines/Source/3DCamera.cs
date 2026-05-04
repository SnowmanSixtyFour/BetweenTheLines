using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using BetweenTheLines.Source;

namespace BetweenTheLines.Source.Graphics
{
    internal class _3DCamera
    {
        public GraphicsDevice graphicsDevice;

        public Vector3 target, position;
        public Matrix projectionMatrix, viewMatrix, worldMatrix;

        public BasicEffect basicEffect;

        public _3DCamera(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;

            // Setup Camera
            target = new Vector3(0f, 0f, 0f);
            position = new Vector3(0f, 0f, -100f);

            projectionMatrix = Matrix.CreatePerspectiveFieldOfView(
                MathHelper.ToRadians(70f),
                graphicsDevice.Viewport.AspectRatio, 0.1f, 1000f);

            viewMatrix = Matrix.CreateLookAt(position, target, Vector3.Up);

            worldMatrix = Matrix.CreateWorld(target, Vector3.Forward, Vector3.Up);

            // Basic Effect (Rendering Shader)
            basicEffect = new BasicEffect(graphicsDevice);
            basicEffect.Alpha = 1.0f;
            basicEffect.VertexColorEnabled = true;
            basicEffect.LightingEnabled = false;
        }

        public void Update(GameTime gameTime)
        {
            // Update View Matrix
            viewMatrix = Matrix.CreateLookAt(position, target, Vector3.Up);
        }

        public void Draw(VertexBuffer vertexBuffer, int amountOfVertices)
        {
            // Set Rendering Shader
            basicEffect.Projection = projectionMatrix;
            basicEffect.View = viewMatrix;
            basicEffect.World = worldMatrix;

            // graphicsDevice.Clear(Color.CornflowerBlue);

            // Draw Geometry
            graphicsDevice.SetVertexBuffer(vertexBuffer);

            // Set Rasterizer State
            RasterizerState rasterizerState = new RasterizerState();
            // Backface Culling
            if (!Global.backculling) rasterizerState.CullMode = CullMode.None;
            graphicsDevice.RasterizerState = rasterizerState;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawPrimitives(PrimitiveType.TriangleStrip, 0, amountOfVertices);
            }
        }
    }
}
