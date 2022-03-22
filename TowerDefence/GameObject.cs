using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public abstract class GameObject
    {
        protected Spritesheet spritesheet;
        protected Level level;
        protected Vector2 position;
        protected Vector2 size;

        protected double animationFrameRate;
        protected double animationTimer;

        public GameObject(Spritesheet spritesheet, Level level, Vector2 position, Vector2 size)
        {
            this.spritesheet = spritesheet;
            this.level = level;
            this.position = position;
            this.size = size;
        }

        public Spritesheet Spritesheet => spritesheet;

        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public Vector2 Size
        {
            get => size;
            set => size = value;
        }

        public Level Level
        {
            get => level;
            set => level = value;
        }

        protected void UpdateAnimation(GameTime gameTime)
        {
            if(animationTimer >= animationFrameRate)
            {
                InternalUpdateAnimation(gameTime);
                animationTimer = 0.0;
            }
            else
            {
                animationTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateAnimation(gameTime);
            InternalUpdate(gameTime);
        }

        protected abstract void InternalUpdateAnimation(GameTime gameTime);

        protected abstract void InternalUpdate(GameTime gameTime);

        public virtual void Draw(SpriteBatch spriteBatch, Color? color = null)
        {
            color = color == null ? Color.White : color;
            spritesheet.Draw(spriteBatch, position, color.Value, 0.0f, new Vector2(spritesheet.Texture.Width * 0.5f, spritesheet.Texture.Height * 0.5f), size, SpriteEffects.None);
        }

        public virtual void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            spritesheet.Draw(spriteBatch, destination, Color.White, 0.0f, Vector2.Zero, size, SpriteEffects.None);
        }

    }
}
