# Win32 API Hooking(Dll Injection/API Hooking)



### DLL Injection과 API Hooking의 관계

일반적으로 API Hooking을 하기 전에 Hooking DLL을 Injection 하는 과정이 선행되어야 합니다.

### DLL Injection

##### 다른 프로세스에 특정 DLL 파일을 강제로 삽입 시키는것

다른 프로세스에게 LoadLibrary() API를 호출하도록 명령하여 내가 원하는 DLL을 loading 시키는 것입니다.

(LoadLibrary : Loads the specified module into the address space of the calling process.  )

LoadLibray함수 예제

```c#
using System;
using System.Runtime.InteropServices;

namespace ApiReference {
    class ApiExample {
        [DllImport("kernel32")]
        public static extern IntPtr LoadLibrary(String fileName);
        
        public static void Main(string[] args) {
        EnterFileName:
            Console.Write("불러올 파일 이름을 입력하세요: ");
            String fileName = Console.ReadLine().Trim();
            if ( String.IsNullOrEmpty(fileName) ) {
                Console.WriteLine("다시 입력하세요");
                goto EnterFileName;
            }
            
            IntPtr libHandle = LoadLibrary(fileName);
            if ( libHandle == IntPtr.Zero )
                Console.WriteLine("파일 불러오기 실패!");
            else
                Console.WriteLine("불러온 파일 핸들: 0x{0:X8}", libHandle.ToInt32());
                
            Console.ReadKey(true);
        }
    }
}

출처: https://slaner.tistory.com/50 [꿈꾸는 프로그래머]
```





따라서 DLL Injection이 일반적인 DLL loading과 다른점은 loading 대상이 되는 프로세스가 내 자신이냐, 아니면 다른 프로세스냐 하는 것입니다.

![1553055671253](C:\Users\Administrator\AppData\Roaming\Typora\typora-user-images\1553055671253.png)

위 그림은 notepad 프로세스에 myhack.dll 을 강제로 삽입 시킨 그림입니다.

notepad 프로세스에 로딩된 myhack.dll은 notepad 프로세스 메모리에 대한 (정당한?) 접근권한시 생겼기 때문에 사용자가 원하는 어떤 일이라도 수행할 수 있습니다.

(예를 들어 notepad에 통신 기능을 추가하여 메신저나 텍스트 웹 브라우저 등으로 바꿀수 있습니다.)



### DLL Injection 사용 목적

LoadLibrary() API를 이용해서 어떤 DLL을 로딩시키면 해당 DLL의 DLLMain() 함수가 실행됩니다.

(DLLMain() :   Dll이 처음 메모리에 로드되면 **DllMain**()이라고 하는 진입점(entry-point) 함수가 호출된다.)

```c++
BOOL APIENTRY DllMain( HINSTANCE hModule, DWORD dwReason, LPVOID lpReserved)
{
    switch(dwReason){
        case DLL_PROCESS_ATTACH:
		// DLL 이 프로세스의 주소 영역에 매핑됨
		// DLL 초기화 코드
		break;

		case DLL_THREAD_ATTACH:
		// 스레드가 생성됨
		break;

		case DLL_THREAD_DETACH:
		// 스레드가 종료됨
		break;

		case DLL_PROCESS_DETACH:
		// DLL 이 프로세스의 주소 영역에서 매핑이 해제됨
		// DLL 종료 처리 코드
		break;
		}
}
```

DLL Injection의 동작 원리는 외부에서 다른 프로세스로 하여금 LoadLibrary() API를 호출하도록 만드는 것이기 때문에 (일반적인 Dll Loading과 마찬가지로) 강제 삽입된 DLL의 DllMain()함수가 실행됩니다.

또한 삽입된 DLL은 해당 프로세스의 메모리에 대한 접근권한을 갖기 때문에 사용자가 원하는 다양한 일(버그 패치, 기능 추가, 기타)을 수행 할 수 있습니다.

##### -악성코드

정상적인 프로세스(winlogon.exe, servicese,exe, svchost,exe, explorer,exe, etc)등에 몰래 숨어 들어가 악의적인 행동을 합니다. 다른 악성파일 다운, 백도어 설치, 키로깅 등...

정상 프로세스 내부에서 벌어지는 일이기 때문에 알아차리기 어려움

##### -유해 프로그램, 사이트 차단 프로그램

