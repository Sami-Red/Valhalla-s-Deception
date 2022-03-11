using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace VallhalasDeception
{
    class ValhallaLevel: Level
    { 
        public override void Start()
        {
            base.Start();
            texNames = new string[]
            {
                "Background/Valhalla/1background",
                "Background/Valhalla/2moon",
                "Background/Valhalla/3stars",
                "Background/Valhalla/5castle",
                "Background/Valhalla/4clouds",
            };
            scrollingSpeeds = new float[]
            {
                0, 0, 10, 0, 50
            };

            player.SetPos(new Point(2 * 32, 30 * 32));
            layoutName = "Level3";
            tileName = "BlueFront";
            enemy = new Enemy(6, 2, new Rectangle(0, 0, 32, 32), new Point(50, 48), new string[] {
                @"Enemies\Sword\WalkRight",
                @"Enemies\Sword\HurtRight",
            });
            enemy.AddAnim("walkR", 0, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.None);
            enemy.AddAnim("walkL", 0, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.FlipHorizontally);
            enemy.AddAnim("hurtR", 1, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.None);
            enemy.AddAnim("hurtL", 1, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.FlipHorizontally);
            enemy.Play("walkR");

            enemy.SetPath(new Point(14 * 32, 21 * 32));
            enemy.SetPos(new Point(14 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(14 * 32, 21 * 32));
            enemy.SetPos(new Point(21 * 32, 30 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(39 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(47 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(55 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(63 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(68 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(76 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(84 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(92 * 32, 30 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(39 * 32, 110 * 32));
            enemy.SetPos(new Point(100 * 32, 30 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(51 * 32, 55 * 32));
            enemy.SetPos(new Point(51 * 32, 24 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(57 * 32, 62 * 32));
            enemy.SetPos(new Point(57 * 32, 24 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(64 * 32, 69 * 32));
            enemy.SetPos(new Point(64 * 32, 24 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(71 * 32, 75 * 32));
            enemy.SetPos(new Point(71 * 32, 24 * 32));
            enemies.Add(enemy.Clone());

            enemy.SetPath(new Point(85 * 32, 87 * 32));
            enemy.SetPos(new Point(85 * 32, 22 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(91 * 32, 93 * 32));
            enemy.SetPos(new Point(91 * 32, 22 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(97 * 32, 99 * 32));
            enemy.SetPos(new Point(97 * 32, 22 * 32));
            enemies.Add(enemy.Clone());
            enemy.SetPath(new Point(103 * 32, 105 * 32));
            enemy.SetPos(new Point(103 * 32, 22 * 32));
            enemies.Add(enemy.Clone());

            boss = new Enemy(40, 2, new Rectangle(125 * 32, 22 * 32, 32, 32), new Point(50, 48), new string[] {
                @"Enemies\Odin\WalkRight",
                @"Enemies\Odin\WalkLeft",
                @"Enemies\Odin\HurtRight",
                @"Enemies\Odin\HurtLeft",
            });
            boss.AddAnim("walkR", 0, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.None);
            boss.AddAnim("walkL", 1, Point.Zero, new Point(2, 0), true, 60, true, SpriteEffects.None);
            boss.AddAnim("hurtR", 2, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.None);
            boss.AddAnim("hurtL", 3, Point.Zero, new Point(2, 0), false, 4, true, SpriteEffects.None);
            boss.Play("walkR");
            boss.SetPath(new Point(109*32,127*32));


            boss.SetDisable(true);
        }

        public override void Update(GameTime gameTime)
        {
            if (player.GetPos().X > 111*32 && boss.GetDisable())
            {
                boss.SetDisable(false);
                SceneManager.instance.NextScene();                
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
