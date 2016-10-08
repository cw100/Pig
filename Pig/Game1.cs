using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
namespace Pig
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        VideoPlayer videoPlayer;
        Video pig;
        Animation pringle;
        public Game1()
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
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        Rectangle rect = new Rectangle(320,200,90,90);
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            videoPlayer = new VideoPlayer();
            pig  = Content.Load<Video>("pig");
            Texture2D pringleTex = Content.Load<Texture2D>("pringle");
            pringle = new Animation(pringleTex,8,2,new Vector2(0,0));
            pringle.isPaused = true;
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
        Texture2D videoTexture = null;

        MouseState mouse;
        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

           
        
            
          
           
            pringle.Position = mouse.Position.ToVector2();
            pringle.Update(gameTime);
            if (rect.Intersects(pringle.hitBox))
            {
                if (videoPlayer.State == MediaState.Paused)
                {
                    videoPlayer.Stop();
                    videoPlayer.Play(pig);
                    pringle.isPaused = false;
                }
            }
            else
            {
                if (videoPlayer.State == MediaState.Playing)
                {
                    videoPlayer.Pause();
                    pringle.isPaused = true;
                }
            }
            if (videoPlayer.State == MediaState.Stopped)
            {


                videoPlayer.Play(pig);

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
          
            if (videoPlayer.State != MediaState.Stopped)
                videoTexture = videoPlayer.GetTexture();
            if (videoTexture != null)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(videoTexture, new Rectangle(0, 0, 800, 480), Color.White);
                pringle.Draw(spriteBatch);
                spriteBatch.End();
            }
           

            base.Draw(gameTime);
        }
    }
}
