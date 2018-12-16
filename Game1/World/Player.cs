using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Try_To_Die.Controllers;
using Try_To_Die.Application;
using Microsoft.Xna.Framework.Audio;

namespace Try_To_Die.World
{
    public class Player : Entity
    {
        public Rectangle pos;

        SoundEffect jump;

        double velocity = 10;

        protected Texture2D playerSprite;

        public int speed { get; }


        // A list of all attacks the player knows.

        public Player(Rectangle spritePos) : base("Sprites/man1", spritePos)
        {
            health = 100;
            speed = 10; 
            pos = spritePos;
        }

        public override void LoadContent(ContentManager content)
        {
            texture = content.Load<Texture2D>("Sprites/man1");
            jump = content.Load<SoundEffect>("Sprites/jumpSound");
        }

        public override void PlayJumpSound()
        {
            jump.Play();
        }

        public override void Update(Rectangle windowDimensions, GameTime gt, ContentManager content)
        {
            if (SpritePosition.Left < 0)
            {
                MoveCommand.MoveRight(this, Math.Abs(0 - SpritePosition.Left));
                health = 0;
            }

            if (SpritePosition.Top < 0)
            {
                MoveCommand.MoveDown(this, Math.Abs(0 - SpritePosition.Top));
            }

            if (SpritePosition.Right > ScreenManager.Instance.Dimensions.Width)
            {
                MoveCommand.MoveLeft(this, Math.Abs(ScreenManager.Instance.Dimensions.Width - SpritePosition.Right));
            }

            if (SpritePosition.Bottom > ScreenManager.Instance.Dimensions.Height)
            {
                MoveCommand.MoveUp(this, Math.Abs(ScreenManager.Instance.Dimensions.Height - SpritePosition.Bottom));
            }


            LoadContent(content);
        }

    }
}