using Microsoft.AspNetCore.Localization;
using SnakeBot.Objects;
using SnakeBot.Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnakeBotAdvanced.Objects
{
    public static class AStarPathFinder
    {
        static int widthField;
        static int heightField;

        static int[,] _field;
        static int wallBlock = -1;

        private static void FillField(MoveGettingInfo data)
        {
            widthField = data.Width;
            heightField = data.Height;
            _field = new int[data.Width, data.Height];

            // Заполняем змей
            foreach (var snake in data.Snakes)
            {
                for (int i = 0; i < snake.Coords.Count; i++)
                {
                    if (i == 0 && snake.Id != data.You)
                    {
                        _field[snake.Coords[i].X, snake.Coords[i].Y] = wallBlock;
                        if (snake.Coords[i].X - 1 >= 0)
                        {
                            _field[snake.Coords[i].X - 1, snake.Coords[i].Y] = wallBlock;
                        }
                        if (snake.Coords[i].X + 1 < widthField)
                        {
                            _field[snake.Coords[i].X + 1, snake.Coords[i].Y] = wallBlock;
                        }
                        if (snake.Coords[i].Y - 1 >= 0)
                        {
                            _field[snake.Coords[i].X, snake.Coords[i].Y - 1] = wallBlock;
                        }
                        if (snake.Coords[i].Y + 1 < heightField)
                        {
                            _field[snake.Coords[i].X, snake.Coords[i].Y + 1] = wallBlock;
                        }
                    }
                    else if (i == snake.Coords.Count - 1)
                    {
                        continue;
                    }
                    else
                    {
                        _field[snake.Coords[i].X, snake.Coords[i].Y] = wallBlock;

                    }
                }
            }
        }
        private static int GetDistanceScore(MyPoint p1, MyPoint p2)
            => (Math.Abs(p1.X - p2.X) * Math.Abs(p1.X - p2.X)) + (Math.Abs(p1.Y - p2.Y) * Math.Abs(p1.Y - p2.Y));
        private static List<Node> GetNeighbours(Node current, MyPoint endPoint)
        {
            List<Node> result = new List<Node>();

            MyPoint temp;
            if (current.Position.X - 1 >= 0 && _field[current.Position.X - 1, current.Position.Y] != wallBlock)
            {
                temp = new MyPoint(current.Position.X - 1, current.Position.Y);
                result.Add(new Node(
                    temp,
                    current.G + 1,
                    GetDistanceScore(temp, endPoint),
                    current));
            }
            if (current.Position.X + 1 < widthField && _field[current.Position.X + 1, current.Position.Y] != wallBlock)
            {
                temp = new MyPoint(current.Position.X + 1, current.Position.Y);
                result.Add(new Node(
                    temp,
                    current.G + 1,
                    GetDistanceScore(temp, endPoint),
                    current));
            }
            if (current.Position.Y - 1 >= 0 && _field[current.Position.X, current.Position.Y - 1] != wallBlock)
            {
                temp = new MyPoint(current.Position.X, current.Position.Y - 1);
                result.Add(new Node(
                    temp,
                    current.G + 1,
                    GetDistanceScore(temp, endPoint),
                    current));
            }
            if (current.Position.Y + 1 < heightField && _field[current.Position.X, current.Position.Y + 1] != wallBlock)
            {
                temp = new MyPoint(current.Position.X, current.Position.Y + 1);
                result.Add(new Node(
                    temp,
                    current.G + 1,
                    GetDistanceScore(temp, endPoint),
                    current));
            }

            return result;
        }
        private static List<Node> GetListByNodes(Node lastNode)
        {
            List<Node> result = new List<Node>() { lastNode };

            while (lastNode.Parent != null)
            {
                result.Add(lastNode.Parent);
                lastNode = lastNode.Parent;
            }

            result.Reverse();
            return result;
        }
        public static string GetTheWay(MoveGettingInfo data)
        {
            // Заполняем поле
            FillField(data);

            // Ищем нашу голову
            MyPoint myHead = data.Snakes.Where(snake => snake.Id == data.You).FirstOrDefault().Coords[0];

            // Выбираем еду, к которой будем двигаться
            List<MyPoint> foods = data.Food.OrderBy(food => GetDistanceScore(myHead, food)).ToList();
            MyPoint myFood = foods[0];

            // Вычисляем путь к выбранной еде
            List<Node> checkedNodes = new List<Node>();
            PriorityQueue<Node> waitingNodes = new PriorityQueue<Node>();
            waitingNodes.Enqueue(new Node(
                myHead,
                0,
                GetDistanceScore(myHead, myFood),
                null), 0);

            Node current;
            bool nodeNotWaiting;
            int tempG = -1;
            while (waitingNodes.Count > 0)
            {
                current = waitingNodes.Dequeue();
                checkedNodes.Add(current);

                if (current.Position.Equals(myFood)) 
                {
                    var theWay = GetListByNodes(current);
                    if (theWay[1].Position.X < myHead.X && theWay[1].Position.Y == myHead.Y)
                    {
                        return "left";
                    }
                    else if (theWay[1].Position.X > myHead.X && theWay[1].Position.Y == myHead.Y)
                    {
                        return "right";
                    }
                    else if (theWay[1].Position.X == myHead.X && theWay[1].Position.Y > myHead.Y)
                    {
                        return "down";
                    }
                    else
                    {
                        return "up";
                    }
                }

                foreach (var item in GetNeighbours(current, myFood))
                {
                    if (!checkedNodes.Contains(item))
                    {
                        nodeNotWaiting = !waitingNodes.Contains(item);

                        if (nodeNotWaiting)
                        {
                            waitingNodes.Enqueue(item, item.G + item.H);
                        }
                        else
                        {
                            tempG = waitingNodes.TryGetEqualItem(item)?.G ?? -1;
                            if (tempG != -1)
                            {
                                if (tempG >= current.G)
                                {
                                    waitingNodes.Remove(item);
                                    waitingNodes.Enqueue(item, item.G + item.H);
                                }
                            }
                        }
                    }
                }
            }

            return null;
        }
    }
}
