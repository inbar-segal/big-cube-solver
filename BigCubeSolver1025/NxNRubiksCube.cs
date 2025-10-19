using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BigCubeSolver1025
{
    internal class NxNRubiksCube
    {
        Cubie[,,] pieces;
        int cubeSize;
        float sideLength;
        float padding;

        public NxNRubiksCube(int cubeSize)
        {
            this.cubeSize = cubeSize;
            sideLength = (3+(1-cubeSize)*padding) / (cubeSize*1.5f);
            //sideLength = 3/cubeSize;
            padding = 0.15f * sideLength;
            pieces = new Cubie[cubeSize, cubeSize, cubeSize];
            for (int i = 0; i < cubeSize; i++)
            {
                for (int j = 0; j < cubeSize; j++)
                {
                    for (int k = 0; k < cubeSize; k++)
                    {
                        pieces[i, j, k] = new Cubie(sideLength);
                    }
                }
            }
        }

        public void Load(GraphicsDevice graphicsDevice)
        {
            foreach (Cubie piece in pieces)
            {
                piece.Load(graphicsDevice);
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Cubie piece in pieces)
            {
                piece.Update(gameTime);
            }
        }
        public void Draw(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            for (int i = 0; i < cubeSize; i++)
            {
                for (int j = 0; j < cubeSize; j++)
                {
                    for (int k = 0; k < cubeSize; k++)
                    {
                        //Matrix position = Matrix.CreateTranslation(
                        //    -i * (1 + padding) + padding,
                        //    -j * (1 + padding) + padding,
                        //    -k * (1 + padding) + padding);

                        Matrix position = Matrix.CreateTranslation(
                            -i * (1 + padding),
                            -j * (1 + padding),
                            -k * (1 + padding));
                        if (/*(i==0&&j==0&&k==0)||*/
                            /*  (i == 1 && j == 0 && k == )0*/
                            /*&&*/true)
                        {
                            pieces[i, j, k].Draw(position * world/** position*/, view, projection, gameTime);
                        }
                    }
                }
            }
        }

        public void Draw2(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            float startValue = 0;
            float paddingPlusLength = padding + sideLength;

            startValue = (float)(-0.5 * (cubeSize - 1) * paddingPlusLength);
            Vector3 cubePos = new Vector3(startValue, startValue, startValue);

            for (int i = 0; i < cubeSize; i++)
            {
                cubePos.Y = startValue;
                for (int j = 0; j < cubeSize; j++)
                {
                    cubePos.X = startValue;
                    for (int k = 0; k < cubeSize; k++)
                    {
                        Matrix squareWorld = Matrix.CreateTranslation(cubePos.X, cubePos.Y, cubePos.Z) * world;
                        //Matrix squareWorld = world * Matrix.CreateTranslation(sqaurePos.X, 0, sqaurePos.Y);
                        pieces[i, j, k].Draw(squareWorld, view, projection, gameTime);

                        cubePos.X += paddingPlusLength;
                    }
                    cubePos.Y += paddingPlusLength;
                }
                cubePos.Z += paddingPlusLength;

            }
        }
    }
}