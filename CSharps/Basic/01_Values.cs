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

    static void Main(string[] args)
    {
        ValueChange();
    }
}