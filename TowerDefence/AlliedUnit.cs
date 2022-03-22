using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public class AlliedUnit : Character
    {

        private Tile targetTile;

        public AlliedUnit(Spritesheet spritesheet, Level level, Vector2 position, Vector2 size) : base(spritesheet, level, position, size)
        {
            if(level != null)
            {
                this.targetTile = level.GetTileFromPosition(position).Previous;
            }

            this.speed = 200.0f;
            this.currentSpeed = speed;
        }

        protected Character GetClosestTarget()
        {
            List<Enemy> enemies = level.Enemies;
            Character closest = null;
            float minDistance = float.MaxValue;

            foreach (Character enemy in enemies)
            {
                float distance = Vector2.Distance(position, enemy.Position);
                if (distance < minDistance && distance < 16.0f)
                {
                    closest = enemy;
                    minDistance = distance;
                }
            }

            return closest;
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            Tile currentTile = level.GetTileFromPosition(position);
            Tile nextTile = currentTile.Previous;

            if (targetTile != null)
            {
                Vector2 direction = Vector2.Zero;
                float distance = Vector2.Distance(position, targetTile.MiddlePoint);
                if (distance <= (speed / distance)* 0.1f)
                {
                    this.targetTile = nextTile;
                }
                else
                {
                    direction = Vector2.Normalize(targetTile.MiddlePoint - position);

                }

                position += direction * speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                health = 0;
            }

            Character enemy = GetClosestTarget();
            if(enemy != null)
            {
                enemy.Damage(50);
                health = 0;
            }
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
        }
    }
}
