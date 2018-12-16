using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;


namespace Try_To_Die.World
{
    public abstract class Fixture : Entity
    {
        protected Fixture(String name, Rectangle spritePos) : base(name, spritePos)
        {
            SpritePosition = spritePos;
        }
    }
}