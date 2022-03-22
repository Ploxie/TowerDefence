using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Editor;

namespace TowerDefence
{
    public class IngameState : GameState
    {
        private Player player;
        private GameWindow window;
        private SpriteBatch spriteBatch;

        private int maxLevelIndex = 4;

        public IngameState(Player player, GameWindow window, SpriteBatch spriteBatch)
        {
            this.player = player;
            this.window = window;
            this.spriteBatch = spriteBatch;
        }

        public HUD Hud
        {
            get;
            set;
        }

        public Level Level
        {
            get;
            set;
        }

        public int LevelIndex
        {
            get;
            private set;
        }


        public void Update(GameTime gameTime)
        {           

            if(Level == null || Level.IsDone)
            {
                LevelIndex++;

                if(LevelIndex <= maxLevelIndex)
                {
                    Level = new Level(LevelData.Load("Level" + LevelIndex + ".lvl"), TextureLoader.graphicsDevice, window, spriteBatch);
                    Level.Player = player;
                    Hud.Level = Level;
                    Level.Hud = Hud;
                }
                else
                {
                    LevelIndex = 0;
                    Level.levelIndex = -1;
                }
            }

            Level.Update(gameTime);

            Hud.ShopMenu.Update(gameTime);
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            Game1.Penumbra.BeginDraw();

            TextureLoader.graphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);

            Level.Draw(spriteBatch);

            spriteBatch.End();
            Game1.Penumbra.Draw(gameTime);

            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp);
            Hud.Draw(spriteBatch);
            spriteBatch.End();
        }
    }
}
