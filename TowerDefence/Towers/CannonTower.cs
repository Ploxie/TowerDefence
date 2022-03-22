using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Effects;

namespace TowerDefence.Towers
{
    public class CannonTower : Tower
    {
        private Texture2D barrelTexture;
        private float barrelLength;

        public CannonTower(Level level, Vector2 position, Vector2 size) : base(new Spritesheet(TextureLoader.Load("cannontower")), level, position, size)
        {
            this.barrelLength = spritesheet.Texture.Height / 2 + 5.0f;
            this.barrelTexture = TextureLoader.CreateFilledBorderedRectangleTexture(4, (int)barrelLength, Color.White, Color.Gray);
        }

        public override int UpgradeCost
        {
            get
            {
                return (int)(((upgrades + 1) * 1.5f) * 10.0f);
            }
        }

        protected override void Shoot(GameTime gameTime, Character target)
        {
            base.Shoot(gameTime, target);

            Vector2 targetPoint = new Vector2(target.Position.X, target.Position.Y);

            level.Effects.Add(new LaserEffect(Color.Orange, position, targetPoint,Vector2.Zero, barrelLength, 75.0f));
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            base.InternalUpdate(gameTime);
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            base.InternalUpdateAnimation(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Color? color = null)
        {
            color = color == null ? Color.White : color;
            base.Draw(spriteBatch, color);

            spriteBatch.Draw(barrelTexture, position, null, color.Value, rotation, new Vector2(2.0f, 1.0f), size, SpriteEffects.None, 1.0f);
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            base.Draw(spriteBatch, destination);

            Vector2 middlePoint = new Vector2(destination.Width * 0.5f, destination.Height * 0.5f);

            spriteBatch.Draw(barrelTexture, new Vector2(destination.X + middlePoint.X, destination.Y + middlePoint.Y), null, Color.White, rotation, new Vector2(2.0f, 1.0f), size, SpriteEffects.None, 1.0f);
        }
    }
}
