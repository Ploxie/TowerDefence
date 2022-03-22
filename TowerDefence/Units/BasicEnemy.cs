using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Units
{
    public class BasicEnemy : Enemy
    {
        public BasicEnemy(Level level, Vector2 position, Vector2 size) : base(new Spritesheet(TextureLoader.Load("basicenemy")), level, position, size, 2)
        {
            this.speed = 50.0f;
            this.currentSpeed = speed;

            if (level != null)
            {
                this.maxHealth = 100.0f * level.LevelDifficulty;
            }
            this.health = maxHealth;
        }
    }
}
