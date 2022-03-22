using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public class Spritesheet
    {

        private Texture2D texture;
        private Vector2 start, spriteSize;
        private int xIndex, yIndex;
        private int columns, rows;

        private int offset;

        public Spritesheet(Texture2D texture)
        {
            this.texture = texture;
            this.start = Vector2.Zero;
            this.spriteSize = new Vector2(texture.Width, texture.Height);

            this.columns = 1;
            this.rows = 1;
        }

        public Spritesheet(Texture2D texture, Vector2 start, Vector2 dimensions, Vector2 spriteSize, int offset = 0)
        {
            this.texture = texture;
            this.start = start;
            this.spriteSize = spriteSize;

            this.columns = (int)(dimensions.X / spriteSize.X);
            this.rows = (int)(dimensions.Y / spriteSize.Y);
            this.offset = offset;
        }

        public Texture2D Texture => texture;

        public int XIndex
        {
            get => xIndex;
            set => this.xIndex = value;
        }

        public int YIndex
        {
            get => yIndex;
            set => this.yIndex = value;
        }

        public int Columns => columns;

        public int Rows => rows;

        public Spritesheet GetSubSheetAt(int x, int y, int width, int height)
        {
            return GetSubSheet(x, y, width, height, spriteSize);
        }

        public Spritesheet GetSubSheet(int x, int y, int width, int height, Vector2 spriteSize)
        {
            return new Spritesheet(texture, new Vector2(start.X + (x * spriteSize.X), start.Y + (y * spriteSize.Y)), new Vector2(width * spriteSize.X, height * spriteSize.Y), spriteSize, offset); 
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position)
        {
            Draw(spriteBatch, position, Color.White, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None);
        }

        public void Draw(SpriteBatch spriteBatch, Vector2 position, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects)
        {
            Vector2 sheetPosition = new Vector2(start.X + (spriteSize.X * XIndex) + (offset * XIndex), start.Y + (spriteSize.Y * YIndex) + (offset * YIndex));
            Rectangle source = new Rectangle((int)sheetPosition.X, (int)sheetPosition.Y, (int)spriteSize.X, (int)spriteSize.Y);
            spriteBatch.Draw(texture, position, source, color, rotation, origin, scale, spriteEffects, 0.8f);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            Draw(spriteBatch, destination, Color.White, 0.0f, Vector2.Zero, Vector2.One, SpriteEffects.None);
        }

        public void Draw(SpriteBatch spriteBatch, Rectangle destination, Color color, float rotation, Vector2 origin, Vector2 scale, SpriteEffects spriteEffects)
        {
            Vector2 sheetPosition = new Vector2(start.X + (spriteSize.X * XIndex) + (offset * XIndex), start.Y + (spriteSize.Y * YIndex) + (offset * YIndex));
            Rectangle source = new Rectangle((int)sheetPosition.X, (int)sheetPosition.Y, (int)spriteSize.X, (int)spriteSize.Y);
            spriteBatch.Draw(texture, destination, source, color, rotation, origin, spriteEffects, 1.0f);
        }

    }
}
