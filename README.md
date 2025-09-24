# ArkRebirth Template

![Image](https://github.com/user-attachments/assets/0b052b22-d746-4790-a9eb-c58bb76ba7c6)

Ark:Rebirth의 개발 구상 단계에서 작성한 프로젝트의 템플릿입니다.

이 템플릿은 다음의 활용법을 검증하기 위한 목적으로 작성 되었습니다.
* 의존성 주입을 활용한 프로젝트 구조
* Rx를 통한 이벤트 처리
* Addressables을 이용한 에셋 관리 및 비동기 처리
* Goal-Oriented Action Planning를 통한 AI 처리
* NavMesh 기반의 이동 처리
* Animator 기반의 애니메이션 처리
* ScriptableObject를 활용한 게임 데이터 처리
* Built-in Render Pipeline 기반 셰이더
* Netcode for GameObject 기반 이동 및 애니메이션 동기화 (_*새로 추가됨_) 

## 실행 방법 및 조작법
Scenes 폴더의 Lobby.unity 씬 활성화 > 플레이 모드 실행 > Start Host 버튼 클릭

_(*필요 시) Netcode for GameObject Client Test: 빌드 후 게임 실행 > 에디터로 플레이 모드 실행 후 Start Host 버튼 클릭 > 빌드 클라이언트의 Start Client 버튼 클릭_

이동: 마우스 좌 클릭

카메라 시점 조절: 마우스 우 클릭 + 드래그

줌 인/아웃: 마우스 휠

Wandering Mode 전환: 화면상의 Wandering Mode 토글 버튼 클릭 (좌 클릭 이동 시 자동 해제)

## 프로젝트 요약

### 의존성 주입
* [GameLifetimeScope.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/GameLifetimeScope.cs) : 의존성 주입의 시작점으로 게임 전체의 의존성 세팅
* [GameUILifetimeScope.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/GameUILifetimeScope.cs) : 게임의 UI 처리에 필요한 의존성 세팅

### Rx를 활용한 UI 이벤트 처리
* [GameUIPresenter.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/GameUIPresenter.cs) : Wandering Mode 토글 처리 및 ReactiveProperty를 통한 Hp바 갱신 처리

### Addressables을 이용한 에셋 관리 및 비동기 처리
* [AvatarModelService.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Avatar/AvatarModelService.cs) : 캐릭터의 모델 에셋 로드

### Goal-Oriented Action Planning를 통한 AI 처리
* [WanderAction.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Avatar/GOAP/WanderAction.cs) : Wandering 액션 진입 및 일정 시간 기다림 처리
* [WanderSensor.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Avatar/GOAP/WanderSensor.cs) : Wandering 위치 탐지 센서 캐릭터 주변의 NavMesh 상의 임의 위치를 참조

### NavMesh 기반의 이동 처리
* [NavMeshMover.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Avatar/NavMeshMover.cs) : NavMeshAgent를 기반으로 이동 및 이벤트 처리

### Animator 기반의 애니메이션 처리
* [AvatarAnimator.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Avatar/AvatarAnimator.cs) : Animator 기반의 애니메이션 처리

### ScriptableObject를 활용한 게임 데이터 처리
* [AvatarAnimationSettings.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Setting/AvatarAnimationSettings.cs) : 캐릭터 애니메이션에 필요한 데이터 처리
* [LevelSettings.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Setting/LevelSettings.cs) : 레벨에 관련된 데이터 처리

### Built-in Render Pipeline 기반 캐릭터 셰이더
* [Avatar.shader](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Shader/Avatar.shader) : 비실사 손맵 기반의 캐릭터 셰이더. 림 라이팅, 셀프 그림자(정점 G채널 이용한 가중치), Hair Gloss, Reflection Map 등의 기능 구현

### Netcode for GameObject
* [AvatarSpawner.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Avatar/AvatarSpawner.cs) : Host 및 Client 접속/종료 시 Avatar Spawn/Despawn 처리
* [NetworkAvatar.cs](https://github.com/Indigorilla/ArkRebirthTemplate/blob/main/Assets/Code/Avatar/NetworkAvatar.cs) : NetworkSpawn 이벤트 처리 및 애니메이션 상태 동기화 처리

## 의존성
* [UniRx](https://github.com/neuecc/UniRx)
* [VContainer](https://github.com/hadashiA/VContainer)
* [GOAP for Unity](https://github.com/crashkonijn/GOAP)
* [UniTask](https://github.com/Cysharp/UniTask)
