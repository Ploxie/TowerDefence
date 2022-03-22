using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    public class Menu : GameState
    {

        private SpriteFont font;
        private Rectangle viewport;

        private int selectedIndex;

        public Menu(SpriteFont font, Rectangle viewport)
        {
            this.font = font;
            this.viewport = viewport;

            this.Options = new List<Option>();
            this.selectedIndex = -1;
        }

        public List<Option> Options
        {
            get;
        }

        private Rectangle GetRectangle(Option option)
        {
            int index = Options.FindIndex((o) => o.Text == option.Text);
            int height = viewport.Height / Options.Count;
            return new Rectangle(viewport.X, viewport.Y + (index * height), viewport.Width, height);
        }

        public void Update(GameTime gameTime)
        {
            Vector2 mousePosition = Input.GetMousePosition();

            foreach(Option option in Options)
            {
                Rectangle hitbox = GetRectangle(option);
                if(hitbox.Contains(mousePosition))
                {
                    int index = Options.FindIndex((o) => o.Text == option.Text);
                    this.selectedIndex = index;

                    if(Input.IsMouseButtonClicked(Input.MouseButton.Left))
                    {
                        option.Action();
                    }

                    break;
                }
            }
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
        {
            foreach(Option option in Options)
            {
                Rectangle bounds = GetRectangle(option);
                int index = Options.FindIndex((o) => o.Text == option.Text);
                Vector2 textDimensions = font.MeasureString(option.Text);
                Vector2 position = new Vector2(bounds.X + (bounds.Width / 2), bounds.Y + (bounds.Height / 2)) - new Vector2(textDimensions.X / 2, textDimensions.Y / 2);
                Color color = index == this.selectedIndex ? Color.Gray : Color.White;
                spriteBatch.DrawString(font, option.Text, position, color);
            }
        }

        public class Option
        {
            public delegate void InternalAction();

            public Option(string text, InternalAction action)
            {
                this.Text = text;
                this.Action = action;
            }

            public string Text
            {
                get;
            }

            public InternalAction Action
            {
                get;
            }
        }
    }
}
