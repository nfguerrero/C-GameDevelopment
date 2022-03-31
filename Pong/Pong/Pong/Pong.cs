using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;

namespace Pong
{
    public class Pong : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Texture2D ball;
        Texture2D p1;
        Texture2D p2;

        Vector2 ballPos;
        Vector2 ballVel;

        Vector2 p1Pos;
        Vector2 p1Vel;

        Vector2 p2Pos;
        Vector2 p2Vel;

        Color[] ballData;
        Color[] p1Data;
        Color[] p2Data;

        Color[,] ballColor;
        Color[,] p1Color;
        Color[,] p2Color;

        bool colliding = true;

        public Pong()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 1080;
            graphics.PreferredBackBufferHeight = 720;
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();            
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            ball = Content.Load<Texture2D>("ball");
            p1 = Content.Load<Texture2D>("paddle");
            p2 = Content.Load<Texture2D>("paddle");

            ballPos = new Vector2(360, 540);
            p1Pos = new Vector2(5, 5);
            p2Pos = new Vector2(1027, 5);

            ballVel = new Vector2(5, 1);
            p1Vel = new Vector2(0);
            p2Vel = new Vector2(0);

            ballData = new Color[ball.Width * ball.Height];
            p1Data = new Color[p1.Width * p1.Height];
            p2Data = new Color[p2.Width * p2.Height];

            ball.GetData<Color>(ballData);
            p1.GetData<Color>(p1Data);
            p2.GetData<Color>(p2Data);

            ballColor = new Color[ball.Width, ball.Height];
            p1Color = new Color[p1.Width, p1.Height];
            p1Color = new Color[p1.Width, p1.Height];

            for (int x = 0; x < ball.Width; x++)
            {
                for (int y = 0; y < ball.Height; y++)
                {
                    ballColor[x, y] = ballData[x + y * ball.Width];
                }
            }

            for (int x = 0; x < p1.Width; x++)
            {
                for (int y = 0; y < p1.Height; y++)
                {
                    p1Color[x, y] = p1Data[x + y * p1.Width];
                }
            }
        }

        protected void UpdateInput()
        {
            KeyboardState currentKeyState = Keyboard.GetState();

            if (currentKeyState.IsKeyDown(Keys.W))
            {
                p1Vel.Y = -5;
            }
            if (currentKeyState.IsKeyDown(Keys.S))
            {
                p1Vel.Y = 5;
            }
        }

        protected void UpdateP2()
        {
            float variance = Math.Abs(p2Pos.Y - ballPos.Y);
            if (variance > 10 && ballPos.Y+(ball.Height/2) > p2Pos.Y+(p2.Height/2) && ballVel.Y > 0 && ballPos.X > 540)
            {
                p2Pos.Y += 5;
            }
            else if (variance > 10 && ballPos.Y+(ball.Height/2) < p2Pos.Y + (p2.Height/2) && ballVel.Y < 0 && ballPos.X > 540)
            {
                p2Pos.Y -= 5;
            }
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            UpdateInput();
            UpdateP2();

            ballPos += ballVel;
            if ((p1Pos.Y > 5 && p1Vel.Y == -5) || (p1Pos.Y + p1.Height < 725 && p1Vel.Y == 5))
                p1Pos += p1Vel;

            p1Vel.Y = 0;
            
            if (ballPos.Y <= -5)
            {
                ballVel = Vector2.Reflect(ballVel, Vector2.UnitY);
            }

            if (ballPos.Y + ball.Height >= 720)
            {
                ballVel = Vector2.Reflect(ballVel, Vector2.UnitY);
            }

            if (ballPos.X <= -5)
            {
                ballVel = Vector2.Reflect(ballVel, Vector2.UnitX);
            }

            if (ballPos.X + ball.Width >= 1080)
            {
                ballVel = Vector2.Reflect(ballVel, Vector2.UnitX);
            }          

            Rectangle c1 = new Rectangle((int)ballPos.X, (int)ballPos.Y, (int)ball.Width, (int)ball.Height);
            Rectangle c2 = new Rectangle((int)p1Pos.X, (int)p1Pos.Y, (int)p1.Width, (int)p1.Height);
            Rectangle c3 = new Rectangle((int)p2Pos.X, (int)p2Pos.Y, (int)p2.Width, (int)p2.Height);

            if (c1.Intersects(c2) || c1.Intersects(c3))
            {
                ballVel.X = -ballVel.X;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();
            spriteBatch.Draw(ball, ballPos, Color.White);
            spriteBatch.Draw(p1, p1Pos, Color.White);
            spriteBatch.Draw(p2, p2Pos, Color.White);
            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
