using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Dungeon_Master
{
    public class Board
    {
        public static readonly int BombSlpash = 4;
        public readonly int Width;
        public readonly int Height;
        public string Map { get; private set; }
        public bool IsSquare { get => Width == Height; }
        public Board(string map)
        {
            Map = map.Replace("\n", "");
            Width = Height = (int)Math.Sqrt(Map.Length);
        }
        public Board(string map, int Width)
        {
            Map = map.Replace("\n", "");
            this.Width = Width;
            Height = Map.Length / Width;
        }
        public Board GetArea(Point A, Point B)
        {
            if (A.X < 0) A.X = 0;
            if (A.Y < 0) A.Y = 0;
            if (A.X > Width) A.X = Width;
            if (A.Y > Height) A.X = Height;
            if (B.X < 0) B.X = 0;
            if (B.Y < 0) B.Y = 0;
            if (B.X > Width) B.X = Width;
            if (B.Y > Height) B.Y = Height;
            string map = "";
            int t = B.X - A.X;
            for (int i = A.Y; i < B.Y; i++)
                map += Map.Substring(Width * i + A.X, t);
            return new Board(map, t);
        }
        public static string ReplaceObj(string s, int Indx, Item item)
        {
            s = s.Remove(Indx, 1);
            s = s.Insert(Indx, ((char)item).ToString());
            return s;
        }
        public void ReplaceObj(int Indx, Item item)
        {
            Map = Map.Remove(Indx, 1);
            Map = Map.Insert(Indx, ((char)item).ToString());
        }
        public int CordsToIndx(int X, int Y) => X + Y * Width;
        public void IndxToCords(int Indx, out int X, out int Y)
        {
            X = Indx % Width;
            Y = Indx / Width;
        }
        public bool IsMyBombermanDead => Map.Contains((char)Item.DEAD_BOMBERMAN);
        public Item GetAt(Point point)
        {
            if (point.IsOutOf(Width, Height))
            {
                return Item.WALL;
            }
            return (Item)Map[CordsToIndx(point.X, point.Y)];
        }
        public Point GetBomberman()
        {
            int Indx = Map.IndexOfAny(new char[] { (char)Item.BOMBERMAN, (char)Item.BOMB_BOMBERMAN, (char)Item.DEAD_BOMBERMAN });
            int X, Y;
            IndxToCords(Indx, out X, out Y);
            return new Point(X, Y);
        }
        public List<Point> Get(Item obj)
        {
            var GetList = new List<Point>();
            string map = Map;
            int Indx = 0;
            while(map.Contains((char)obj))
            {
                int ind = map.IndexOf((char)obj);
                Indx += ind;
                int X, Y;
                IndxToCords(Indx, out X, out Y);
                GetList.Add(new Point(X, Y));
                map = map.Remove(0, ind + 1);
                Indx++;
            }
            return GetList;
        }
        public Point Nearest(params Item[] Obj)
        {
            var Me = GetBomberman();
            List<Point> ObjList = new List<Point>();
            for (int i = 0; i < Obj.Length; i++)
                ObjList = new List<Point>(ObjList.Concat(Get(Obj[i])));
            var tempMinDir = (ObjList[0] - Me).SquareLength();
            Point Near = ObjList[0];
            foreach (var ob in ObjList)
            {
                var d = (ob - Me).SquareLength();
                if (d < tempMinDir)
                {
                    tempMinDir = d;
                    Near = ob;
                }
            }
            return Near;
        }
    }
}
