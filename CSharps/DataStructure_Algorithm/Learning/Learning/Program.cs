using System;

namespace Algorithm
{
    //List는 내부적으로 Array를 활용합니다.
    //공간이 꽉차면 여유 공간을 확보하기 위해 추가 공간을 확장할 때 기존 공간보다 2배를 확장시켜 여유 공간을 확보해둡닌다.
    //때문에 대용량의 Add를 호출하면 메모리 공간 낭비가 발생 할 수 있기 때문에 명확한 List의 수를 알 수 있으면
    //Capacity나 리스트 선언과 동시에 공간을 미리 지정하는 방법이 효율적입니다.


    //리스트의 공간 정책은
    //내부 구조는 배열이기 때문에 현재 허용치를 넘으면 현재 크기의 1.5~2배로 크기를 늘랴 공간을 확보한다
    //데이터가 많아 질수럭 배수의 배수의 크기를 확보하기 때문에 공간 재할당을 하는 횟수가 오히려 줄어든다


    class MyList<T> //List는 Generic 내부는 배열이다
    {
        const int DEFAULTSize = 1;
        T[] _data = new T[DEFAULTSize];

        public int Count = 0; //실제로 사용중인 데이터 갯수
        public int Capacity { get { return _data.Length; } }//예약된 데이터 개수


        //시간 복잡도 O(N)으로 생각 할 수 있으나 if문이 부분의 예외 케이스가 있기 때문에 O(1)로 본다

        public void Add(T item)
        {
            //1. 공간이 충분히 남아 있는지확인한다
            //2. 공간이 충분하지 않으면 늘린다
            if (Count >= Capacity)
            {
                //공간을 늘리는 정책은 현재 갯수의 1.5배에서 2배 사이로 늘린다
                //공간을 확보한 후 데이터들을 옮기고 기존의 배열과 치환한다

                T[] newArray = new T[Count * 2];

                for (int i = Count; i < Count; ++i)
                {
                    newArray[i] = _data[i];
                }

                _data = newArray;
            }

            //3. 공간에 데이터를 넣어준다
            _data[Count] = item;
            Count++;
        }

        //시간 복잡도 O(1)
        public T this[int index]
        {
            get { return _data[index]; }
            set { _data[index] = value; }
        }

        //시간 복잡도 데이터 크기에 비례한 루프를 돌고 있다
        //O(1)이 될 수 있으나 최악의 경우를 고려해서 표기한다 : O(N)
        public void RemoveAt(int index)
        {
            //공간이 빌것이기 때문에 한칸씩 땡긴다
            for(int i = index; i < Count - 1; ++i)
            {
                _data[i] = _data[i + 1];
            }


            Count--;
        }
    }


    //연결리스트 : 중간 접근을 지원하지 않는다. 
    

    class Node<T>
    {
        public T Data; //현재 노드의 
        public Node<T> Next; //다음 노드 주소
        public Node<T> Prev; //이전 노드 주소
    }

    class MyLinkedList<T>
    {
        //Linked List는 맨처음과 맨끝을 알고있다
        public Node<T> Head = null;
        public Node<T> Tail = null;
        public int Count = 0;

        //시간 복잡도 : O(1)
        //맨끝에 방을 추가한Node
        public Node<T> AddLast(T data)
        {
            Node<T> newNode = new Node<T>();
            newNode.Data = data;

            //맨앞과 맨끝을 업데이트를 해준다
            //1. 처음 생성하는것이면 새로 추가한 노드가 첫번째 노드가 됩니다
            if (Head == null)
            {
                Head = newNode;
            }

            //기존에 마지막 노드가 있는지 체크를 한다
            //1. Tail이 존재한다면 새로 추가 된 존재와 링크를 연결한 다음 Tail을 교체하는 작업을한다
            if (Tail != null)
            {
                Tail.Next = newNode;
                newNode.Prev = Tail;
            }

            //2. 새로 추가 되는 노드를 마지막 노드로 인정한다
            Tail = newNode;
            Count++;

            return newNode;
        }

        //시간 복잡도 : O(1)
        //따로 값을 받는것이 아니라 노드를 받는다
        //MyLinkedList에 포함되는 노드라고 가정을한/
        public void Remove(Node<T> node)
        {
            //1. 방을 날린다는거는 없앨 노드의 앞의 노드와 뒤의 노드간에 이어주면
            //맨첨을 지우는 경우 예외를 해준다
            //기존 Head를 Head의 다음 값으로 바꾼다  
            if(Head == node)
                Head = Head.Next;


            //2. 가장 마지막을 없앨 경우 Tail에 기존 Tail 이전 값으로 바꾼다 
            if (Tail == node)
                Tail = Tail.Prev;


            //3. 중간의 값을 지울 떄 지우려는 node의 이전값이 null이 아니라ㅑ Next에 지우려는 node의 Next를 연
            if (node.Prev != null)
                node.Prev.Next = node.Next;

            if (node.Next != null)
                node.Next.Prev = node.Prev;

            Count--;
        }
    }
}
