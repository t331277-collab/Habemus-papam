using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

// 공작의 발동 시기
public enum TriggerType
{
    OnSelect,           // 공작을 선택하는 순간
    OnPray,             // 기도 행동 시
    OnSpeech,           // 연설 행동 시
    OnTurnStart,        // 턴 시작 시
    OnDeathPrevent      // 사망 직전
}

// 공작의 타겟
public enum TargetType
{
    Self,               // 자신
    AllCardinal,        // 모든 경쟁자
    SelectedCardinal,   // 플레이어가 선택한 경쟁자
    LowestStatCardinal, // 특정 스탯이 가장 낮은 경쟁자
    HighestStatCardinal // 특정 스탯이 가장 높은 경쟁자
}

// 변화 타입
public enum ChangeType
{
    Increase,           // 증가
    Decrease,           // 감소
    Multiply            // 배수
}

// 공작 조건 클래스
[System.Serializable]
public class PlotCondition
{
    public string statType;        // 조건 타입 (hp, influence, piety)
    public string compareType;     // compare 종류 (이상, 이하 등)
    public float value;
}

/* 클래스 이름 : Plot
 * 클래스 기능 : 공작 정보 구조를 정의하는 클래스
 * 메서드 : Equals(object obj)
 *          기능 : Plot 클래스 간 Equals 비교를 할 때 단순 값 비교가 아닌 Plot 클래스 내의 이름(아이디)를 비교하도록 override
 *          파라미터 : object obj 오브젝트를 인자로 받음
 *          반환값 : 두 공작의 이름(아이디)이 같으면 true, 다르면 false 
 */
[System.Serializable]
public class Augment
{
    // 공작 데이터 변수
    public string plotID;       // 공작 아이디
    public string plotName;     // 공작 이름
    public string plotDesc;     // 공작 설명
    public string plotEffect;   // 공작 효과

    public List<PlotCondition> plotCondition;   // 공작 조건 리스트

    public TriggerType trigger;     // 공작 발동 트리거

    public TargetType plotTarget;   // 공작 적용할 타겟
    public string targetStat;       // 타겟을 정하는 기준 스탯 (어떤 스탯이 최소 최대인지 hp, influence, piety)

    public int pietyCost;       // 공작 비용

    public ChangeType hpChangeType;         // 체력 변화 타입 (증가, 감소, Multiply)
    public int hpChange;                    // 체력 변화량
    public ChangeType influenceChangeType;  // 정치력 변화 타입 (증가, 감소, Multiply)
    public int influenceChange;             // 정치력 변화량
    public ChangeType pietyChangeType;      // 경건함 변화 타입 (증가, 감소, Multiply)
    public int pietyChange;                 // 경건함 변화량

    public int plotWeight;      // 공작 가중치

    /* 함수 이름 : Equals
     * 함수 기능 : 기존 오브젝트 내 메서드인 Equals 를 Plot 에 적용하기 위해 공작의 아이디를 비교하도록 override
     * 파라미터 : 비교 대상 오브젝트
     * 반환값 : 두 공작의 아이디가 같으면 true, 다르면 false 
     */
    public override bool Equals(object obj)
    {
        if (obj is Augment other)   // 입력받은 인자가 Plot 클래스이면 other에 대입
            return this.plotID == other.plotID;   // 두 공작의 이름(아이디)이 같으면 true, 다르면 false
        return false;   // 입력받은 인자가 Plot 이 아니라면 false 반환
    }
}


[CreateAssetMenu(fileName = "PlotSO", menuName = "Scriptable Objects/PlotSO")]
public class PlotSO : ScriptableObject
{
    // 공작 리스트
    public List<Augment> plotList = new List<Augment>();
}
