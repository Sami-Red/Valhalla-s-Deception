using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VallhalasDeception
{
    class HellLevel: Level
    {
        public override void Start()
        {
            base.Start();
            texNames = new string[]
            {
                "Background/Hell/1Background",
                "Background/Hell/2Clouds",
                "Background/Hell/3Backmountain",
                "Background/Hell/4FrontMountain",
                "Background/Hell/5Fence"
            };
            scrollingSpeeds = new float[]
            {
                0, 25, 50, 75, 100 
            };

            layoutName = "Level1";
            tileName = "RedFront";
            player.SetPos(new Point(2 * 32, 30 * 32));
            enemy = new Enemy(3,2,new Rectangle(0,0, 32,32), new Point(61, 55), new string[] {
                @"Enemies\Axe\WalkRight",
                @"Enemies\Axe\HurtRight",
            });
            enemy.AddAnim("walkR", 0, Point.Zero, new Point(1, 0), true, 60, true, SpriteEffects.None);
            enemy.AddAnim("walkL", 0, Point.Zero, new Point(1, 0), true, 60, true, SpriteEffects.FlipHorizontally);
            enemy.AddAnim("hurtR", 1, Point.Zero, new Point(1, 0), false, 4, true, SpriteEffects.None);
            enemy.AddAnim("hurtL", 1, Point.Zero, new Point(1, 0), false, 4, true, SpriteEffects.FlipHorizontally);
            enemy.Play("walkR");

            enemy.SetPath(new Point(0, 30 * 32));
            enemy.SetPos(new Point(16 * 32, 30 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(33 * 32, 62 * 32));
            enemy.SetPos(new Point(48 * 32, 29 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(65 * 32, 94 * 32));
            enemy.SetPos(new Point(80 * 32, 28 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(97 * 32, 117 * 32));
            enemy.SetPos(new Point(111 * 32, 27 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(81 * 32, 110 * 32));
            enemy.SetPos(new Point(95 * 32, 15 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(50 * 32, 78 * 32));
            enemy.SetPos(new Point(64 * 32, 14 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(18 * 32, 46 * 32));
            enemy.SetPos(new Point(32 * 32, 13 * 32));
            enemies.Add(enemy.Clone());

            player.SetPos(new Point(8 * 32, 29 * 32));

            Createladders(new Point(2 * 32, 11 * 32));
            //Createladders(new Point(6 * 32, 30 * 32));
            Debug.WriteLine("Start");
        }

        public override void Update(GameTime gameTime)
        {
            if (ladders.Count > 0)
                if (ladders[0].GetRect().Intersects(player.GetRect()))
                {
                    SceneManager.instance.NextScene();
                }
            base.Update(gameTime);
        }
    }
}
