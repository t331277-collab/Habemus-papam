using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/* 클래스 이름 : Augment
 * 클래스 기능 : 증강 정보 구조를 정의하는 클래스
 * 메서드 : Equals(object obj)
 *          기능 : Augment 클래스 간 Equals 비교를 할 때 단순 값 비교가 아닌 Augment 클래스 내의 이름(아이디)를 비교하도록 override
 *          파라미터 : object obj 오브젝트를 인자로 받음
 *          반환값 : 두 증강의 이름(아이디)이 같으면 true, 다르면 false 
 */
[System.Serializable]
public class Augment
{
    // 증강 데이터 변수
    public string augName;
    public string augDesc;
    public TriggerType augType;
    public TargetType augTarget;
    public int augPolCond;

    public int hpAugChange;
    public int polAugChange;
    public int pietyAugChange;

    public int augWeight;

    /* 함수 이름 : Equals
     * 함수 기능 : 기존 오브젝트 내 메서드인 Equals 를 Augment에 적용하기 위해 증강의 이름(아이디)를 비교하도록 override
     * 파라미터 : 비교 대상 오브젝트
     * 반환값 : 두 증강의 이름(아이디)이 같으면 true, 다르면 false 
     */
    public override bool Equals(object obj)
    {
        if (obj is Augment other)   // 입력받은 인자가 Augment클래스이면 other에 대입
            return this.augName == other.augName;   // 두 증강의 이름(아이디)이 같으면 true, 다르면 false
        return false;   // 입력받은 인자가 Augment가 아니라면 false 반환
    }
}

// 증강의 발동 시기
public enum TriggerType
{
    OnSelect,           // 증강을 선택하는 순간
    OnPray,             // 기도 행동 시
    OnSpeech,           // 연설 행동 시
    OnTurnStart,        // 턴 시작 시
    OnDeathPrevent      // 사망 직전
}

// 증강의 타겟
public enum TargetType
{
    Self,           // 자신
    AllOppo,        // 모든 경쟁자
    SelectedOppo,   // 플레이어가 선택한 경쟁자
    LowestStatOppo, // 특정 스탯이 가장 낮은 경쟁자
    HighestStatOppo // 특정 스탯이 가장 높은 경쟁자
}



[CreateAssetMenu(fileName = "AugmentSO", menuName = "Scriptable Objects/AugmentSO")]
public class AugmentSO : ScriptableObject
{
    // 증강 리스트
    public List<Augment> augmentList = new List<Augment>();
}
