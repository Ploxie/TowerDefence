using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Towers;

namespace TowerDefence
{
    public class ShopMenu
    {
        public class ShopItem
        {
            public GameObject Item
            {
                get;
                set;
            }

            public int Cost
            {
                get;
                set;
            }
        }

        private Player player;
        private Rectangle viewport;

        private Texture2D background;
        private Texture2D border;

        private Texture2D previewBackground;

        private Texture2D goldTexture;
        private SpriteFont font;

        private Texture2D upgradeTexture;

        private List<ShopItem> items;
        private ShopItem hoveredItem;

        private float padding;
        private float previewPadding;
        private float previewSize;
        private HUD hud;

        public ShopMenu(Player player,HUD hud, Rectangle viewport, SpriteFont font)
        {
            this.player = player;
            this.hud = hud;
            this.viewport = viewport;
            this.font = font;

            this.goldTexture = TextureLoader.Load("coins");

            this.padding = 10.0f;
            this.previewPadding = 5.0f;
            this.previewSize = 50.0f;

            this.background = TextureLoader.CreateFilledRectangleTexture(viewport.Width, viewport.Height, new Color(0.1f, 0.1f, 0.1f, 0.5f));
            this.border = TextureLoader.CreateRectangleTexture(viewport.Width, viewport.Height, new Color(0.3f, 0.3f, 0.3f, 0.8f));
            this.previewBackground = TextureLoader.CreateFilledBorderedRectangleTexture((int)previewSize, (int)previewSize, new Color(0.3f, 0.3f, 0.3f, 0.8f), Color.Black);
            this.upgradeTexture = TextureLoader.Load("upgrade");

            this.items = new List<ShopItem>();

            items.Add(new ShopItem()
            {
                Cost = 5,
                Item = new CannonTower(null, Vector2.Zero, new Vector2(1.0f))
            });

            items.Add(new ShopItem()
            {
                Cost = 10,
                Item = new DoubleBarrelTower(null, Vector2.Zero, new Vector2(1.0f))
            });

            items.Add(new ShopItem()
            {
                Cost = 20,
                Item = new LaserTower(null, Vector2.Zero, new Vector2(1.0f))
            });

            items.Add(new ShopItem()
            {
                Cost = 25,
                Item = new FrostTower(null, Vector2.Zero, new Vector2(1.0f))
            });

            items.Add(new ShopItem()
            {
                Cost = 30,
                Item = new FlamethrowerTower(null, Vector2.Zero, new Vector2(1.0f))
            });

            items.Add(new ShopItem()
            {
                Cost = 50,
                Item = new GranadeTower(null, Vector2.Zero, new Vector2(1.0f))
            });            

            items.Add(new ShopItem()
            {
                Cost = 2,
                Item = new AlliedUnit(new Spritesheet(TextureLoader.Load("basically")), null, Vector2.Zero, new Vector2(1.0f))
            });

            items.Add(new ShopItem()
            {
                Cost = 0,
                Item = null
            });


        }

        public List<ShopItem> Items => items;

        public ShopItem SelectedItem
        {
            get;
            set;
        }


        public bool HoveringMenu()
        {
            return viewport.Contains(Input.GetMousePosition());
        }

        public void Update(GameTime gameTime)
        {
            if (!HoveringMenu())
            {
                return;
            }

            int calculatedColumns = Math.Min(items.Count, (int)((viewport.Width - (padding * 2)) / previewSize));
            int calculatedPadding = (int)(viewport.Width - (previewSize * calculatedColumns)) / (calculatedColumns + 1);

            hoveredItem = null;

            int columns = calculatedColumns;

           

            for (int i = 0; i < items.Count; i++)
            {
                ShopItem item = items[i];
                int x = i % columns;
                int y = i / columns;
                int offset = 0;

                if (i >= items.Count-2)
                {
                    offset = 2;
                }

                Rectangle previewBounds = new Rectangle(viewport.X + (calculatedPadding * (x + 1)) + (int)(x * previewSize), viewport.Y + (int)((y + 1 + offset) * padding) + (int)(y * previewSize), (int)previewSize, (int)previewSize);
                if(previewBounds.Contains(Input.GetMousePosition()))
                {
                    hoveredItem = item;
                    if (Input.IsMouseButtonClicked(Input.MouseButton.Left) && player.Gold >= item.Cost)
                    { 
                        SelectedItem = item;
                    }
                }
            }

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(background, viewport, null, Color.White);
            spriteBatch.Draw(border, viewport, null, Color.White);

            int calculatedColumns = Math.Min(items.Count, (int)((viewport.Width - (padding * 2)) / previewSize));
            int calculatedPadding = (int)(viewport.Width - (previewSize * calculatedColumns)) / (calculatedColumns + 1);

            int columns = calculatedColumns;
            for (int i = 0; i < items.Count;i++)
            {
                ShopItem item = items[i];
                int x = i % columns;
                int y = i / columns;
                int cost = item.Cost;

                if (item.Item == null && hud.Level.SelectedTower != null)
                {
                    cost = hud.Level.SelectedTower.UpgradeCost;
                }

                Color color = SelectedItem == item ? Color.Green : (player.Gold < cost || cost == 0) ? Color.Red : hoveredItem == item ? Color.LightGreen : Color.White;

                Rectangle previewBounds = new Rectangle(viewport.X + (calculatedPadding * (x + 1)) + (int)(x * previewSize), viewport.Y + (int)((y + 1) * padding) + (int)(y * previewSize), (int)previewSize, (int)previewSize);
                spriteBatch.Draw(previewBackground, previewBounds, null, color, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                Rectangle previewInnerBounds = new Rectangle((int)(previewBounds.X + previewPadding), (int)(previewBounds.Y + previewPadding), (int)(previewSize - (previewPadding * 2.0f)), (int)(previewSize - (previewPadding * 2.0f)));


                if(item.Item != null)
                {
                    item.Item.Draw(spriteBatch, previewInnerBounds);
                }
                else
                {
                    spriteBatch.Draw(upgradeTexture, previewInnerBounds, null, color, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }

                if(player.Gold < cost)
                {
                    spriteBatch.Draw(previewBackground, previewBounds, null, color, 0.0f, Vector2.Zero, SpriteEffects.None, 1.0f);
                }

                string goldString = "" + (cost == 0 ? " " : ""+cost);
                Vector2 goldStringDimensions = font.MeasureString(goldString);
                spriteBatch.Draw(goldTexture, new Rectangle((int)(previewBounds.X + goldStringDimensions.X + 10), (int)(previewBounds.Y + previewBounds.Height - goldStringDimensions.Y), 12, 13), null, Color.White, 0.0f, new Vector2(0.5f, 0.5f), SpriteEffects.None, 1.0f);
                spriteBatch.DrawString(font, goldString, new Vector2(previewBounds.X + 5, (int)(previewBounds.Y + previewBounds.Height - goldStringDimensions.Y)), Color.Black, 0.0f, Vector2.Zero, 1.0f, SpriteEffects.None, 1.0f);
            }


        }
    }
}
