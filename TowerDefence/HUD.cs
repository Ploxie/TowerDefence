using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public class HUD
    {

        private Level level;
        private ShopMenu shop;

        private Rectangle topBar;
        private Texture2D topBarBackground;
        private Texture2D topBarBorder;

        private Rectangle shopBar;

        private SpriteFont font;
        private Texture2D goldTexture;
        private Texture2D livesTexture;
        private Texture2D timeTexture;

        public HUD(Rectangle topBar, Rectangle shopBar, SpriteFont font)
        {
            this.topBar = topBar;
            this.shopBar = shopBar;
            this.font = font;

            this.topBarBackground = TextureLoader.CreateFilledRectangleTexture(topBar.Width, topBar.Height, new Color(0.1f, 0.1f, 0.1f, 0.5f));
            this.topBarBorder = TextureLoader.CreateRectangleTexture(topBar.Width, topBar.Height, new Color(0.3f, 0.3f, 0.3f, 0.8f));
            this.goldTexture = TextureLoader.Load("coins");
            this.livesTexture = TextureLoader.Load("lives");
            this.timeTexture = TextureLoader.Load("time2");
        }

        public Player Player
        {
            get;
            set;
        }

        public Level Level
        {
            get;
            set;
        }

        public ShopMenu ShopMenu
        {
            get;
            set;
        }

        private void DrawTopBar(SpriteBatch spriteBatch, Rectangle viewport)
        {
            int gold = Player.Gold;
            int lives = Player.Lives;

            int wave = Level.WaveIndex+1;
            int maxWave = Level.WaveCount;
            float waveTimer = Math.Max(0.0f, (float)Level.WaveTimer / 1000.0f);

            float y = viewport.Y + (viewport.Height / 2) - (font.MeasureString("A").Y / 2);
            float columnWidth = viewport.Width / 5;

            float columnBaseX = viewport.X + (columnWidth / 2);

            spriteBatch.Draw(topBarBackground, viewport, null, Color.White);
            spriteBatch.Draw(topBarBorder, viewport, null, Color.White);

            string goldString = " " + gold;
            int spriteSize = viewport.Height / 2;
            float spriteY = viewport.Y + (viewport.Height / 2) - (spriteSize / 2);
            Vector2 goldStringDimensions = font.MeasureString(goldString) + new Vector2(spriteSize, 0);
            spriteBatch.Draw(goldTexture, new Rectangle((int)(columnBaseX - (goldStringDimensions.X / 2)), (int)spriteY, spriteSize, spriteSize), null, Color.White, 0.0f, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(font, goldString, new Vector2(columnBaseX - ((goldStringDimensions.X) / 2) + spriteSize, y), Color.White);

            string livesString = " " + lives;
            Vector2 livesStringDimensions = font.MeasureString(livesString);
            spriteBatch.Draw(livesTexture, new Rectangle((int)((columnBaseX + columnWidth) - (livesStringDimensions.X / 2)), (int)spriteY, spriteSize, spriteSize), null, Color.White, 0.0f, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(font, livesString, new Vector2((columnBaseX + columnWidth) - (livesStringDimensions.X / 2) + spriteSize, y), Color.White);

            

            string levelString = "Level: "+Level.LevelDifficulty+"/"+4;
            Vector2 levelStringDimensions = font.MeasureString(levelString);
            spriteBatch.DrawString(font, levelString, new Vector2((columnBaseX + (columnWidth * 2)) - (levelStringDimensions.X / 2), y), Color.White);

            string waveString = "Wave: " + wave + "/"+maxWave;
            Vector2 waveStringDimensions = font.MeasureString(waveString);
            spriteBatch.DrawString(font, waveString, new Vector2((columnBaseX + (columnWidth * 3)) - (waveStringDimensions.X / 2), y), Color.White);

            string waveTimerString = " " + String.Format("{0:0.0}", waveTimer);
            Vector2 waveTimerStringDimenions = font.MeasureString(waveTimerString);
            spriteBatch.Draw(timeTexture, new Rectangle((int)((columnBaseX + (columnWidth * 4)) - (waveTimerStringDimenions.X / 2)), (int)spriteY, spriteSize, spriteSize), null, Color.White, 0.0f, new Vector2(0.5f, 0.5f), SpriteEffects.None, 0.0f);
            spriteBatch.DrawString(font, waveTimerString, new Vector2((columnBaseX + (columnWidth * 4)) - (waveTimerStringDimenions.X / 2) + spriteSize, y), Color.White);



        }

        public void Draw(SpriteBatch spriteBatch)
        {

            DrawTopBar(spriteBatch, topBar);
            ShopMenu.Draw(spriteBatch);
        }
    }
}
