using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Units
{
    public class TankEnemy : Enemy
    {
        public TankEnemy(Level level, Vector2 position, Vector2 size) : base(new Spritesheet(TextureLoader.Load("tankenemy")), level, position, size, 7)
        {
            this.speed = 50.0f;
            this.currentSpeed = speed;

            if (level != null)
            {
                this.maxHealth = 300.0f * level.LevelDifficulty;
            }
            this.health = maxHealth;
        }
    }
}
