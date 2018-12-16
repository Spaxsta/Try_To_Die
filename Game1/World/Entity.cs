using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace Try_To_Die.World
{
    /// <summary>
    /// An Entity is any Sprite that is 'alive.'
    /// </summary>
    public abstract class Entity 
    {
        protected String name;

        public Texture2D texture;

        public double speed {get; set;}

        public double health { get; set; }

        public String Name { get; protected set; }

        public Rectangle SpritePosition { get; set; }


        // Default Constructor for entities.
        protected Entity(String name, Rectangle spritePos)
        {
            this.name = name;
            SpritePosition = spritePos;
        }

        public virtual void PlayJumpSound(){
}

        public abstract void LoadContent(ContentManager content);

        public abstract void Update(Rectangle windowDimensions, GameTime gt, ContentManager content);

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(texture, SpritePosition, Color.White);
            spriteBatch.End();
        }
    }
}