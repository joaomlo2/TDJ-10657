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
    public class Jogador
    {
        public float X { get; set; }
        public float Y { get; set; }
        public Vector2 mira { get; set; }
        public bool morreu { get; set; }
        public int sqX { get; set; }
        public int sqY { get; set; }
        public int Pontos { get; set; }
        public Sprite sprt { get; set; }
        public Texture2D Sprite_Mira { get; set; }
        public float Rotation { get; set; }
        private Camera2 camera;
        
        public Jogador(Camera2 c)
        {
            Rotation = 0.0f;
            camera = c;
            Pontos = 0;
            morreu = false;
        }

        public void Actualizar_Pos_Sprite()
        {
            sprt.SetPosition(new Vector2(this.X,this.Y));
        }

        public void Actualizar_Rotação()
        {
            Vector2 dir=new Vector2();
            dir = Global.camera.ScreenToWorld( mira) - new Vector2(X * sprt.image.Width, Y * sprt.image.Height);
            dir.Normalize();
            sprt.SetRotation((float)Math.Atan2((double)dir.Y, (double)dir.X));
        }

        public void Actualizar_Posição_na_Grelha(float X,float Y)
        {
            this.sqX = (int)(this.X);
            this.sqY = (int)(this.Y);
        }

        public void Disparo(Vector2 Rato,Enemy e)
        {
            if(Rato.X>e.X && Rato.Y>e.Y && Rato.X<=e.X+64 && Rato.X<=e.Y+64&&!e.IsDestroyed)
            {
                e.X = 0;
                e.Y = 0;
                e.IsDestroyed = true;
                Pontos++;
            }
        }

        public void Draw_Mira_e_Pontos(SpriteBatch s,ContentManager c,SpriteFont f)
        {
            s.End();
            s.Begin();
            s.Draw(Sprite_Mira, new Vector2(mira.X,mira.Y), null, null, null, 0.0f, new Vector2(1,1), Color.White, SpriteEffects.None, LayerDepth.Figures);
            s.DrawString(f,Pontos.ToString(),new Vector2(10,10),Color.Yellow);
            s.End();
            s.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.camera.TranslationMatrix);
            s.Draw(c.Load<Texture2D>("Floor"), new Vector2(mira.X, mira.Y) * 64, null, null, null, 0.0f, Vector2.One, Color.Blue * .2f, SpriteEffects.None, LayerDepth.Figures);
        }
    }
}
