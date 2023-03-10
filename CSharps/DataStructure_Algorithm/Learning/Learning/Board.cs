using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    //배열을 이용해서 미로를 만들자

    class Board
    {
        public TileType[,] _tile;
        public int _size;
        const char CIRCLE = '\u25cf';

        public enum TileType
        {
            Empty,
            Wall
        }


        private void BoardSetting_Basic()
        {
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x == 0 || x == _size - 1 || y == 0 || y == _size - 1)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }
        }

        //바이너리 트리 미로 알고리즘
        private void BoardSetting_Binary()
        {
            //1.기을 다 막는 작업
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if(x % 2 == 0 || y % 2 == 0)
                        _tile[y, x] = TileType.Wall;
                    else
                        _tile[y, x] = TileType.Empty;
                }
            }


            Random rand = new Random();
            //2. 랜덤으로 우칙 혹은 아래로 길을 뚫는 작업
            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue; //위에서 이미 막아 뒀기 때문에 다른 작업을 하지 않고 루프를 다시 태운다

                    if (y == _size - 2 && x == _size - 2)
                        continue;

                    if (y == _size - 2)
                    {
                        _tile[y, x+1] = TileType.Empty;
                        continue;
                    }

                    if (x == _size - 2)
                    {
                        _tile[y +1, x ] = TileType.Empty;
                        continue;
                    }



                    //랜덤하게 0 혹은 1이 된다(2는 포함하지 않습니다.)
                    //0이면 우측으로 긿을 뚫는다 
                    if(rand.Next(0, 2) == 0)
                    {
                        _tile[y, x+1] = TileType.Empty;
                    }
                    else
                    {
                        _tile[y+1, x ] = TileType.Empty;
                    }

                }
            }
        }

        public void Initialize(int size)
        {
            //미로는 홀수여야한다.
            if (size % 2 == 0)
                return;

            _tile = new TileType[size, size];
            _size = size;

            BoardSetting_Binary();

        }

        public void Renderer()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < _size; y++)
            {
                for (int x = 0; x < _size; x++)
                {
                    Console.ForegroundColor = GetTileColor(_tile[y, x]);
                    Console.Write(CIRCLE);
                }
                Console.WriteLine();
            }

            Console.ForegroundColor = prevColor;
        }


        ConsoleColor GetTileColor(TileType type)
        {
            switch (type)
            {
                case TileType.Empty:
                    return ConsoleColor.Green;
                case TileType.Wall:
                    return ConsoleColor.Red;
                default:
                    return ConsoleColor.Green;
            }

        }

    }
}
