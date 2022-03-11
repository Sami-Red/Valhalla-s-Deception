using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VallhalasDeception
{
    public class Player : AnimatedObject
    {
        Point newPos;
        int speed;
        int speedY;
        bool jumpUp, dead, invi, canHit, canJump = true;
        int health;
        int invidelay;
        Rectangle attackRect;
        
        public Player(Rectangle rect, Point cellSize, string[] _paths) : base(rect, cellSize,_paths )
        {
            health = 3;
            speed = 3;
            crop = new Rectangle(0, 0, 50, 48);
            AddAnim("idle",0,Point.Zero, Point.Zero,false,1);
            AddAnim("walk",0,Point.Zero, new Point(2,0),true,15);
            AddAnim("jump",1,Point.Zero, new Point(2,0), false, 15);
            AddAnim("hurt",2,Point.Zero, new Point(2,0), false, 4);
            AddAnim("attack", 3, new Point[] { 
                new Point(0, 0), 
                new Point(1, 0), 
                new Point(2, 0), 
                new Point(0, 0), 
            }, false, 3);
            Play("idle");
            invidelay = 1500;

        }

        public void Update(GameTime gametime)
        {
            if (!dead)
            {
                newPos = rect.Location;
                if (Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    newPos.X += speed;
                    effect = SpriteEffects.None;
                    if (actualAnimation.name != "hurt" && actualAnimation.name != "attack" || actualAnimation.IsDone())
                        Play("walk");
                }
                else if (Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    newPos.X -= speed;
                    effect = SpriteEffects.FlipHorizontally;
                    if (actualAnimation.name != "hurt" && actualAnimation.name != "attack" || actualAnimation.IsDone())
                        Play("walk");
                }
                else if (actualAnimation.name == "walk")
                {
                    if (actualAnimation.name != "hurt" && actualAnimation.name != "attack" || actualAnimation.IsDone())
                        Play("idle");
                }

                if (effect == SpriteEffects.None)
                    attackRect = new Rectangle(rect.Location + new Point(32, 0), new Point(16, 32));
                else
                    attackRect = new Rectangle(rect.Location + new Point(-16, 0), new Point(16, 32));

                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    if (jumpUp && canJump)
                    {
                        canJump = false;
                        jumpUp = false;
                        speedY = -13;
                        if (actualAnimation.name != "hurt" && actualAnimation.name != "attack" || actualAnimation.IsDone())
                            Play("jump");
                    }
                }
                else
                    jumpUp = true;

                if (speedY < 5)
                {
                    speedY += 1;
                }
                else
                    speedY = 5;
                newPos.Y += speedY;

                if (Keyboard.GetState().IsKeyDown(Keys.RightShift))
                {
                    if (actualAnimation.name != "attack" || actualAnimation.IsDone())
                    {
                        Play("idle");
                        Play("attack");
                        canHit = true;
                    }
                }

                if (invi)
                {
                    invidelay -= gametime.ElapsedGameTime.Milliseconds;
                    if (invidelay < 0)
                    {
                        invi = false;
                        invidelay = 1500;
                    }
                }
            }
            else
            {
                Debug.WriteLine("DEATH");
                SceneManager.instance.GoToMenu();
            }
        }

        public void ReceiveDamage()
        {
            if (!invi)
            {
                invi = true;
                health--;
                Debug.WriteLine(health);
                if (health == 0)
                {
                    dead = true;
                }

                Play("idle");
                Play("hurt");
            }
        }

        public Point GetNewPos()
        {
            return newPos;
        }

        public void ResetJump()
        {
            if (actualAnimation.name == "jump")
                Play("idle");
            canJump = true;
        }

        public Rectangle GetAttackRect()
        {
            if (actualAnimation.name == "attack")
            {
                return attackRect;
            }
            else
                return new Rectangle();
        }

        public void SetCanHit(bool value)
        {
            canHit = value;
        }
        public bool GetCanHit()
        {
            return canHit;
        }

        public void SetHealth(int value)
        {
            health = value;
            dead = false;
        }
        public void SetNewPos(Point pos)
        {
            newPos = pos;
        }

        public int GetHealth()
        {
            return health;
        }

        public void SetSpeedY(int value)
        {
            speedY = value;
        }
    }
}
