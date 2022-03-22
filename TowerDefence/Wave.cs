using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Units;

namespace TowerDefence
{
    public class Wave
    {

        public int SpawnIndex
        {
            get;
            set;
        }

        public List<Enemy> Enemies
        {
            get;
            set;
        }

        public double EnemiesPerSecond
        {
            get;
            set;
        }


        public static List<List<Wave>> WAVES;

        public static void CreateWaves()
        {
            WAVES = new List<List<Wave>>();
            List<Wave> firstLevel = new List<Wave>() // FIRST LEVEL
            {
                new Wave()
                {
                    EnemiesPerSecond = 1,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 1,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 1,
                    Enemies = new List<Enemy>()
                    {
                        new FastEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FrostResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FireResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new FastEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FrostResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FireResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                }
            };

            List<Wave> secondLevel = new List<Wave>() // SECOND LEVEL
            {
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 1,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                }
            };

            List<Wave> thirdLevel = new List<Wave>() // FIRST LEVEL
            {
                new Wave()
                {
                    EnemiesPerSecond = 1,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 1,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 1,
                    Enemies = new List<Enemy>()
                    {
                        new FastEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FrostResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FireResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new FastEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FrostResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new FireResistantEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                }
            };

            List<Wave> fourthLevel = new List<Wave>() // SECOND LEVEL
            {
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                },
                new Wave()
                {
                    EnemiesPerSecond = 2,
                    Enemies = new List<Enemy>()
                    {
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                        new TankEnemy(null, Vector2.Zero, Vector2.Zero),
                         new BasicEnemy(null, Vector2.Zero, Vector2.Zero),
                    },
                }
            };

            WAVES.Add(firstLevel);
            WAVES.Add(secondLevel);
            WAVES.Add(thirdLevel);
            WAVES.Add(fourthLevel);

            // Denna koden är 100% pga för lite tid...
        }
    }
}
