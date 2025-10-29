using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using static BigCubeSolver1025.Utils.Types;

namespace BigCubeSolver1025.Logic
{
    internal class Cubie
    {

        public static readonly float Ninty_Degrees = MathHelper.ToRadians((float)90.0);
        Square[] faces;
        float sideLength;
        public Cubie(float sideLength)
        {
            this.sideLength = sideLength;

            faces = new Square[6];
            //for (int i = 0; i < faces.Length; i++)
            //{
            //    faces[i] = new Square(colors[i], sideLength);
            //}

        }



        public void Load(GraphicsDevice graphicDevice, bool isColored)
        {
            Color[] colors = {Color.White, Color.Orange,
                              Color.Green, Color.Red,
                              Color.Blue, Color.Yellow};
            //Todo remove iscolored
            for (int i = 0; i < faces.Length; i++)
            {
                Color color = isColored ? colors[i] : Color.Black;
                faces[i] = new Square(color, sideLength);
                //faces[i] = new Square(colors[i], sideLength);
                //faces[i] = new Square(Color.Black, sideLength);
                faces[i].Load(graphicDevice);
            }

        }

        public Square GetFace(Direction direction)
        {
            Square face;

            switch (direction)
            {
                case Direction.Up: face = faces[0]; break;
                case Direction.Left: face = faces[1]; break;
                case Direction.Front: face = faces[2]; break;
                case Direction.Right: face = faces[3]; break;
                case Direction.Back: face = faces[4]; break;
                case Direction.Down: face = faces[5]; break;
                default: return null;
            }

            return face;
            //TODO: SEE IF ITs GOOD
        }
        public void UpdateColorOrder(Direction direction)
        {
            //Square[] faces2= new Square[6];
            switch (direction)
            {
                case Direction.Up: faces = new Square[] { faces[0], faces[2], faces[3], faces[4], faces[1], faces[5] }; break;
                case Direction.Left: faces = new Square[] { faces[4], faces[1], faces[0], faces[3], faces[5], faces[2] }; break;
                case Direction.Front: faces = new Square[] { faces[1], faces[5], faces[2], faces[0], faces[4], faces[3] }; break;
                case Direction.Right:
                {
                    for (int i = 0; i < 3; i++)
                        UpdateColorOrder(Direction.Left);
                    break;
                }
                case Direction.Back:
                {
                    for (int i = 0; i < 3; i++)
                        UpdateColorOrder(Direction.Front);
                    break;
                }
                case Direction.Down:
                {
                    for (int i = 0; i < 3; i++)
                        UpdateColorOrder(Direction.Up);
                    break;
                }
                default: break;
            }
        }
        public void Update(GameTime gameTime)
        {
            foreach (Square face in faces)
            {
                face.Update(gameTime);
            }
        }

        float ninetyDegrees = MathHelper.ToRadians((float)90.0);

        public void Draw(Matrix world, Matrix view, Matrix projection, GameTime gameTime)
        {
            //const float ninetyDegrees = MathHelper.ToRadians(90);

            Matrix[] facesPosition =
            {
                Matrix.CreateTranslation(0,0,0.5f* sideLength),
                Matrix.CreateRotationX(ninetyDegrees)* Matrix.CreateTranslation(0,-0.5f* sideLength,0),
                Matrix.CreateRotationY(ninetyDegrees)* Matrix.CreateTranslation(0.5f* sideLength,0 ,0),
                Matrix.CreateRotationX(ninetyDegrees)* Matrix.CreateTranslation(0,0.5f* sideLength,0),
                Matrix.CreateRotationY(ninetyDegrees)* Matrix.CreateTranslation(-0.5f* sideLength,0 ,0),
                Matrix.CreateTranslation(0,0,-0.5f* sideLength)
            };

            //            Matrix[] facesPosition =
            //{
            //                Matrix.CreateRotationY(ninetyDegrees)* Matrix.CreateTranslation(0.5f* sideLength,0 ,0),
            //                Matrix.CreateRotationX(ninetyDegrees)* Matrix.CreateTranslation(0,0.5f* sideLength,0),
            //                Matrix.CreateTranslation(0,0,0.5f* sideLength),
            //                Matrix.CreateRotationX(ninetyDegrees)* Matrix.CreateTranslation(0,-0.5f* sideLength,0),
            //                Matrix.CreateTranslation(0,0,-0.5f* sideLength),
            //                Matrix.CreateRotationY(ninetyDegrees)* Matrix.CreateTranslation(-0.5f* sideLength,0 ,0)
            //            };


            for (int i = 0; i < faces.Length; i++)
            {
                //if(facesPosition[i]!=Matrix.Identity)
                faces[i].Draw(facesPosition[i] * world, view, projection, gameTime);
                //faces[i].Draw(world * facesPosition[i], view, projection, gameTime);

            }

        }
    }
}

