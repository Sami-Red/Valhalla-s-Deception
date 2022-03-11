using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    public class CutScene: IScene
    {
        protected Texture2D[] frames;
        protected string[] names;
        protected int index;
        bool pressed = true;
        
        public virtual void Start()
        {
            index = 0;
        }
        
        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.Enter) && !pressed)
            {
                pressed = true;
                if (index < frames.Length - 1)
                {
                    index++;
                }
                else
                {
                    SceneManager.instance.NextScene();
                }
            }
            else if (Keyboard.GetState().IsKeyUp(Keys.Enter))
            {
                pressed = false;
            }
        }
        //Draw all  scenes here
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(frames[index],new Rectangle(new Point(0,0),Game1.instance.GetScreenSize()),Color.White);
            spriteBatch.End();

        }
        //Load all assets here
        public virtual void Load(ContentManager content)
        {
            frames = new Texture2D[names.Length];
            for (int i = 0; i < names.Length; i++) 
            {
                frames[i] = content.Load<Texture2D>(names[i]);
            }
        }

        public IScene Clone()
        {
            return (IScene)MemberwiseClone();
        }

        public void Reset()
        {
            index = 0;
            pressed = true;
        }
    }
}
