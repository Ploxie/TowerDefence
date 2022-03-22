using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Editor;
using TowerDefence.Towers;
using TowerDefence.Units;
using Effect = TowerDefence.Effects.Effect;

namespace TowerDefence
{
    public class Level
    {
        public static int levelIndex = -1;

        private int width, height;
        private int tileSize;
        private Tile[] tiles;
        private Tile[] startTiles;
        private Tile endTile;

        private int startTileIndex;

        private List<Enemy> enemies;
        private List<AlliedUnit> allies;
        private List<Tower> towers;
        private List<Effect> effects;
        
        private GraphicsDevice graphicsDevice;
        private SpriteBatch spriteBatch;
        private RenderTarget2D placemap;
        private Texture2D placemapTexture;
        private Texture2D radiusTexture;
        private Texture2D baseTexture;
        private Color grassColor;

        private bool completedLevel;
        private bool readyForNext;

        private bool canPlace;
        private Vector2 placePos;

        private List<Wave> waves;
        private int waveIndex;
        private double waveTimer;
        private double spawnTimer;

        private Tower hoveredTower;
        private Tower selectedTower;

        private SpriteFont font;
        private double allySpawnTimer;
        private int alliesSpawned;

        private GameWindow window;
        private int level;


        public Level(LevelData data, GraphicsDevice graphicsDevice, GameWindow window, SpriteBatch spriteBatch)
        {
            levelIndex++;
            this.level = levelIndex+1;
            this.window = window;
            this.width = data.Width;
            this.height = data.Height;
            this.tileSize = data.TileSize;
            this.startTiles = new Tile[data.StartPositions.Length];

            this.enemies = new List<Enemy>();
            this.allies = new List<AlliedUnit>();
            this.towers = new List<Tower>();
            this.effects = new List<Effect>();
            this.font = TextureLoader.LoadFont("font");

            this.graphicsDevice = graphicsDevice;
            this.spriteBatch = spriteBatch;
            this.placemap = new RenderTarget2D(graphicsDevice, window.ClientBounds.Width, window.ClientBounds.Height);
            this.grassColor = data.GrassColor;

            this.radiusTexture = TextureLoader.CreateCenteredCircleTexture(2000, 2000, 1000, new Color(64, 255, 64, 128));
            this.baseTexture = TextureLoader.Load("base");
            
            Texture2D roadTileSet = TextureLoader.Load(data.TilesheetPath);
            Texture2D grassTexture = TextureLoader.CreateFilledRectangleTexture(32, 32, data.GrassColor);
            Texture2D[] roadTextures = new Texture2D[16];
            for (int i = 0; i < roadTextures.Length; i++)
            {
                roadTextures[i] = roadTileSet.GetSubTexture(new Rectangle((i % (4)) * 32, (i / (4)) * 32, 32, 32));
                roadTextures[i].SwapColor(Color.Red, data.RoadColor);
                roadTextures[i].SwapColor(new Color(0, 255, 0), data.BorderColor);
                roadTextures[i].SwapColor(Color.Blue, data.GrassColor);
            }

            this.tiles = new Tile[width * height];
            for(int i = 0; i < tiles.Length;i++)
            {
                int x = i % width;
                int y = i / width;

                int textureID = data.TileIDs[i];
                Texture2D texture = textureID != -1 ? roadTextures[textureID] : grassTexture;
                tiles[i] = new Tile(new Vector2(x * tileSize, y * tileSize), texture);
            }

            for(int i = 0; i < data.PathIDs.Length; i++)
            {
                int id = data.PathIDs[i];
                if(id != -1)
                {
                    tiles[i].Next = tiles[id];
                    tiles[i].Next.Previous = tiles[i];
                }
            }

            for(int i = 0; i < data.StartPositions.Length; i++)
            {
                int index = (int)(data.StartPositions[i].Y * width + data.StartPositions[i].X);
                startTiles[i] = tiles[index];
            }

            endTile = tiles[(int)(data.EndPosition.Y * width + data.EndPosition.X)];

            this.waves = Wave.WAVES[levelIndex];
            this.waveIndex = 0;
            this.waveTimer = 10000.0f;


            SpawnAlly(endTile.MiddlePoint);
            UpdatePlaceMap();
        }

        public int Width => width;

        public int Height => height;

        public int TileSize => tileSize;

