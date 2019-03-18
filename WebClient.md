# WebClient 

웹서버에서 데이터를 가져오거나 웹 서버로 데이터를 보내기 위해 간단한 유틸리티 클래스이다.

1. 데이터를 가져오기 위해 여러 Download 메서드
2. 데이터를 보내기 위해 여러 upload 메서드
3. 데이터를 스트림 형태로 읽어오기 위한 Openread메서드
4. 데이터를 스트림 형태로 쓰기 위한 OpenWirte 메서드

## WebClient Download

웹서버에서 HTML이나 문자열을 다운로드 받위 위해서는 DownloadString()메서드를 사용하면 된다.

아레 예제는 구글 홈페이지 HTML을 동기적 방식으로 다운로드 받는 코드

```c#
string url = "http://www.google.com";

WebClient webClinet = new WebClient();
string html = webClinet.Downloadstring(url);
```



비동기적 방식으로 HTML이나 문자열을 다운로드 받기 위해서는 DownloadStringTastAsync()메서드를 사용하고 await하면 된다.

```c#
string url = "http://www.google.com";
WebClient webClinet = new WebClient();
string html = await webClient.DonloadStringTaskAsynce(url);

Console.WriteLine(html);
```



## WebClient Upload

WebClient로 문자열을 보내기 위해서는 UploadString(동기) 혹은 UploadStringTaskAsync(비동기) 메서드를 사용한다. 텍스트가 아닌 바이너리 데이터를 업로드하기 위해서는 UploadData(동기적) 혹은 UploadDataTaskAsync(비동기적) 메서드를 사용하며, 파일을 업로드하기 위해서는 UploadFile(동기적) 혹은 UploadFileTaskAsync(비동기적) 메서드를 사용한다. 





## HttpWebRequest활용

HttpWebRequest와 HttpWebResponse클래스를 사용

#### HttpWebrequest Get 예제

HttpWebRequest 객체는 WebRequest.Create() 메서드를 사용하여 생성한다.

이렇게 생성된 HttpWebRequest 객체에 GET, POST, PUT, DELETE, HEAD 등의 HTTP Verb를 Method 속성에 지정할 수 있으며,  Headers 컬렉션에 필요한 HTTP 헤더를 추가할 수 있고, 기타 다양한 HttpWebRequest 속성들을 설정할 수 있다. 

