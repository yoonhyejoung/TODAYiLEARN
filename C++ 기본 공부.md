# C++ 기본 공부

```c++
#include <iostream>
//iostream은 입출력을 위한 헤더 파일(stdio.h)와 같은 역할
using namespace std;

int main()
{
	//cout와 cin는 콘솔 입출력 명령으로 iostream헤더 파일에 std 네임스페이스 안에 존재한다.
	//따라서 #include <iostream>으로 헤더 파일을 포함시키고, using namespacee std;로 네임스페이스 사용을 선언해야 한다.

	//cout은 console output의 약자로 콘솔 출력 명령어이다.
	//<< 를 사용하여 다음에 오는 문자, 문자열, 숫자 등을 출력해준다.
	//<< 이후에 endl(end line)이 오면 줄이 끝났음을 말해주고 개행시켜준다.

	cout << "Hello World" << endl;

	//cin은 console input의 약자로 콘솔 입력 명령어 이다.
	//>>우꺽쇠를 사용하여 다음에 오는 변수(int, float, double, char, bool)에 입력되는 값을 대입해준다.
	//입력 버퍼를 사용하기 때문에 엔터키를 눌러야 입력이 마무리 된다.
	//해당 변수에 맞지 않는 값을 입력(예를 들어 int 변수에 문자열을 대입해준다든지)하면 이상한 값이 대입된다는 것을 주의해야 한다.

	

	cout << 'A' << 'B' << 'C';
	cout << endl; //endl로 개행
	cout << "문자열 입니다" << endl;


	//숫자 출력
	cout << 123 << endl;
	cout << 1 << 2 << 3 << endl;
	cout << "1 + 2 - 3 ==" << 1 + 2 - 3 << endl;
	cout << endl << endl << endl;

	int nInput;
	cout << "정수(int)값을 입력하시오 " << endl;
	cin >> nInput;

	float fInput;
	cout << "실수(float)값을 입력하시오 " << endl;
	cin >> fInput;

	cout << nInput << endl;
	cout << fInput << endl;

	cout << nInput + fInput << endl;
	system("pause");
	return 0;
}
```

