using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Particles;

namespace TowerDefence.Towers
{
    public class GranadeTower : Tower
    {

        private Texture2D barrelTexture;
        private Texture2D barrelBaseTexture;
        private float barrelLength;

        private GrenadeEmittor emittor;

        public GranadeTower(Level level, Vector2 position, Vector2 size) : base(new Spritesheet(TextureLoader.Load("grenadetower")), level, position, size)
        {
            this.barrelLength = spritesheet.Texture.Height / 2 + 5.0f;
            this.barrelTexture = TextureLoader.CreateFilledBorderedRectangleTexture(8, (int)barrelLength - 5, Color.DarkGreen, Color.DarkGray);
            this.barrelBaseTexture = TextureLoader.CreateFilledBorderedRectangleTexture(16, 16, Color.DarkGreen, Color.DarkGray);

            this.attackSpeed = 0.75f;
            this.visionRadius = 200.0f;

            this.emittor = new GrenadeEmittor(level, this.damage, position, 10.0f, 200.0f, Color.DarkGreen, Color.DarkGreen);
            this.emittor.FieldOfView = 0.0f;
            emittor.Active = false;
        }

        protected override void Shoot(GameTime gameTime, Character target)
        {
            //base.Shoot(gameTime, target);
            attackTimer = 0;
            emittor.Target = (Enemy)target;
            emittor.Position = new Vector2(position.X - ((float)Math.Sin(rotation) * barrelLength), position.Y + ((float)Math.Cos(rotation) * barrelLength));
            emittor.Rotation = rotation;
            emittor.UpdateEmission(gameTime);
        }

        protected override void InternalUpdate(GameTime gameTime)
        {
            base.InternalUpdate(gameTime);

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
