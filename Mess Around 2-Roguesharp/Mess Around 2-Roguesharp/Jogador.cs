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
    class Jogador : Sprite
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 aim { get; set; }
        public int sqX { get; set; }
        public int sqY { get; set; }
        public Texture2D Sprite { get; set; }
        public Texture2D Sprite_Mira { get; set; }
        public float Rotation { get; set; }
        List<Bullet> mbullets = new List<Bullet>();
        ContentManager mContentManager;

        Vector2 mDirection = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);
        Vector2 mSpeed = new Vector2(0.5f, 0.5f);

        public void LoadContent(ContentManager theContentManager)
        {
            mContentManager = theContentManager;

            foreach (Bullet aBullet in mbullets)
            {
                aBullet.LoadContent(theContentManager);
            }
            
            Vector2 Position = new Vector2(X, Y);
            Vector2 Source = new Vector2(Mouse.GetState().X, Mouse.GetState().Y);

        }

        private void UpdateBullets(GameTime gameTime)
        {
            foreach (Bullet aBullet in mbullets)
            {
                aBullet.Update(gameTime);
            }

            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                Shoot();
            }
        }

        private void Shoot()
        {
            if (Mouse.GetState().LeftButton == ButtonState.Pressed)
            {
                bool aCreateNew = true;
                foreach (Bullet aBullet in mbullets)
                {
                    if (aBullet.Visible == false)
                    {
                        aCreateNew = false;
                        aBullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                            new Vector2(1, 0), new Vector2(1, 0));
                        break;
                    }
                }

                if (aCreateNew == true)
                {
                    Bullet aBullet = new Bullet();
                    aBullet.LoadContent(mContentManager);
                    aBullet.Fire(Position + new Vector2(Size.Width / 2, Size.Height / 2),
                        new Vector2(1, 1), new Vector2(1, 0));
                    mbullets.Add(aBullet);
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateBullets(gameTime);
        }

        public Jogador()
        {
            Rotation = 0.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (Bullet aBullet in mbullets)
            {
                aBullet.Draw(spriteBatch);
            }
            spriteBatch.Draw(Sprite, new Vector2(X * Sprite.Width, Y * Sprite.Height), null, null, new Vector2(32,32),Rotation, Vector2.One, Color.White, SpriteEffects.None, LayerDepth.Figures);
        }

        public void Actualizar_Rotação()
        {
            Vector2 dir=new Vector2();
            dir = Global.camera.ScreenToWorld( aim) - new Vector2(X * Sprite.Width, Y * Sprite.Height);
            dir.Normalize();
            Rotation = (float)Math.Atan2((double)dir.Y, (double)dir.X);
        }

        public void Actualizar_Posição_na_Grelha(float X,float Y)
        {
            this.sqX = (int)(this.X);
            this.sqY = (int)(this.Y);
        }

        public void Draw_Mira(SpriteBatch s)
        {
            s.End();
            SpriteBatch ns=new SpriteBatch(s.GraphicsDevice);
            ns.Begin();
            ns.Draw(Sprite_Mira, new Vector2(aim.X,aim.Y), null, null, null, 0.0f, new Vector2(1,1), Color.White, SpriteEffects.None, LayerDepth.Figures);
            ns.End();
        }
    }
}
