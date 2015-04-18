#region Using Statements
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;
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
        Texture2D mira,t1,t2,t3,t4,t5,t6,t7,t8,t9,t10,t11,t12,t13,t14,t15,t16,t17,t18,t19;
        AnimatedSprite Hero_Sprite;
        Actor Hero;
        SoundEffect pistol, armed;
        MouseState Rato;
        SpriteFont test;
        Vector2 PosRato,direction;
        Level level;
        int pistol_counter=0,armed_counter=0;
        float rotation;
        public Game1()
            : base()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = 800;
            graphics.PreferredBackBufferHeight = 600;
            graphics.IsFullScreen = true;
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
            mira = Content.Load<Texture2D>("mira");
            test = Content.Load<SpriteFont>("test");
            Hero_Sprite = new AnimatedSprite(Content.Load<Texture2D>("hero_walking_normal"),4,5);
            Hero = new Actor();
            pistol = Content.Load<SoundEffect>("BF4 1911");
            t1 = Content.Load<Texture2D>("1");
            t2 = Content.Load<Texture2D>("2");
            t3 = Content.Load<Texture2D>("3");
            t4 = Content.Load<Texture2D>("4");
            t5 = Content.Load<Texture2D>("5");
            t6 = Content.Load<Texture2D>("6");
            t7 = Content.Load<Texture2D>("7");
            t8 = Content.Load<Texture2D>("8");
            t9 = Content.Load<Texture2D>("9");
            t10 = Content.Load<Texture2D>("10");
            t11 = Content.Load<Texture2D>("11");
            t12 = Content.Load<Texture2D>("12");
            t13 = Content.Load<Texture2D>("13");
            t14 = Content.Load<Texture2D>("14");
            t15 = Content.Load<Texture2D>("15");
            t16 = Content.Load<Texture2D>("16");
            t17 = Content.Load<Texture2D>("17");
            t18 = Content.Load<Texture2D>("18");
            t19 = Content.Load<Texture2D>("19");
            armed = Content.Load<SoundEffect>("Arming");
            Rato = Mouse.GetState();
            level = new Level();
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
            Rato = Mouse.GetState();
            PosRato = new Vector2(Rato.X, Rato.Y);
            direction = PosRato - new Vector2(Hero.X, Hero.Y);
            direction.Normalize();
            rotation = (float)Math.Atan2((double)direction.Y, (double)direction.X);
            
            if(Hero.Armed==true)
            {
                Hero_Sprite.Texture = Content.Load<Texture2D>("hero_walking_armed");
            }
            else
            {
                Hero_Sprite.Texture = Content.Load<Texture2D>("hero_walking_normal");
            }
            
            if(Rato.RightButton==ButtonState.Pressed)
            {
                if (Hero.Armed == true)
                {
                    Hero.Armed = false;
                }
                else
                {
                    Hero.Armed = true;
                }
            }
            if (Rato.RightButton == ButtonState.Released)
            {
                armed_counter = 0;
            }
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            if(Keyboard.GetState().IsKeyDown(Keys.W))
            {
               
                Hero_Sprite.Update();
                Hero.Y--;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)&&Hero.Armed==false)
                    Hero.Y--;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.S))
            {
                Hero_Sprite.Update();
                Hero.Y++;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Hero.Armed == false)
                    Hero.Y++;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.A))
            {
                Hero_Sprite.Update();
                Hero.X--;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Hero.Armed == false)
                    Hero.X--;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.D))
            {
                
                Hero_Sprite.Update();
                Hero.X++;
                if (Keyboard.GetState().IsKeyDown(Keys.LeftShift) && Hero.Armed == false)
                    Hero.X++;
            }
            if(Keyboard.GetState().IsKeyDown(Keys.R))
            {
                level = new Level();
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
            for (int i = 0; i < 12; i++)
            {
                for (int j = 0; j < 16; j++)
                {
                    if (level.map[i, j] == 1)
                        spriteBatch.Draw(t1, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 2)
                        spriteBatch.Draw(t2, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 3)
                        spriteBatch.Draw(t3, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 4)
                        spriteBatch.Draw(t4, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 5)
                        spriteBatch.Draw(t5, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 6)
                        spriteBatch.Draw(t6, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 7)
                        spriteBatch.Draw(t7, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 8)
                        spriteBatch.Draw(t8, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 9)
                        spriteBatch.Draw(t9, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 10)
                        spriteBatch.Draw(t10, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 11)
                        spriteBatch.Draw(t11, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 12)
                        spriteBatch.Draw(t12, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 13)
                        spriteBatch.Draw(t13, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 14)
                        spriteBatch.Draw(t14, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 15)
                        spriteBatch.Draw(t15, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 16)
                        spriteBatch.Draw(t16, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 17)
                        spriteBatch.Draw(t17, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 18)
                        spriteBatch.Draw(t18, new Vector2(j * 50, i * 50), Color.White);
                    if (level.map[i, j] == 19)
                        spriteBatch.Draw(t19, new Vector2(j * 50, i * 50), Color.White);
                }
            }
            Hero_Sprite.Draw(spriteBatch, new Vector2(Hero.X, Hero.Y), rotation);
            spriteBatch.Draw(mira, PosRato);
            spriteBatch.DrawString(test,gameTime.TotalGameTime.Seconds.ToString(), new Vector2(100, 50), Color.Red);
            spriteBatch.End();
            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
