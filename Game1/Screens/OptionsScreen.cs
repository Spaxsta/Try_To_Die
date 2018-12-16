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
    public class OptionsScreen : GameScreen
    {
        Texture2D BackGround;
        Texture2D mouse;
        Rectangle BackGroundPos;

        static int buttonWidth = 300;
        static int buttonHeight = 150;
        Dictionary<String, Rectangle> buttons = new Dictionary<String, Rectangle>();
        //List<Rectangle> buttons = new List<Rectangle>();
        Rectangle plusButtonPos = new Rectangle(ScreenManager.Instance.Dimensions.Width / 3,
                ScreenManager.Instance.Dimensions.Height / 4, buttonWidth, buttonHeight);
        Rectangle minusButtonPos = new Rectangle(ScreenManager.Instance.Dimensions.Width / 2,
                ScreenManager.Instance.Dimensions.Height / 4, buttonWidth, buttonHeight);
        Rectangle returnButtonPos = new Rectangle(ScreenManager.Instance.Dimensions.Width / 3,
                ScreenManager.Instance.Dimensions.Height / 2, buttonWidth, buttonHeight);



        double timer;
        Texture2D plusButton;
        Texture2D minusButton;
        Texture2D returnButton;
        SoundEffect buttonClick;
        Song bgMusic;

        private TitleScreen titlescreen;

        public OptionsScreen(TitleScreen titlescreen)
        {
            this.titlescreen = titlescreen;
        }

        public override void LoadContent()
        {
            base.LoadContent();

            timer = 0.5;
            buttons.Add("plus", plusButtonPos);
            buttons.Add("minus", minusButtonPos);
            buttons.Add("return", returnButtonPos);
            buttonClick = Content.Load<SoundEffect>("Menu/ButtonClick");
            bgMusic = Content.Load<Song>("Menu/bg");
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
            plusButton = Content.Load<Texture2D>("Menu/PlusButton");
            minusButton = Content.Load<Texture2D>("Menu/MinusButton");
            returnButton = Content.Load<Texture2D>("Menu/ReturnButton");

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
                        if (button.Key.Equals("plus") && timer<= 0)
                        {
                            buttonClick.Play();
                            MediaPlayer.Volume = MediaPlayer.Volume + 0.1F;
                            timer = 0.1;
                        }
                        else if (button.Key.Equals("minus") && timer <= 0)
                        {
                            buttonClick.Play();
                            MediaPlayer.Volume = MediaPlayer.Volume - 0.1F;
                            timer = 0.1;
                        }
                        else if (button.Key.Equals("return") && timer <= 0)
                        {
                            buttonClick.Play();
                            ScreenManager.Instance.ChangeScreen(titlescreen, false);
                            timer = 0.1;
                        }
                    }
                }
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(BackGround, BackGroundPos, Color.White);
            spriteBatch.Draw(plusButton, plusButtonPos, Color.White);
            spriteBatch.Draw(minusButton, minusButtonPos, Color.White);
            spriteBatch.Draw(returnButton, returnButtonPos, Color.White);
            spriteBatch.Draw(mouse, new Rectangle(Mouse.GetState().X, Mouse.GetState().Y, 20, 30), Color.White);
            spriteBatch.End();
        }
    }
}