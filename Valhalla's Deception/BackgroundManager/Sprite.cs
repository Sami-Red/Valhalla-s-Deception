using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    public class Sprite
    {
        private Texture2D Texture;        //The image to use
        private Vector2 Offset;          //Speed of movement of parallax effect
        public float Zoom;              //Zoom level of  image
        public float Speed;


        //Calculate Rectangle dimensions, based on offset/viewport/zoom values
        private Rectangle Rectangle
        {
            get { return new Rectangle((int)(Offset.X), (int)(Offset.Y), (int)(Game1.instance.GetScreenSize().X / Zoom), (int)(Game1.instance.GetScreenSize().Y / Zoom)); }
        }

        public Sprite(Texture2D texture, float speed, float zoom)
        {
            Texture = texture;
            Offset = Vector2.Zero;
            Speed = speed;
            Zoom = zoom;
        }

        public void Update(GameTime gametime, Vector2 direction)
        {
            float elapsed = (float)gametime.ElapsedGameTime.TotalSeconds;

            //Calculate the distance to move our image, based on speed
            Vector2 distance = direction * Speed * elapsed;

            //Update our offset
            Offset += distance;
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(Texture, Vector2.Zero, Rectangle, Color.White, 0, Vector2.Zero, Zoom, SpriteEffects.None, 1);
        }
    }
}
