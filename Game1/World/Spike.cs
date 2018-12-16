using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Try_To_Die.World
{
    class Spike : Fixture
    {
        protected Texture2D platformSprite;
        public Rectangle SpritePosition { get; set; }

        public Spike(String name, Rectangle spritePos) : base(name, spritePos)
        {
            SpritePosition = spritePos;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(platformSprite, SpritePosition, Color.White);
            spriteBatch.End();
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites/Spike");
        }

        public override void Update(Rectangle windowDimensions, GameTime gt, ContentManager content)
        {

        }

    }
}
