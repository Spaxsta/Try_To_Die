using System;

using Try_To_Die.Application;
using System.Collections.Generic;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Try_To_Die.Screens;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace Try_To_Die.Screens
{
    public class TitleScreen : GameScreen
    {
        Texture2D BackGround;
        Texture2D mouse;
        Rectangle BackGroundPos;

        static int buttonWidth = 300;
        static int buttonHeight = 150;
        Dictionary<String, Rectangle> buttons = new Dictionary<String, Rectangle>();

        Rectangle localButtonPos = new Rectangle(ScreenManager.Instance.Dimensions.Width / 3,
                ScreenManager.Instance.Dimensions.Height / 4, buttonWidth, buttonHeight);
        Rectangle multiplayerButtonPos = new Rectangle(ScreenManager.Instance.Dimensions.Width / 2,
                ScreenManager.Instance.Dimensions.Height / 4, buttonWidth, buttonHeight);
        Rectangle optionsButtonPos = new Rectangle(ScreenManager.Instance.Dimensions.Width / 3,
                ScreenManager.Instance.Dimensions.Height / 2, buttonWidth, buttonHeight);
        Rectangle exitButtonPos = new Rectangle(ScreenManager.Instance.Dimensions.Width / 2,
                ScreenManager.Instance.Dimensions.Height / 2, buttonWidth, buttonHeight);


        double timer;
        Texture2D localButton;
        Texture2D multiplayerButton;
        Texture2D optionsButton;
        Texture2D exitButton;
        SoundEffect buttonClick;
        SoundEffect errorClick;
        Song bgMusic;

        public override void LoadContent()
        {
            base.LoadContent();

            timer = 0.5;
            buttons.Add("local", localButtonPos);
            buttons.Add("multiplayer", multiplayerButtonPos);
            buttons.Add("options", optionsButtonPos);
            buttons.Add("exit", exitButtonPos);
            buttonClick = Content.Load<SoundEffect>("Menu/selectButton");
            errorClick = Content.Load<SoundEffect>("Menu/errorButton");
            //bgMusic = Content.Load<Song>("Menu/bg");
            Point topLeftPosition = new Point(0, 0);

            Point heightAndWidth = new Point(
                ScreenManager.Instance.Dimensions.Width,
                ScreenManager.Instance.Dimensions.Height);

            // The rectangle that the logo fills.
            BackGroundPos = new Rectangle(
                topLeftPosition,
                heightAndWidth);

            BackGround = Content.Load<Texture2D>("Menu/BackGround");
            mouse = Content.Load<Texture2D>("Menu/Cursor1");
            localButton = Content.Load<Texture2D>("Menu/LocalPlay");
            multiplayerButton = Content.Load<Texture2D>("Menu/MultiplayerButton");
            optionsButton = Content.Load<Texture2D>("Menu/OptionsButton");
            exitButton = Content.Load<Texture2D>("Menu/ExitButton");

            MediaPlayer.IsRepeating = true;

            MediaPlayer.Play(bgMusic);
            MediaPlayer.Volume = 0.4F;
        }

        public override void Update(GameTime gameTime)
        {
            MouseState mouseClick = Mouse.GetState();

            timer -= gameTime.ElapsedGameTime.TotalSeconds;
            foreach (var button in buttons)
            {
                if (Mouse.GetState().X > button.Value.Left && Mouse.GetState().X < button.Value.Right && Mouse.GetState().Y > button.Value.Top
                    && Mouse.GetState().Y < button.Value.Bottom)
                {
                    if (mouseClick.LeftButton == ButtonState.Pressed)
                    {
                        if (button.Key.Equals("local"))
                        {
                            buttonClick.Play();
                            ScreenManager.Instance.ChangeScreen(new SplashScreen(), true);
                            MediaPlayer.Stop();

                        }
                        else if (button.Key.Equals("multiplayer")) //this feature isn't implemented yet?
                        {
                            errorClick.Play();
                        }
                        else if (button.Key.Equals("options") && timer <= 0)
                        {
                            buttonClick.Play();
                            timer = 0.1;
                            TitleScreen s = this;
                            ScreenManager.Instance.ChangeScreen(new OptionsScreen(s), true);
                        }
                        else if (button.Key.Equals("exit"))
                        {
                            Environment.Exit(0);
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BackGround, BackGroundPos, Color.White);
            spriteBatch.Draw(localButton, localButtonPos, Color.White);
            spriteBatch.Draw(multiplayerButton, multiplayerButtonPos, Color.White);
            spriteBatch.Draw(optionsButton, optionsButtonPos, Color.White);
            spriteBatch.Draw(exitButton, exitButtonPos, Color.White);
            spriteBatch.Draw(mouse, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 20, 30), Color.White);
            spriteBatch.End();
        }
    }
}