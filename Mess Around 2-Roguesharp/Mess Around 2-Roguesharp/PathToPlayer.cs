#region Using Statements
using System;
using System.Linq;
using System.Collections.Generic;
using RogueSharp;
using RogueSharp.Random;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
#endregion

namespace Mess_Around_2_Roguesharp
{
    public class PathToPlayer
    {
        private readonly Jogador jogador;
        private readonly IMap map;
        private readonly Texture2D sprite;
        private readonly PathFinder pathFinder;
        private IEnumerable<Cell> cells;

        public PathToPlayer(Jogador p, IMap m, Texture2D s)
        {
            jogador = p;
            map = m;
            sprite = s;
            pathFinder = new PathFinder(map);
        }
        public Cell FirstCell
        {
            get
            {
                return cells.First();
            }
        }
        public void CreateFrom(int x, int y)
        {
            cells = pathFinder.ShortestPath(map.GetCell(x, y),map.GetCell(jogador.sqX, jogador.sqY));
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            if (cells != null)
            {
                foreach (Cell cell in cells)
                {
                    if (cell != null)
                    {
                        float multiplier = sprite.Width;
                        spriteBatch.Draw(sprite, new Vector2(cell.X * multiplier, cell.Y * multiplier), null, null, null, 0.0f, Vector2.One, Color.Blue * .2f, SpriteEffects.None, LayerDepth.Figures);
                    }
                }
            }
        }
    }

}
