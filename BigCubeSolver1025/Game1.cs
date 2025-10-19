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
        Vector3 viewPos;
        Matrix world;
        Matrix view;
        Matrix projection;

        NxNRubiksCube rubiksCube= new NxNRubiksCube(6);

        Cubie cubie;

        SquaresRotate squaresRotate;
        SquaresRotate squaresRotate2;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);


            squaresRotate = new SquaresRotate(5, 0.3f, Color.Green);
            squaresRotate.Load(GraphicsDevice);

            cubie = new Cubie(1);
            cubie.Load(GraphicsDevice);

            rubiksCube.Load(GraphicsDevice);

            squaresRotate2 = new SquaresRotate(5, 0.3f, Color.White);
            squaresRotate2.Load(GraphicsDevice);

            viewPos = new Vector3(1, 1, 1)*3;
            world = Matrix.CreateTranslation(0, 0, 0);
            view = Matrix.CreateLookAt(viewPos, new Vector3(0, 0, 0), new Vector3(0, 0, 1));
            //view = Matrix.CreateLookAt(viewPos, new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            squaresRotate.Update(gameTime);

            squaresRotate2.Update(gameTime);

            rubiksCube.Update(gameTime);
            cubie.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.FrontToBack);
            RasterizerState rasterizerState = new RasterizerState() { CullMode = CullMode.None };
            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.Clear(Color.CornflowerBlue);
            //GraphicsDevice.DepthStencilState= DepthStencilState.
            GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            float p = 0.1f * 0.3f;
            float d = 0.3f;
            //squaresRotate.Draw(world, view, projection, gameTime);
            //squaresRotate2.Draw(world * Matrix.CreateRotationX(MathHelper.ToRadians(90)) *
            //    Matrix.CreateTranslation(0, (float)(2.5 * d + 3 * p), (float)(2.5 * d + 3 * p))
            //    , view, projection, gameTime) ;

            //cubie.Draw(world*Matrix.CreateTranslation(0,0, (float)gameTime.TotalGameTime.TotalSeconds/10), view, projection, gameTime);

            rubiksCube.Draw2(world, view, projection, gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
