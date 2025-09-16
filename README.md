# ArkRebirth Template

Ark:Rebirth의 개발 구상 단계에서 작성한 프로젝트의 템플릿입니다.

이 템플릿은 다음의 활용법을 검증 및 공유하기 위한 목적으로 작성 되었습니다.
* 의존성 주입을 활용한 프로젝트 구조
* Rx를 통한 이벤트 처리
* Adressable을 이용한 에셋 관리 및 비동기 처리
* Goal-Oriented Action Planning를 통한 AI 처리
* NavMesh 기반의 이동 처리
* Animator 기반의 애니메이션 처리
* ScriptableObject를 활용한 게임 데이터 처리

## 실행 방법 및 조작법
Scenes 폴더의 Game.unity 씬 활성화 > 플레이 모드 실행

이동: 마우스 좌 클릭

카메라 시점 조절: 마우스 우 클릭 + 드래그

줌 인/아웃: 마우스 휠

Wandering Mode 전환: 화면상의 Wandering Mode 토글 버튼 클릭 (좌 클릭 이동 시 자동 해제)

## 프로젝트 요약

### 의존성 주입
* GameLifetimeScope.cs : 의존성 주입의 시작점으로 게임 전체의 의존성 세팅
* GameUILifetimeScope.cs : 게임의 UI 처리에 필요한 의존성 세팅

### Rx를 활용한 UI 이벤트 처리
* GameUIPresenter.cs : Wandering Mode 토글 처리 및 ReactiveProperty를 통한 Hp바 갱신 처리

### Adressable을 이용한 에셋 관리 및 비동기 처리
* AvatarModelService.cs : 캐릭터의 에셋 로드

### Goal-Oriented Action Planning를 통한 AI 처리
Code/Avatar/GOAP 경로에 위치

* WanderGoal.cs : Wandering Mode의 목표 설정
* WanderSensor.cs : Wandering 위치 탐지 센서 캐릭터 주변의 NavMesh 상의 임의 위치를 참조

### NavMesh 기반의 이동 처리
* NavMeshMover.cs : NavMeshAgent를 기반으로 이동 및 이벤트 처리

### Animator 기반의 애니메이션 처리
* AvatarAnimator.cs : Animator 기반의 애니메이션 처리

### ScriptableObject를 활용한 게임 데이터 처리
Code/Setting 경로에 위치
* AvatarAnimationSettings.cs : 캐릭터 애니메이션에 필요한 데이터 처리
* LevelSettings.cs : 레벨에 관련된 데이터 처리

## 의존성
* [UniRx](https://github.com/neuecc/UniRx)
* [VContainer](https://github.com/hadashiA/VContainer)
* [GOAP for Unity](https://github.com/crashkonijn/GOAP)
