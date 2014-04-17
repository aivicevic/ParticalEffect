using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace ParticalProject
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState mouse;
        ParticleGenerator SpellEffect;
        BackGround BG;
        Camera cam1;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            Window.AllowUserResizing = true;
            graphics.IsFullScreen = false;
            graphics.ApplyChanges();
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
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            cam1 = new Camera(this);
            SpellEffect = new ParticleGenerator(this, spriteBatch, Content.Load<Texture2D>("Assets/partical"));
            BG = new BackGround(this);
            BG.LoadTexture(Content.Load<Texture2D>("Assets/BG1"));
            BG.LoadTexture(Content.Load<Texture2D>("Assets/BG2"));
            BG.LoadTexture(Content.Load<Texture2D>("Assets/BG3"));

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
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
            KeyboardState keyState = Keyboard.GetState();
            // Allows the game to exit
            if (keyState.IsKeyDown(Keys.Escape))
                this.Exit();
            if (keyState.IsKeyDown(Keys.A))
                cam1.position += new Vector2(-10, 0);
            if (keyState.IsKeyDown(Keys.D))
                cam1.position += new Vector2(10, 0);
            if (keyState.IsKeyDown(Keys.W))
                cam1.position += new Vector2(0, -10);
            if (keyState.IsKeyDown(Keys.S))
                cam1.position += new Vector2(0, 10);
            //SpellEffect.color = Color.Black;
            mouse = Mouse.GetState();
            Vector2 mousePos = new Vector2(mouse.X, mouse.Y);

            SpellEffect.count = 500;
            SpellEffect.density = 10;
            SpellEffect.intensity = 4;
            SpellEffect.Update(gameTime, ParticalEffect.Fog);
            // TODO: Add your update logic here

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.RoyalBlue);
            BG.Draw(gameTime, cam1);
            SpellEffect.DrawIntoCamera(cam1);
            spriteBatch.Begin();
            // TODO: Add your drawing code here
            //SpellEffect.Draw(gameTime);
            spriteBatch.End();
            base.Draw(gameTime);

        }
    }
}
