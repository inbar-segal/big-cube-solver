using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace BigCubeSolver1025
{
    internal class Cubie
    {

        readonly float Ninty_Degrees = MathHelper.ToRadians((float)90.0);
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

        public void Load(GraphicsDevice graphicDevice)
        {
            Color[] colors = {Color.White, Color.Orange,
                              Color.Green, Color.Red,
                              Color.Blue, Color.Yellow};
            for (int i = 0; i < faces.Length; i++)
            {
                faces[i] = new Square(colors[i], sideLength);
                faces[i].Load(graphicDevice);
            }
            //foreach (Square face in faces)
            //{
            //    face.Load(graphicDevice);
            //}

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
                faces[i].Draw(facesPosition[i]* world, view, projection, gameTime);
                //faces[i].Draw(world * facesPosition[i], view, projection, gameTime);

            }

        }
    }
}

