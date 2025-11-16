using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/* 클래스 이름 : GameManager
 * 클래스 기능 : 인게임에서 게임 진행을 관리
 * 필드 :       statsUIManager      StatsUIManager 스크립트를 참조하는 변수
 *              statsManager        StatsManager 스크립트를 참조하는 변수
 *              augmentManager      AugmentManager 스크립트를 참조하는 변수
 *              day                 몇번째 날 인지 저장하는 변수
 *              turn                몇번째 턴 인지 저장하는 변수
 *              limitTime           제한시간을 저장하는 변수
 *              remainTime                제한시간을 저장하는 변수
 *              
 * 매서드 :    NextTurn             투표가 끝나고 새로운 턴이 시작될 때 정보 초기화
 *             OnPrayAction         UI에서 Pray 버튼을 클릭했을 때 호출하여 statsManager의 Pray함수 실행
 *             OnSpeechAction       UI에서 Speech 버튼을 클릭했을 때 호출하여 statsManager의 Speech함수 실행
 *             OnSelectAugment      UI에서 세개의 증강 중 하나를 선택했을 때 선택된 증강의 index를 받아 선택된 증강을 저장하는 SelectAugment 함수 실행
 */
public class GameManager : MonoBehaviour
{
    public StatsUIManager statsUIManager;   // StatsUIManager 스크립트를 참조하는 변수

    public StatsManager statsManager;       // StatsManager 스크립트를 참조하는 변수
    public AugmentManager augmentManager;   // AugmentManager 스크립트를 참조하는 변수

    // 몇번째 날 인지, 몇번째 턴 인지 나타내는 변수
    int day = 1;
    int turn = 0;

    // 턴 당 제한시간
    float limitTime = 10f;  // 우선 10초로 설정
    float remainTime;       // 현재 남은 시간

    void Awake()
    {
        statsManager = GetComponent<StatsManager>();        // StatsManager 컴포넌트를 받아 저장
        augmentManager = GetComponent<AugmentManager>();    // AugmentManager 컴포넌트를 받아 저장
        statsManager.SetStats();    // SetStats 함수를 호출하여 처음 캐릭터 스탯 초기화
    }

    void Start()
    {
        NextTurn();     // 새로운 턴 정보 초기화
    }

    void Update()
    {
        remainTime -= Time.deltaTime;
        statsUIManager.SetDayTime($"{day}day {turn}turn\n{remainTime.ToString("0.0")}");

        statsUIManager.SetStatsInfo(statsManager.characters);

        if (remainTime <= 0f)
        {
            NextTurn();
        }
    }

    /* 함수 이름 : NextTurn
     * 함수 기능 : 투표가 끝나고 새로운 턴이 시작 될때 정보 초기화
     * 함수 파라미터 : 없음
     * 반환값 : 없음 
     */
    void NextTurn()
    {
        augmentManager.UpdateAugBuffer(statsManager.characters[0].pol); // 새로운 턴에서 사용 가능한 증강 업데이트
        augmentManager.GetTotalWeight();    // 이번 턴에 사용 가능한 증강들의 총 가중치 합 구하기
        augmentManager.GetThreeAugment();   // 가중치 뽑기를 통해 선택 창에 띄울 세가지 증강 저장

        statsUIManager.SetAugmentUI(augmentManager.availAugmentList);   // 세가지 증강을 UI 정보에 전달

        // 턴과 날짜 갱신
        // 하루 3개의 턴, turn이 4이상이면 day를 1 증가시키고 다시 turn을 1로 설정
        turn++;
        if (turn >= 4)
        {
            day++;
            turn = 1;
        }

        remainTime = limitTime;     // 제한시간 초기화
    }

    // UI에서 Pray 버튼을 클릭했을 때 호출하여 statsManager의 Pray함수 실행
    public void OnPrayAction()
    {
        statsManager.Pray();
    }

    // UI에서 Speech 버튼을 클릭했을 때 호출하여 statsManager의 Speech함수 실행
    public void OnSpeechAction()
    {
        statsManager.Speech();
    }

    // UI에서 세개의 증강 중 하나를 선택했을 때 선택된 증강의 index를 받아 선택된 증강을 저장하는 SelectAugment 함수 실행
    public void OnSelectAugment(int index)
    {
        augmentManager.SelectAugment(index);
        // augmentManager.ApplyAugment();       증강을 적용하는 함수 (예정)
    }

}
