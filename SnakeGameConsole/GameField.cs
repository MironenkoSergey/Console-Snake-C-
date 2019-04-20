using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGameConsole
{
    class GameField
    {
        //двумерный массив содержащий поле
        public char[,] field;
        //размер поля
        private static int sizeOfField;
        //привязка к змейке
        Snake snake;
        //еда
        public Food food;
        //счетчик
        public int counter;

        //конструктор
        public GameField()
        {
            sizeOfField = 10;
            InitField();
            food = new Food(this);
            counter = 0;
        }
        //получить размер поля
        public int Size => sizeOfField;
        //привязать змею
        public void setSnake(Snake newSnake)
        {
            snake = newSnake;
        }
        //инициализация поля и заполнение *
        private void InitField()
        {
            field = new char[sizeOfField,sizeOfField];
            for (int i = 0; i < sizeOfField; i++)
            {
                field[0, i] = '*';
                field[sizeOfField - 1, i] = '*';
                field[i, 0] = '*';
                field[i, sizeOfField-1] = '*';
            }
        }
        //изображает змейку на поле
        public void drawSnake()
        {
            foreach (Point point in snake.coordinates)
            {
                field[point.y, point.x] = 's';
            }
            field[snake.coordinates.Last().y,snake.coordinates.Last().x] = 'S';
        }
        //перерисовывает змейку
        public void RedrawSnake(Point point)
        {
            //удаляет не нужный хвост змейки
            field[point.y, point.x] = ' ';
            //вновь рисует змейку по новым координатам
            drawSnake();
        }
        //движение змейки
        public bool SnakeMove(ConsoleKeyInfo consoleKeyInfo)
        {
            //создает клон "хвоста"
            Point point = (Point)snake[0].Clone();
            //переменная с информацией о нажатой кнопке
            //ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            //двигает змейку в соответствии с направлением нажатой кнопки
            //движение вправо
            if (consoleKeyInfo.Key.Equals(ConsoleKey.RightArrow))
            {   
                //истина - змейка врезается в правую стенку
                //if (snake.Head.x == sizeOfField - 2)
                //    return false;
                //змейка двигается
                snake.MoveRight();
                //движение отображается на поле
                RedrawSnake(point);
            }
            //движение влево
            if (consoleKeyInfo.Key.Equals(ConsoleKey.LeftArrow))
            {
                //if (snake.Head.x == 1)
                //    return false;
                snake.MoveLeft();
                RedrawSnake(point);
            }
            //движение вверх (не фильм)
            if (consoleKeyInfo.Key.Equals(ConsoleKey.UpArrow))
            {
                //if (snake.Head.y == 1)
                //    return false;
                snake.MoveUp();
                RedrawSnake(point);
            }
            //движение вниз
            if (consoleKeyInfo.Key.Equals(ConsoleKey.DownArrow))
            {
                //if (snake.Head.y == sizeOfField - 2)
                //    return false;
                snake.MoveDown();
                RedrawSnake(point);
            }
            //если не было нажато ни на одну из стрелок игра продолжается
            return true;
        }
        //показать поле
        public void ShowField()
        {
            for (int i = 0; i < sizeOfField; i++)
            {
                for (int j = 0; j < sizeOfField; j++)
                {
                    Console.Write(field[i,j]+" ");

                }
                Console.Write('\n');
            }
        }
        //генерация еды
        public void TakeFood()
        {
            if (!food.isFoodExcist)
            {
                food = new Food(this);
            }
            field[food.coordinate.y, food.coordinate.x] = food.foodSymbol;
        }
        //проверка съедена ли еда
        //если координаты еды и головы совпадут
        //то возвращается true
        public void IsEatFood()
        {
            if ((food.coordinate.x == snake.Head.x)
                    && (food.coordinate.y == snake.Head.y))
            {
                food.isFoodExcist = false;
                snake.GroveUp(snake[0].x, snake[0].y);
                counter++;
            }
        }
        //проверка не вризается ли змея в стену
        public bool IsSnakeInField()
        {
            if (snake.Head.x == 0 ||
               snake.Head.x == sizeOfField - 1 ||
               snake.Head.y == 0 ||
               snake.Head.y == sizeOfField - 1)
                return false;
            return true;
        }
    }
}
