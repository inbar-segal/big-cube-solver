using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigCubeSolver1025
{
    internal class SquaresRotate
    {

        Square[,] sqauresMatrix;
        int matrixSize;
        float squareLength;
        public SquaresRotate(int matrixSize, float squareLength, Color color)
        {
            this.matrixSize = matrixSize;
            this.squareLength = squareLength;
            sqauresMatrix = new Square[matrixSize, matrixSize];

            for (int i = 0; i < matrixSize; i++)
            {
                for (int j = 0; j < matrixSize; j++)
                {
                    sqauresMatrix[i, j] = new Square(color, squareLength);
                }
            }
        }

        public void Load(GraphicsDevice graphicsDevice)
        {
            foreach (Square square in sqauresMatrix)
            {
                square.Load(graphicsDevice);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Square square in sqauresMatrix)
            {
                square.Update(gameTime);
            }
        }

        public void Draw(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            float padding = 0.1f* squareLength;
            float startValue = 0;
            float paddingPlusLength= padding+ squareLength;

            startValue = (float)(-0.5 * (matrixSize - 1) * paddingPlusLength);
            Vector2 sqaurePos = new Vector2(startValue, startValue);

            for (int i = 0; i < matrixSize; i++)
            {
                sqaurePos.X = startValue;

                for (int j = 0; j < matrixSize; j++)
                {
                    Matrix squareWorld = Matrix.CreateTranslation(sqaurePos.X, 0, sqaurePos.Y)* world;
                    //Matrix squareWorld = world * Matrix.CreateTranslation(sqaurePos.X, 0, sqaurePos.Y);
                    sqauresMatrix[i, j].Draw(squareWorld, view, projection, gameTime);

                    sqaurePos.X += paddingPlusLength;
                }
                sqaurePos.Y += paddingPlusLength;
            }

        }
    }
}
