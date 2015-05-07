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
namespace Mess_Around
{
    class Mira
    {
        Texture2D mira;

        public Mira(ContentManager c)
        {
            mira = c.Load<Texture2D>("mira");
        }

        public void Draw(SpriteBatch s)
        {
            MouseState ms = Mouse.GetState();
            Vector2 p=new Vector2(ms.Position.X,ms.Position.Y);
            s.Draw(mira,p);
        }
    }
}
