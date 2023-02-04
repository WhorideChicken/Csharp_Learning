using System;

struct Animal
{
    public int age;
}

class Man
{
    public int age;

    //Reference형의 깊은 복사를 위한 함수
    public Man Clone()
    {
        Man m = new Man();
        m.age = this.age;
        return m;
    }
}

//Value and Reference
//Struct는 Value, Class는 Reference 데이터 타입입니다.
//Animal은 깊은복사, Man은 얕은 복사가 일어납니다.

class MainClass
{
    private static void ValueChange()
    {
        //Animal은 값형식의 자료형이기 때문에 깊은 복사가 발생하여
        //_a2 = _a1일 경우 _a2에는 새로운 객체가 할당 되었기 때문에 
        //_a1과 _a2는 각각 할당한 age값이 출력 됩니다.
        Animal _a1 = new Animal();
        _a1.age = 10;

        Animal _a2 = _a1;
        _a2.age = 20;

        Console.WriteLine("Value Chabge "+ _a1.age +",   " + _a2.age);

        //Animal과 달리 Man은 Reference 타입이기 때문에 얕은 복사가 발생하여
        //_m2 = _m1 부분에서 _m2는 _m1을 가르키게 되며 _m2의 값을 변경하면 _m1의 값이 변경 됩니다.
        Man _m1 = new Man();
        _m1.age = 30;
        
        Man _m2 = _m1;
        _m2.age = 20;

        //class와 같은 Reference 타입을 깊은 복사를 하고 싶으면 Man class에 구현한
        //새로운 객체를 반환하는 Clone 함수를 활용합니다.
        //결과는 _m2의 값을 바꾸면 _m1의 값이 바뀌지만 _m3의 값을 바꾸면 _m2의 값이 바뀌지 않습니다.
        Man _m3 = _m2.Clone(); 
        _m3.age = 50;

        Console.WriteLine("Reference Chabge "+ _m1.age +",   " + _m2.age+",    "+_m3.age);

    }

    //메모리 측면에서의 내용을 덧붙이겠습니다.
    //프로그램이 실행 되면 OS는 프로그램을 실행 할 수 있는 메모리를 할당합니다.
    // 얕은 복사와 깊은 복사를 설명하려면 메모리의 Stack과 Heap에 대한 이해가 필요합니다.

    //Stack에는 잠깐만 필요한 메모리로써 임시적으로 사용하는 메모리입니다.
    //어떤 함수를 호출 할 때 함수 안에 존재하는 로컬 변수는 함수 내에서 연산이 일어난 후 함수가 끝나면 사라집니다.
    //이런한 상황에서 Stack이 사용됩니다.

    //Heap 메모리에 할당을 하면 해체하기 전까지 계속 Heap영역에 쌓아둡니다.
    //cpp의 경우 개발자가 수동으로 Heap영역의 메모리를 해체해야하지만 C#의 경우 GC가 알아서 메모리를 해제해줍니다.
    //위의 코드에서 Animal은 복사 타입이고 Man은 참조 타입입니다.
    //복사 타입은 선언 시 크기 만큼 stack에 쌓입니다.
    //반면 참조 타입은 해당 값의 주소가 stack에 쌓이며 본래의 값은 Heap에 쌓입니다.
    //좀 더 설명을 덧붙이자면 new를 통해 객체를 만들면 객체 본체는 Heap에 쌓이고 해당 값을 참조 할 수 있도록
    //Heap 메모리내의 주소 값을 Stack에 쌓습니다.
    //때문에 _m2 = _m1이라 했을 때 _m2에는 _m1의 본체를 가르키는 주소가 할당이 되어 값이 같이 변경 됩니다.

    //C#에서 값타입
    //참조형 : class, array, string
    //값형 : 내장타입(int, long, char ~), struct

    static void Main(string[] args)
    {
        ValueChange();
    }
}