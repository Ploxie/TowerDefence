using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Penumbra;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Particles;

namespace TowerDefence.Effects
{
    public class ExplosionEffect : Effect
    {
        private Vector2 position;
        private ParticleEmittor emittor;

        private double lifeTime;
        private double lifeTimer;

        private Color startColor;
        private Color endColor;

        public ExplosionEffect(Vector2 position)
        {
            this.lifeTime = 1000.0f;

            this.startColor = Color.Red;
            this.endColor = new Color(255, 255, 255, 0);

            this.position = position;
            this.emittor = new PulseEmittor(position, 100.0f, 5000.0f, lifeTime, 5.0f, 100.0f, Color.Red, new Color(255,255,0,0));
            this.emittor.Active = true;
        }

        public override bool IsDone
        {
            get => !emittor.Active;
        }

        public override void Update(GameTime gameTime)
        {
            emittor.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            emittor.Draw(spriteBatch);
        }

    }
}
