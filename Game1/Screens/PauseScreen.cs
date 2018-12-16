using System;

using Try_To_Die.Application;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Try_To_Die.Screens;
using Microsoft.Xna.Framework.Input;

namespace Try_To_Die.Screens
{
    public class PauseScreen : GameScreen
    {
        Texture2D logo;
        Rectangle logoPosition;
        double timer;
        private LevelScreen levelScreen;

        public PauseScreen(LevelScreen levelScreen)
        {
            this.levelScreen = levelScreen;
        }

        public override void LoadContent()
        {
            base.LoadContent();
            timer = 0.2;
            Point topLeftPosition = new Point(0, 0);

            Point heightAndWidth = new Point(
                ScreenManager.Instance.Dimensions.Width,
                ScreenManager.Instance.Dimensions.Width);

            // The rectangle that the logo fills.
            logoPosition = new Rectangle(
                topLeftPosition,
                heightAndWidth);

            logo = Content.Load<Texture2D>("Logos/logo");
        }

        public override void Update(GameTime gameTime)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                ScreenManager.Instance.ChangeScreen(levelScreen, false);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(logo, logoPosition, Color.White);
            spriteBatch.End();
        }
    }
}