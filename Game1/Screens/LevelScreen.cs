using System;
using System.Collections;
using System.Collections.Generic;
using Try_To_Die.Application;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Try_To_Die.Controllers;
using Try_To_Die.World;
using Microsoft.Xna.Framework.Input;

namespace Try_To_Die.Screens
{
    public class LevelScreen : GameScreen
    {
        Player player;
        Player player2;
        Controller controller;
        Controller controller2;

        List<Entity> sprites = new List<Entity>();
        double timer;

        public override void LoadContent()
        {
            base.LoadContent();
            timer = 0;
            player = new Player(new Rectangle(600, 500, 50, 50));
            player2 = new Player(new Rectangle(800,800, 50, 50));

            Platform platform = new Platform("Platform", new Rectangle(700, 800, 100, 30));
            Platform platform2 = new Platform("Platform", new Rectangle(600, 900, 100, 30));
            Platform platform3 = new Platform("Platform", new Rectangle(550, 700, 100, 30));
            Platform platform4 = new Platform("Platform", new Rectangle(0, 950, 10000, 30));
            Platform platform5 = new Platform("Platform", new Rectangle(1000, 400, 30, 600));
            Spike spike = new Spike("Spike", new Rectangle(900, 900, 30, 100));
            BouncePad bouncePad = new BouncePad("BouncePad", new Rectangle(200, 950, 60, 10));
            Pillow pillow = new Pillow("Pillow", new Rectangle(300, 920, 30, 20));
            sprites.Add(platform);
            sprites.Add(platform2);
            sprites.Add(platform3);
            sprites.Add(platform4);
            sprites.Add(platform5);
            sprites.Add(player2);
            sprites.Add(player);
            sprites.Add(spike);
            sprites.Add(bouncePad);
            sprites.Add(pillow);

            foreach (var s in sprites)
            {
                s.LoadContent(Content);
            }
            controller = new GameController(1);
            controller2 = new GameController(2);
           
        }

        public override void Update(GameTime gameTime)
        {
            controller.Update(player, gameTime, sprites);
            controller2.Update(player2, gameTime, sprites);

            player.Update(ScreenManager.Instance.Dimensions, gameTime, Content);

            foreach(Entity s in sprites)
            {
                if(s is Pillow)
                {
                    Pillow p = (Pillow)s;
                    if (p.holding)
                    {
                        Rectangle playerHolding = new Rectangle(player.SpritePosition.X, player.SpritePosition.Y, 30,20);
                        playerHolding.Offset(-30, 0);
                        s.SpritePosition = playerHolding;
                    }
                }
            }

            if(timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                LevelScreen s = this;
                ScreenManager.Instance.ChangeScreen(new PauseScreen(s), true);
                timer = 0.2;
            }

            if (player.health <= 0 || player2.health <= 0)
            {
                ScreenManager.Instance.ChangeScreen(new DeathScreen(), true);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var s in sprites)
            {
                s.Draw(spriteBatch);
            }
            controller.Draw(spriteBatch);
        }
    }
}