#region Using Statements
using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.Random;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Mess_Around_2_Roguesharp
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        MouseState m;
        Texture2D floor, wall,mira;
        Jogador jogador;
        IMap map;

        public Game1()
            : base()
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
            IMapCreationStrategy<Map> mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>(50, 30, 100, 7, 3);
            map = Map.Create(mapCreationStrategy);
            jogador = new Jogador();
            Cell rndpos = GetRandomEmptyCell();
            jogador.X = rndpos.X;
            jogador.Y = rndpos.Y;
            jogador.Scale = 0.25f;
            m = Mouse.GetState();
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
            floor = Content.Load<Texture2D>("Floor");
            wall = Content.Load<Texture2D>("Wall");
            mira = Content.Load<Texture2D>("mira");
            jogador.Sprite = Content.Load<Texture2D>("actor");
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
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                jogador.Y-=0.05f;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                jogador.Y+=0.05f;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                jogador.X-=0.05f;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D))
            {
                jogador.X+=0.05f;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                IMapCreationStrategy<Map> mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>(50, 30, 100, 7, 3);
                map = Map.Create(mapCreationStrategy);

                Cell rndpos = GetRandomEmptyCell();
                jogador.X = rndpos.X;
                jogador.Y = rndpos.Y;
            }
            // TODO: Add your update logic here
            m = Mouse.GetState();
            Vector2 PosRato = new Vector2(m.X, m.Y);
            jogador.mira_X = m.X;
            jogador.mira_Y = m.Y;
            Vector2 Direcção = PosRato - new Vector2(jogador.X, jogador.Y);
            Direcção.Normalize();
            jogador.Rotation = (float)Math.Atan2((double)Direcção.Y, (double)Direcção.X);
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
            spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend);
            jogador.Draw_Mira(spriteBatch);
            int sizeOfSprites = 64;
            float scale = .25f;
            foreach (Cell cell in map.GetAllCells())
            {
                var position = new Vector2(cell.X * sizeOfSprites * scale, cell.Y * sizeOfSprites * scale);
                if (cell.IsWalkable)
                {
                    spriteBatch.Draw(floor, position,
                      null, null, null, 0.0f, new Vector2(scale, scale),
                      Color.White, SpriteEffects.None, 0.8f);
                }
                else
                {
                    spriteBatch.Draw(wall, position,
                       null, null, null, 0.0f, new Vector2(scale, scale),
                       Color.White, SpriteEffects.None, 0.8f);
                }
            }
            jogador.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private Cell GetRandomEmptyCell()
        {
            IRandom random = new DotNetRandom();

            while (true)
            {
                int x = random.Next(49);
                int y = random.Next(29);
                if (map.IsWalkable(x, y))
                {
                    return map.GetCell(x, y);
                }
            }
        }
    }
}
