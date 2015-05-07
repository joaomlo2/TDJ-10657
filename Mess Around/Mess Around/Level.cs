using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Audio;

namespace Mess_Around
{
    class Level
    {
        public int[,] map;

        public Level(ContentManager c,Scene scene)
        {
            Random r = new Random();
            map = new int[10, 10];
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    map[i, j] = r.Next(0,2);
                    if (map[i, j] == 1)
                    {
                        Sprite t = new Sprite(c, "Tile");
                        t.SetPosition(new Vector2(i, j));
                        scene.AddSprite(t);
                        t.EnableCollisions();
                    }
                }
            }
        }

     /*   public void Draw(SpriteBatch s, Texture2D tile)
        {
            for(int i=0;i<10;i++)
            {
                for(int j=0;j<10;j++)
                {
                    if (map[i, j] == 0)
                        s.Draw(tile, new Vector2(i * (tile.Width), j * (tile.Height)), Color.White);
                    else
                        s.Draw(tile, new Vector2(i * (tile.Width), j * (tile.Height)), Color.Black);
                }
            }
        }*/
    }
}