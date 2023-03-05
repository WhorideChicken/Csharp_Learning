using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Learning
{
    class MainPrograms
    {


        public static void Main(string[] args)
        {
            Board board = new Board();
            Player player = new Player();
            board.Initialize(25, player);
            player.Initialize(1, 1, board);
            //커서가 안보이도록 환경 설정
            Console.CursorVisible = false;

            //프레임 관리
            //FPS 프레임(60프레임 정도면 부드럽고 30프레임 이하면 게임 하기 어렵다)
            //1초에 아래 입력, 로직, 랜더링 로직이 몇번 진행되는지가 프렘임이다
            //프레임 계산

            const int WAIT_TICK = 1000 / 30;
            //마지막으로 측정한 시간
            int lastTick = 0;

            while (true)
            {
                #region Frame
                //프레임 : 현재 시간
                int currentTick = System.Environment.TickCount;//밀리 세컨드이다 1초 == 1000ms
                                                                 //경과 된 시간 현재시간 - 마지막 시간
                                                                 //만약에 경과한 시간이 1/30초보다 작다면 continue 밀리 세컨드이다 1초 == 1000ms
                if (currentTick - lastTick < WAIT_TICK)
                    continue; //30프레임이 안나오면 아래 로직을 돌리지 않고 다시 while문 초기로 

                //1/30보다 경과 됐다면 시간을 기록하여 다음 루프 때 다시 측정한다. 
                int deltaTick = currentTick - lastTick;
                lastTick = currentTick;
                
                #endregion Frame

                //입력 : 키보드 마우스 인풋

                //로직 : 입력에 따른 동작된 게임 로직
                player.Update(deltaTick);
                //랜더링 : 게임을 그려주는 랜더링

                //콘솔 커서 위치 고정
                Console.SetCursorPosition(0, 0);
                board.Renderer();
            }
        }
    }
}
