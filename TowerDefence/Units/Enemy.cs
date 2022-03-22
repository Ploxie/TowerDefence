using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Units;

namespace TowerDefence
{
    public abstract class Enemy : Character
    {
        private Tile targetTile;

        private double frostTimer;
        protected double frostTime;

        protected int reward;
        protected bool fireResistant;
        protected bool frostResistant;

        public Enemy(Spritesheet spritesheet, Level level, Vector2 position, Vector2 size, int reward) : base(spritesheet, level, position, size)
        {
            if(level != null)
            {
                this.targetTile = level.GetTileFromPosition(position).Next;
                this.reward = reward * level.LevelDifficulty;
            }


            this.frostTime = 1000.0f;

            this.fireResistant = false;
            this.frostResistant = false;
        }

        public int Reward => reward;

        public bool FireResistant => fireResistant;

        public bool FrostResistant => frostResistant;

        public static Enemy Create(Enemy enemy, Level level, Vector2 position)
        {
            Enemy result = null;
            if (enemy is BasicEnemy)
            {
                result = new BasicEnemy(level, position, Vector2.One);
            }
            else if (enemy is FastEnemy)
            {
                result = new FastEnemy(level, position, Vector2.One);
            }
            else if (enemy is TankEnemy)
            {
                result = new TankEnemy(level, position, Vector2.One);
            }
            else if (enemy is FireResistantEnemy)
            {
                result = new FireResistantEnemy(level, position, Vector2.One);
            }
            else if (enemy is FrostResistantEnemy)
            {
                result = new FrostResistantEnemy(level, position, Vector2.One);
            }

            return result;
        }

        public void Freeze()
        {
            frostTimer = frostTime;
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            if(frostTimer > 0.0)
            {
                currentSpeed = speed * 0.25f;
            }
            else
            {
                currentSpeed = speed;
            }

            frostTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;

            Tile currentTile = level.GetTileFromPosition(position);
            Tile nextTile = currentTile.Next;

            if (targetTile != null)
            {
                Vector2 direction = Vector2.Zero;
                float distance = Vector2.Distance(position, targetTile.MiddlePoint);
                if (distance <= (currentSpeed / distance) * 0.1f)
                {
                    this.targetTile = nextTile;
                }
                else
                {
                    direction = Vector2.Normalize(targetTile.MiddlePoint - position);

                }

                position += direction * currentSpeed * (float)gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                level.Player.Gold -= reward;
                level.Player.Lives -= 1;
                health = 0;
            }
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {

        }
    }
}
