using System;

using Try_To_Die.Application;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Try_To_Die.Screens;

namespace Try_To_Die.Screens
{
    public class DeathScreen : GameScreen
    {
        Texture2D logo;
        Rectangle logoPosition;
        double timer;

        public override void LoadContent()
        {
            base.LoadContent();

            // We only want to spend 4 seconds on the splash screen.
            timer = 1;

            // The logo should appear in the centre of the screen, and slightly
            // above 1/2 way up to make room for the Text.

            // The logo dimensions are the (screen height / 3 * screen height / 3).

            Point topLeftPosition = new Point(0, 0);

            Point heightAndWidth = new Point(
                ScreenManager.Instance.Dimensions.Width,
                ScreenManager.Instance.Dimensions.Width);

            // The rectangle that the logo fills.
            logoPosition = new Rectangle(
                topLeftPosition,
                heightAndWidth);

            logo = Content.Load<Texture2D>("Sprites/man1");
        }

        public override void Update(GameTime gameTime)
        {
            if (timer > 0)
            {
                timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                ScreenManager.Instance.ChangeScreen(new TitleScreen(), true);
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