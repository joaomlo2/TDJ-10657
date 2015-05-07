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
        public float mira_X { get; set; }
        public float mira_Y { get; set; }
        public int sqX { get; set; }
        public int sqY { get; set; }
        public float Scale { get; set; }
        public Texture2D Sprite { get; set; }
        public Texture2D Sprite_Mira { get; set; }
        public float Rotation { get; set; }
        
        public void Draw(SpriteBatch spriteBatch)
        {
            float multiplier = Scale * Sprite.Width;
            Rotation = 0.0f;
            spriteBatch.Draw(Sprite, new Vector2(X * multiplier, Y * multiplier),null, null, null, Rotation, new Vector2(Scale, Scale),Color.White, SpriteEffects.None, 0.5f);
        }

        public void Draw_Mira(SpriteBatch s)
        {
            float multiplier = Scale * Sprite.Width;
            s.Draw(Sprite_Mira, new Vector2(mira_X * multiplier, mira_Y * multiplier), null, null, null, 0.0f, new Vector2(Scale, Scale), Color.White, SpriteEffects.None, 0.5f);
        }
    }
}
