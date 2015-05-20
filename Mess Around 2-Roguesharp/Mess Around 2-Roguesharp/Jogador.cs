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
    class Jogador
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 mira { get; set; }
        public int sqX { get; set; }
        public int sqY { get; set; }
        public Texture2D Sprite { get; set; }
        public Texture2D Sprite_Mira { get; set; }
        public float Rotation { get; set; }
        
        public Jogador()
        {
            Rotation = 0.0f;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Sprite, new Vector2(X * Sprite.Width, Y * Sprite.Height), null, null, new Vector2(32,32),Rotation, Vector2.One, Color.White, SpriteEffects.None, LayerDepth.Figures);
        }

        public void Actualizar_Rotação()
        {
            Vector2 dir=new Vector2();
            dir = Global.camera.ScreenToWorld( mira) - new Vector2(X * Sprite.Width, Y * Sprite.Height);
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
            ns.Draw(Sprite_Mira, new Vector2(mira.X,mira.Y), null, null, null, 0.0f, new Vector2(1,1), Color.White, SpriteEffects.None, LayerDepth.Figures);
            ns.End();
        }
    }
}