        public Tile[] Tiles => tiles;

        public List<Enemy> Enemies => enemies;

        public List<Effect> Effects => effects;

        public int WaveIndex => waveIndex;

        public int WaveCount => waves.Count;

        public double WaveTimer => waveTimer;

        public bool IsDone => completedLevel && readyForNext;

        public int LevelDifficulty => level;

        public Player Player
        {
            get;
            set;
        }

        public HUD Hud
        {
            get;
            set;
        }

        public Tower SelectedTower => selectedTower;

        private Tile GetTileAt(int x, int y)
        {
            if(x < 0 || x >= width || y < 0 || y >= height)
            {
                return null;
            }
            return tiles[y * width + x];
        }

        public Tile GetTileFromPosition(Vector2 position)
        {
            return GetTileAt((int)position.X / TileSize, (int)position.Y / TileSize);
        }

        private Texture2D CreatePlaceMapTexture()
        {
            Texture2D texture = new Texture2D(graphicsDevice, placemap.Width, placemap.Height);
            Color[] pixels = new Color[placemap.Width * placemap.Height];
            placemap.GetData(pixels);
            texture.SetData(pixels);
            return texture;
        }

        public void SpawnEnemy(Enemy enemy, Vector2 position)
        {
            Tile tile = GetTileFromPosition(position);
            enemies.Add(Enemy.Create(enemy, this, tile.MiddlePoint));
        }

        public void SpawnAlly(Vector2 position)
        {
            Spritesheet spritesheet = new Spritesheet(TextureLoader.Load("basically"));
            Tile tile = GetTileFromPosition(position);
            allies.Add(new AlliedUnit(spritesheet, this, tile.MiddlePoint, new Vector2(1.0f)));
        }

        public void PlaceTower(Tower tower, Vector2 position)
        {
            if(CanPlaceTower(position))
            {
                towers.Add(Tower.Create(tower,this, position));
            }
        }

        public bool CanPlaceTower(Vector2 position)
        {
            Texture2D towerTexture = TextureLoader.Load("towerbase");
            Color[] towerPixels = new Color[towerTexture.Width * towerTexture.Height];
            Color[] placePixels = new Color[towerPixels.Length];
            towerTexture.GetData(towerPixels);
            Rectangle towerHitbox = new Rectangle((int)(position.X - (towerTexture.Width * 0.5f)), (int)(position.Y - (towerTexture.Height * 0.5f)), towerTexture.Width, towerTexture.Height);
            if(towerHitbox.X < 0 || towerHitbox.Y < 0 || towerHitbox.Right > placemap.Width || towerHitbox.Bottom > placemap.Height)
            {
                return false;
            }
            placemap.GetData(0, towerHitbox, placePixels, 0, placePixels.Length);
            for(int i = 0; i < towerPixels.Length; i++)
            {
                if(towerPixels[i].A > 0.0f && placePixels[i] != grassColor)
                {
                    return false;
                }
            }
            return true;
        }

        float rot;

