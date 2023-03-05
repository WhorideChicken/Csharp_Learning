using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    class StackPos
    {
        public StackPos(int y, int x) { Y = y; X = x; }
        public int Y;
        public int X;
    }

    class Player
    {
        //1. Player를 Board판 위에 랜더링한다.
        //2. 인풋을 받아 Player가 움직인다.
        public int PosY { get; private set; }
        public int PosX { get; private set; }
        Random _random = new Random();
        Board _board;

        enum Dir
        {
            Up = 0,
            Left = 1,
            Down = 2, 
            Right = 3
        }

        //현재 바라보고 있는 방향을 기준으로 , 좌표 변화를 나타낸다.
        //위 왼 아래 우로 이동 했을 시 Y좌표의 변화
        int[] forntY = new int[] { -1, 0, 1, 0 };

        //위 왼 아래 우로 이동 했을 시 X좌표의 변화
        int[] forntX = new int[] { 0, -1, 0, 1 };


        //바라보고 있는 방향에 기준에서 오른쪽이 어떤 방향인지
        //내가 현재 위를 바라보고 있으면 그걸  기준으로 오른쪽은 
        //내가 왼쪽을 바라보고 있으면 그걸 기준으로 오른쪽은 위를 향하는 방향이니까 -1
        int[] rightY = new int[] { 0, -1, 0, 1 };

        int[] rightX = new int[] { 1, 0, -1, 0 };

        int _dir = (int)Dir.Up;
        List<StackPos> _pos = new List<StackPos>();

        //dest = 목표좌표
        //_board 를 돌아 다녀야하기 때문에 참조를 위해
        public void Initialize(int posY, int posX, Board board)
        {
            PosX = posX;
            PosY = posY;

            _board = board;

            _pos.Add(new StackPos(PosY, PosX));
            RightHandMove();

        }

        //길찾기 알고리즘의 앞서 오른손 법칙 알고리즘 먼저 실습
        private void RightHandMove()
        {
            //목표 지점에 도달 할 떄 까지 Loop
            while(PosX != _board.DestX || PosY != _board.DestY)
            {
                //1. 현재 바라보는 방향을 기준으로 오른쪽으로 갈 수 있는지 확인
                if(_board.Tile[PosY + rightY[_dir], PosX + rightX[_dir]] == Board.TileType.Empty)
                {
                    //갈 수 있으면 오른쪽 방향으로 90도 회전
                    _dir = (_dir - 1 + 4) % 4;

                    //*-> 나머지를 활용한 루프 방법 
                    //Up = 0 => 3 % 4 = 3
                    //Left = 1 => 4 % 4 = 0
                    //Down = 2 => 5 % 4 = 1
                    //Right = 3 => 6 % 4 = 2
                    //오른쪽 회전 공식 (현재값 - 1 + 전채 수) % 전체 수

                    //앞으로 전진   
                    PosY += forntY[_dir];
                    PosX += forntX[_dir];
                    _pos.Add(new StackPos(PosY, PosX));

                }
                //2. 오른쪽이 불가능하면 현재 바라보는 방향을 기준으로 전진 할 수 있는 체크
                else if(_board.Tile[PosY + forntY[_dir], PosX + forntX[_dir]] == Board.TileType.Empty)
                {
                    //앞으로 전진
                    PosY += forntY[_dir];
                    PosX += forntX[_dir];
                    _pos.Add(new StackPos(PosY, PosX));
                }
                else
                {
                    //왼쪽 방향으로 90도 회전
                    //오른쪽 회전 공식 (현재값 + 1 + 전채 수) % 전체 수
                    _dir = (_dir + 1 + 4) % 4;
                }

            }
        }


        private void PlayerMoveRandom()
        {
            //상하좌우니 4가지
            int randomValue = _random.Next(0, 5);
            switch (randomValue)
            {
                case 0: //상
                    if (PosY - 1 >= 0 && _board.Tile[PosY - 1, PosX] == Board.TileType.Empty)
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

        const int MOVE_TICK = 10; //100인 이유 1초는 1000ms이기 때문에 0.1초는 100이다
        int _sumTick = 0;
        int laistIndex;
        public void Update(int deltaTIck)
        {
            if (laistIndex >= _pos.Count)
                return;

            //30프레임 주기로 이동할 텐데 너무 빠리기 때문에 deltaTIck으로 관리한다.
            //deltaTIck 을 통해 이전 시간과 현재 시간의 차이를 알 수 있어 관리르 통해 0.1초마다 이동하도록 수정해보자
            _sumTick += deltaTIck;//0.1초가 될 때까지 더하다 조건이 충족 되면 이동시키고 sum을 초기화한다.
            if(_sumTick >= MOVE_TICK)
            {
                _sumTick = 0;
                PosY = _pos[laistIndex].Y;
                PosX = _pos[laistIndex].X;
                laistIndex++;
            }
        }
    }
}
