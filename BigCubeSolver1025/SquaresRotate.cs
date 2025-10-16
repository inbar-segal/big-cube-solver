using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace BigCubeSolver1025
{
    internal class SquaresRotate
    {

        Square[,] sqaures;
        int size;
        public SquaresRotate(int size)
        {
            this.size = size;
            sqaures = new Square[size, size];

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    sqaures[i, j] = new Square();
                }
            }


        }

        public void Load(GraphicsDevice graphicsDevice)
        {
            //for (int i = 0; i < size; i++)
            //{
            //    for (int j = 0; j < size; j++)
            //    {
            //        sqaures[i, j].Load(graphicsDevice, new Vector2(0, 0));
            //    }
            //}


            foreach (Square square in sqaures)
            {
                square.Load(graphicsDevice, new Vector2(0, 0));
            }
        }

        public void Update(GameTime gameTime)
        {
            foreach (Square square in sqaures)
            {
                square.Update(gameTime);
            }
        }


        public void Draw(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            float padding = 0.1f;
            float startValue = 0;

            if (size % 2 == 1)
            {
                startValue = -size / 2 * (padding + 1);
            }

            Vector2 sqaurePos = new Vector2(startValue, startValue);

            for (int i = 0; i < size; i++)
            {
                sqaurePos.X = startValue;

                for (int j = 0; j < size; j++)
                {
                    sqaures[i, j].Draw(world * Matrix.CreateTranslation(sqaurePos.X, 0, sqaurePos.Y), view, projection, gameTime);
                    sqaurePos.X += padding + 1;
                }
                sqaurePos.Y += padding + 1;
            }

        }
    }
}
