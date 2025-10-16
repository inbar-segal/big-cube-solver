using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace BigCubeSolver1025
{
    internal class Square
    {
        private Color color= Color.Red;
        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        BasicEffect basicEffect;
        GraphicsDevice graphicsDevice;
        VertexPositionColor[] vertices;

        Matrix rotate;
        Vector2 place;

        public void Load(GraphicsDevice graphicsDevice, Vector2 place)
        {
            this.graphicsDevice = graphicsDevice;
            basicEffect = new BasicEffect(graphicsDevice);

            this.place= place;
            vertices = new VertexPositionColor[]
            {
                new VertexPositionColor(new Vector3(-0.5f, 0, -0.5f), color),
                new VertexPositionColor(new Vector3(-0.5f, 0, 0.5f), color),
                new VertexPositionColor(new Vector3(0.5f, 0, -0.5f), color),
                new VertexPositionColor(new Vector3(0.5f, 0, 0.5f), color)
            };

            short[] indices = new short[]
               {
                0, 1, 2,
                1, 2, 3
               };

            rotate = Matrix.Identity;

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData(vertices);
            indexBuffer.SetData(indices);

            basicEffect = new BasicEffect(graphicsDevice);

        }



        public void Update(GameTime gameTime)
        {
            rotate *= Matrix.CreateRotationY((float)gameTime.ElapsedGameTime.TotalSeconds);
        }


        public void Draw(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            basicEffect.World = world* Matrix.CreateTranslation(place.X, 0, place.Y)*rotate;
            basicEffect.View = view;
            basicEffect.Projection = projection;
            basicEffect.VertexColorEnabled = true;

            graphicsDevice.SetVertexBuffer(vertexBuffer);
            graphicsDevice.Indices = indexBuffer;

            foreach (EffectPass pass in basicEffect.CurrentTechnique.Passes)
            {
                pass.Apply();
                graphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 2);
            }
        }
    }
}
