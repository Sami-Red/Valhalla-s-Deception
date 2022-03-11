using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    class EarthLevel: Level
    {

        public override void Start()
        {
            base.Start();
            texNames = new string[]
            {
                "Background/Earth/1Background",
                "Background/Earth/2Clouds",
                "Background/Earth/3Mountain",
                "Background/Earth/4Hill",
                "Background/Earth/5Field",
                "Background/Earth/6FrontTrees"
            };
            scrollingSpeeds = new float[]
            {
                0, 16, 32, 48, 64, 100
            };
            player.SetPos(new Point(2 * 32, 30 * 32));

            layoutName = "Level2";
            tileName = "GreenFront";

            enemy = new Enemy(6, 2, new Rectangle(0, 0, 32, 32), new Point(50, 48), new string[] {
                @"Enemies\Sword\WalkRight",
                @"Enemies\Sword\HurtRight",
            });
            enemy.AddAnim("walkR", 0, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.None);
            enemy.AddAnim("walkL", 0, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.FlipHorizontally);
            enemy.AddAnim("hurtR", 1, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.None);
            enemy.AddAnim("hurtL", 1, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.FlipHorizontally);
            enemy.Play("walkR");

            enemy.SetPath(new Point(15 * 32, 22 * 32));
            enemy.SetPos(new Point(15 * 32, 30 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(30 * 32, 37 * 32));
            enemy.SetPos(new Point(30 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(30 * 32, 37 * 32));
            enemy.SetPos(new Point(37 * 32, 30 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(45 * 32, 50 * 32));
            enemy.SetPos(new Point(45 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(45 * 32, 50 * 32));
            enemy.SetPos(new Point(50 * 32, 30 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(58 * 32, 76 * 32));
            enemy.SetPos(new Point(58 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(58 * 32, 76 * 32));
            enemy.SetPos(new Point(61 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(58 * 32, 76 * 32));
            enemy.SetPos(new Point(64 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(58 * 32, 76 * 32));
            enemy.SetPos(new Point(67 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(58 * 32, 76 * 32));
            enemy.SetPos(new Point(70 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(58 * 32, 76 * 32));
            enemy.SetPos(new Point(73 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(58 * 32, 76 * 32));
            enemy.SetPos(new Point(76 * 32, 30 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(93 * 32, 103 * 32));
            enemy.SetPos(new Point(93 * 32, 30 * 32));
            enemies.Add(enemy.Clone());


            boss = new Enemy(30,1,new Rectangle(116*32,9*32,32,32),new Point(50,48), new string[] {
                @"Enemies\Loki\WalkLeft",
                @"Enemies\Loki\HurtRight",
            });
            boss.AddAnim("walkR", 0, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.None);
            boss.AddAnim("walkL", 0, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.FlipHorizontally);
            boss.AddAnim("hurtR", 1, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.None);
            boss.AddAnim("hurtL", 1, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.FlipHorizontally);
            boss.Play("walkR");
            boss.SetPath(new Point(104*32, 127*32));

            Createladders(new Point(125*32, 9*32));
            //Createladders(new Point(6 * 32, 30 * 32));
            boss.SetDisable(true);
        }

        public override void Update(GameTime gameTime)
        {
            if (ladders!= null)
            {
                if (ladders[0].GetRect().Intersects(player.GetRect()))
                {
                    ladders = null;
                    boss.SetDisable(false);
                    SceneManager.instance.NextScene();
                }
            }
            else
            {
                boss.Update(gameTime);
                if (boss.GetRect().Intersects(player.GetRect()))
                {
                    player.ReceiveDamage();
                }
                if (boss.GetRect().Intersects(player.GetAttackRect()))
                {
                    if (player.GetCanHit())
                    {
                        player.SetCanHit(false);
                        if (boss.ReceiveDamage())
                            SceneManager.instance.NextScene();
                    }
                }
            }
            base.Update(gameTime);
        }
    }
}
