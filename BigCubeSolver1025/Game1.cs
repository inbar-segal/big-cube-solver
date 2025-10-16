using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection;

namespace BigCubeSolver1025
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        Square square;
        Square[] squares;
        Vector3 viewPos;
        Matrix world;
        Matrix view;
        Matrix projection;
        Square[] testSquare;
        float padding=0.1f;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            square= new Square();
            testSquare= new Square[5];
            testSquare[0]=new Square();
            testSquare[1]=new Square();
            testSquare[2]=new Square();
            testSquare[3]=new Square();
            testSquare[4]=new Square();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            testSquare[0].Load(GraphicsDevice, new Vector2(0, 0));
            testSquare[1].Load(GraphicsDevice, new Vector2(1, 1));
            testSquare[2].Load(GraphicsDevice, new Vector2(-1, 1));
            testSquare[3].Load(GraphicsDevice, new Vector2(-1, -1));
            testSquare[4].Load(GraphicsDevice, new Vector2(1, -1));
            squares = new Square[4];

            //if((int)Math.Sqrt(squares.Length) % 2 == 0)
            {
                //x= (0.5*0.5+)* (1+padding)
               // float x = (float)(-0.5 - 0.5 * padding + (Math.Sqrt(squares.Length) - 1) * 0.5 * (-1 - padding));
               //float y = x;
            }

            float x = (float)(-0.5 - 0.5 * padding + (Math.Sqrt(squares.Length) - 1) * 0.5 * (-1 - padding));
            float y = x;
            for (int i=0; i< (int)Math.Sqrt(squares.Length); i++)
            {
                x = (float)(-0.5 - 0.5 * padding + (Math.Sqrt(squares.Length) - 1) * 0.5 * (-1 - padding));

                for (int j = 0; j < (int)Math.Sqrt(squares.Length); j++)
                {

                    int index = j + i * (int)Math.Sqrt(squares.Length);
                    squares[index] = new Square();
                    squares[index].Load(GraphicsDevice, new Vector2(x, y)); 

                    x += padding + 1;
                }
                y -= padding + 1;
            }

            viewPos = new Vector3(0, 6f, 0);
            world = Matrix.CreateTranslation(0, 0, 0);
            view = Matrix.CreateLookAt(viewPos, new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);
            //for (int index = 0; index < squares.Length; index++)
            //{
            //    int i = index %(int)Math.Sqrt(squares.Length);
            //    int j = index / (int)Math.Sqrt(squares.Length);


            //    squares[index] = new Square();
            //    //squares[i].Load(GraphicsDevice);
            //}
            //square.Load(GraphicsDevice);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            square.Update(gameTime);
            foreach (Square square in squares)
            {
                square.Update(gameTime);
            }


            testSquare[0].Update(gameTime);
            testSquare[1].Update(gameTime);
            testSquare[2].Update(gameTime);
            testSquare[3].Update(gameTime);
            testSquare[4].Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            RasterizerState rasterizerState = new RasterizerState() { CullMode = CullMode.None };
            GraphicsDevice.RasterizerState= rasterizerState;
            GraphicsDevice.Clear(Color.CornflowerBlue);


            //square.Draw(world, view, projection, gameTime);

            foreach(Square square in squares)
            {
                //square.Draw(world, view, projection, gameTime);
            }

            testSquare[0].Draw(world, view, projection, gameTime);
            testSquare[1].Draw(world, view, projection, gameTime);
            testSquare[2].Draw(world, view, projection, gameTime);
            testSquare[3].Draw(world, view, projection, gameTime);
            testSquare[4].Draw(world, view, projection, gameTime);
            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
