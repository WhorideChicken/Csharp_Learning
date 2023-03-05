using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    class Player
    {
        //1. Player를 Board판 위에 랜더링한다.
        //2. 인풋을 받아 Player가 움직인다.
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();
        Board _board;

        //dest = 목표좌표
        //_board 를 돌아 다녀야하기 때문에 참조를 위해
        public void Initialize(int posY, int posX, int destX, int destY, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;
        }

        const int MOVE_TICK = 100; //100인 이유 1초는 1000ms이기 때문에 0.1초는 100이다
        int _sumTick = 0;
        public void Update(int deltaTIck)
        {
            //30프레임 주기로 이동할 텐데 너무 빠리기 때문에 deltaTIck으로 관리한다.
            //deltaTIck 을 통해 이전 시간과 현재 시간의 차이를 알 수 있어 관리르 통해 0.1초마다 이동하도록 수정해보자
            _sumTick += deltaTIck;//0.1초가 될 때까지 더하다 조건이 충족 되면 이동시키고 sum을 초기화한다.
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;

                //상하좌우니 4가지
                int randomValue = _random.Next(0, 5);
                switch (randomValue)
                {
                    case 0: //상
                        if(PosY - 1 >= 0 && _board.Tile[PosY-1,PosX] == Board.TileType.Empty)
                        {
                            PosY -= 1;
                        }
                        break;
                    case 1: //하
                        if (PosY + 1 < _board.Size && _board.Tile[PosY + 1, PosX] == Board.TileType.Empty)
                        {
                            PosY += 1;
                        }
                        break;
                    case 2: //좌
                        if (PosX - 1 >= 0 && _board.Tile[PosY, PosX - 1] == Board.TileType.Empty)
                        {
                            PosX -= 1;
                        }
                        break;
                    case 3: //우
                        if (PosX + 1 < _board.Size && _board.Tile[PosY, PosX + 1] == Board.TileType.Empty)
                        {
                            PosX += 1;
                        }
                        break;
                }


            }
        }
    }
}
