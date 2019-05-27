# GIT에서 강제로 PUSH하기

  실제 에러가 발생하는 부분을 고칠수도 있지만 임시 방편으로 "+" 를 이용하여 해결이 가능합니다.

아래와 같이 강제로 push 를 진행하게 되면 에러 상관없이 강제로 Push 하게 되어 이슈를 넘어갈 수 있습니다.
**[물론 임시 방편입니다.]**

기존명령:  user #> git push -u origin master
강제명령:  user #> git push -u origin +master

강제적으로 push 가 됨을 확인할 수 있다.   