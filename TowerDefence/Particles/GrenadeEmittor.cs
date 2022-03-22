using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Particles
{
    public class GrenadeEmittor : ParticleEmittor
    {
        private Level level;
        private float damage;

        public GrenadeEmittor(Level level, float damage, Vector2 position, float size, float speed, Color color) : base(position, 1.0f, 2000.0f, size, speed, color)
        {
            this.level = level;
            this.damage = damage;
        }

        public GrenadeEmittor(Level level, float damage, Vector2 position, float size, float speed, Color startColor, Color endColor) : base(position, 1.0f, 2000.0f, size, speed, startColor, endColor)
        {
            this.level = level;
            this.damage = damage;
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

        public Enemy Target
        {
            get;
            set;
        }

        public override void UpdateEmission(GameTime gameTime)
        {
            float rotation = MathHelper.ToDegrees(Rotation) + 90.0f;
            float fov = FieldOfView;

            float randomAngle = (float)(Game1.Random.NextDouble() * fov - (fov * 0.5f));

            float angle = MathHelper.ToRadians(randomAngle + rotation);
            Vector2 velocity = new Vector2((float)Math.Cos(angle), (float)Math.Sin(angle));
            //velocity *= (float)Game1.Random.NextDouble();

            Particle particle = new GrenadeParticle(level, Target, damage, particleTexture, Position, velocity * speed, startColor, endColor, lifeTime, size);
            particles.Add(particle);
        }
    }
}
