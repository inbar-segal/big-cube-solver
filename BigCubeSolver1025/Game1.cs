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

        SquaresRotate squaresRotate;
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

            viewPos = new Vector3(0, 9f, 0);
            world = Matrix.CreateTranslation(0, 0, 0);
            view = Matrix.CreateLookAt(viewPos, new Vector3(0, 0, 0), new Vector3(1, 0, 0));
            projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.01f, 100f);
        }
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            squaresRotate.Update(gameTime);
            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin();
            RasterizerState rasterizerState = new RasterizerState() { CullMode = CullMode.None };
            GraphicsDevice.RasterizerState = rasterizerState;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            squaresRotate.Draw(world, view, projection, gameTime);

            _spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
