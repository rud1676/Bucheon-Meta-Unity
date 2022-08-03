# LOGINSCENE

BUCHOEN 게임 씬 안에 

## area

지형과 잔디,게시판,가로등,쓰레기박스(?) 관련 모델

## around

아파트, 강 등 모델

## bridge

지형 중 다리 부분 모델

## Bunker

???

## Character

캐릭터 모델

## cityhall

부청시청 모델

## Images

UI 관련된 이미지들

## map

아스팔트, 밴치, 바닥타일 등

## model

??

## park

공원 관련된

## RandomTrash

trash폴더의 모델을 prefab과 관련 Script로 나눔

## Script

게임매니저, camera 등 스크립트

## sign

게임에 표시되는 안내 정보

## UI

UI관련된 스크립트, 프리펩스


## Scripts

정리중

## API

|요청값|성공/실패여부|반환값|
|---|---|---|
| 이메일,패스워드 | 성공 | 해당 이메일의 => 오늘의 미션, 부천시 클린 포인트, 닉네임, 시티패스 연동여부, 이메일, 이메일 계정유형, 날짜별 부천시 클릭 획득 포인트, 캐릭터 정보 |
|게임씬 들어가면(오늘날짜를 요청하는건지...?)||게시판 이미지|
| 적립포인트 랭킹 요청 | 성공 | 1~100위까지 => 순위, 닉네임, 부천시 클린 포인트 |

## 잘모르겠는것

이메일 주소 => 회원가입 코드 입력 프로세스

타임어택 미션 => 수시로 대기중이였다가 전송이 오면 타임어택 시작 (방법을 모르겟다)

## Bucheon Scene관련

TimerScript => Enable,Disable,viewTimer

- Enable로 시작시간 전달
- viewTimer로 초바뀔때마다 화면반영
- Disable로 종료