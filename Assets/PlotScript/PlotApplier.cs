using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 클래스 이름 : PlotApplier
 * 클래스 기능 : 인게임에서의 공작 적용 관련 함수 관리
 * 필드 :   target        공작이 적용될 타겟들을 저장하는 리스트
 *          
 * 메소드 : ApplyPlot              공작을 적용하는 함수
 *          GetTarget               증강이 적용되는 타겟을 정하는 함수
 *          GetLowestCardinal       특정 능력치가 가장 낮은 카디널을 반환하는 함수
 *          GetHighestCardinal      특정 능력치가 가장 높은 카디널을 반환하는 함수
 */
public class PlotApplier : MonoBehaviour
{
    List<Character> targets; // 공작을 적용할 카디널을 저장하는 리스트

    /* 함수 이름 : ApplyPlot
     * 함수 기능 : 최종적으로 공작을 적용하는 함수
     * 파라미터 : 적용할 공작 appliedPlot, 카디널들 정보 cardinals
     * 반환값 : 없음
     */
    public void ApplyPlot(Augment appliedPlot, List<Character> cardinals)
    {
        //GetTarget 함수를 실행하여 적용하려는 공작의 타겟 리스트 할당
        targets = GetTarget(appliedPlot, cardinals);

        // 각 타겟들에게 증강 적용
        foreach (Character c in targets)
        {
            // 체력 변화량 적용
            switch (appliedPlot.hpChangeType)
            {
                // 공작의 능력치 변화 타입에 따라 적용
                case ChangeType.Increase:
                    c.IncreaseHp(appliedPlot.hpChange);
                    break;
                case ChangeType.Decrease:
                    c.DecreaseHp(appliedPlot.hpChange);
                    break;
                case ChangeType.Multiply:
                    // 배수로 변화 시키는 로직 추가 예정
                    break;
                default:
                    break;
            }

            // 정치력 변화량 적용
            switch (appliedPlot.influenceChangeType)
            {
                // 공작의 능력치 변화 타입에 따라 적용
                case ChangeType.Increase:
                    c.IncreaseInfluence(appliedPlot.influenceChange);
                    break;
                case ChangeType.Decrease:
                    c.DecreaseInfluence(appliedPlot.influenceChange);
                    break;
                case ChangeType.Multiply:
                    // 배수로 변화 시키는 로직 추가 예정
                    break;
                default:
                    break;
            }

            // 경건함 변화량 적용
            switch (appliedPlot.pietyChangeType)
            {
                // 공작의 능력치 변화 타입에 따라 적용
                case ChangeType.Increase:
                    c.IncreasePiety(appliedPlot.pietyChange);
                    break;
                case ChangeType.Decrease:
                    c.DecreasePiety(appliedPlot.pietyChange);
                    break;
                case ChangeType.Multiply:
                    // 배수로 변화 시키는 로직 추가 예정
                    break;
                default:
                    break;
            }
        }
    }

    /* 함수 이름 : GetTarget
     * 함수 기능 : 공작의 타겟을 확인하고 공작을 적용할 카디널의 리스트를 반환
     * 파라미터 : 적용할 공작 appliedPlot, 카디널들 정보 cardinals
     * 반환값 : 공작을 적용할 카디널 리스트 targets
     */
    List<Character> GetTarget(Augment appliedPlot, List<Character> cardinals)
    {
        // 공작이 적용될 카디널들을 저장하는 리스트
        List<Character> targets = new List<Character>();

        // 공작의 타겟 타입에 따라 공작이 적용될 카디널을 targets에 추가
        switch (appliedPlot.plotTarget)
        {
            case TargetType.Self:           // player를 targets에 추가
                targets.Add(cardinals[0]);
                break;

            case TargetType.AllCardinal:    // cardinals의 모든 카다널을 targets에 추가
                foreach (Character c in cardinals)
                {
                    targets.Add(c);
                }
                break;

            case TargetType.SelectedCardinal:   // 선택한 cardinal을 targets에 추가
                // 카디널 선택 로직 추가 예정
                break;

            case TargetType.LowestStatCardinal: // 특정 스탯이 가장 낮은 카디널을 targets에 추가
                targets.Add(GetLowestCardinal(appliedPlot, cardinals));
                break;

            case TargetType.HighestStatCardinal: // 특정 스탯이 가장 높은 가디널을 targets에 추가
                targets.Add(GetHighestCardinal(appliedPlot, cardinals));
                break;
        }

        // targets 리턴
        return targets;
    }

    /* 함수 이름 : GetLowestCardinal
     * 함수 기능 : 특정 스탯이 가장 낮은 카디널을 반환
     * 파라미터 : 적용할 공작 appliedPlot, 카디널들 정보 cardinals
     * 반환값 : 공작을 적용할 카디널 리스트 targets
     */
    Character GetLowestCardinal(Augment appliedPlot, List<Character> cardinals)
    {
        Character lowest = cardinals[1];

        switch (appliedPlot.targetStat)
        {
            case "hp":
                for (int i = 1; i < cardinals.Count; i++)
                {
                    if (cardinals[i].hp < lowest.hp)
                    {
                        lowest = cardinals[i];
                    }
                }
                break;

            case "influence":
                for (int i = 1; i < cardinals.Count; i++)
                {
                    if (cardinals[i].influence < lowest.influence)
                    {
                        lowest = cardinals[i];
                    }
                }
                break;

            case "piety":
                for (int i = 1; i < cardinals.Count; i++)
                {
                    if (cardinals[i].piety < lowest.piety)
                    {
                        lowest = cardinals[i];
                    }
                }
                break;
        }

        return lowest;
    }

    /* 함수 이름 : GetHighestCardinal
     * 함수 기능 : 특정 스탯이 가장 높은 카디널을 반환
     * 파라미터 : 적용할 공작 appliedPlot, 카디널들 정보 cardinals
     * 반환값 : 공작을 적용할 카디널 리스트 targets
     */
    Character GetHighestCardinal(Augment appliedPlot, List<Character> cardinals)
    {
        Character highest = cardinals[1];

        switch (appliedPlot.targetStat)
        {
            case "hp":
                for (int i = 1; i < cardinals.Count; i++)
                {
                    if (cardinals[i].hp > highest.hp)
                    {
                        highest = cardinals[i];
                    }
                }
                break;

            case "influence":
                for (int i = 1; i < cardinals.Count; i++)
                {
                    if (cardinals[i].influence > highest.influence)
                    {
                        highest = cardinals[i];
                    }
                }
                break;

            case "piety":
                for (int i = 1; i < cardinals.Count; i++)
                {
                    if (cardinals[i].piety > highest.piety)
                    {
                        highest = cardinals[i];
                    }
                }
                break;
        }

        return highest;
    }
}
