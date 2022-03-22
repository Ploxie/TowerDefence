using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Editor
{
    public class LevelEditor : GameState
    {
        public class Tile
        {
            public int x, y;
            public Texture2D texture;
            public bool isPath;
            public Tile next;

            public Tile(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        private int width, height, tileSize;
        private string tilesheetPath;

        private Tile[] tiles;

        private Tile selectedTile;
        private Tile lastSelectedTile;
                
        private Texture2D[] roadTextures;

        private List<AStarResult> pathResults = new List<AStarResult>();
        private List<Tile> startTiles = new List<Tile>();
        private Tile endTile;

        private Tile lastTile;
        private Texture2D nodeTexture;
        private Texture2D lineTexture;

        private float rotation;

        private Texture2D roadTileSet;
        private Color roadColor;
        private Color borderColor;
        private Color grassColor;

        private int levelIndex = 1;

        private Texture2D gridTexture;

        public LevelEditor()
        {
            CreateOrLoadLevel("level1.lvl");
            levelIndex = 1;
        }

        public LevelData ToLevelData()
        {
            LevelData data = new LevelData();

            data.TilesheetPath = tilesheetPath;
            data.TileSize = tileSize;
            data.Width = width;
            data.Height = height;
            data.TileIDs = new int[width * height];
            data.RoadColor = roadColor;
            data.BorderColor = borderColor;
            data.GrassColor = grassColor;

            data.StartPositions = new Vector2[startTiles.Count];
            for(int i = 0; i < startTiles.Count;i++)
            {
                data.StartPositions[i] = new Vector2(startTiles[i].x, startTiles[i].y);
            }
            data.EndPosition = new Vector2(endTile.x, endTile.y);

            data.PathIDs = new int[width * height];
            Array.Fill(data.PathIDs, -1);
            for (int i = 0; i < width * height; i++)
            {
                data.TileIDs[i] = GetRoadIndex(tiles[i]);
            }

            for(int i = 0; i < tiles.Length; i++)
            {
                Tile next = tiles[i].next;
                if(next == null)
                {
                    continue;
                }
                int nextIndex = next.y * width + next.x;
                data.PathIDs[i] = nextIndex;
            }

            return data;
        }

        private void CalculatePaths()
        {
            foreach(Tile tile in tiles)
            {
                tile.next = null;
            }
            foreach(AStarResult pathResult in pathResults)
            {
                for (int i = 0; i < pathResult.path.Count - 1; i++)
                {
                    int index = pathResult.path[i].y * width + pathResult.path[i].x;
                    int next = pathResult.path[i + 1].y * width + pathResult.path[i + 1].x;
                    this.tiles[index].next = this.tiles[next];
                }
            }
        }

        public void CreateLevel(int width, int height, int tileSize, string tilesheetPath)
        {
            pathResults.Clear();
            startTiles.Clear();

            this.width = width;
            this.height = height;
            this.tileSize = tileSize;
            this.tilesheetPath = tilesheetPath;

            this.roadTileSet = TextureLoader.Load(tilesheetPath);

            nodeTexture = TextureLoader.CreateCenteredCircleTexture(32, 32, 5, Color.Green);
            lineTexture = TextureLoader.CreateFilledRectangleTexture(1, 16, Color.Green);
                       

            this.roadTextures = new Texture2D[16];
            gridTexture = TextureLoader.CreateRectangleTexture(tileSize, tileSize, Color.Gray);
            GenerateColors();

            

            this.tiles = new Tile[width * height];
            for (int i = 0; i < width * height; i++)
            {
                this.tiles[i] = new Tile(i % width, i / width);
                this.tiles[i].texture = gridTexture;
            }
            
        }

        public void LoadLevel(LevelData data)
        {
            CreateLevel(data.Width, data.Height, data.TileSize, data.TilesheetPath);

            this.grassColor = data.GrassColor;
            this.borderColor = data.BorderColor;
            this.roadColor = data.RoadColor;
            for (int i = 0; i < roadTextures.Length; i++)
            {
                this.roadTextures[i] = roadTileSet.GetSubTexture(new Rectangle((i % (4)) * 32, (i / (4)) * 32, 32, 32));
                this.roadTextures[i].SwapColor(Color.Red, roadColor);
                this.roadTextures[i].SwapColor(new Color(0, 255, 0), borderColor);
                this.roadTextures[i].SwapColor(Color.Blue, grassColor);
            }

            for (int i = 0; i < tiles.Length; i++)
            {
                int x = i % width;
                int y = i / width;

                int textureID = data.TileIDs[i];
                Texture2D texture = textureID != -1 ? roadTextures[textureID] : gridTexture;
                tiles[i].texture = texture;
                tiles[i].isPath = textureID != -1;
            }

            this.startTiles = new List<Tile>();
            for (int i = 0; i < data.StartPositions.Length; i++)
            {
                int index = (int)(data.StartPositions[i].Y * width + data.StartPositions[i].X);
                startTiles.Add(tiles[index]);
            }

            endTile = tiles[(int)(data.EndPosition.Y * width + data.EndPosition.X)];

            if (startTiles.Count > 0 && endTile != null)
            {
                pathResults.Clear();
                foreach (Tile startTile in startTiles)
                {
                    pathResults.Add(AStar.Find(tiles, width, height, startTile, endTile));
                }
                CalculatePaths();
            }

            CalculatePaths();
            CalculateRoad();
        }
        
        private void GenerateColors()
        {
            this.grassColor = new Color((float)Game1.Random.NextDouble(), (float)Game1.Random.NextDouble(), (float)Game1.Random.NextDouble());
            this.roadColor = new Color((float)Game1.Random.NextDouble(), (float)Game1.Random.NextDouble(), (float)Game1.Random.NextDouble());
            this.borderColor = new Color((float)Game1.Random.NextDouble(), (float)Game1.Random.NextDouble(), (float)Game1.Random.NextDouble());

            for (int i = 0; i < roadTextures.Length; i++)
            {
                this.roadTextures[i] = roadTileSet.GetSubTexture(new Rectangle((i % (4)) * 32, (i / (4)) * 32, 32, 32));
                this.roadTextures[i].SwapColor(Color.Red, roadColor);
                this.roadTextures[i].SwapColor(new Color(0, 255, 0), borderColor);
                this.roadTextures[i].SwapColor(Color.Blue, grassColor);
            }
        }

        private int GetRoadIndex(Tile tile)
        {
            if(!tile.isPath)
            {
                return -1;
            }
            int index = 0;
            Tile neighbour;
            index += (neighbour = GetTileAt(tile.x - 1, tile.y)) != null && neighbour.isPath ? 1 : 0;
            index += (neighbour = GetTileAt(tile.x + 1, tile.y)) != null && neighbour.isPath ? 2 : 0;
            index += (neighbour = GetTileAt(tile.x, tile.y - 1)) != null && neighbour.isPath ? 4 : 0;
            index += (neighbour = GetTileAt(tile.x, tile.y + 1)) != null && neighbour.isPath ? 8 : 0;
            return index;
        }

        private Tile GetTileAt(int x, int y)
        {
            if(x < 0 || x >= width || y < 0 || y >= height)
            {
                return null;
            }
            return tiles[y * width + x];
        }

        private void CalculateRoad()
        {
            foreach(Tile tile in tiles)
            {
                if(!tile.isPath)
                {
                    continue;
                }
                int roadIndex = GetRoadIndex(tile);
                
                tile.texture = roadTextures[roadIndex];
            }
        }

        private void CreateOrLoadLevel(string level)
        {
            LevelData data = LevelData.Load(level);
            if (data == null)
            {
                CreateLevel(40, 24, 64, "road_tiles3");
            }
            else
            {
                LoadLevel(data);
            }
        }

        public void Update(GameTime gameTime)
        {

            Vector2 mousePosition = Input.GetMousePosition();
            selectedTile = GetTileAt((int)(mousePosition.X / tileSize), (int)(mousePosition.Y / tileSize));

            if(Input.IsMouseButtonClicked(Input.MouseButton.Left))
            {
                if(Input.IsKeyDown(Keys.S))
                {
                    if(endTile != selectedTile)
                    {
                        if (!startTiles.Contains(selectedTile))
                        {
                            startTiles.Add(selectedTile);
                        }
                        else
                        {
                            startTiles.Remove(selectedTile);
                        }
                    }
                }
                else if(Input.IsKeyDown(Keys.E))
                {
                    endTile = selectedTile;
                }
                else
                {
                    if(endTile == null)
                    {
                        endTile = selectedTile;
                    }
                    selectedTile.isPath = !selectedTile.isPath;
                    lastTile = selectedTile;
                    if(!selectedTile.isPath)
                    {
                        selectedTile.texture = gridTexture;
                    }
                    CalculateRoad();
                }
                if (startTiles.Count > 0 && endTile != null)
                {
                    pathResults.Clear();
                    foreach (Tile startTile in startTiles)
                    {
                        pathResults.Add(AStar.Find(tiles, width, height, startTile, endTile));
                    }
                    CalculatePaths();
                }
            }

            if(Input.IsKeyClicked(Keys.R))
            {
                GenerateColors();
                CalculateRoad();
            }

            if(Input.IsKeyClicked(Keys.Enter))
            {
                LevelData data = ToLevelData();
                LevelData.Save(data, "Level"+levelIndex+".lvl");
            }

            if (Input.IsKeyClicked(Keys.F1))
            {
                CreateOrLoadLevel("Level1.lvl");                
                levelIndex = 1;
            }

            if (Input.IsKeyClicked(Keys.F2))
            {
                CreateOrLoadLevel("Level2.lvl");                
                levelIndex = 2;
            }

            if (Input.IsKeyClicked(Keys.F3))
            {
                CreateOrLoadLevel("Level3.lvl");
                levelIndex = 3;
            }

            if (Input.IsKeyClicked(Keys.F4))
            {
                CreateOrLoadLevel("Level4.lvl");
                levelIndex = 4;
            }

            rotation += (float)gameTime.ElapsedGameTime.TotalSeconds;

            lastSelectedTile = selectedTile;
        }              

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            spriteBatch.Begin(SpriteSortMode.FrontToBack, BlendState.NonPremultiplied, SamplerState.PointClamp);
            TextureLoader.graphicsDevice.Clear(Color.Black);
            foreach (Tile tile in tiles)
            {
                Color color = Color.White;
                if(endTile == tile)
                {
                    color = new Color(0.8f, 0.1f, 0.1f, 0.8f);
                }
                else if(startTiles.Contains(tile))
                {
                    color = new Color(0.1f, 0.8f, 0.1f, 0.8f);
                }
                else if(tile == selectedTile)
                {
                    color = new Color(0.5f, 0.5f, 0.5f, 0.8f);
                }
                spriteBatch.Draw(tile.texture, new Rectangle(tile.x * tileSize, tile.y * tileSize, tileSize, tileSize), null, color, 0.0f, Vector2.Zero, SpriteEffects.None, 0.8f);

                                
                if (tile.next != null)
                {
                    Vector2 nodePosition = new Vector2(tile.x * tileSize, tile.y * tileSize);
                    spriteBatch.Draw(this.nodeTexture, new Rectangle((int)nodePosition.X, (int)nodePosition.Y, tileSize, tileSize), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                    Vector2 parentPosition = new Vector2(tile.next.x * tileSize, tile.next.y * tileSize) + new Vector2(tileSize * 0.5f);
                    Vector2 nodeMiddle = nodePosition + new Vector2(tileSize * 0.5f);
                    float rotation = (float)Math.Atan2(nodeMiddle.Y - parentPosition.Y, nodeMiddle.X - parentPosition.X) + MathHelper.ToRadians(90);
                    spriteBatch.Draw(this.lineTexture, new Rectangle(tile.x * tileSize + (int)(tileSize * 0.5f)+1, tile.y * tileSize + (int)(tileSize * 0.5f)+1, 1, (int)(tileSize * 0.5f)), null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 1.0f);
                }
            }
            spriteBatch.End();

            /*if(pathResult != null)
            {
                foreach(Node node in pathResult.path)
                {
                    Vector2 nodePosition = new Vector2(node.x * tileSize, node.y * tileSize);
                    spriteBatch.Draw(this.nodeTexture, new Rectangle((int)nodePosition.X, (int)nodePosition.Y, tileSize, tileSize), null, Color.White, 0.0f, Vector2.Zero, SpriteEffects.None, 0.0f);
                    if(node.parent != null)
                    {
                        Vector2 parentPosition = new Vector2(node.parent.x * tileSize, node.parent.y * tileSize) + new Vector2(tileSize * 0.5f);
                        Vector2 nodeMiddle = nodePosition + new Vector2(tileSize * 0.5f);
                        float rotation = (float)Math.Atan2(nodeMiddle.Y - parentPosition.Y, nodeMiddle.X - parentPosition.X) + MathHelper.ToRadians(90);
                        spriteBatch.Draw(this.lineTexture, new Rectangle(node.x * tileSize + (int)(tileSize * 0.5f), node.y * tileSize + (int)(tileSize * 0.5f), 1, tileSize), null, Color.White, rotation, Vector2.Zero, SpriteEffects.None, 0.0f);
                    }
                }
            }*/
        }
    }
}
