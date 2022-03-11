using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    class Background
    {
        private List<Sprite> sprite = new List<Sprite>();

        Vector2 direction;


        public Background(Texture2D[] textures,float[] _speed)
        {
            for(int i =0; i<textures.Length; i++) 
            {
                sprite.Add(new Sprite(textures[i], _speed[i], 1));
            }
            direction = Vector2.UnitX;
        }

        public void Update(GameTime gameTime, float multi)
        {
            foreach(Sprite s in sprite)
            {
                s.Update(gameTime, direction* multi);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < sprite.Count; i++)
                sprite[i].Draw(spriteBatch);
        }
    }
}
