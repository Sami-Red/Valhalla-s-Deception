using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace VallhalasDeception
{
    public interface IScene
    {
        public void Start();
        public void Update(GameTime gameTime);
        public void Draw(SpriteBatch spriteBatch);
        public void Load(ContentManager content);
        public void Reset();
    }
    public class SceneManager
    {
        IScene[] scenes;
        SceneType sceneType;
        IScene actualScene;
        public static SceneManager instance;

        public SceneManager()
        {
            scenes = new IScene[]
            {
                new Menu(),
                new Intro(),
                new HellLevel(),
                new EarthLevel(),
                new CutScene2(),
                new ValhallaLevel(),
                new CutScene3(),
                new CutScene4(),               
                new Credits(),               
            };
            foreach (IScene s in scenes)
            {
                s.Start();
            }
            instance = this;
        }

        public void Update(GameTime gameTime)
        {
            actualScene.Update(gameTime);
        }

        public void NextScene()
        {
            switch (sceneType)
            {
                case SceneType.Open:
                    actualScene = scenes[0];
                    sceneType = SceneType.Menu;
                    Debug.WriteLine("Menu");
                    break;
                case SceneType.Menu:
                    actualScene = scenes[1];
                    sceneType = SceneType.Intro;
                    Debug.WriteLine("Intro");
                    break;
                case SceneType.Intro:
                    actualScene = scenes[2];
                    sceneType = SceneType.HellLevel;
                    Debug.WriteLine("Level 1 - Hell");
                    break;
                case SceneType.HellLevel:
                    actualScene = scenes[3];
                    sceneType = SceneType.EarthLevel;
                    Debug.WriteLine("Level 2 - Earth");
                    break;
                case SceneType.EarthLevel:
                    actualScene = scenes[4];
                    sceneType = SceneType.CutScene2;
                    Debug.WriteLine("CutScene 2");
                    break;
                case SceneType.CutScene2:
                    actualScene = scenes[3];
                    sceneType = SceneType.Boss1;
                    Debug.WriteLine("Boss 1");
                    break;
                case SceneType.Boss1:
                    actualScene = scenes[5];
                    sceneType = SceneType.ValhallaLevel;
                    Debug.WriteLine("Level 3 - Vallhala");
                    break;
                case SceneType.ValhallaLevel:
                    actualScene = scenes[6];
                    sceneType = SceneType.CutScene3;
                    Debug.WriteLine("CutScene 3");
                    break;
                case SceneType.CutScene3:
                    actualScene = scenes[5];
                    sceneType = SceneType.Boss2;
                    Debug.WriteLine("Boss 2");
                    break;
                case SceneType.Boss2:
                    actualScene = scenes[7];
                    sceneType = SceneType.CutScene4;
                    Debug.WriteLine("CutScene 4");
                    break;
                case SceneType.CutScene4:
                    actualScene = scenes[8];
                    Debug.WriteLine("Credits");
                    sceneType = SceneType.Credits;
                    break;
                case SceneType.Credits:
                    Debug.WriteLine("End");
                    sceneType = SceneType.End;
                    break;
                case SceneType.End:
                    GoToMenu();
                    break;
            }
        }
        public void GoToMenu()
        {
            actualScene = scenes[0];
            foreach (IScene s in scenes)
            {
                s.Reset();
            }
            sceneType = SceneType.Menu;
        }

        public void LoadAllScenes(ContentManager content)
        {

            foreach (IScene s in scenes)
            {
                s.Load(content);
            }
            NextScene();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            actualScene.Draw(spriteBatch);
        }

        enum SceneType
        {
            Open,
            Menu,
            Intro,
            CutScene2,
            CutScene3,
            CutScene4,
            HellLevel,
            EarthLevel,
            ValhallaLevel,
            Boss1,
            Boss2,
            Credits,
            End
        }
    }
}
