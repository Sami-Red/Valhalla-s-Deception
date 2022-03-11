using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    public class Menu:CutScene
    {
        SpriteFont font;
        string msg;
        public override void Start()
        {
            names = new string[]
            {
                "MainMenu",
            };
            msg = "Press Enter to Start!";
        }

        public override void Load(ContentManager content)
        {
            base.Load(content);
            font = content.Load<SpriteFont>("Font");
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            Vector2 size = font.MeasureString(msg);
            spriteBatch.Begin();
            spriteBatch.Draw(frames[index], new Rectangle(new Point(Game1.instance.GetScreenSize().X/2 - Game1.instance.GetScreenSize().Y/2, 0), new Point(Game1.instance.GetScreenSize().Y, Game1.instance.GetScreenSize().Y)), Color.White);
            spriteBatch.DrawString(font, msg, Game1.instance.GetScreenSize().ToVector2()/2- size/2+new Vector2(0,300), Color.White);
            spriteBatch.End();
        }
    }
}
