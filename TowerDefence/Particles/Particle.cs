using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public class Particle
    {

        private Texture2D texture;
        protected Vector2 position;
        private Vector2 velocity;
        private Color color;
        private Color startColor;
        private Color endColor;
        private float size;

        protected double lifeTime;
        protected double lifeTimer;

        private Light light;

        public Particle(Texture2D texture, Vector2 position, Vector2 velocity, Color startColor, Color endColor, double lifeTime, float size)
        {
            this.texture = texture;
            this.position = position;
            this.velocity = velocity;

            this.color = startColor;
            this.startColor = startColor;
            this.endColor = endColor;

            this.lifeTime = lifeTime;
            this.size = size;

            this.light = new PointLight()
            {
                Position = position,//(100.0f * (float)lifeTime) / 1000.0f, // particle speed * particle lifetime / one second
                ShadowType = ShadowType.Solid,
                Scale = new Vector2(size * 8),
                Color = Color.White
            };


            Game1.Penumbra.Lights.Add(light);
        }

        public Light Light => light;

        public virtual bool IsDead
        {
            get => lifeTimer >= lifeTime;
        }

        public virtual void Update(GameTime gameTime)
        {
            position += velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

            float deathPercentage = (float)(lifeTimer / lifeTime);
            
            color = Color.Lerp(startColor, endColor, deathPercentage);

            lifeTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            light.Position = position;
            light.Color = color;
            light.Scale = new Vector2((size * 8.0f) * (1.0f - deathPercentage));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if(!IsDead)
            {
                spriteBatch.Draw(texture, position, null, color, 0.0f, new Vector2(0.5f), size, SpriteEffects.None, 1.0f);
            }
        }

    }
}
