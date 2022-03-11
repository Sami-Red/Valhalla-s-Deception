using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    public class GameObject
    {
        protected Texture2D texture;
        protected Rectangle rect;
        protected Rectangle crop;
        protected float rot;
        protected Vector2 origin;
        protected Point cellSize;
        protected SpriteEffects effect;

        public GameObject(Rectangle rect, Point cellSize)
        {
            this.rect = rect;
            this.cellSize = cellSize;
            effect = SpriteEffects.None;
        }

        public void Load(ContentManager content, string path)
        {
            texture = content.Load<Texture2D>(path);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (crop == new Rectangle())
                spriteBatch.Draw(texture, rect, null, Color.White, rot, origin, effect, 0);
            else
                spriteBatch.Draw(texture, rect, crop, Color.White, rot, origin, effect, 0);
        }

        
        public void SetPos(Point pos)
        {
            rect.Location = pos;
        }

        public Point GetPos()
        {
            return rect.Location;
        }

        public Rectangle GetRect()
        {
            return rect;
        }

        public void SetTexture(Texture2D tex)
        {
            texture = tex;
        }

        public GameObject Clone()
        {
            return (GameObject)MemberwiseClone();
        }

    }
}
