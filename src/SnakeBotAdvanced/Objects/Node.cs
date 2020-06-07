using SnakeBotAdvanced.Objects;
using System;
using System.Collections.Generic;
using System.Text;

namespace SnakeBot.Objects
{
    public class Node
    {
        public MyPoint Position { get; set; }
        public int G { get; set; }
        public int H { get; set; }
        public Node Parent { get; set; }

        public Node()
        {
        }

        public Node(MyPoint position, int g, int h, Node parent)
        {
            this.Position = position;
            this.G = g;
            this.H = h;
            this.Parent = parent;
        }

        public override bool Equals(object node)
        {
            if (node is Node)
            {
                return this.Position.X == ((Node)node).Position.X && this.Position.Y == ((Node)node).Position.Y;
            }

            return base.Equals(node);
        }

        public override int GetHashCode() => base.GetHashCode();

    }
}
