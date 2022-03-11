using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    public class Camera
    {
        Matrix transform;
        Viewport viewport;
        Vector2 center;
        int direction;

        public Camera(Viewport viewport)
        {
            this.viewport = viewport;
        }

        public void Update(Vector2 target, GameTime gameTime)
        {
            if (target.X < viewport.Width / 2)
            {
                target.X = viewport.Width / 2;
            }
            else if (target.X > 128 * 64 - viewport.Width / 2)
            {
                target.X = 128 * 64 - viewport.Width / 2;
            }

            if (target.Y < viewport.Height / 2)
            {
                target.Y = viewport.Height / 2;
            }
            else if (target.Y > 32 * 64 - viewport.Height / 2)
            {
                target.Y = 32 * 64 - viewport.Height / 2;
            }

            Vector2 _center =  new Vector2(target.X - viewport.Width / 2, target.Y - viewport.Height / 2);

            if (center.X > _center.X)
                direction = -1;
            else if (center.X < _center.X)
                direction = 1;
            else
                direction = 0;
            center = _center;
            transform = Matrix.CreateScale(new Vector3(2,2,0));
            transform *= Matrix.CreateTranslation(new Vector3(-center.X, -center.Y,0));
        }

        public int GetDirection()
        {
            return direction;
        }

        public Matrix GetTransform()
        {
            return transform;
        }
    }
}
