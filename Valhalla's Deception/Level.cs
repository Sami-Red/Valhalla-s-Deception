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
    class Level : IScene
    {
        protected Background bg;
        protected string[] texNames;
        protected float[] scrollingSpeeds;

        protected List<GameObject> level;
        protected List<GameObject> spikes;
        protected List<GameObject> ladders;
        protected List<GameObject> laddersBackup;
        protected List<Enemy> enemies;
        protected List<Enemy> enemiesBackup;
        protected Enemy enemy;
        protected Enemy boss;
        protected Enemy bossBackup;
        protected GameObject tile;
        protected GameObject[] spike;
        protected GameObject ladder;
        Texture2D layout;
        protected string layoutName;
        protected string tileName;

        protected Player player;
        Texture2D hearth;
        public virtual void Start()
        {
            player = new Player(new Rectangle(8*32,20*32,31,31), new Point(50,48), new string[] {
                @"Ragnar\WalkRight",
                @"Ragnar\JumpRight",
                @"Ragnar\HurtRight",
                @"Ragnar\AttackRight"
                });

            ladder = new GameObject(new Rectangle(0, 0, 32, 32), new Point(32, 32));
            ladders = new List<GameObject>();
            enemies = new List<Enemy>();
        }

        public void Load(ContentManager content)
        {
            spikes = new List<GameObject>();
            spike = new GameObject[]{
                new GameObject(new Rectangle(0,0,32,32),new Point(32,32)),
                new GameObject(new Rectangle(0,0,32,32),new Point(32,32)),
                new GameObject(new Rectangle(0,0,32,32),new Point(32,32)),
            };
            spike[0].Load(content, "Spike1");
            spike[1].Load(content, "Spike2");
            spike[2].Load(content, "Spike3");

            Texture2D[] tex = new Texture2D[texNames.Length];
            for (int i = 0; i < texNames.Length; i++)
            {
                tex[i] = content.Load<Texture2D>(texNames[i]);
            }
            bg = new Background(tex, scrollingSpeeds);

            level = new List<GameObject>();
            tile = new GameObject(new Rectangle(0, 0, 32, 32), new Point(32, 32));
            tile.Load(content, tileName);
            level.Add(tile.Clone());

            layout = content.Load<Texture2D>(layoutName);
            Color[] colors1D = new Color[layout.Width * layout.Height];
            layout.GetData(colors1D);
            for (int y = 0; y < layout.Height; y++)
            {
                tile.SetPos(new Point(-32, y * 32));
                level.Add(tile.Clone());
                tile.SetPos(new Point(layout.Width * 32, y * 32));
                level.Add(tile.Clone());
                for (int x = 0; x < layout.Width; x++)
                {
                    tile.SetPos(new Point(x * 32, layout.Height*32));
                    level.Add(tile.Clone());
                    if (colors1D[x + y * layout.Width] == Color.Black)
                    {
                        tile.SetPos(new Point(x * 32, y * 32));
                        level.Add(tile.Clone());
                    }
                    if (colors1D[x + y * layout.Width] == Color.Red)
                    {
                        int i = new Random().Next(spike.Length);
                        spike[i].SetPos(new Point(x * 32, y * 32));
                        spikes.Add(spike[i].Clone());
                    }
                }
            }

            Texture2D _tex = content.Load<Texture2D>("LadderMiddle");
            foreach(GameObject l in ladders)
            {
                l.SetTexture(_tex);
            }
            player.Load(content);
            enemy.Load(content);
            if (boss != null)
            {
                boss.Load(content);
                bossBackup = boss.Clone();
            }
            enemiesBackup = new List<Enemy>();
            foreach(Enemy e in enemies.ToArray())
            {
                enemiesBackup.Add(e.Clone());
            }
            laddersBackup = new List<GameObject>();
            if (ladders != null)
                foreach (GameObject l in ladders)
                    laddersBackup.Add(l.Clone());

            hearth = content.Load<Texture2D>("Hearth");
        }

        public virtual void Update(GameTime gameTime)
        {
            player.Update(gameTime);
            Game1.instance.camera.Update(player.GetPos().ToVector2()*2 ,gameTime);
            
            bool collisionX = false;
            bool collisionY = false;
            Rectangle r = player.GetRect();
            Point newPos = player.GetNewPos();
            foreach (GameObject tile in level)
            {                
                if (tile.GetRect().Intersects(new Rectangle(new Point(newPos.X, r.Y), r.Size)))
                {
                    collisionX = true;
                }
                if (tile.GetRect().Intersects(new Rectangle(new Point(r.X , newPos.Y), r.Size)))
                {
                    collisionY = true;
                    if (tile.GetPos().Y > r.Y)
                    {
                        player.ResetJump();
                        newPos.Y = tile.GetRect().Top-32;
                    }
                    if (tile.GetPos().Y > r.Y)
                    {
                        newPos.Y = r.Y;
                        player.SetSpeedY(0);
                    }
                }
            }

            foreach (GameObject s in spikes)
            {
                if (s.GetRect().Intersects(r))
                {
                    player.ReceiveDamage();
                }
            }                       

            foreach (Enemy e in enemies.ToArray()) 
            {
                e.Update(gameTime);
                if (r.Intersects(e.GetRect()))
                    player.ReceiveDamage();
                if (e.GetRect().Intersects(player.GetAttackRect()))
                {
                    if (player.GetCanHit())
                    {
                        player.SetCanHit(false);
                        if(e.ReceiveDamage())
                            enemies.Remove(e);
                    }
                }
            }

            if (collisionX)
                newPos.X = r.X;
            if (collisionY)
                newPos.Y = r.Y;

            bg.Update(gameTime, Game1.instance.camera.GetDirection());

            player.SetPos(newPos);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointWrap, null, null);
            bg.Draw(spriteBatch);
            DrawHUD(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, null, null, null,Game1.instance.camera.GetTransform());
            if(ladders!=null)
                foreach (GameObject l in ladders)
                    l.Draw(spriteBatch);

            foreach (GameObject s in spikes)
                s.Draw(spriteBatch);

            foreach (GameObject t in level)
            {
                t.Draw(spriteBatch);
            }
            foreach (Enemy e in enemies)
                e.Draw(spriteBatch);
            if(boss != null)
                boss.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null);
            DrawHUD(spriteBatch);
            spriteBatch.End();
        }

        void DrawHUD(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(hearth, new Rectangle(10,16,64,64), Color.Black);
            spriteBatch.Draw(hearth, new Rectangle(84, 16, 64, 64), Color.Black);
            spriteBatch.Draw(hearth, new Rectangle(158, 16, 64, 64), Color.Black);
            for (int i = 0; i < player.GetHealth(); i++)
            {
                spriteBatch.Draw(hearth, new Rectangle(74 *i +10, 16, 64, 64), Color.White);
            }
        }

        protected void Createladders(Point pos)
        {
            ladders = new List<GameObject>();
            do
            {
                ladder.SetPos(pos);
                ladders.Add(ladder.Clone());
                pos -= new Point(0, 32);
            } while (pos.Y>=0);
        }

        public IScene Clone()
        {
            return (IScene)MemberwiseClone();
        }

        public void Reset()
        {
            if (bossBackup != null)
                boss = bossBackup.Clone();
            enemies = new List<Enemy>();
            foreach (Enemy e in enemiesBackup.ToArray())
            {
                enemies.Add(e.Clone());
            }


            ladders = new List<GameObject>();
            if (laddersBackup != null)
                foreach (GameObject l in laddersBackup)
                    ladders.Add(l.Clone());

            player.SetPos(new Point(2 * 32, 30 * 32));
            player.SetNewPos((new Point(2 * 32, 30 * 32)));
            player.SetHealth(3);
        }
    }
}
