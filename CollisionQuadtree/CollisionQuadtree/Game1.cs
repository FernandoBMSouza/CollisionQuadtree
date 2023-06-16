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
        List<BaseElement> elements;
        Random random;
        Quadtree quadtree;
        private float count, interval = 1f;

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
            elements = new List<BaseElement>();
            quadtree = new Quadtree(0, new Rectangle(0, 0, SCREEN_WIDTH, SCREEN_HEIGHT));
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
            quadtree.Insert(player);

            for (int i = 0; i < 50; i++)
            {
                elements.Add(new StaticElement(this, staticElementTexture, redTexture, random));
                elements.Add(new DinamicElement(this, dinamicElementTexture, redTexture, random));
            }

            foreach (BaseElement element in elements)
                quadtree.Insert(element);
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
            count += gameTime.ElapsedGameTime.Milliseconds * 0.001f;
            // Allows the game to exit
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
                this.Exit();

            // TODO: Add your update logic here
            player.Move(gameTime);

            Quadtree playerQuadrant = quadtree.GetQuadrant(player);
            List<BaseElement> elementsInPlayerQuadrant = quadtree.GetElementsInQuadrant(playerQuadrant, player);
            Window.Title = "Quantidade de Elementos na Regiao onde o Player esta: " + elementsInPlayerQuadrant.Count;

            player.Collision(elementsInPlayerQuadrant);

            foreach (BaseElement item in elements)
            {
                if (item is DinamicElement)
                    item.Move(gameTime);

                Quadtree itemQuadrant = quadtree.GetQuadrant(item);
                List<BaseElement> elementsInItemQuadrant = quadtree.GetElementsInQuadrant(itemQuadrant, item);
                item.Collision(elementsInItemQuadrant);
            }

            // Reinicia a Quadtree por causa dos elementos que se movem
            if (count > interval)
            {
                count = 0;
                quadtree.Clear();
                quadtree.Insert(player);
                foreach (BaseElement element in elements)
                    quadtree.Insert(element);
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

            foreach (BaseElement item in elements)
                item.Draw(spriteBatch);

            //quadtree.Draw(spriteBatch);

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
