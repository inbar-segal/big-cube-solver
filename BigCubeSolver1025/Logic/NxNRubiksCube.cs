using BigCubeSolver1025.Utils;
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
        Matrix[,,] rotationMatrices;
        Rotation rotation;
        float rotationAngle;
        
        public NxNRubiksCube(int cubeSize)
        {
            this.cubeSize = cubeSize;
            sideLength = (3 + (1 - cubeSize) * padding) / (cubeSize * 1.5f);
            //sideLength = 3/cubeSize;
            padding = 0.15f * sideLength;
            pieces = new Cubie[cubeSize, cubeSize, cubeSize];
            rotationMatrices= new Matrix[cubeSize, cubeSize, cubeSize];
            rotation= new Rotation(Direction.None, 0, false);
            for (int i = 0; i < cubeSize; i++)
            {
                for (int j = 0; j < cubeSize; j++)
                {
                    for (int k = 0; k < cubeSize; k++)
                    {
                        pieces[i, j, k] = new Cubie(sideLength);
                        rotationMatrices[i, j, k] = Matrix.Identity;
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
            if (rotation.rotatingSide != Direction.None)
            {
                if (rotationAngle >= Cubie.Ninty_Degrees)
                {
                    rotation.rotatingSide = Direction.None;
                    rotationAngle = 0;
                    //TODO לאפס משתנים
                    for (int i = 0; i < cubeSize; i++)
                    {
                        for (int j = 0; j < cubeSize; j++)
                        {
                            for (int k = 0; k < cubeSize; k++)
                            {
                                rotationMatrices[i, j, k] = Matrix.Identity;
                            }
                        }
                    }
                    return;
                }
                rotationAngle += (float)gameTime.ElapsedGameTime.TotalSeconds;
                for(int i=0; i<cubeSize; i++)
                {
                    for (int j = 0; j < cubeSize; j++)
                    {
                        Vector3 pos= GetPieceIndecies(rotation.rotatingSide, i, j, rotation.layerNum);
                        if(rotation.rotatingSide== Direction.Up)
                        rotationMatrices[(int)pos.X, (int)pos.Y, (int)pos.Z] = Matrix.CreateRotationZ(-rotationAngle);
                    //TODO complete
                    }
                }
            }
        }

        public void StartRotation(Direction direction, int layerNum, bool isWidemove)
        {
            if (rotation.rotatingSide == Direction.None)
            {

                rotation.rotatingSide = direction;
                rotation.layerNum = layerNum;
                rotation.isWidemove = isWidemove;
            }
        }
        private Vector3 GetPieceIndecies(Direction direction, int width, int height, int layerNum=0)
        {
            switch (direction)
            {
                case Direction.Up: return new Vector3(width, height, layerNum);
                case Direction.Front: return new Vector3(layerNum, height, width);
                case Direction.Right: return new Vector3(height, layerNum, width);
                case Direction.Back: return new Vector3(cubeSize - 1-layerNum, height, width);
                case Direction.Left: return new Vector3(height, cubeSize - 1-layerNum, width);
                case Direction.Down: return new Vector3(height, width, cubeSize - 1-layerNum);
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

                        //Vector3 v = GetPieceIndecies(Direction.Front, 1, 0);
                        //Vector3 v1 = GetPieceIndecies(Direction.Back, 0, 0);
                        pieces[k, j, i].Draw(squareWorld * rotationMatrices[k, j, i], view, projection, gameTime);
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