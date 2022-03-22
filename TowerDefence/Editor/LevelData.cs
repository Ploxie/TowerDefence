using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace TowerDefence.Editor
{
    public class LevelData
    {        
        public string TilesheetPath
        {
            get;
            set;
        }

        public int TileSize
        {
            get;
            set;
        }

        public int Width
        {
            get;
            set;
        }

        public int Height
        {
            get;
            set;
        }

        public int[] TileIDs
        {
            get;
            set;
        }

        public int[] PathIDs
        {
            get;
            set;
        }

        public Vector2[] StartPositions
        {
            get;
            set;
        }

        public Vector2 EndPosition
        {
            get;
            set;
        }
        
        public Color RoadColor
        {
            get;
            set;
        }

        public Color BorderColor
        {
            get;
            set;
        }

        public Color GrassColor
        {
            get;
            set;
        }

        public static LevelData Load(string path)
        {
            LevelData data = null;
            if (!File.Exists(path))
            {
                return data;
            }
            using (BinaryReader reader = new BinaryReader(File.Open(path, FileMode.Open)))
            {
                string spritesPath = reader.ReadString();
                int tileSize = reader.ReadInt32();
                int width = reader.ReadInt32();
                int height = reader.ReadInt32();
                Color roadColor = reader.ReadColor();
                Color borderColor = reader.ReadColor();
                Color grassColor = reader.ReadColor();

                int startPositionSize = reader.ReadInt32();
                Vector2[] startPositions = new Vector2[startPositionSize];
                for(int i = 0; i < startPositionSize;i++)
                {
                    startPositions[i] = reader.ReadVector2();
                }

                Vector2 endPosition = reader.ReadVector2();


                int[] ids = new int[width * height];
                int[] pathIDs = new int[width * height];

                for (int i = 0; i < ids.Length; i++)
                {
                    ids[i] = reader.ReadInt32();
                }

                for (int i = 0; i < pathIDs.Length; i++)
                {
                    pathIDs[i] = reader.ReadInt32();
                }

                data = new LevelData();
                data.TilesheetPath = spritesPath;
                data.TileSize = tileSize;
                data.Width = width;
                data.Height = height;
                data.TileIDs = ids;
                data.RoadColor = roadColor;
                data.BorderColor = borderColor;
                data.GrassColor = grassColor;
                data.PathIDs = pathIDs;
                data.StartPositions = startPositions;
                data.EndPosition = endPosition;
            }
            return data;
        }

        public static void Save(LevelData data, string path)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(path, FileMode.Create)))
            {
                writer.Write(data.TilesheetPath);
                writer.Write(data.TileSize);
                writer.Write(data.Width);
                writer.Write(data.Height);
                writer.Write(data.RoadColor);
                writer.Write(data.BorderColor);
                writer.Write(data.GrassColor);
                writer.Write(data.StartPositions.Length);
                foreach(Vector2 position in data.StartPositions)
                {
                    writer.Write(position);
                }
                writer.Write(data.EndPosition);

                for (int i = 0; i < data.Width * data.Height; i++)
                {
                    writer.Write(data.TileIDs[i]);
                }
                for (int i = 0; i < data.Width * data.Height; i++)
                {
                    writer.Write(data.PathIDs[i]);
                }
            }
        }
    }

    public static class BinaryIOExtensions
    {
        public static Color ReadColor(this BinaryReader reader)
        {
            Color color = new Color();
            color.R = reader.ReadByte();
            color.G = reader.ReadByte();
            color.B = reader.ReadByte();
            color.A = reader.ReadByte();
            return color;
        }

        public static void Write(this BinaryWriter writer, Color color)
        {
            writer.Write(color.R);
            writer.Write(color.G);
            writer.Write(color.B);
            writer.Write(color.A);
        }

        public static Vector2 ReadVector2(this BinaryReader reader)
        {
            Vector2 vector = new Vector2();
            vector.X = reader.ReadSingle();
            vector.Y = reader.ReadSingle();
            return vector;
        }

        public static void Write(this BinaryWriter writer, Vector2 vector)
        {
            writer.Write(vector.X);
            writer.Write(vector.Y);
        }

    }
}
