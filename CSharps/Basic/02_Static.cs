using System;

//Static 키워드
//static으로 선언된 변수나 함수는 해당 class에 종속되기 때문에 유일성이 보장됩니다.
//class내 static 변수는 각 객체에 종속적인게 아니기 때문에 
//여러 객체를 만들어도 하나의 static 변수를 공유하여 사용합니다.

//Guy라는 간단한 클래스입니다.
//age는 일반 변수이기 때문에 생성 되는 Guy 객체마다 각각 다른값을 가질 수 있습니다.
//반면에, Sex는 static 변수입니다. 생성 되는 Guy 객체들이 공유하는 값으로 
//하나의 클레스가 공통으로 갖는 고윳값을 사용하면 좋습니다.
class Guy
{
    int age;
    static string Sex;

    //함수에도 static 키워드를 사용 할 수 있는데
    //해당 함수도 class에 유일성이 보장됩니다.
    //때문에 static함수 내에서는 static 변수만 변경이 가능합니다.
    //일반 변수를 바꾸려 들면 어떤 객체의 변수 값을 변경 할 지 알 수 없기 때문에.

    //물로 이와 같은 방식으로 (깊은 복사를 만들어내는) 값을 바꿀 수도 있습니다.
    static Guy ChangeValue()
    {
        Guy g = new Guy();
        g.age = 100;
        return g;
    }
}