        public void Update(GameTime gameTime)
        {

            rot += MathHelper.ToRadians(90) * (float)gameTime.ElapsedGameTime.TotalSeconds;

            waveTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            allySpawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

            if(alliesSpawned < 4 && allySpawnTimer >= 200.0)
            {
                SpawnAlly(endTile.MiddlePoint);
                alliesSpawned++;
                allySpawnTimer = 0.0;
            }
            
            if(waveIndex+1 < waves.Count && waveTimer <= 0.0)
            {
                spawnTimer += gameTime.ElapsedGameTime.TotalMilliseconds;

               
                if(waves[waveIndex].SpawnIndex >= waves[waveIndex].Enemies.Count)
                {
                    // WAVE COMPLETE
                    waveIndex++;
                    spawnTimer = 0.0;
                    waveTimer = 5000.0f;
                }
                else if (spawnTimer >= 1000.0 / waves[waveIndex].EnemiesPerSecond)
                {
                    SpawnEnemy(waves[waveIndex].Enemies[waves[waveIndex].SpawnIndex++], startTiles[startTileIndex].MiddlePoint);
                    startTileIndex++;
                    startTileIndex %= startTiles.Length;
                    spawnTimer = 0.0;
                }
            }

            if (enemies.Count > 0)
            {
                for (int i = enemies.Count - 1; i >= 0; i--)
                {
                    enemies[i].Update(gameTime);
                    if (enemies[i].Health <= 0.0f)
                    {
                        Player.Gold += enemies[i].Reward;
                        enemies.RemoveAt(i);
                    }
                }
            }
            
            if (allies.Count > 0)
            {
                for (int i = allies.Count - 1; i >= 0; i--)
                {
                    allies[i].Update(gameTime);
                    if (allies[i].Health <= 0.0f)
                    {
                        allies.RemoveAt(i);
                    }
                }
            }

            if (waveIndex+1 >= waves.Count  && enemies.Count == 0)
            {
                completedLevel = true;
            }

            hoveredTower = null;

            if (Input.IsMouseButtonClicked(Input.MouseButton.Right))
            {
                selectedTower = null;
            }

            foreach (Tower tower in towers)
            {
                tower.Update(gameTime);

                if(tower.Bounds.Contains(Input.GetMousePosition()) && Hud.ShopMenu.SelectedItem == null)
                {
                    hoveredTower = tower;
                    if(Input.IsMouseButtonClicked(Input.MouseButton.Left))
                    {
                        selectedTower = tower;
                    }
                }
            }



            if (Input.IsKeyClicked(Microsoft.Xna.Framework.Input.Keys.I))
            {
                SpawnEnemy(new BasicEnemy(this, Vector2.Zero, Vector2.Zero), startTiles[Game1.Random.Next(0,startTiles.Length)].MiddlePoint);
                SpawnAlly(endTile.MiddlePoint);
            }

            if (!completedLevel && selectedTower != null && Input.IsKeyClicked(Microsoft.Xna.Framework.Input.Keys.Delete))
            {
                ShopMenu.ShopItem item = Hud.ShopMenu.Items.Find((item) => item.Item.GetType() == selectedTower.GetType());
                Player.Gold += item != null ? item.Cost : 0;
                towers.Remove(selectedTower);
                selectedTower = null;
            }




            if (effects.Count > 0)
            {
                for (int i = effects.Count - 1; i >= 0; i--)
                {
                    effects[i].Update(gameTime);
                    if (effects[i].IsDone)
                    {
                        effects.RemoveAt(i);
                    }
                }
            }


            if (Hud.ShopMenu.SelectedItem != null)
            {
                if (Hud.ShopMenu.SelectedItem.Item == null)
                {
                    if(selectedTower != null)
                    {
                        if(Player.Gold >= selectedTower.UpgradeCost && selectedTower.Upgrade())
                        {
                            Player.Gold -= selectedTower.UpgradeCost;
                        }
                    }                    
                    Hud.ShopMenu.SelectedItem = null;
                }
                else if ((Hud.ShopMenu.SelectedItem.Item is Character))
                {
                    SpawnAlly(endTile.MiddlePoint);
                    Player.Gold -= Hud.ShopMenu.SelectedItem.Cost;
                    Hud.ShopMenu.SelectedItem = null;
                }
                else
                {
                    canPlace = CanPlaceTower(Input.GetMousePosition());
                    placePos = Input.GetMousePosition();
                    if (!Hud.ShopMenu.HoveringMenu() && Input.IsMouseButtonClicked(Input.MouseButton.Left))
                    {
                        UpdatePlaceMap();
                        PlaceTower((Tower)Hud.ShopMenu.SelectedItem.Item, Input.GetMousePosition());
                        Player.Gold -= Hud.ShopMenu.SelectedItem.Cost;

                        if (!Input.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.LeftShift))
                        {
                            Hud.ShopMenu.SelectedItem = null;
                        }
                    }
                    else if (!Hud.ShopMenu.HoveringMenu() && Input.IsMouseButtonClicked(Input.MouseButton.Right))
                    {
                        Hud.ShopMenu.SelectedItem = null;
                    }
                }
            }

            if(completedLevel)
            {
                if(Input.IsKeyClicked(Microsoft.Xna.Framework.Input.Keys.Space))
                {
                    readyForNext = true;
                }
            }
        }

