using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Units
{
    public class FastEnemy : Enemy
    {
        public FastEnemy(Level level, Vector2 position, Vector2 size) : base(new Spritesheet(TextureLoader.Load("fastenemy")), level, position, size, 4)
        {
            this.speed = 100.0f;
            this.currentSpeed = speed;

            if(level != null)
            {
                this.maxHealth = 100.0f * level.LevelDifficulty;
            }
            this.health = maxHealth;
        }
    }
}
