using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SnakeGameConsole
{
    //используется интерфейс для создания нового объекта класса
    //а не копирования
    class Point : ICloneable
    {
        public int x;
        public int y;

        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        //метод для клонирования объектов
        public object Clone()
        {
            return new Point(this.x,this.y);
        }

        public bool Equals(Point p)
        {
            return p != null &&
                   x == p.x &&
                   y == p.y;
        }
    }
}
