using System.Collections.Generic;
using System.Linq;

namespace SnakeGameConsole
{
    class Snake
    {
        //массив координат,привязка к полю и размер змейки
        public List<Point> coordinates;
        public GameField gameField;
        public int size;
        //конструктор
        public Snake(GameField newField)
        {
            gameField = newField;
            size = 3;
            InitSnake();
        }
        //заполнение массива координат со змейкой
        private void InitSnake()
        {
            coordinates = new List<Point>();
            for (int i = 0; i < size; i++)
            {
                coordinates.Add(new Point(i + 1, 1));
            }
        }
        //свойтво для получения последнего элемента в массиве или же головы
        public Point Head => coordinates.Last();
        //предпоследний элемент
        public Point Neck => coordinates[coordinates.Count - 2];
        //размер змеи
        public int Size => coordinates.Count;
        //индексатор для массива с координатами
        public Point this[int index]
        {
            get
            {
                return coordinates[index];
            }
            set
            {
                coordinates[index] = value;
            }
        }
        //функция движения змейки вправо
        public void MoveRight()
        {
            if ((Head.x > Neck.x) || (Head.y != Neck.y))
                Move(Head.x + 1, Head.y);
        }
        //функция движения змейки влево
        public void MoveLeft()
        {
            if ((Head.x < Neck.x) || (Head.y != Neck.y))
                Move(Head.x - 1, Head.y);
        }
        //функция движения вверх
        public void MoveUp()
        {
            if ((Head.y < Neck.y) || (Head.x != Neck.x))
                Move(Head.x, Head.y - 1);
        }
        public void MoveDown()
        {
            if ((Head.y > Neck.y) || (Head.x != Neck.x))
                Move(Head.x, Head.y + 1);
        }
        //Координата из хвоста удаляется и еще одна добавляется в голову
        //Получается передвижение змейки этоудаление хвоста и добавления
        //нового элемента в голову, соответсвующего движению
        public void Move(int x, int y)
        {
            coordinates.Add(new Point(x, y));
            coordinates.Remove(coordinates.First());
        }
        //рост змейки
        public void GroveUp(int x, int y)
        {
            coordinates.Insert(0, new Point(x, y));
        }
        //если змейка есть сама себя
        public bool IsCannibal()
        {
            for (int i = 0; i < coordinates.Count - 2; i++)
            {
                if (coordinates[i].Equals(Head))
                {
                    return false;
                }
            }
            return true;
        }
        public bool IsSnakeInField()
        {
            if (Head.x == 0 ||
               Head.x == gameField.Size - 1 ||
               Head.y == 0 ||
               Head.y == gameField.Size - 1)
                return false;
            return true;
        }
    }
}