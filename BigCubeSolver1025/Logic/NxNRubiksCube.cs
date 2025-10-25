using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static BigCubeSolver1025.Utils.Types;

namespace BigCubeSolver1025.Logic
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
            sideLength = (3 + (1 - cubeSize) * padding) / (cubeSize * 1.5f);
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

            Color[] colors = {Color.White, Color.Orange,
                              Color.Green, Color.Red,
                              Color.Blue, Color.Yellow};
            Direction[] dirArray = {
                                    Direction.Up,
                                    Direction.Left,
                                    Direction.Front,
                                    Direction.Right,
                                    Direction.Back,
                                    Direction.Down
            };
            for (int dirIndex = 0; dirIndex < dirArray.Length; dirIndex++)
            {
                Direction dir = (Direction)dirArray[dirIndex];
                {
                    for (int i = 0; i < cubeSize; i++)
                    {
                        for (int j = 0; j < cubeSize; j++)
                        {
                            Vector3 indencies = GetPieceIndecies(dir, i, j);
                            pieces[(int)indencies.X, (int)indencies.Y, (int)indencies.Z].GetFace(dir)
                                .SetColor(colors[dirIndex]);
                        }
                    }
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Cubie piece in pieces)
            {
                piece.Update(gameTime);
            }
        }

        private Vector3 GetPieceIndecies(Direction direction, int width, int height)
        {
            switch (direction)
            {
                case Direction.Up: return new Vector3(width, height, 0);
                case Direction.Front: return new Vector3(0, height, width);
                case Direction.Right: return new Vector3(height, 0, width);
                case Direction.Back: return new Vector3(cubeSize - 1, height, width);
                case Direction.Left: return new Vector3(height, cubeSize - 1, width);
                case Direction.Down: return new Vector3(height, width, cubeSize - 1);
            }

            return new Vector3(0, 0, 0);
        }
        public void Draw(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            for (int i = 0; i < cubeSize; i++)
            {
                for (int j = 0; j < cubeSize; j++)
                {
                    for (int k = 0; k < cubeSize; k++)
                    {
                        Matrix position = Matrix.CreateTranslation(
                            -i * (1 + padding),
                            -j * (1 + padding),
                            -k * (1 + padding));
                        pieces[i, j, k].Draw(position * world, view, projection, gameTime);
                    }
                }
            }
        }

        public void Draw2(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            float startValue = 0;
            float paddingPlusLength = padding + sideLength;

            startValue = (float)(0.5 * (cubeSize - 1) * paddingPlusLength);
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

                        Vector3 v = GetPieceIndecies(Direction.Front, 1, 0);
                        Vector3 v1 = GetPieceIndecies(Direction.Back, 0, 0);
                        pieces[k, j, i].Draw(squareWorld, view, projection, gameTime);
                        //TODO see if its good

                        cubePos.X -= paddingPlusLength;
                    }
                    cubePos.Y -= paddingPlusLength;
                }
                cubePos.Z -= paddingPlusLength;
            }
        }
    }
}