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
using Rectangle = Microsoft.Xna.Framework.Rectangle;
#endregion


namespace Mess_Around_2_Roguesharp
{
    public class Enemy
    {
        public Texture2D en { get; set; }
        public Sprite sprite { get; set; }
        public PathToPlayer path;
        public float X;
        public float Y;
        public bool IsDestroyed;
        public bool IsAgressive { get; set; }
        public int sqX;
        public int sqY;
        public float Rotation;

        public Enemy(PathToPlayer p)
        {
            this.Rotation = 0.0f;
            path = p;
            IsDestroyed = false;
        }

        public void Draw(GraphicsDevice gra, GameTime g)
        {
            SpriteBatch a=new SpriteBatch(gra);
            a.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.camera.TranslationMatrix);
            sprite.Draw2(g, a);
            //path.Draw(a);
            a.End();
        }

        public void Draw_next_Cell(GraphicsDevice gra,GameTime g,Map m)
        {
            SpriteBatch s=new SpriteBatch(gra);
            s.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, null, null, null, null, Global.camera.TranslationMatrix);
            s.End();
        }

        public void Has_Detected_Player(IMap m,Cell a)
        {
            IEnumerable<Cell> surrondings = m.GetBorderCellsInArea((int)X, (int)Y, 5);
            bool encontrou = false;
            foreach(Cell c in surrondings)
            {
                if(c.X<=a.X && c.Y<=a.Y)
                {
                    encontrou = true;
                }
            }
            IsAgressive = encontrou;
        }

        public void Update()
        {
            path.CreateFrom((int)X,(int)Y);
            X = path.FirstCell.X;
            Y = path.FirstCell.Y;
        }
        public void Respawn(Cell startingCell, Jogador jogador,IMap map, ContentManager Content)
        {
            if (IsDestroyed)
            {
                var pathFromAggressiveEnemy = new PathToPlayer(jogador, map, Content.Load<Texture2D>("White"));
                pathFromAggressiveEnemy.CreateFrom(startingCell.X, startingCell.Y);
                path = pathFromAggressiveEnemy;
                this.X = startingCell.X;
                this.Y = startingCell.Y;
                IsDestroyed = false;
            }
        }
        public void Attack_Player(IMap m,Jogador j)
        {
            IEnumerable<Cell> vizinhanca = m.GetBorderCellsInArea((int)X, (int)Y, 1);
            foreach(Cell c in vizinhanca)
            {
                if(c.X==j.sqX && c.Y==j.sqY)
                {
                    j.morreu = true;
                }
            }
        }
    }
}