DLL Injection 기법으로 정상 프로세스에 살짝 숨어들어가 들키지도 않고, 프로세스 강제 종료도 할 수 있습니다.(Windows 핵심 프로세스를 종료하면 시스템이 같이 종료되기 때문에 결국 '차단'하려는 목적을 이룰수 있습니다.)

##### -기능 개선 및 (버그) 패치

어떤 프로그램의 소스 코드가 없거나 수정이 여의치 않을 때 DLL Injection을 사용하여 전혀 새로운 기능을 추가하거나 문제가 있는 코드, 데이터를 수정할 수 있습니다.

##### -API Hooking

정상적인 API 호출을 중간에서 후킹하여 원하는 기능을 추가(혹은 기존 기능을 제거)하는 목적으로 사용됩니다.



### DLL Injection 구현 방법

##### -CreateRemoteThread() API를 이용하는 방법

Injection 시킬 myhack.dll 소스 코드

```c++
//myhack.cpp
#include "stdio.h"
#include "windows.h"

#pragma comment(lib, "urlmon.lib")

#define DEF_NAVER_ADDR ("http://www.naver.com/index.html")
#define DEF_INDEX_PATH ("C:\\index.html")

DWORD WINAPI ThreadProc(LPVOID lParam)
{
//urlmon.dll의 URLDownloadToFile()함수를 실행시켜서 네이버 초기화면(index.html)을 다운받습니다.
    URLDownloadToFile(NUll, DEF_NAVER_ADDR, DEF_INDEX_PATH, 0 , NULL);
    return 0;
}
//DLL 인젝션이 발생하면 DLL의 DllMain() 함수가 호출됩니다.
BOOL WINAPI DllMain(HINSTANCE hinstDLL, DWORD fdwReason, LPVOID lpvReserved)
{
    HANDLE hThread = NULL;
    switch(fdwReason)
    {
        case DLL_PROCESS_ATTACH :
            //DLL이 로딩 될때 스레드를 실행합니다.
            //별도의 스레드를 생성해서 호출합니다.
        	hTHread = CreateThread(NULL, 0, ThreadProc, NULL, 0 NULL);
        	CloseHandle(hThread);
        	break;
    }
    return true;
}
```

DLL 인젝션이 발생하면 DLL의 DllMain() 함수가 호출됩니다. 따라서 notepad.exe 프로세스에 myhack.dll이 Injection 되면 결국 URLDownloadToFile()함수가 실행됩니다.



myhack.dll을 notepad.exe 프로세스에 Injection 시켜줄 프로그램(InjectDLL.exe)의 소스코드입니다.

```c++
//InjectDll.exe

#include "stdio.h"
#include "windows.h"
#include "tlhep32.h"

#define DEF_PROC_NAME ("notepad.exe")
#define DEF_DLL_PATH ("c:\\myhack.dll")

DWORD FindProcessID(LPCTSTR szProcessName);
BOOL InjectDLL(DWORD dwPID, LPCTSTR szDllName);

int main(int argc, char* argv[])
{
    DWORD dwPID = 0xFFFFFFFF;
    
    //find process
    dwPID = FindProcessID(DEF_PROC_NAME);
    if (dwPID == 0xFFFFFFFFFF)
    {
        princf("there is no <%s> process!\n", DEF_PROC_NAME)
        return 1;
    }
    
    //inject dll
    InjectDll(dwPIE, DEF_DLL_PATH);
    
    return 0;
}

DWORD FindProcessID(LPCTSTR szProcessNAme)
{
    DWORD dwPID = 0xFFFFFFFF;
    HANDEL hSnapShot = INVALID_HANDLE_VALUE;
    PROCESSENTRY32 pe;
    
    //Get the snapshot of the system
    pe.dwSize = sizeof(PROCESSENTRY32);
    hSnapShot = CreateToolhelp32Snapshot(TH32CS_SNAPALL, NULL);
    
    //find process
    Process32First(hSnapShot, &pe);
    do
    {
        if(!_stricmp(szProcessName, pe.szExeFile))
        {
            dwPid = pe.th32ProcessID;
            break;
        }
    }
    while(Process32Next(hSnapShot, &pe));
    
    CloseHandle(hSnapShot);
    
    return dwPID;
}

BOOL InjectDll(DWORD dwPID, LPCTSTR szDllName)
{
    HANDLE hprocess, hThread;
    HMODULE hMod;
    
}
```





















