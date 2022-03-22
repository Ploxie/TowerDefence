using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace TowerDefence
{
    
    public static class TextureLoader
    {
        

        private static ContentManager content;
        public static GraphicsDevice graphicsDevice;
        private static Dictionary<string, Texture2D> textures;

        public static void Initialize(ContentManager content, GraphicsDevice graphicsDevice)
        {
            TextureLoader.content = content;
            TextureLoader.graphicsDevice = graphicsDevice;
            textures = new Dictionary<string, Texture2D>();
        }

        public static Texture2D Load(string path)
        {
            Texture2D texture = Get(path);
            if(texture == null)
            {
                texture = content.Load<Texture2D>(path);
                textures[path] = texture;
                return texture;
            }
            return texture;
        }

        public static SpriteFont LoadFont(string path)
        {
            return content.Load<SpriteFont>(path);
        }

        public static Texture2D Get(string name)
        {
            if(!textures.ContainsKey(name))
            {
                return null;
            }
            return textures[name];
        }

        public static Texture2D CreateEmptyTexture(int width, int height)
        {
            return new Texture2D(graphicsDevice, width, height);
        }

        public static Texture2D CreateRectangleTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            Color[] colors = new Color[width * height];

            Array.Fill(colors, Color.Transparent);

            for (int x = 0; x < width; x++)
            {
                colors[x] = color;
                colors[(height - 1) * width + x] = color;
            }

            for (int y = 0; y < height; y++)
            {
                colors[y * width] = color;
                colors[y * width + (width - 1)] = color;
            }

            texture.SetData(colors);

            return texture;
        }

        public static Texture2D CreateFilledRectangleTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            Color[] colors = new Color[width * height];

            Array.Fill(colors, color);
            texture.SetData(colors);

            return texture;
        }

        public static Texture2D CreateFilledBorderedRectangleTexture(int width, int height, Color fillColor, Color borderColor)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            Color[] colors = new Color[width * height];

            Array.Fill(colors, fillColor);

            for (int x = 0; x < width; x++)
            {
                colors[x] = borderColor;
                colors[(height - 1) * width + x] = borderColor;
            }

            for (int y = 0; y < height; y++)
            {
                colors[y * width] = borderColor;
                colors[y * width + (width - 1)] = borderColor;
            }

            texture.SetData(colors);
            return texture;
        }

        public static Texture2D CreateLineTexture(int width, int height, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);

            Color[] colors = new Color[width * height];

            Array.Fill(colors, Color.Transparent);

            int middleX = width / 2;
            int middleY = height / 2;

            for(int y = 0; y < middleY; y++)
            {
                colors[y * width + middleX] = color;
            }

            texture.SetData(colors);

            return texture;
        }

        public static Texture2D CreateCenteredCircleTexture(int width, int height, int radius, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, width, height);
            Color[] colorData = new Color[width * height];

            float diameter = radius / 2f;
            float diameterSquared = diameter * diameter;
            Vector2 middle = (new Vector2(width, height) / 2.0f) - new Vector2(radius / 2.0f);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    int index = y * width + x;
                    Vector2 pos = new Vector2(x - diameter, y - diameter);
                    float distance = Vector2.Distance(pos, middle);
                    if (distance * distance <= diameterSquared)
                    {
                        colorData[index] = color;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }

        public static Texture2D CreateCircleTexture(int radius, Color color)
        {
            Texture2D texture = new Texture2D(graphicsDevice, radius, radius);
            Color[] colorData = new Color[radius * radius];

            float diameter = radius / 2f;
            float diameterSquared = diameter * diameter;

            for (int x = 0; x < radius; x++)
            {
                for (int y = 0; y < radius; y++)
                {
                    int index = x * radius + y;
                    Vector2 pos = new Vector2(x - diameter, y - diameter);
                    if (pos.LengthSquared() <= diameterSquared)
                    {
                        colorData[index] = color;
                    }
                    else
                    {
                        colorData[index] = Color.Transparent;
                    }
                }
            }

            texture.SetData(colorData);
            return texture;
        }

    }

    public static class Texture2DExtensions
    {
        public static Texture2D GetSubTexture(this Texture2D texture, Rectangle source)
        {
            Texture2D result = new Texture2D(TextureLoader.graphicsDevice, source.Width, source.Height);
            Color[] colors = new Color[source.Width * source.Height];
            Color[] textureColors = new Color[texture.Width * texture.Height];
            texture.GetData(textureColors);

            for (int y = 0; y < source.Height; y++)
            {
                for (int x = 0; x < source.Width; x++)
                {
                    int i = y * source.Width + x;
                    int i2 = (y + source.Y) * texture.Width + (x + source.X);
                    colors[y * source.Width + x] = textureColors[(y + source.Y) * texture.Width + (x + source.X)];
                }
            }

            result.SetData(colors);

            return result;
        }

        public static Texture2D SwapColor(this Texture2D texture, Color a, Color b)
        {
            Color[] pixels = new Color[texture.Width * texture.Height];
            texture.GetData(pixels);
            for(int i = 0; i < pixels.Length; i++)
            {
                pixels[i] = pixels[i] == a ? b : pixels[i];
            }
            texture.SetData(pixels);
            return texture;
        }
    }

}
