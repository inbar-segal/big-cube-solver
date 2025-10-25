using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;


namespace BigCubeSolver1025.Logic
{
    internal class Square
    {
        private Color color;
        float squareLength;

        VertexBuffer vertexBuffer;
        IndexBuffer indexBuffer;

        BasicEffect basicEffect;
        GraphicsDevice graphicsDevice;
        VertexPositionColor[] vertices;
        Matrix UpdateMatrix;

        public Square(Color color, float squareLength)
        {
            this.color = color;
            this.squareLength = squareLength;
            UpdateMatrix = Matrix.Identity;
        }
        public void Load(GraphicsDevice graphicsDevice)
        {
            this.graphicsDevice = graphicsDevice;
            basicEffect = new BasicEffect(graphicsDevice);

            vertices = new VertexPositionColor[]
                {
                new VertexPositionColor(new Vector3(-0.5f, -0.5f,0 )* squareLength, color),
                new VertexPositionColor(new Vector3(-0.5f, 0.5f, 0)* squareLength, color),
                new VertexPositionColor(new Vector3(0.5f, -0.5f,0 )* squareLength, color),
                new VertexPositionColor(new Vector3(0.5f, 0.5f,0 ) * squareLength, color)
                };

            short[] indices = new short[]
               {
                0, 1, 2,
                1, 2, 3
               };

            vertexBuffer = new VertexBuffer(graphicsDevice, typeof(VertexPositionColor), vertices.Length, BufferUsage.WriteOnly);
            indexBuffer = new IndexBuffer(graphicsDevice, typeof(short), indices.Length, BufferUsage.WriteOnly);

            vertexBuffer.SetData(vertices);
            indexBuffer.SetData(indices);

            basicEffect = new BasicEffect(graphicsDevice);

        }

        public Color GetColor()
        {
            return color;
        }
        public void SetColor(Color newColor)
        {
            color = newColor;
            Load(graphicsDevice);
        }

        public void Update(GameTime gameTime)
        {
            UpdateMatrix *= Matrix.CreateRotationY((float)gameTime.ElapsedGameTime.TotalSeconds * 3f);
            //UpdateMatrix *= Matrix.CreateRotationX((float)gameTime.ElapsedGameTime.TotalSeconds);
            //UpdateMatrix *= Matrix.CreateRotationZ((float)gameTime.ElapsedGameTime.TotalSeconds);
        }


        public void Draw(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            basicEffect.World = world * UpdateMatrix;
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
