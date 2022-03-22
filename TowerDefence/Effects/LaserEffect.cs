using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence.Effects
{
    public class LaserEffect : Effect
    {
        private Texture2D texture;

        private Color color;
        private Vector2 startPoint;
        private Vector2 targetPoint;
        private Vector2 offset;
        private float barrelLength;

        private double decayTimer;

        private Light light;

        public LaserEffect(Color color, Vector2 startPoint, Vector2 targetPoint, Vector2 offset, float barrelLength, double decayTime)
        {
            this.color = color;
            this.startPoint = startPoint;
            this.targetPoint = targetPoint;
            this.offset = offset;
            this.barrelLength = barrelLength;
            this.decayTimer = decayTime;
            this.texture = TextureLoader.Load("laser");

            this.light = new Spotlight()
            {
                Color = color,
                ConeDecay = 1.0f

            };

            Game1.Penumbra.Lights.Add(light);
        }

        public override bool IsDone => decayTimer <= 0.0f;


        public override void Update(GameTime gameTime)
        {
            decayTimer -= gameTime.ElapsedGameTime.TotalMilliseconds;
            if(IsDone)
            {
                Game1.Penumbra.Lights.Remove(light);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (decayTimer > 0.0)
            {
                float laserLength = Vector2.Distance(targetPoint, startPoint) - barrelLength;
                float barrelPercentage = (barrelLength * 3.0f) / laserLength;

                Rectangle destinationRectangle = new Rectangle((int)startPoint.X, (int)(startPoint.Y), texture.Width, (int)laserLength);
                float angle = (float)Math.Atan2(startPoint.Y - targetPoint.Y, startPoint.X - targetPoint.X);
                light.Position = startPoint;// new Vector2(startPoint.X + barrelLength + (float)Math.Cos(angle), startPoint.Y + barrelLength + (float)Math.Cos(angle));
                light.Rotation = angle + MathHelper.ToRadians(180);
                light.Radius = laserLength;
                spriteBatch.Draw(texture, destinationRectangle, null, color, angle + MathHelper.ToRadians(90), new Vector2(2.0f + offset.X, -barrelPercentage + offset.Y), SpriteEffects.None, 1.0f);
            }
        }

    }
}
