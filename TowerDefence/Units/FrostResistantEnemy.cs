using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Units
{
    public class FrostResistantEnemy : Enemy
    {
        public FrostResistantEnemy(Level level, Vector2 position, Vector2 size) : base(new Spritesheet(TextureLoader.Load("frostresistant")), level, position, size, 5)
        {
            this.speed = 50.0f;
            this.currentSpeed = speed;

            if (level != null)
            {
                this.maxHealth = 100.0f * level.LevelDifficulty;
            }
            this.health = maxHealth;

            this.frostResistant = true;
        }
    }
}
