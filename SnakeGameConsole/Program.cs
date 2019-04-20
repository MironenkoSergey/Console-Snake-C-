using System;
using System.Threading;

namespace SnakeGameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            //создаем объект игры
            Game game = new Game();
            //запускаем старт
            game.GameStart();
        }
    }
}
