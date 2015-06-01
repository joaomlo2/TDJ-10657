#region Using Statements
using System;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.Random;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
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
        SoundEffect shot;
        SpriteFont font;
        Cell startingCell;
        MouseState m;
        Texture2D floor, wall,mira,actor,White;
        Jogador jogador;
        bool Shoot_Counter = false;
        int enemy_move_delay = 0;
        Enemy en1,en2,en3,en4,en5;
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
            IMapCreationStrategy<Map> mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>(Global.MapWidth, Global.MapHeight, 100, 7, 3);
            map = Map.Create(mapCreationStrategy);
            Global.camera.ViewportWidth = graphics.GraphicsDevice.Viewport.Width;
            Global.camera.ViewportHeight = graphics.GraphicsDevice.Viewport.Height;
            jogador = new Jogador(Global.camera);
            Cell rndpos = GetRandomEmptyCell();
            jogador.X = rndpos.X;
            jogador.Y = rndpos.Y;
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
            shot = Content.Load<SoundEffect>("shot");
            font = Content.Load<SpriteFont>("Arcade");
            floor = Content.Load<Texture2D>("Floor");
            wall = Content.Load<Texture2D>("Wall");
            mira = Content.Load<Texture2D>("mira");
            actor=Content.Load<Texture2D>("actor");
            jogador.sprt = new Sprite(Content,"actor");
            
            startingCell = GetRandomEmptyCell();
            var pathFromAggressiveEnemy = new PathToPlayer( jogador, map, Content.Load<Texture2D>("White"));
            pathFromAggressiveEnemy.CreateFrom( startingCell.X, startingCell.Y );
            en1 = new Enemy(pathFromAggressiveEnemy);
            en1.X = startingCell.X;
            en1.Y = startingCell.Y;
            en1.sprite = new Sprite(Content, "enemy2");
            
            startingCell = GetRandomEmptyCell();
            pathFromAggressiveEnemy = new PathToPlayer(jogador, map, Content.Load<Texture2D>("White"));
            pathFromAggressiveEnemy.CreateFrom(startingCell.X, startingCell.Y);
            en2 = new Enemy(pathFromAggressiveEnemy);
            en2.X = startingCell.X;
            en2.Y = startingCell.Y;
            en2.sprite = new Sprite(Content, "enemy2");

            startingCell = GetRandomEmptyCell();
            pathFromAggressiveEnemy = new PathToPlayer(jogador, map, Content.Load<Texture2D>("White"));
            pathFromAggressiveEnemy.CreateFrom(startingCell.X, startingCell.Y);
            en3 = new Enemy(pathFromAggressiveEnemy);
            en3.X = startingCell.X;
            en3.Y = startingCell.Y;
            en3.sprite = new Sprite(Content, "enemy2");

            startingCell = GetRandomEmptyCell();
            pathFromAggressiveEnemy = new PathToPlayer(jogador, map, Content.Load<Texture2D>("White"));
            pathFromAggressiveEnemy.CreateFrom(startingCell.X, startingCell.Y);
            en4 = new Enemy(pathFromAggressiveEnemy);
            en4.X = startingCell.X;
            en4.Y = startingCell.Y;
            en4.sprite = new Sprite(Content, "enemy2");

            startingCell = GetRandomEmptyCell();
            pathFromAggressiveEnemy = new PathToPlayer(jogador, map, Content.Load<Texture2D>("White"));
            pathFromAggressiveEnemy.CreateFrom(startingCell.X, startingCell.Y);
            en5 = new Enemy(pathFromAggressiveEnemy);
            en5.X = startingCell.X;
            en5.Y = startingCell.Y;
            en5.sprite = new Sprite(Content, "enemy2");
            
            jogador.Sprite_Mira = Content.Load<Texture2D>("mira");
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
            Vector2 prev_pos = new Vector2(jogador.sqX, jogador.sqY), new_pos;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();
            else
            {
                if (jogador.morreu == false)
                {
                    startingCell=GetRandomEmptyCell();
                    en1.Respawn(startingCell, jogador, map, Content);
                    startingCell = GetRandomEmptyCell();
                    en2.Respawn(startingCell, jogador, map, Content);
                    startingCell = GetRandomEmptyCell();
                    en3.Respawn(startingCell, jogador, map, Content);
                    startingCell = GetRandomEmptyCell();
                    en4.Respawn(startingCell, jogador, map, Content);
                    startingCell = GetRandomEmptyCell();
                    en5.Respawn(startingCell, jogador, map, Content);
                    if (Keyboard.GetState().IsKeyDown(Keys.R))
                    {
                        IMapCreationStrategy<Map> mapCreationStrategy = new RandomRoomsMapCreationStrategy<Map>(Global.MapWidth, Global.MapHeight, 100, 7, 3);
                        map = Map.Create(mapCreationStrategy);
                        Cell rndpos = GetRandomEmptyCell();
                        jogador.X = rndpos.X;
                        jogador.Y = rndpos.Y;
                    }
                    else
                    {
                        if (Keyboard.GetState().IsKeyDown(Keys.A) && CanGo_LEFT(jogador))
                        {
                            jogador.X -= 0.05f;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.D) && CanGo_RIGHT(jogador))
                        {
                            jogador.X += 0.05f;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.W) && CanGo_UP(jogador))
                        {
                            jogador.Y -= 0.05f;
                        }
                        if (Keyboard.GetState().IsKeyDown(Keys.S) && CanGo_DOWN(jogador))
                        {
                            jogador.Y += 0.05f;
                        }
                    }

                    // TODO: Add your update logic here
                    jogador.Actualizar_Pos_Sprite();
                    jogador.Actualizar_Posição_na_Grelha(jogador.X, jogador.Y);
                    m = Mouse.GetState();
                    Vector2 PosRato = new Vector2(m.X, m.Y);
                    Vector2 PosRato2 = Global.camera.ScreenToWorld(PosRato) / 64;
                    jogador.mira = PosRato;
                    if (m.LeftButton == ButtonState.Pressed && !Shoot_Counter)
                    {
                        jogador.Disparo(PosRato2, en1);
                        jogador.Disparo(PosRato2, en2);
                        jogador.Disparo(PosRato2, en3);
                        jogador.Disparo(PosRato2, en4);
                        jogador.Disparo(PosRato2, en5);
                        shot.Play();
                        Shoot_Counter = true;
                    }
                    if (m.LeftButton == ButtonState.Released)
                    {
                        Shoot_Counter = false;
                    }
                    jogador.Actualizar_Rotação();

                    new_pos = new Vector2(jogador.sqX, jogador.sqY);
                    Cell jog = map.GetCell(jogador.sqX, jogador.sqY);
                    if (enemy_move_delay == 15)
                    {
                        en1.Has_Detected_Player(map, jog);
                        en1.Attack_Player(map, jogador);
                        if (en1.IsAgressive && !en1.IsDestroyed)
                            en1.Update();
                        en1.sprite.SetPosition(new Vector2(en1.X, en1.Y));

                        en2.Attack_Player(map, jogador);
                        en2.Has_Detected_Player(map, jog);
                        if (en2.IsAgressive && !en2.IsDestroyed)
                            en2.Update();
                        en2.sprite.SetPosition(new Vector2(en2.X, en2.Y));

                        en3.Attack_Player(map, jogador);
                        en3.Has_Detected_Player(map, jog);
                        if (en3.IsAgressive && !en3.IsDestroyed)
                            en3.Update();
                        en3.sprite.SetPosition(new Vector2(en3.X, en3.Y));

                        en4.Attack_Player(map, jogador);
                        en4.Has_Detected_Player(map, jog);
                        if (en4.IsAgressive && !en4.IsDestroyed)
                            en4.Update();
                        en4.sprite.SetPosition(new Vector2(en4.X, en4.Y));

                        en5.Attack_Player(map, jogador);
                        en5.Has_Detected_Player(map, jog);
                        if (en5.IsAgressive && !en5.IsDestroyed)
                            en5.Update();
                        en5.sprite.SetPosition(new Vector2(en5.X, en5.Y));

                        enemy_move_delay = 0;
                    }
                    else
                        enemy_move_delay++;
                    Global.camera.CenterOn(new Vector2(jogador.X * 64, jogador.Y * 64));
                }
                base.Update(gameTime);
            }
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            if (jogador.morreu == false)
            {
                // TODO: Add your drawing code here
                spriteBatch.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.camera.TranslationMatrix);
                //jogador.Draw_Mira(spriteBatch);

                int sizeOfSprites = Global.SpriteWidth;
                foreach (Cell cell in map.GetAllCells())
                {
                    var position = new Vector2(cell.X * sizeOfSprites, cell.Y * sizeOfSprites);
                    if (cell.IsWalkable)
                    {
                        spriteBatch.Draw(floor, position,
                          null, null, null, 0.0f, null,
                          Color.White, SpriteEffects.None, LayerDepth.Cells);
                    }
                    else
                    {
                        spriteBatch.Draw(wall, position,
                           null, null, null, 0.0f, null,
                           Color.White, SpriteEffects.None, LayerDepth.Cells);
                    }
                }
                jogador.sprt.Draw(gameTime, spriteBatch);
                jogador.Draw_Mira_e_Pontos(spriteBatch, Content, font);
                spriteBatch.End();
                en1.Draw(GraphicsDevice, gameTime);
                en2.Draw(GraphicsDevice, gameTime);
                en3.Draw(GraphicsDevice, gameTime);
                en4.Draw(GraphicsDevice, gameTime);
                en5.Draw(GraphicsDevice, gameTime);
                base.Draw(gameTime);
            }
            else
            {
                spriteBatch.Begin();
                spriteBatch.DrawString(font, "PERDEU", new Vector2(350, 200), Color.Red);
                spriteBatch.DrawString(font, "PONTOS:", new Vector2(290, 220), Color.Red);
                spriteBatch.DrawString(font, jogador.Pontos.ToString(), new Vector2(425, 220), Color.White);
                spriteBatch.End();
            }
        }
        private Cell GetRandomEmptyCell()
        {
            IRandom random = new DotNetRandom();

            while (true)
            {
                int x = Global.Random.Next( 49 );
                int y = Global.Random.Next( 29 );
                if (map.IsWalkable(x, y))
                {
                    return map.GetCell(x, y);
                }
            }
        }

        private bool CanGo_UP(Jogador j)
        {
            if(!map.GetCell(j.sqX,(j.sqY-1)).IsWalkable)
            {
                if(j.Y-j.sqY<=0.25f)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool CanGo_DOWN(Jogador j)
        {
            if (!map.GetCell(j.sqX, (j.sqY + 1)).IsWalkable)
            {
                if (j.Y - j.sqY >= 0.75f)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool CanGo_LEFT(Jogador j)
        {
            if (!map.GetCell((j.sqX-1), j.sqY).IsWalkable)
            {
                if (j.X-j.sqX<=0.25f)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }

        private bool CanGo_RIGHT(Jogador j)
        {
            if (!map.GetCell((j.sqX + 1), j.sqY).IsWalkable)
            {
                if ((j.X) - j.sqX >= 0.75f)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return true;
            }
        }
    }
}
