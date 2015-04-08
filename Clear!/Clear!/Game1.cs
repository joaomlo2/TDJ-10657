#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
#endregion

namespace Clear_
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
        Texture2D hero_walking_unholstered_sheet;
        Texture2D mira;
        AnimatedSprite Hero_Unholstered;
        Actor Hero;
        MouseState rato;
        SpriteFont test;
        Vector2 PosRato,direction;
        float rotation;
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
            hero_walking_unholstered_sheet = Content.Load<Texture2D>("hero_walking_normal");
            mira = Content.Load<Texture2D>("mira");
            test = Content.Load<SpriteFont>("test");
            Hero_Unholstered = new AnimatedSprite(hero_walking_unholstered_sheet,4,5);
            Hero = new Actor();
            rato = Mouse.GetState();
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

            rato = Mouse.GetState();
            PosRato = new Vector2(rato.X, rato.Y);
            
            direction = PosRato - new Vector2(Hero.X, Hero.Y);
            direction.Normalize();

            rotation = (float)Math.Atan2((double)direction.Y, (double)direction.X);
            
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
                Hero_Unholstered.Update();
                Hero.Y--;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Hero_Unholstered.Update();
                Hero.Y++;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Hero_Unholstered.Update();
                Hero.X--;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D))
            {
                Hero_Unholstered.Update();
                Hero.X++;
            }
            // TODO: Add your update logic here
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
            spriteBatch.Begin();
            Hero_Unholstered.Draw(spriteBatch,new Vector2(Hero.X,Hero.Y),rotation);
            spriteBatch.Draw(mira, PosRato);
            spriteBatch.DrawString(test, rato.X.ToString(), new Vector2(100, 50), Color.Red);
            spriteBatch.DrawString(test, rato.Y.ToString(), new Vector2(150, 50), Color.Red);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
