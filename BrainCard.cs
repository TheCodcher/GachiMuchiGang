using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Dungeon_Master
{
    class BrainCard
    {
        private static int CardRow = Enum.GetValues(typeof(Motivate)).Length;
        private static int CardColumn = Enum.GetValues(typeof(Direction)).Length;
        Dictionary<BotAction, KTable> KeyCard = new Dictionary<BotAction, KTable>();
        /// <summary>
        /// Содержит чувствительность ко всем объектам в нынешнем ходу
        /// </summary>
        Cell[,] OneRunTable = new Cell[CardRow, CardColumn];
        public BrainCard()
        {
            KeyCard = InitializeDictinory<BotAction, KTable>();
            UpdateOneRunTable();
        }
        /// <summary>
        /// Работа ведется с ныннешнем состоянием поля OneRunTable
        /// </summary>
        /// <returns></returns>
        public BotAction MakeDecision()
        {
            var Relevance = InitializeDictinory<BotAction, double>();
            foreach(var key in Relevance)
            {
                for (int i = 0; i < CardRow; i++)
                    for (int j = 0; j < CardColumn; j++)
                        Relevance[key.Key] += Math.Abs(OneRunTable[i, j].Value - KeyCard[key.Key].TakePlace((Motivate)i, (Direction)j));
            }
            BotAction dicision = (BotAction)0;
            double tempMin = Relevance[dicision];
            foreach (var key in Relevance)
                if (key.Value < tempMin) dicision = key.Key;
            return dicision;
        }
        /// <summary>
        /// Добавляет состояние объекта(расстояние до него в необходимом направлении) в таблицу OneRunTable
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="place"></param>
        /// <param name="distance"></param>
        public void WriteOneRunValue(Item obj, Direction place, double distance)
        {
            var vull = OneRunTable[(int)GetMotivate(obj), (int)place];
            vull.Value = (vull.Value * vull.Weight + distance) / (vull.Weight + 1);
            vull.Weight++;
        }
        /// <summary>
        /// Отчищает значения в таблице OneTableRun
        /// </summary>
        public void UpdateOneRunTable()
        {
            OneRunTable = new Cell[CardRow, CardColumn];
            for (int i = 0; i < CardRow; i++)
                for (int j = 0; j < CardColumn; j++)
                    OneRunTable[i, j] = new Cell();
        }
        /// <summary>
        /// Добавляет нынешнее состояние таблицы OneTableRun в таблицу коэфицентов
        /// </summary>
        /// <param name="dision"></param>
        public void AcceptOneRunTable(BotAction dision)
        {
            ref var ktable = ref KeyCard[dision].RateTable;
            ref var ktableweigth = ref KeyCard[dision].UseCounter;
            for (int i = 0; i < CardRow; i++)
                for (int j = 0; j < CardColumn; j++)
                {
                    ktable[i,j]= (ktable[i, j] * ktableweigth + OneRunTable[i,j].Value) / (ktableweigth + 1);
                }
            ktableweigth++;
        }
        /// <summary>
        /// Возвращает коэфициент из таблицы коэфициентов определенного решения
        /// </summary>
        /// <param name="act">Решение</param>
        /// <param name="obj">Для какого объекта</param>
        /// <param name="dir">В каком направлении</param>
        /// <returns></returns>
        public double GetObjRate(BotAction act, Item obj, Direction dir) => KeyCard[act].TakePlace(GetMotivate(obj), dir); //если умножить расстояние на это коэфициент
        /// <summary>
        /// Возвращает мотиватор, который принадлежит объекту
        /// </summary>
        /// <param name="it">Объект на поле</param>
        /// <returns></returns>
        public static Motivate GetMotivate(Item it)
        {
            if (it == Item.OTHER_DEAD_BOMBERMAN || it == Item.DestroyedWall || it == Item.DeadMeatChopper) return Motivate.TempLocker;
            if (it == Item.MY_BOMB_TIMER_5 || it == Item.MY_BOMB_TIMER_4 || it == Item.MY_BOMB_TIMER_3) return Motivate.MyBomb543;
            else return (Motivate)it;
        }
        class KTable
        {
            public double[,] RateTable = new double[CardRow, CardColumn];
            public int UseCounter = 0;
            public ref double TakePlace(Motivate motive, Direction dir) => ref RateTable[(int)motive, (int)dir];
        }
        class Cell
        {
            public double Value = 0;
            public int Weight = 0;
        }
        public static Dictionary<T, V> InitializeDictinory<T, V>() 
            where T:Enum
            where V:new()
        {
            var dct = new Dictionary<T, V>();
            foreach (T a in Enum.GetValues(typeof(T)))
            {
                dct.Add(a, new V());
            }
            return dct;
        }
    }
}
