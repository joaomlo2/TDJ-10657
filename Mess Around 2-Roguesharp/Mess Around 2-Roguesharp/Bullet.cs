﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mess_Around_2_Roguesharp
{
    class Bullet : Sprite
    {
        public float maxDistance = 2;
        public float velocity = 4;
        private Vector2 sourcePosition;
        private Vector2 direction;
        public Bullet(ContentManager cManager,
                      Vector2 sourcePosition,
                      float rotation)
            : base(cManager, "bullet")
        {
            this.position = sourcePosition;
            this.sourcePosition = sourcePosition;
            this.rotation = rotation;
            this.Scale(0.05f);
            this.direction = new Vector2((float)Math.Sin(rotation),
                                         (float)Math.Cos(rotation));
        }

        public override void Update(GameTime gameTime)
        {
            position = position + direction * velocity *
                  (float)gameTime.ElapsedGameTime.TotalSeconds;

            if ((position - sourcePosition).Length() > maxDistance) 
            {
                this.Destroy();
            }


            base.Update(gameTime);
        }

        public override void Destroy()
        {
            AnimatedSprite explosion;
            explosion = new AnimatedSprite(cManager, "explosion", 1, 12);
            scene.AddSprite(explosion);
            explosion.SetPosition(this.position);
            explosion.Scale(.3f);
            explosion.Loop = false;
            base.Destroy();
        }



    }
}
