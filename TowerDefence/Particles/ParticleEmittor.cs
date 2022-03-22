using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace TowerDefence.Particles
{
    public class ParticleEmittor
    {
        protected Texture2D particleTexture;

        protected Vector2 position;

        protected float particlesPerSecond;
        protected double lifeTime;
        protected float size;
        protected float speed;

        protected Color startColor;
        protected Color endColor;

        protected double emittorTimer;
        
        protected List<Particle> particles;
        
        public ParticleEmittor(Vector2 position,  float particlesPerSecond, double lifeTime, float size, float speed, Color color)
        {
            this.particleTexture = TextureLoader.CreateFilledRectangleTexture(1, 1, new Color(255,255,255, 255));

            this.position = position;
            this.particlesPerSecond = particlesPerSecond;
            this.lifeTime = lifeTime;
            this.size = size;
            this.speed = speed;
            this.startColor = color;
            this.endColor = color;

            Active = true;

            this.particles = new List<Particle>();
        }
        public ParticleEmittor(Vector2 position, float particlesPerSecond, double lifeTime, float size, float speed, Color startColor, Color endColor)
        {
            this.particleTexture = TextureLoader.CreateFilledRectangleTexture(1, 1, new Color(255, 255, 255, 255));

            this.position = position;
            this.particlesPerSecond = particlesPerSecond;
            this.lifeTime = lifeTime;
            this.size = size;
            this.speed = speed;
            this.startColor = startColor;
            this.endColor = endColor;

            Active = true;

            this.particles = new List<Particle>();
        }
        public Vector2 Position
        {
            get => position;
            set => position = value;
        }

        public virtual bool Active
        {
            get;
            set;
        }


        protected virtual void UpdateParticles(GameTime gameTime)
        {
            for (int i = particles.Count - 1; i >= 0; i--)
            {
                particles[i].Update(gameTime);
                if (particles[i].IsDead)
                {
                    Game1.Penumbra.Lights.Remove(particles[i].Light);
                    particles.RemoveAt(i);
                }
            }
        }

        public virtual void UpdateEmission(GameTime gameTime)
        {
            emittorTimer += gameTime.ElapsedGameTime.TotalMilliseconds;
            while (emittorTimer >= 1000.0f / particlesPerSecond)
            {
                float angle = MathHelper.ToRadians((float)(Game1.Random.NextDouble() * 360.0f));
                Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                velocity *= (float)Game1.Random.NextDouble();

                Particle particle = new Particle(particleTexture, position, velocity * speed, startColor, endColor, lifeTime, size);
                particles.Add(particle);
                emittorTimer -= 1000.0f / particlesPerSecond;
            }
        }

        public void Update(GameTime gameTime)
        {
            UpdateParticles(gameTime);

            if(Active)
            {
                UpdateEmission(gameTime);           
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(Particle particle in particles)
            {
                particle.Draw(spriteBatch);
            }
        }

    }
}
