# LOGINSCENE

로그인 씬과 관련된 에셋

## Image

받은 디자인 파일과 예시 레이아웃이 저장되어있습니다.

## Scripts

- Manager: SingleTon패턴으로 짜여진 Popup과 View를 관리하는 Manager코드입니다.
- AbstractClass: 각각 View와 Popup에 기본적으로 달린 기능을 abstract class로 정의했습니다.
  - hide,show,Initialize 함수

- ViewScript: Scene에 올라간 각각 View에 대한 Script입니다.(EmailRegistView는 VIewManager의 typeString에 의존합니다.(기본값 google))
- Alert: Popup prefab에 달린 각각의 코드입니다.(StopAlertPopUp만 사용중)


## Prefabs

Resources - LOGINSCENE에 StopAlert를 팝업창으로 사용합니다.