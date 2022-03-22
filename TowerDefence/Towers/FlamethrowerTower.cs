using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Effects;
using TowerDefence.Particles;
using Effect = TowerDefence.Effects.Effect;

namespace TowerDefence.Towers
{
    public class FlamethrowerTower : Tower
    {

        private Texture2D barrelTexture;
        private Texture2D barrelBaseTexture;
        private float barrelLength;
        
        private FlameEmittor emittor;

        public FlamethrowerTower(Level level, Vector2 position, Vector2 size) : base(new Spritesheet(TextureLoader.Load("flametower")), level, position, size)
        {
            this.barrelLength = spritesheet.Texture.Height / 2 + 5.0f;
            this.barrelTexture = TextureLoader.CreateFilledBorderedRectangleTexture(8, (int)barrelLength - 5, Color.DarkRed, Color.Red);
            this.barrelBaseTexture = TextureLoader.CreateFilledBorderedRectangleTexture(16, 16, Color.DarkRed, Color.Red);

            this.attackSpeed = 12.0f;
            this.damage = 5;

            this.emittor = new FlameEmittor(position, 200.0f, 500.0f, 5.0f, 200.0f, Color.Red, Color.Orange);
            this.emittor.FieldOfView = 45.0f;
        }

        protected override void Shoot(GameTime gameTime, Character target)
        {
            foreach(Enemy enemy in level.Enemies)
            {
                if(Vector2.Distance(target.Position, enemy.Position) <= 65.0f)
                {
                    if(!enemy.FireResistant)
                    {
                        enemy.Damage(damage);
                    }
                }
            }

            
            attackTimer = 0.0f;            
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            base.InternalUpdate(gameTime);
            emittor.Active = currentTarget != null;

            emittor.Position = new Vector2(position.X - ((float)Math.Sin(rotation) * barrelLength), position.Y + ((float)Math.Cos(rotation) * barrelLength));
            emittor.Rotation = rotation;
            emittor.Update(gameTime);
        }

        protected override void InternalUpdateAnimation(GameTime gameTime)
        {
            base.InternalUpdateAnimation(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch, Color? color = null)
        {
            color = color == null ? Color.White : color;
            base.Draw(spriteBatch, color);

            spriteBatch.Draw(barrelBaseTexture, position, null, color.Value, rotation, new Vector2(8.0f, 8.0f), size, SpriteEffects.None, 1.0f);
            spriteBatch.Draw(barrelTexture, position, null, color.Value, rotation, new Vector2(4.0f, 1.0f), size, SpriteEffects.None, 1.0f);
            
            emittor.Draw(spriteBatch);
        }

        public override void Draw(SpriteBatch spriteBatch, Rectangle destination)
        {
            base.Draw(spriteBatch, destination);

            Vector2 middlePoint = new Vector2(destination.Width * 0.5f, destination.Height * 0.5f);

            spriteBatch.Draw(barrelTexture, new Vector2(destination.X + middlePoint.X, destination.Y + middlePoint.Y), null, Color.White, rotation, new Vector2(4.0f, 1.0f), size, SpriteEffects.None, 1.0f);
        }

    }
}
