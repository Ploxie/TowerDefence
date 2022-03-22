using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public class Tile
    {

        private Vector2 position;
        private Texture2D texture;

        public Tile(Vector2 position, Texture2D texture)
        {
            this.position = position;
            this.texture = texture;
        }

        public Vector2 Position => position;
        public Vector2 MiddlePoint
        {
            get => new Vector2(position.X + 32, position.Y + 32);
        }

        public Texture2D Texture => texture;        

        public Tile Next
        {
            get;
            set;
        }

        public Tile Previous
        {
            get;
            set;
        }
    }
}
