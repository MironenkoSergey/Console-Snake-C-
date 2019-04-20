using System;
using System.Threading;

namespace SnakeGameConsole
{
    class Game
    {
        //переменная игры, если проиграл, то станет false
        bool isCanPlayTheGame;
        //информация о нажатых кнопках во время игры
        ConsoleKeyInfo consoleKeyInfo;
        //сообщение о проигрыше
        string message = "загрызла сама себя :'((";
        //пустой конструктор
        public Game()
        {
            isCanPlayTheGame = true;
        }
        //начало игры
        public void GameStart()
        {
            //поток, отлавливающий нажатие кнопок
            Thread switchKeyThread = new Thread(new ThreadStart(SnakeMove));
            //запрашиваем имя игрока
            Console.WriteLine("Введите имя ");
            string nameOfPlayer = Console.ReadLine();
            //создаем игровое поле и собственно, змеюку
            GameField gameField = new GameField();
            Snake snake = new Snake(gameField);
            //привязываем змейку к полю
            gameField.setSnake(snake);
            //отривовываем поле и изображаем его на консоль
            gameField.drawSnake();
            gameField.ShowField();
            //запускаем поток с отливливанием
            switchKeyThread.Start();
            //основной игровой цикл
            try { 
                while (isCanPlayTheGame)
                {
                    //проверяем съедина ли наша еда (по умолчанию ее нет)
                    gameField.IsEatFood();
                    //размещаем еду
                    gameField.TakeFood();
                    //проверяем нажатие кнопки и врезания в стену
                    gameField.SnakeMove(consoleKeyInfo);
                    isCanPlayTheGame = snake.IsSnakeInField();
                    //проверяем не кушает ли змейка себя
                    isCanPlayTheGame = snake.IsCannibal();
                    //очищаем консоль до отрисовки заново
                    Console.Clear();
                    //заново отрисовываем поле
                    gameField.ShowField();
                    //останавливаем основной поток на 0.4 с
                    Thread.Sleep(400);
                }
            } catch(IndexOutOfRangeException e)
            {
                message = "пробила головой стену";
                switchKeyThread.Abort();
            }
            //сообщение о проигрыше
            SendAboutGameOver(nameOfPlayer, gameField.counter, message);
        }
        //вспомогательная функция
        private void SnakeMove()
        {
            while (isCanPlayTheGame)
            {
                //отлавливаем нажатие клавиши
                consoleKeyInfo = Console.ReadKey();
                //вспомогательный поток засыпает на 0.4 с
                Thread.Sleep(400);
            }
        }
        //сообщение о проигреше
        private void SendAboutGameOver(string name, int counter,string msg)
        {
            Console.WriteLine($"Змея {name} {message} \nсчет : {counter}");
        }
    }
}
