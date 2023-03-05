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
        public TileType[,] Tile { get; private set; }
        public int Size { get; private set; }
        const char CIRCLE = '\u25cf';


        public int DestY { get; private set; }
        public int DestX { get; private set; }


        Player _player;
        public enum TileType
        {
            Empty,
            Wall
        }

        public void Initialize(int size, Player player)
        {
            //미로는 홀수여야한다.
            if (size % 2 == 0)
                return;

            _player = player;

            Tile = new TileType[size, size];
            Size = size;

            DestY = Size - 2;
            DestX = Size - 2;

            BoardSettingSizeWinder();

        }



        private void BoardSetting_Basic()
        {
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x == 0 || x == Size - 1 || y == 0 || y == Size - 1)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }
        }

        //바이너리 트리 미로 알고리즘
        private void BoardSetting_Binary()
        {
            //1.기을 다 막는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if(x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }


            Random rand = new Random();
            //2. 랜덤으로 우칙 혹은 아래로 길을 뚫는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue; //위에서 이미 막아 뒀기 때문에 다른 작업을 하지 않고 루프를 다시 태운다

                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x+1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y +1, x ] = TileType.Empty;
                        continue;
                    }



                    //랜덤하게 0 혹은 1이 된다(2는 포함하지 않습니다.)
                    //0이면 우측으로 긿을 뚫는다 
                    if(rand.Next(0, 2) == 0)
                    {
                        Tile[y, x+1] = TileType.Empty;
                    }
                    else
                    {
                        Tile[y+1, x ] = TileType.Empty;
                    }

                }
            }
        }


        private void BoardSettingSizeWinder()
        {
            //1.기을 다 막는 작업
            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        Tile[y, x] = TileType.Wall;
                    else
                        Tile[y, x] = TileType.Empty;
                }
            }


            Random rand = new Random();
            //2. 랜덤으로 우칙 혹은 아래로 길을 뚫는 작업
            for (int y = 0; y < Size; y++)
            {
                int count = 1;
                for (int x = 0; x < Size; x++)
                {
                    if (x % 2 == 0 || y % 2 == 0)
                        continue; //위에서 이미 막아 뒀기 때문에 다른 작업을 하지 않고 루프를 다시 태운다


                    if (y == Size - 2 && x == Size - 2)
                        continue;

                    if (y == Size - 2)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        continue;
                    }

                    if (x == Size - 2)
                    {
                        Tile[y + 1, x] = TileType.Empty;
                        continue;
                    }

                    //랜덤하게 0 혹은 1이 된다(2는 포함하지 않습니다.)
                    //우측으로가 다가 특정 좌표를 찾아내어 아래로 뚫는다
                    if (rand.Next(0, 2) == 0)
                    {
                        Tile[y, x + 1] = TileType.Empty;
                        count++;//우측으로 긿을 뚫을 때 마다 카운트 증가
                    }
                    else
                    {
                        //우측으로 뚫다가 우측으로 뚫은 것들 중 랜덤하게 그중 하나에서 아래로 긿을 뚫기 위해
                        //count중에서 랜덤하게 뽑는다.
                        //x - randomIndex * 2 : x좌표 하마다 벽으로 이어져 있기 때문에(Empty - Wall \ Empty)
                        int randomIndex = rand.Next(0, count);
                        Tile[y + 1, x - randomIndex * 2] = TileType.Empty;
                        count = 1; //아래로 뚫었으니 카운트 초기화
                    }

                }
            }
        }

        public void Renderer()
        {
            ConsoleColor prevColor = Console.ForegroundColor;

            for (int y = 0; y < Size; y++)
            {
                for (int x = 0; x < Size; x++)
                {
                    //플레이어 좌표를 갖고 와서 그 좌표랑 현재 y,x가 일치하면 플레이어 전용 색상으로 표시
                    if (x == _player.PosX && y == _player.PosY)
                        Console.ForegroundColor = ConsoleColor.Blue;
                    else if (x == DestX && y == DestY)
                        Console.ForegroundColor = ConsoleColor.Yellow;
                    else
                        Console.ForegroundColor = GetTileColor(Tile[y, x]);

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
