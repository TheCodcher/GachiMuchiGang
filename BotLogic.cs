using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon_Master
{
    static class BotLogic
    {
        public static void Go(Board map, Point Me, int i, ref Dictionary<Direction, int> DicisionPriority, ref int SetBombRate)
        {
            int Angle = i * 90;
            var isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.Clear)
            {
                DicisionPriority[(Direction)i] += 1;

                //новые два блока
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
                if ((isBlock == Motivate.MyBomb2) || (isBlock == Motivate.MyBomb543))
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -2).Rotate(Angle)));
                    if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2))
                    {
                        DicisionPriority[(Direction)i] -= 100;
                    }

                }

                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -1).Rotate(Angle)));
                if ((isBlock == Motivate.MyBomb2) || (isBlock == Motivate.MyBomb543))
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -2).Rotate(Angle)));
                    if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2))
                    {
                        DicisionPriority[(Direction)i] -= 100;
                    }

                }

                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
                if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.Ghost) || (isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.MyBomb543) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2))
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -1).Rotate(Angle)));
                    if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.Ghost) || (isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.MyBomb543) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2))
                    {
                        DicisionPriority[(Direction)i] -= 30;
                    }

                }






                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
                if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 2;
                if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 4;
                if (isBlock == Motivate.MyBomb2) DicisionPriority[(Direction)i] -= 15;
                if (isBlock == Motivate.MyBomb1) DicisionPriority[(Direction)i] -= 100;
                if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2)) DicisionPriority[(Direction)i] -= 4;
                if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.TempLocker)) DicisionPriority[(Direction)i] -= 1;

                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -1).Rotate(Angle)));
                if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 2;
                if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 4;
                if (isBlock == Motivate.MyBomb2) DicisionPriority[(Direction)i] -= 15;
                if (isBlock == Motivate.MyBomb1) DicisionPriority[(Direction)i] -= 100;
                if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2)) DicisionPriority[(Direction)i] -= 4;
                if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.TempLocker)) DicisionPriority[(Direction)i] -= 1;



                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -2).Rotate(Angle)));
                if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.TempLocker)) DicisionPriority[(Direction)i] -= 1;
                if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 5;
                if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2)) DicisionPriority[(Direction)i] -= 4;
                if ((isBlock == Motivate.Bomb5) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb3)) DicisionPriority[(Direction)i] -= 3;
                if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2)) DicisionPriority[(Direction)i] -= 3;
                if (isBlock == Motivate.OtherBomberman) DicisionPriority[(Direction)i] -= 1;
                if (isBlock == Motivate.OtherBombBomberman) DicisionPriority[(Direction)i] -= 2;




                if (isBlock == Motivate.Clear)
                {
                    DicisionPriority[(Direction)i] += 2;


                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.Clear)
                    {
                        DicisionPriority[(Direction)i] += 1;
                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(2, -2).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 1;
                        if (isBlock == Motivate.Bomb1) DicisionPriority[(Direction)i] -= 1;
                        if (isBlock == Motivate.MyBomb1) DicisionPriority[(Direction)i] -= 1;
                    }
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 2;
                    if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2)) DicisionPriority[(Direction)i] -= 1;
                    if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2)) DicisionPriority[(Direction)i] -= 1;
                    if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall)) DicisionPriority[(Direction)i] -= 1;

                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.Clear)
                    {
                        DicisionPriority[(Direction)i] += 1;
                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-2, -2).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 1;
                        if (isBlock == Motivate.Bomb1) DicisionPriority[(Direction)i] -= 1;
                        if (isBlock == Motivate.MyBomb1) DicisionPriority[(Direction)i] -= 1;
                    }
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 2;
                    if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2)) DicisionPriority[(Direction)i] -= 1;
                    if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2)) DicisionPriority[(Direction)i] -= 1;
                    if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall)) DicisionPriority[(Direction)i] -= 1;



                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -3).Rotate(Angle)));
                    if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 3;
                    if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2)) DicisionPriority[(Direction)i] -= 1;
                    if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 2;
                    if (isBlock == Motivate.OtherBombBomberman) DicisionPriority[(Direction)i] -= 1;

                    if (isBlock == Motivate.Clear)
                    {
                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -3).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 1;
                        if (isBlock == Motivate.Bomb1) DicisionPriority[(Direction)i] -= 1;
                        if (isBlock == Motivate.MyBomb1) DicisionPriority[(Direction)i] -= 1;

                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -3).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 1;
                        if (isBlock == Motivate.Bomb1) DicisionPriority[(Direction)i] -= 1;
                        if (isBlock == Motivate.MyBomb1) DicisionPriority[(Direction)i] -= 1;

                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -4).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) DicisionPriority[(Direction)i] += 3;
                        if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2)) DicisionPriority[(Direction)i] -= 1;
                        if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 1;
                    }

                }
            }
            else
            {
                DicisionPriority[(Direction)i] -= 10;
            }

            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.TempLocker)) DicisionPriority[(Direction)i] -= 20;
            if (isBlock == Motivate.Ghost) DicisionPriority[(Direction)i] -= 50;
            if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb2)) DicisionPriority[(Direction)i] -= 20;
            if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb543) || (isBlock == Motivate.MyBomb2)) DicisionPriority[(Direction)i] -= 20;
            if (isBlock == Motivate.OtherBomberman) DicisionPriority[(Direction)i] -= 1;
            if (isBlock == Motivate.OtherBombBomberman) DicisionPriority[(Direction)i] -= 2;
            if (isBlock == Motivate.Boom) DicisionPriority[(Direction)i] -= 100;

            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, 0).Rotate(Angle)));
            if (isBlock == Motivate.Boom) DicisionPriority[(Direction)i] += 25;
            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, 0).Rotate(Angle)));
            if (isBlock == Motivate.Boom) DicisionPriority[(Direction)i] += 25;
        }
        public static void Set(Board map, Point Me, int i, ref int SetBombRate)
        {
            int Answer = 0;
            int Angle = i * 90;
            var isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock != Motivate.Wall)
            {

                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -2).Rotate(Angle)));

                if (isBlock == Motivate.OtherBombBomberman) Answer += 2;
                if (isBlock == Motivate.OtherBomberman) Answer += 4;
                if (isBlock == Motivate.Ghost) Answer += 2;
                if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall)) Answer -= 1;
                if (isBlock == Motivate.MyBomb1) Answer -= 50;
                if (isBlock == Motivate.MyBomb2) Answer += 1;
                if (isBlock == Motivate.MyBomb543) Answer += 2;
                if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.Boom)) Answer -= 150;


                if (isBlock != Motivate.Wall)
                {
                    //2 горизонталь боковые
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                    if (isBlock == Motivate.OtherBomberman) Answer += 3;
                    if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2)) Answer -= 2;
                    if (isBlock == Motivate.Ghost) Answer += 1;
                    if (isBlock != Motivate.Wall)
                    {
                        Answer += 1;
                        //2 горизонталь дальние боковые
                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(2, -2).Rotate(Angle)));
                        if (isBlock == Motivate.Ghost) Answer += 1;
                        if (isBlock == Motivate.OtherBomberman) Answer += 1;
                    }
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                    if (isBlock == Motivate.OtherBomberman) Answer += 3;
                    if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2)) Answer -= 2;
                    if (isBlock == Motivate.Ghost) Answer += 1;
                    if (isBlock != Motivate.Wall)
                    {
                        Answer += 1;
                        //2 горизонталь дальние боковые
                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-2, -2).Rotate(Angle)));
                        if (isBlock == Motivate.Ghost) Answer += 1;
                        if (isBlock == Motivate.OtherBomberman) Answer += 1;
                    }


                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -3).Rotate(Angle)));
                    if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                    if (isBlock == Motivate.OtherBomberman) Answer += 2;
                    if (isBlock == Motivate.MyBomb1) Answer -= 50;
                    if (isBlock == Motivate.MyBomb2) Answer += 1;
                    if (isBlock == Motivate.MyBomb543) Answer += 2;
                    if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.Boom)) Answer -= 150;
                    if (isBlock != Motivate.Wall)
                    {
                        //3 горизонталь боковые
                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -3).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) Answer += 1;
                        if (isBlock == Motivate.OtherBomberman) Answer += 1;
                        if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                        if (isBlock == Motivate.Ghost) Answer += 1;

                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -3).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) Answer += 1;
                        if (isBlock == Motivate.OtherBomberman) Answer += 1;
                        if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                        if (isBlock == Motivate.Ghost) Answer += 1;

                        //расстояние -4
                        isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -4).Rotate(Angle)));
                        if (isBlock == Motivate.Clear) Answer += 1;
                        if (isBlock == Motivate.Ghost) Answer += 1;
                        if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                        if (isBlock == Motivate.OtherBomberman) Answer += 1;
                        if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3)) Answer -= 2;
                        if ((isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5)) Answer -= 1;
                        if ((isBlock == Motivate.MyBomb2) || (isBlock == Motivate.MyBomb1)) Answer -= 150;
                    }
                    //при расстоянии -2

                }
                //при расстоянии -1
                //1 горизонталь бока
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
                if (isBlock == Motivate.Clear) Answer += 1;
                if (isBlock == Motivate.DistroybleWall) Answer += 1;
                if (isBlock == Motivate.OtherBomberman) Answer += 1;
                if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                if (isBlock == Motivate.Ghost) Answer += 1;

                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -1).Rotate(Angle)));
                if (isBlock == Motivate.Clear) Answer += 1;
                if (isBlock == Motivate.DistroybleWall) Answer += 1;
                if (isBlock == Motivate.OtherBomberman) Answer += 1;
                if (isBlock == Motivate.OtherBombBomberman) Answer += 1;
                if (isBlock == Motivate.Ghost) Answer += 1;



            }
            //когда что-то стоит вплотную
            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.DistroybleWall) Answer += 1;
            if ((isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.Boom)) Answer -= 150;
            if ((isBlock == Motivate.OtherBomberman) || (isBlock == Motivate.OtherBombBomberman)) Answer += 10;
            if (isBlock == Motivate.MyBomb543) Answer += 2;
            if (isBlock == Motivate.MyBomb1) Answer -= 50;
            if (isBlock == Motivate.Ghost) Answer += 1;

            isBlock = BrainCard.GetMotivate(map.GetAt(Me));
            if (isBlock == Motivate.Boom) Answer -= 150;

            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.MyBomb543)
            {
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
                if (isBlock == Motivate.MyBomb543 )
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(2, -1).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                }
            }

            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.MyBomb543)
            {
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -1).Rotate(Angle)));
                if (isBlock == Motivate.MyBomb543)
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-2, -1).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                }
            }


            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.MyBomb543)
            {
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -2).Rotate(Angle)));
                if (isBlock == Motivate.MyBomb543)
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                }
            }


            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.MyBomb543)
            {
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
                if (isBlock == Motivate.MyBomb543)
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, 0).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -2).Rotate(Angle)));
                    if (isBlock == Motivate.MyBomb2 || isBlock == Motivate.MyBomb1)
                    {
                        Answer -= 100;
                    }
                }
            }


            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
            if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.Ghost) || (isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.MyBomb543) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2))
            {
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -1).Rotate(Angle)));
                if ((isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.Ghost) || (isBlock == Motivate.Bomb1) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb4) || (isBlock == Motivate.Bomb5) || (isBlock == Motivate.MyBomb543) || (isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2))
                {
                    Answer -= 2;
                }

            }
            if ((BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me + new Point(0, 1))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, 0))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me + new Point(1, 0))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me)) == Motivate.Boom)) Answer += 10000;

            //сложная конструкция с проверкой угла после ряда бомб
            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.Clear)
            {
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, 0).Rotate(Angle)));
                if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2) || (isBlock == Motivate.MyBomb543))
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, -1).Rotate(Angle)));
                    if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2) || (isBlock == Motivate.MyBomb543) || (isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb1))
                    {
                        Answer -= 55;
                    }
                }
            }


            isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1).Rotate(Angle)));
            if (isBlock == Motivate.Clear)
            {
                isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(1, 0).Rotate(Angle)));
                if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2) || (isBlock == Motivate.MyBomb543))
                {
                    isBlock = BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, -1).Rotate(Angle)));
                    if ((isBlock == Motivate.MyBomb1) || (isBlock == Motivate.MyBomb2) || (isBlock == Motivate.MyBomb543) || (isBlock == Motivate.Wall) || (isBlock == Motivate.DistroybleWall) || (isBlock == Motivate.Bomb2) || (isBlock == Motivate.Bomb3) || (isBlock == Motivate.Bomb1))
                    {
                        Answer -= 55;
                    }
                }
            }

            if ((BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me + new Point(0, 1))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, 0))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me + new Point(1, 0))) != Motivate.Clear) &&
                    (BrainCard.GetMotivate(map.GetAt(Me)) == Motivate.Boom)) { Answer += 10000; }

            if (((BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1))) == Motivate.Wall) || (BrainCard.GetMotivate(map.GetAt(Me + new Point(0, -1))) == Motivate.DistroybleWall)) &&
                    ((BrainCard.GetMotivate(map.GetAt(Me + new Point(0, 1))) == Motivate.Wall) || (BrainCard.GetMotivate(map.GetAt(Me + new Point(0, 1))) == Motivate.DistroybleWall)) &&
                    ((BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, 0))) == Motivate.Wall) || (BrainCard.GetMotivate(map.GetAt(Me + new Point(-1, 0))) == Motivate.DistroybleWall)) &&
                    ((BrainCard.GetMotivate(map.GetAt(Me + new Point(1, 0))) == Motivate.Wall) || (BrainCard.GetMotivate(map.GetAt(Me + new Point(1, 0))) == Motivate.DistroybleWall))) { Answer += 10000; }


            SetBombRate += Answer;
            //Console.Write($"{Answer.ToString()} ");
        }
    }
}
