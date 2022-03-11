using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VallhalasDeception
{
    public class Enemy : AnimatedObject
    {
        int direction, speed, health;
        Point path;
        bool dead;
        bool disable;
        public Enemy(int _health, int _speed, Rectangle rect, Point cellSize, string[] _paths) : base(rect, cellSize, _paths)
        {
            health = _health;
            speed = _speed;
            crop = new Rectangle(0, 0, 50, 48);
            direction = 1;
        }

        public void Update(GameTime gameTime)
        {
            if (!dead && !disable)
            {
                if (!actualAnimation.name.Contains("hurt") || actualAnimation.name.Contains("hurt") && actualAnimation.IsDone() )
                    if (direction == 1)
                        Play("walkR");
                    else
                        Play("walkL");
                
                if (rect.X < path.X)
                {
                    effect = SpriteEffects.None;
                    direction = 1;
                }
                else if (rect.X > path.Y)
                {
                    effect = SpriteEffects.FlipHorizontally;
                    direction = -1;
                    
                }
                rect.X += direction * speed;
            }
        }

        public bool GetDisable()
        {
            return disable;
        }

        public void SetDisable(bool value)
        {
            disable = value;
        }

        public Enemy Clone()
        {
            return (Enemy)MemberwiseClone();
        }
        public void SetPath(Point _path)
        {
            path = _path;
        }

        public bool ReceiveDamage()
        { 
            Debug.WriteLine("HitE");
            health--;
            if (health == 0)
            {
                dead = true;
            }
            else
            {
                if(actualAnimation.name == "walkR")
                    Play("hurtR");
                else if (actualAnimation.name == "walkL")
                    Play("hurtL");
            }

            return dead;
        }

        public void Draw(SpriteBatch sp)
        {
            if (!disable)
                base.Draw(sp);
        }
    }
}