        private void UpdatePlaceMap()
        {
            graphicsDevice.SetRenderTarget(placemap);

            graphicsDevice.Clear(Color.Transparent);
            spriteBatch.Begin();
            foreach (Tile tile in tiles)
            {
                if (tile.Texture != null)
                {
                    spriteBatch.Draw(tile.Texture, new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tileSize, tileSize), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                }
            }

            spriteBatch.Draw(baseTexture, new Rectangle((int)endTile.Position.X, (int)endTile.Position.Y, tileSize, tileSize), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);


            foreach (Tower tower in towers)
            {
                tower.Draw(spriteBatch);
            }
            spriteBatch.End();

            graphicsDevice.SetRenderTarget(null);

            placemapTexture = CreatePlaceMapTexture();
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (Tile tile in tiles)
            {
                if(tile.Texture != null)
                {
                    spriteBatch.Draw(tile.Texture, new Rectangle((int)tile.Position.X, (int)tile.Position.Y, tileSize, tileSize), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);               
                }
            }

            
            foreach (Character enemy in enemies)
            {
                enemy.Draw(spriteBatch);
            }
            foreach (Character enemy in enemies)
            {
                enemy.DrawHealthbar(spriteBatch);
            }
            foreach (Character ally in allies)
            {
                ally.Draw(spriteBatch);
            }
            foreach (Character ally in allies)
            {
                ally.DrawHealthbar(spriteBatch);
            }

            Vector2 baseDiff = endTile.Position - endTile.Previous.Position;
            float baseRotation = baseDiff.X > 0.0f ? MathHelper.ToRadians(270) : baseDiff.X < 0.0f ? MathHelper.ToRadians(90) : baseDiff.Y < 0.0f ? MathHelper.ToRadians(180) : 0.0f;

            spriteBatch.Draw(baseTexture, new Rectangle((int)(endTile.MiddlePoint.X), (int)(endTile.MiddlePoint.Y), tileSize, tileSize), null, Color.White, baseRotation, new Vector2(tileSize * 0.25f), SpriteEffects.None, 0.0f);


            foreach (Tower tower in towers)
            {
                Color color = selectedTower == tower ? new Color(255, 255, 0, 255) : hoveredTower == tower ? new Color(128, 128, 0, 255) : Color.White;
                tower.Draw(spriteBatch, color);

                if((hoveredTower == tower || selectedTower == tower) && Hud.ShopMenu.SelectedItem == null)
                {
                    float scale = tower.VisionRadius / (radiusTexture.Width * 0.5f) * 2.0f;
                    spriteBatch.Draw(radiusTexture, tower.Position, null, Color.White, 0.0f, new Vector2(radiusTexture.Width / 2, radiusTexture.Height / 2), scale, SpriteEffects.None, 1.0f);
                }
            }

            foreach(Effect effect in effects)
            {
                effect.Draw(spriteBatch);
            }

            if(Hud.ShopMenu.SelectedItem != null && Hud.ShopMenu.SelectedItem.Item is Tower)
            {
                Color color = canPlace ? Color.White : Color.Red;

                float scale = ((Tower)Hud.ShopMenu.SelectedItem.Item).VisionRadius / (radiusTexture.Width * 0.5f) * 2.0f;
                spriteBatch.Draw(radiusTexture, Input.GetMousePosition(), null, Color.White, 0.0f, new Vector2(radiusTexture.Width / 2, radiusTexture.Height / 2), scale, SpriteEffects.None, 1.0f);
                spriteBatch.Draw(Hud.ShopMenu.SelectedItem.Item.Spritesheet.Texture, new Rectangle((int)(placePos.X - (Hud.ShopMenu.SelectedItem.Item.Spritesheet.Texture.Width * 0.5f)), (int)(placePos.Y - (Hud.ShopMenu.SelectedItem.Item.Spritesheet.Texture.Height * 0.5f)), Hud.ShopMenu.SelectedItem.Item.Spritesheet.Texture.Width, Hud.ShopMenu.SelectedItem.Item.Spritesheet.Texture.Height), null, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
            }
            
            if(completedLevel)
            {
                Vector2 stringDim = font.MeasureString("Level complete! Press space to go to next level!") * 3;
                Vector2 stringPos = new Vector2(window.ClientBounds.Width / 2 - (stringDim.X) / 2, window.ClientBounds.Height / 2 - stringDim.Y / 2);
                spriteBatch.DrawString(font, "Level complete! Press space to go to next level!", stringPos, Color.White, 0.0f, Vector2.Zero, 3.0f, SpriteEffects.None, 1.0f);
            }

        }

    }
}
