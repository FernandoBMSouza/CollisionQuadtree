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

namespace CollisionQuadtree
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        private const int SCREEN_WIDTH = 1280, SCREEN_HEIGHT = 720;

        BaseElement player;
        BaseElement[] staticElements;
        BaseElement[] dinamicElements;
        Random random;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";

            graphics.PreferredBackBufferWidth = SCREEN_WIDTH;
            graphics.PreferredBackBufferHeight = SCREEN_HEIGHT;
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
            random = new Random();
            staticElements = new StaticElement[50];
            dinamicElements = new DinamicElement[50];
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

            // TODO: use this.Content to load your game content here
            IsMouseVisible = true;
            Texture2D playerTexture = Content.Load<Texture2D>("blue");
            Texture2D staticElementTexture = Content.Load<Texture2D>("green");
            Texture2D dinamicElementTexture = Content.Load<Texture2D>("yellow");
            Texture2D redTexture = Content.Load<Texture2D>("red");

            player = new Player(playerTexture, this);

            for (int i = 0; i < staticElements.Length; i++)
                staticElements[i] = new StaticElement(this, staticElementTexture, redTexture, random);

            for (int i = 0; i < dinamicElements.Length; i++)
                dinamicElements[i] = new DinamicElement(this, dinamicElementTexture, redTexture, random);
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
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            player.Move(gameTime);
            player.Collision(dinamicElements);
            player.Collision(staticElements);

            foreach (DinamicElement de in dinamicElements)
            {
                de.Move(gameTime);
                de.Collision(player);
                de.Collision(dinamicElements);
                de.Collision(staticElements);
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

            // TODO: Add your drawing code here
            spriteBatch.Begin();

            player.Draw(spriteBatch);

            foreach (StaticElement se in staticElements)
                se.Draw(spriteBatch);

            foreach (DinamicElement de in dinamicElements)
                de.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
