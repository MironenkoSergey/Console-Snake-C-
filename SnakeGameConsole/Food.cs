using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameConsole
{
    class Food
    {
        //символ на поле
        public char foodSymbol;
        //координаты
        public Point coordinate;
        public GameField gameField;
        //съедена ли еда
        public bool isFoodExcist;
        //конструктор
        public Food(GameField game)
        {
            isFoodExcist = true;
            foodSymbol = 'f';
            gameField = game;
            coordinate =getFood();
        }
        //Генерация координат еды
        public Point getFood()
        {
            char c;
            Point point;
            do
            {
                Random random = new Random();
                //генерируем случайно координаты
                point = new Point(random.Next(1, gameField.Size-1), random.Next(1, gameField.Size - 1));
                c = gameField.field[point.y, point.x];
            //будет выполняться пока координаты не будут совпадать со змейкой
            } while (c == 's');
            return point;
        }

    }
}

