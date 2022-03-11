using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace VallhalasDeception
{
    public class AnimatedObject : GameObject
    {
        Texture2D[] textures;
        protected List<Animation> animations;
        protected Animation actualAnimation;
        protected string[] paths;
        public AnimatedObject(Rectangle _rect, Point _cellSize, string[] _paths) : base(_rect, _cellSize)
        {
            paths = _paths;
            animations = new List<Animation>();
            cellSize = _cellSize;
            textures = new Texture2D[paths.Length];
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (actualAnimation == null)
            {
                if (animations.Count == 0)
                    return;
                actualAnimation = animations[0];
            }
            texture = textures[actualAnimation.indexTexture];
            Point pos = actualAnimation.GetFrame();
            crop = new Rectangle(pos.X * cellSize.X, pos.Y * cellSize.Y, cellSize.X, cellSize.Y);
            if(actualAnimation.useEffect)
                effect = actualAnimation.effect;
            spriteBatch.Draw(texture, rect, crop, Color.White, rot, origin, effect, 0);

        }

        public void AddAnim(string _name, int _tex, Point start, Point end, bool _loop, int _speed = 1, bool _useEffect = false, SpriteEffects _effect = SpriteEffects.None)
        {
            List<Point> _pos = new List<Point>();
            for (int y = start.Y; y <= end.Y; y++)
            {
                for (int x = start.X; x <= end.X; x++)
                {
                    _pos.Add(new Point(x, y));
                }
            }
            animations.Add(new Animation(_name, _tex, _pos, _loop, _speed, _useEffect, _effect));
        }

        public void AddAnim(string _name, int _tex, Point[] frames, bool _loop, int _speed = 1, bool _useEffect = false, SpriteEffects _effect = SpriteEffects.None)
        {
            animations.Add(new Animation(_name, _tex, frames.ToList(), _loop, _speed, _useEffect, _effect));
        }

        public void Load(ContentManager content)
        {
            for(int i=0; i < paths.Length; i++)
            {
                textures[i] = content.Load<Texture2D>(paths[i]);
            }
        } 

        public void AddAllAnim(Animation[] _animations)
        {
            animations = _animations.ToList();
        }

        public void Play(string _name)
        {
            foreach (Animation anim in animations)
            {
                if (anim.name == _name)
                {
                    if (anim != actualAnimation)
                    {
                        actualAnimation = anim;
                        actualAnimation.index = 0;
                    }
                    break;
                }
            }
        }

        public bool IsDone()
        {
            return actualAnimation.IsDone();
        }

        public class Animation
        {
            internal string name;
            internal int index;
            internal int indexTexture;
            internal List<Point> pos;
            internal bool loop, useEffect;
            internal SpriteEffects effect;
            int speed;
            int count;

            public Animation(string _name,int tex, List<Point> _pos, bool _loop, int _speed = 1, bool _useEffect = false, SpriteEffects _effect = SpriteEffects.None)
            {
                name = _name;
                pos = _pos;
                loop = _loop;
                speed = _speed;
                indexTexture = tex;
                effect = _effect;
                useEffect = _useEffect;
            }

            public Point GetFrame()
            {
                Point _pos = pos[index];
                count++;
                if (count == speed)
                {
                    count = 0;
                    index++;
                }
                if (index == pos.Count)
                {
                    if (loop)
                        index = 0;
                    else
                        index = pos.Count - 1;
                }
                return _pos;
            }

            public bool IsDone()
            {
                return index == pos.Count - 1;
            }
        }
    }
}
