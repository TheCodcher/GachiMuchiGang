using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace Dungeon_Master
{
    class BomberBot : AbstractSolver
    {
        const int VisibleArea = 35;
        const int SetBombPriority = 10;
        const int GhostAgressive = 1;
        const int OtherBomberAgressive = 5;
        List<OtherBot> EnemyList = new List<OtherBot>();
        List<Bomb> MyBombList = new List<Bomb>();
        public BomberBot(string ServerUrl):base(ServerUrl)
        {

        }
        protected override string DoAction(Board Map)
        {
            var DicisionPriority = BrainCard.InitializeDictinory<Direction, int>();
            Point myBomberman = Map.GetBomberman();
            var _map = Map.GetArea(myBomberman - VisibleArea, myBomberman + VisibleArea);
            //относительно моего бомбермена найти новый координаты для бомб. 
            Point myAreaBomberman = _map.GetBomberman();
            foreach (var mab in MyBombList)
                mab.LocalLocation = mab.Location - myBomberman + myAreaBomberman;

            void ReplaceAll(Item Now, Item Next)
            {
                while (_map.Map.Contains((char)Now))
                    _map.ReplaceObj(_map.Map.IndexOf((char)Now), Next);
            }
            //движение людей
            ReplaceAll(Item.BOOM, Item.Space);
            ReplaceAll(Item.BOMB_TIMER_1, Item.BOOM);
            ReplaceAll(Item.BOMB_TIMER_2, Item.BOMB_TIMER_1);
            ReplaceAll(Item.BOMB_TIMER_3, Item.BOMB_TIMER_2);
            ReplaceAll(Item.BOMB_TIMER_4, Item.BOMB_TIMER_3);
            ReplaceAll(Item.BOMB_TIMER_5, Item.BOMB_TIMER_4);
            ReplaceAll(Item.DestroyedWall, Item.Space);
            ReplaceAll(Item.OTHER_DEAD_BOMBERMAN, Item.Space);
            ReplaceAll(Item.DeadMeatChopper, Item.Space);

            Queue<Point> BoomList = new Queue<Point>(_map.Get(Item.BOOM).ToArray());
            bool Expl(int i, int j)
            {
                bool breakpoint = true;
                var it = _map.GetAt(new Point(i, j));
                switch (it)
                {
                    //case Item.OTHER_BOMBERMAN:
                    //    _map.ReplaceObj(_map.CordsToIndx(i, j), Item.OTHER_DEAD_BOMBERMAN); break;
                    case Item.DESTROYABLE_WALL:
                        _map.ReplaceObj(_map.CordsToIndx(i, j), Item.DestroyedWall); break;
                    case Item.OTHER_BOMB_BOMBERMAN:
                    case Item.BOMB_BOMBERMAN:
                    case Item.BOMB_TIMER_1:
                    case Item.BOMB_TIMER_2:
                    case Item.BOMB_TIMER_3:
                    case Item.BOMB_TIMER_4:
                    case Item.BOMB_TIMER_5:
                        {
                            _map.ReplaceObj(_map.CordsToIndx(i, j), Item.BOOM);
                            BoomList.Enqueue(new Point(i, j));
                            break;
                        }
                    case Item.Space:
                    case Item.MEAT_CHOPPER:
                    case Item.OTHER_BOMBERMAN:
                    case Item.OTHER_DEAD_BOMBERMAN:
                    case Item.DEAD_BOMBERMAN:
                    case Item.BOMBERMAN:
                        {
                            _map.ReplaceObj(_map.CordsToIndx(i, j), Item.BOOM);
                            breakpoint = false;
                            break;
                        }
                    case Item.BOOM:
                        breakpoint = false; break;
                }
                return breakpoint;
            }

            while(BoomList.Count!=0)
            {
                var b = BoomList.Dequeue();
                for (int i = b.X; i > b.X - Board.BombSlpash; i--)
                    if (Expl(i, b.Y)) break;
                for (int j = b.Y; j > b.Y - Board.BombSlpash; j--)
                    if (Expl(b.X, j)) break;
                for (int i = b.X; i < b.X + Board.BombSlpash; i++)
                    if (Expl(i, b.Y)) break;
                for (int j = b.Y; j < b.Y + Board.BombSlpash; j++)
                    if (Expl(b.X, j)) break;
            }

            Item CheckMyBomb(Bomb bomb)
            {
                if (Map.GetAt(bomb.Location) == Item.BOOM) bomb.BOOM = true;
                var isBlock = _map.GetAt(bomb.LocalLocation);
                switch (isBlock)
                {
                    case Item.BOMB_TIMER_5: return Item.MY_BOMB_TIMER_5;
                    case Item.BOMB_TIMER_4: return Item.MY_BOMB_TIMER_4;
                    case Item.BOMB_TIMER_3: return Item.MY_BOMB_TIMER_3;
                    case Item.BOMB_TIMER_2: return Item.MY_BOMB_TIMER_2;
                    case Item.BOMB_TIMER_1: return Item.MY_BOMB_TIMER_1;
                    case Item.BOOM:
                        {
                            bomb.BOOM = true;
                            return Item.BOOM;
                        }
                }
                return isBlock;
            }

            foreach (var b in MyBombList)
            {
                var item = CheckMyBomb(b);
                if (item != Item.WALL)
                    _map.ReplaceObj(_map.CordsToIndx(b.LocalLocation.X, b.LocalLocation.Y), item);
            }
            MyBombList.RemoveAll((b) => b.BOOM = false);

            var GhostList = _map.Get(Item.MEAT_CHOPPER);
            foreach (var g in GhostList)
            {
                for (int i = 0; i < 360; i += 90) 
                {
                    var dir = g + new Point(0, -1).Rotate(i);
                    if (_map.GetAt(dir) == Item.Space)
                        _map.ReplaceObj(_map.CordsToIndx(dir.X, dir.Y), Item.MEAT_CHOPPER);
                }
            }
            int SetBombRate = 0;
            for (int i = 0; i < 4; i++)
                BotLogic.Go(_map, myAreaBomberman, i, ref DicisionPriority, ref SetBombRate);

            if (OtherBomberAgressive > 0)
            {
                var NearBmber = Map.Nearest(Item.OTHER_BOMBERMAN, Item.OTHER_BOMB_BOMBERMAN);
                if (NearBmber.X > myBomberman.X) DicisionPriority[Direction.Right] += OtherBomberAgressive;
                else DicisionPriority[Direction.Left] += OtherBomberAgressive;
                if (NearBmber.Y > myBomberman.Y) DicisionPriority[Direction.Down] += OtherBomberAgressive;
                else DicisionPriority[Direction.Up] += OtherBomberAgressive;
            }
            if (GhostAgressive > 0)
            {
                var NearGhst = Map.Nearest(Item.MEAT_CHOPPER);
                if (NearGhst.X > myBomberman.X) DicisionPriority[Direction.Right] += GhostAgressive;
                else DicisionPriority[Direction.Left] += GhostAgressive;
                if (NearGhst.Y > myBomberman.Y) DicisionPriority[Direction.Down] += GhostAgressive;
                else DicisionPriority[Direction.Up] += GhostAgressive;
            }

            //int asd = 0;
            //Console.Clear();
            //while (asd < _map.Map.Length)
            //{
            //    Console.WriteLine(_map.Map.Substring(asd, _map.Width));
            //    asd += _map.Width;
            //}
            //Console.Write($"{DicisionPriority[(Direction)0].ToString()} {DicisionPriority[(Direction)1].ToString()} {DicisionPriority[(Direction)2].ToString()} {DicisionPriority[(Direction)3].ToString()} {SetBombRate.ToString()} ");

            Direction dicision = 0;
            double tempMax = 0;
            foreach (var key in DicisionPriority)
                if (key.Value > tempMax)
                {
                    dicision = key.Key;
                    tempMax = key.Value;
                }

            string answer = tempMax == 0 ? BotAction.Stop.ToString() : dicision.ToString();
            for (int i = 0; i < 4; i++)
                BotLogic.Set(_map, myAreaBomberman, i, ref SetBombRate);
            if (SetBombRate >= SetBombPriority)
            {
                answer = answer.Insert(0, BotAction.Act.ToString());
                MyBombList.Add(new Bomb(myBomberman));
            }

            Console.Write($"{DicisionPriority[(Direction)0].ToString()} {DicisionPriority[(Direction)1].ToString()} {DicisionPriority[(Direction)2].ToString()} {DicisionPriority[(Direction)3].ToString()} {SetBombRate.ToString()} ");
            return answer;
        }
        class Bomb
        {
            public Point LocalLocation;
            public Point Location;
            public bool BOOM = false;
            public Bomb(Point Location)
            {
                this.Location = Location;
            }
        }
    }
    class OtherBot
    {
        BrainCard Logic = new BrainCard();
        Point Location = new Point();
        public OtherBot(Point Location)
        {
            this.Location = Location;
        }
        public void TrackNewPos(Board Map)
        {
            Point dir = new Point(0,1);
            List<Item> objList = new List<Item>();
            for (int i = 0; i < 4; i++)
            {
                objList.Add(Map.GetAt(Location + dir));
                dir = dir.Rotate(90);
            }
            objList.Add(Map.GetAt(Location));
            //objList.con
        }
    }
}
