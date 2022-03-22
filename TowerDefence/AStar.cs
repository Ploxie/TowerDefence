using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using TowerDefence.Editor;

namespace TowerDefence
{

    public class Node
    {
        public LevelEditor.Tile tile;
        public int x, y;
        public Node parent;
        public Node next;
        public float gCost;
        public float hCost;
        public float fCost;
    }

    public class AStarResult
    {
        public Node[] nodes;
        public List<Node> path;
    }    

    public class AStar
    {
        private class AStarData
        {
            public int width, height;
            public Node[] nodes;
            public Node start, end;
            public bool acceptDiagonal = false;
            public bool searchAll = false;

            public Node Get(int x, int y)
            {
                if(x < 0 || x >= width || y < 0 || y >= height)
                {
                    return null;
                }
                return nodes[y * width + x];
            }

            public Node Get(LevelEditor.Tile tile)
            {
                return Array.Find(nodes, node => node.tile.x == tile.x && node.tile.y == tile.y);
            }

            public List<Node> GetNeighbours(Node node)
            {
                List<Node> list = new List<Node>();
                Node neighbour;
                if (acceptDiagonal && (neighbour = Get(node.x - 1, node.y - 1)) != null)
                {
                    list.Add(neighbour);
                }
                if ((neighbour = Get(node.x, node.y - 1)) != null)
                {
                    list.Add(neighbour);
                }
                if (acceptDiagonal && (neighbour = Get(node.x + 1, node.y - 1)) != null)
                {
                    list.Add(neighbour);
                }
                if ((neighbour = Get(node.x - 1, node.y)) != null)
                {
                    list.Add(neighbour);
                }
                if ((neighbour = Get(node.x + 1, node.y)) != null)
                {
                    list.Add(neighbour);
                }
                if (acceptDiagonal && (neighbour = Get(node.x - 1, node.y + 1)) != null)
                {
                    list.Add(neighbour);
                }
                if ((neighbour = Get(node.x, node.y + 1)) != null)
                {
                    list.Add(neighbour);
                }
                if (acceptDiagonal && (neighbour = Get(node.x + 1, node.y + 1)) != null)
                {
                    list.Add(neighbour);
                }
                return list;
            }
        }

        public static AStarResult Find(LevelEditor.Tile[] tiles, int width, int height, LevelEditor.Tile start, LevelEditor.Tile end)
        {
            AStarData data = new AStarData
            {
                width = width,
                height = height,
                nodes = new Node[tiles.Length]
            };
            for (int i = 0; i < tiles.Length; i++)
            {
                Node node = new Node();
                node.tile = tiles[i];
                node.x = i % width;
                node.y = i / width;
                data.nodes[i] = node;
            }

            data.start = data.Get(start);
            data.end = data.Get(end);

            AStarResult result = new AStarResult();
            result.nodes = data.nodes;
            result.path = new List<Node>();
                        
            List<Node> closedList = new List<Node>();
            List<Node> openList = new List<Node>(tiles.Length);
            List<Node> neighbours;


            Node currentNode = data.start;
            currentNode.hCost = Math.Abs(Vector2.Distance(new Vector2(currentNode.x, currentNode.y), new Vector2(data.end.x, data.end.y)));
            currentNode.fCost = currentNode.gCost + currentNode.hCost;
            openList.Add(data.start);

            while(openList.Count > 0 && (data.searchAll || !closedList.Exists(node => node.tile.x == end.x && node.tile.y == end.y)))
            {
                currentNode = openList[0];
                openList.Remove(currentNode);
                closedList.Add(currentNode);

                neighbours = data.GetNeighbours(currentNode);

                foreach(Node neighbour in neighbours)
                {
                    if(neighbour.tile.isPath && !closedList.Contains(neighbour) && !openList.Contains(neighbour))
                    {
                        neighbour.parent = currentNode;
                        neighbour.gCost = Math.Abs(Vector2.Distance(new Vector2(neighbour.x, neighbour.y), new Vector2(neighbour.parent.x, neighbour.parent.y))) + neighbour.parent.gCost;
                        neighbour.hCost = Math.Abs(Vector2.Distance(new Vector2(neighbour.x, neighbour.y), new Vector2(data.end.x, data.end.y)));
                        neighbour.fCost = neighbour.gCost + neighbour.hCost;
                        openList.Add(neighbour);
                        openList.Sort((a, b) => (int)(a.fCost - b.fCost));
                    }
                }
            }
            

            Node backtrace = data.end;
            while(backtrace != null)
            {
                result.path.Add(backtrace);
                backtrace = backtrace.parent;
            }

            result.path.Reverse();

            return result;
        }

    }
}
