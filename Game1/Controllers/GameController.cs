using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using Try_To_Die.Controllers;
using Try_To_Die.World;
using Microsoft.Xna.Framework.Audio;

namespace Try_To_Die.Controllers
{
    public class GameController : Controller
    {
        double speed = 10; //speed at which the player moves
        double timer = 0;
        double jumpTime = 0.4;
        double distanceFallen = 0;
        Boolean moving = false;
        int playerIndex;
        Boolean bouncing = false;

        public GameController(int playerIndex)
        {
            this.playerIndex = playerIndex;
        }


        public override void Update(Entity entity, GameTime gameTime, List<Entity> sprites)
        {
            
            UseControllerInput(entity);
            UseKeyboardInputs(entity, gameTime, sprites);
            speed = entity.speed;
            if(!moving)
            {
                entity.speed = 10;
            }

            foreach(Entity s in sprites)
            {
                if(s is Spike)
                {
                    List<Entity> spike = new List<Entity>();
                    spike.Add(s);
                    if(!CheckLeftCollision(entity, spike) || !CheckRightCollision(entity, spike) || !CheckUpCollision(entity, spike) || !CheckDownCollision(entity, spike))
                    {
                        entity.health = 0;
                    }
                }

                if(s is BouncePad)
                {
                    List<Entity> bouncePad = new List<Entity>();
                    bouncePad.Add(s);
                    if (!CheckLeftCollision(entity, bouncePad) || !CheckRightCollision(entity, bouncePad) || !CheckUpCollision(entity, bouncePad) || !CheckDownCollision(entity, bouncePad))
                    {
                        timer = 0.6;
                        bouncing = true;
                    }
                }
            }

            if(timer >= 0)
            {
                if (timer > jumpTime/2)
                {
                    if (CheckUpCollision(entity, sprites))
                    {
                        MoveCommand.MoveUp(entity, 10);
                    }
                    else
                    {
                        timer = jumpTime / 2;
                    }
                } else
                {
                    if (CheckDownCollision(entity, sprites))
                    {
                        MoveCommand.MoveDown(entity, 10);
                    }
                }
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (CheckDownCollision(entity, sprites))
            {
                distanceFallen += 10;
                MoveCommand.MoveDown(entity, 10);
            }
            else 
            {
                if (distanceFallen > 200 && !bouncing)
                {
                    entity.PlayJumpSound();
                    entity.health = 0;
                }
                bouncing = false;
                distanceFallen = 0;
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
        }

        Boolean CheckLeftCollision(Entity player, List<Entity> sprites)
        {
            foreach(var s in sprites)
            {
                if(player.SpritePosition.Left < s.SpritePosition.Right && player.SpritePosition.Top < s.SpritePosition.Bottom - speed && player.SpritePosition.Right > s.SpritePosition.Left && player.SpritePosition.Bottom > s.SpritePosition.Top + speed)
                {
                    if (player.SpritePosition.Left > s.SpritePosition.Left)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        Boolean CheckRightCollision(Entity player, List<Entity> sprites)
        {
            foreach (var s in sprites)
            {
                if (player.SpritePosition.Left < s.SpritePosition.Right && player.SpritePosition.Top < s.SpritePosition.Bottom - speed && player.SpritePosition.Right > s.SpritePosition.Left && player.SpritePosition.Bottom > s.SpritePosition.Top + speed)
                {
                    if (player.SpritePosition.Right < s.SpritePosition.Right)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        Boolean CheckDownCollision(Entity player, List<Entity> sprites)
        {
            foreach (var s in sprites)
            {
                if (player.SpritePosition.Left < s.SpritePosition.Right - speed && player.SpritePosition.Top < s.SpritePosition.Bottom && player.SpritePosition.Right > s.SpritePosition.Left + speed && player.SpritePosition.Bottom > s.SpritePosition.Top - 10)
                {
                    if (player.SpritePosition.Bottom  < s.SpritePosition.Bottom)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        Boolean CheckUpCollision(Entity player, List<Entity> sprites)
        {
            foreach (var s in sprites)
            {
                if (player.SpritePosition.Left < s.SpritePosition.Right - speed && player.SpritePosition.Top < s.SpritePosition.Bottom + speed && player.SpritePosition.Right > s.SpritePosition.Left + speed && player.SpritePosition.Bottom > s.SpritePosition.Top)
                {
                    if (player.SpritePosition.Top > s.SpritePosition.Top)
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private void interact(Entity entity, List<Entity> sprites)
        {
            foreach(Entity s in sprites)
            {
                if(s is Pillow)
                {
                    List<Entity> pillow = new List<Entity>();
                    pillow.Add(s);
                    if (!CheckRightCollision(entity, pillow) || !CheckLeftCollision(entity, pillow))
                    {
                        //entity.PlayJumpSound();
                        Pillow p = (Pillow)s;
                        p.holding = true;
                    }
                }
            }

        }

        public void UseControllerInput(Entity entity)
        {
        }

        private void UseKeyboardInputs(Entity entity, GameTime gameTime, List<Entity> sprites)
        {
            moving = false;
            if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {

            }
            if (playerIndex == 1)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.D) && CheckRightCollision(entity, sprites))
                {
                    MoveCommand.MoveRight(entity, (int)speed);
                    moving = true;
                    if (entity.speed < 20)
                    {
                        entity.speed += 0.4;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.A) && CheckLeftCollision(entity, sprites))
                {
                    MoveCommand.MoveLeft(entity, (int)speed);
                    moving = true;
                    if (entity.speed < 20)
                    {
                        entity.speed += 0.4;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.W) && timer <= 0 && !CheckDownCollision(entity, sprites))
                {
                    entity.PlayJumpSound();
                    timer = jumpTime;

                }

                if (Keyboard.GetState().IsKeyDown(Keys.E))
                {
                    interact(entity, sprites);

                }

            } else if(playerIndex == 2)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.L) && CheckRightCollision(entity, sprites))
                {
                    MoveCommand.MoveRight(entity, (int)speed);
                    moving = true;
                    if (entity.speed < 20)
                    {
                        entity.speed += 0.4;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.J) && CheckLeftCollision(entity, sprites))
                {
                    MoveCommand.MoveLeft(entity, (int)speed);
                    moving = true;
                    if (entity.speed < 20)
                    {
                        entity.speed += 0.4;
                    }
                }

                if (Keyboard.GetState().IsKeyDown(Keys.I) && timer <= 0 && !CheckDownCollision(entity, sprites))
                {
                    entity.PlayJumpSound();
                    timer = jumpTime;

                }
            }

        }
    }
}
