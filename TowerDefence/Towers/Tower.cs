using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Effects;
using TowerDefence.Towers;
using Effect = TowerDefence.Effects.Effect;

namespace TowerDefence
{
    public abstract class Tower : GameObject
    {
        
        protected float visionRadius;

        protected float rotation;

        protected float attackSpeed;
        protected float damage;
        protected double attackTimer;

        protected Character currentTarget;
        protected Character lastTarget;

        protected int upgrades = 0;
        
        public Tower(Spritesheet spritesheet, Level level, Vector2 position, Vector2 size) : base(spritesheet, level, position, size)
        {        
            this.visionRadius = 100.0f;

            this.attackSpeed = 3.0f;
            this.damage = 15.0f;
            this.rotation = MathHelper.ToRadians(180.0f);
        }

        public float VisionRadius => visionRadius;

        public Rectangle Bounds
        {
            get => new Rectangle((int)(position.X - ((size.X * spritesheet.Texture.Width) / 2)), (int)(position.Y - ((size.Y * spritesheet.Texture.Height) / 2)), (int)(size.X * spritesheet.Texture.Width), (int)(size.Y * spritesheet.Texture.Height));
        }

        public virtual int UpgradeCost
        {
            get
            {
                return 0;
            }
        }

        public static Tower Create(Tower tower, Level level, Vector2 position)
        {
            Tower result = null;
            if (tower is CannonTower)
            {
                result = new CannonTower(level, position, Vector2.One);
            }
            else if (tower is DoubleBarrelTower)
            {
                result = new DoubleBarrelTower(level, position, Vector2.One);
            }
            else if (tower is LaserTower)
            {
                result = new LaserTower(level, position, Vector2.One);
            }
            else if (tower is FrostTower)
            {
                result = new FrostTower(level, position, Vector2.One);
            }
            else if (tower is FlamethrowerTower)
            {
                result = new FlamethrowerTower(level, position, Vector2.One);
            }
            else if (tower is GranadeTower)
            {
                result = new GranadeTower(level, position, Vector2.One);
            }

            return result;
        }
        
        public bool Upgrade()
        {
            if(upgrades <= 2) {
                attackSpeed *= 1.2f;
                damage *= 1.2f;
                visionRadius *= 1.2f;

                upgrades++;
                return true;
            }
            return false;
        }

        protected Character GetClosestTarget()
        {
            List<Enemy> enemies = level.Enemies;
            Character closest =  null;
            float minDistance = float.MaxValue;

            foreach(Character enemy in enemies)
            {
                float distance = Vector2.Distance(position, enemy.Position);
                if (distance < minDistance && distance < visionRadius)
                {
                    closest = enemy;
                    minDistance = distance;
                }
            }

            return closest;
        }

        protected virtual void Shoot(GameTime gameTime, Character target)
        {
            target.Damage(damage);
            attackTimer = 0.0f;
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            Character target = GetClosestTarget();
            currentTarget = target;
            if(target != null)
            {
                float angle = (float)Math.Atan2(position.Y - target.Position.Y, position.X - target.Position.X);
                rotation = angle + MathHelper.ToRadians(90);

                if(attackTimer >= 1000.0f / attackSpeed)
                {
                    Shoot(gameTime, target);
                }

                lastTarget = target;
            }

            attackTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {

        }

        
    }
}
