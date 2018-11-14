using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace flappy_bird
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class FlappyMain : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        SpriteFont font;
        int score;

        Pileczka ball;

        Rectangle paletka;
        Texture2D texture_paletka;

        public FlappyMain()
        {
            graphics = new GraphicsDeviceManager(this);
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
            ball = new Pileczka(this.GraphicsDevice);
            
            // = new Rectangle(GraphicsDevice.Viewport.Width / 2-50, 0, 100, 20);
            paletka = new Rectangle(0, 0, 100, 20);
            texture_paletka = new Texture2D(this.GraphicsDevice, 100, 20);
            Color[] color_paletka = new Color[20*100];
            for(int i = 0; i<2000; i++)
            {
                color_paletka[i] = new Color(Color.Black, 1.0f);
            }
            texture_paletka.SetData<Color>(color_paletka);


            score = 0;


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
            font = Content.Load<SpriteFont>("ScoreFont");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Up))
            {
                ball.SpeedUp();
            }
            if (Keyboard.GetState().IsKeyDown(Keys.Down))
            {
                ball.SlowDown();
            }
            
            // Tutaj przesuwamy paletkę
            if (Keyboard.GetState().IsKeyDown(Keys.A) && paletka.X >= 0)
            {
                paletka.X -= (int)(0.3 * gameTime.ElapsedGameTime.Milliseconds);
            }
            if (Keyboard.GetState().IsKeyDown(Keys.D) && paletka.X <= GraphicsDevice.Viewport.Width-paletka.Width)
            {
                paletka.X += (int)(0.3 * gameTime.ElapsedGameTime.Milliseconds);
            }



            // Wyjście
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            int odbicie = ball.Update(paletka.X,paletka.Size.X,paletka.Y);
            if (odbicie == -1)
                score = 0;
            else if (odbicie == 1)
                score++;

            // Odbijanie się piłki

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
            spriteBatch.Draw(ball.getTexture(), ball.getSprite(), Color.Red);
            spriteBatch.Draw(texture_paletka, paletka, Color.Black);
            // Alternatywny przykład formatowania: string.Format("{0}", score);
            spriteBatch.DrawString(font,"Score: "+ score, new Vector2(30, GraphicsDevice.Viewport.Height - 30), Color.Black);
            spriteBatch.End();

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
