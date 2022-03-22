using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Effects;

namespace TowerDefence.Particles
{
    public class GrenadeParticle : Particle
    {
        private Level level;
        private Enemy target;
        private float damage;

        private bool exploded;

        public GrenadeParticle(Level level, Enemy target, float damage, Texture2D texture, Vector2 position, Vector2 velocity, Color startColor, Color endColor, double lifeTime, float size) : base(texture, position, velocity, startColor, endColor, lifeTime, size)
        {
            this.level = level;
            this.target = target;
            this.damage = damage;
        }

        public override bool IsDead => base.IsDead || exploded;

        protected Character GetClosestTarget()
        {
            List<Enemy> enemies = level.Enemies;
            Character closest = null;
            float minDistance = float.MaxValue;

            foreach (Character enemy in enemies)
            {
                float distance = Vector2.Distance(position, enemy.Position);
                if (distance < minDistance && distance <= 20.0f)
                {
                    closest = enemy;
                    minDistance = distance;
                }
            }

            return closest;
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            Character closest = GetClosestTarget();
            Character t = closest ?? target;
            if (Vector2.Distance(t.Position, position) <= 20.0f || IsDead)
            {
                foreach (Enemy enemy in level.Enemies)
                {
                    if (Vector2.Distance(position, enemy.Position) <= 55.0f)
                    {
                        enemy.Damage(damage);
                    }
                }

                level.Effects.Add(new ExplosionEffect(position));
                lifeTimer = lifeTime;
                exploded = true;
            }
        }

    }
}
