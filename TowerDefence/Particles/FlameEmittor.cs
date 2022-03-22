using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Particles
{
    public class FlameEmittor : ParticleEmittor
    {

        public FlameEmittor(Vector2 position, float particlesPerPulse, double lifeTime, float size, float speed, Color color) : base(position, particlesPerPulse, lifeTime, size, speed, color)
        {
        }

        public FlameEmittor(Vector2 position, float particlesPerPulse, double lifeTime, float size, float speed, Color startColor, Color endColor) : base(position, particlesPerPulse, lifeTime, size, speed, startColor, endColor)
        {
        }

        public float Rotation
        {
            get;
            set;
        }

        public float FieldOfView
        {
            get;
            set;
        }

        public override void UpdateEmission(GameTime gameTime)
        {
            emittorTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            while (emittorTimer >= 1000.0f / particlesPerSecond)
            {
                /*Vector2 velocity = new Vector2(
                 1f * (float)(Game1.Random.NextDouble() * 2 - 1),
                 1f * (float)(Game1.Random.NextDouble() * 2 - 1)
                );*/

                float rotation = MathHelper.ToDegrees(Rotation) + 90.0f;
                float fov = FieldOfView;

                float randomAngle = (float)(Game1.Random.NextDouble() * fov - (fov * 0.5f));

                float angle = MathHelper.ToRadians(randomAngle + rotation);
                Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
                velocity *= (float)Game1.Random.NextDouble();

                Particle particle = new Particle(particleTexture, Position, velocity * speed, startColor, endColor, lifeTime, size);
                particles.Add(particle);
                emittorTimer -= 1000.0f / particlesPerSecond;
            }
        }

    }
}
