using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Effects;
using TowerDefence.Particles;

namespace TowerDefence
{
    public abstract class Character : GameObject
    {
        protected float speed;
        protected float currentSpeed;
        protected float health;
        protected float maxHealth;

        private Texture2D healthbarTexture;
        private Texture2D healthbarBackgroundTexture;
        
        public Character(Spritesheet spritesheet, Level level, Vector2 position, Vector2 size) : base(spritesheet, level, position, size)
        {
            this.speed = 50.0f;
            this.currentSpeed = speed;

            this.healthbarBackgroundTexture = TextureLoader.CreateFilledBorderedRectangleTexture(50, 8, Color.Red, Color.Black);
            this.healthbarTexture = TextureLoader.CreateFilledRectangleTexture(48, 6, Color.Green);

            this.maxHealth = 100.0f;
            this.health = maxHealth;
        }

        public float Health => health;

        public void Damage(float damage)
        {
            this.health -= damage;
        }

        
        public override void Draw(SpriteBatch spriteBatch, Color? color = null)
        {
            base.Draw(spriteBatch, color);

            //DrawHealthbar(spriteBatch);

        }

        public void DrawHealthbar(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(healthbarBackgroundTexture, position + new Vector2(0, -25), null, Color.White, 0.0f, new Vector2(25, 4), Vector2.One, SpriteEffects.None, 1.0f);

            float healthPercentage = health / maxHealth;

            spriteBatch.Draw(healthbarTexture, position + new Vector2(1, -24), new Rectangle(0, 0, (int)(healthbarTexture.Width * healthPercentage), healthbarTexture.Height), Color.White, 0.0f, new Vector2(25, 4), Vector2.One, SpriteEffects.None, 1.0f); ;
        }

    }
}
