using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Particles
{
    public class PulseEmittor : ParticleEmittor
    {
        private double pulseInterval;

        private bool hasEmitted;

        public PulseEmittor(Vector2 position, float particlesPerPulse, double pulseInterval, double lifeTime, float size, float speed, Color color) : base(position, particlesPerPulse, lifeTime, size, speed, color)
        {
            this.pulseInterval = pulseInterval;
            emittorTimer = pulseInterval;
        }

        public PulseEmittor(Vector2 position, float particlesPerPulse, double pulseInterval, double lifeTime, float size, float speed, Color startColor, Color endColor) : base(position, particlesPerPulse, lifeTime, size, speed, startColor, endColor)
        {
            this.pulseInterval = pulseInterval;
            emittorTimer = pulseInterval;
        }

        public override bool Active 
        { 
            get => emittorTimer < pulseInterval + lifeTime; 
            set => base.Active = value;
        }

        public override void UpdateEmission(GameTime gameTime)
        {
            emittorTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if (emittorTimer >= pulseInterval && !hasEmitted)
            {
                for (int i = 0; i < particlesPerSecond; i++)
                {
                    /*Vector2 velocity = new Vector2(
                     1f * (float)(Game1.Random.NextDouble() * 2 - 1),
                     1f * (float)(Game1.Random.NextDouble() * 2 - 1)
                    );*/

                    float angle = MathHelper.ToRadians((float)(Game1.Random.NextDouble() * 360.0f));
                    Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                    velocity *= (float)Game1.Random.NextDouble();
                    //velocity.Normalize();

                    Particle particle = new Particle(particleTexture, position, velocity * speed, startColor, endColor, lifeTime, size);
                    particles.Add(particle);
                }
                hasEmitted = true;
            }
        }
    }
}
