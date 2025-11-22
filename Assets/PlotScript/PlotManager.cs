using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

/* 클래스 이름 : PlotManager
 * 클래스 기능 : 인게임에서의 증강 관련 데이터, 함수 관리
 * 필드 :   plotSO              PlotSO 형식으로 전체 증강의 리스트를 저장
 *          plotBuffer          현재 조건을 만족하여 사용할 수 있는 증강의 리스트
 *          availPlotList       plotBuffer 에서 가중치 뽑기를 통해 뽑은 증강 3개를 저장하는 리스트
 *          selPlot             선택된 증강을 저장하는 변수
 *          totalWeight         plotBuffer 내의 증강들의 총 가중치 합을 구하는 변수
 *          
 * 매서드 : UpdatePlotBuffer    해당 라운드에서 사용가능한 (조건을 충족한) 증강 리스트(plotBuffer)를 업데이트 하는 함수
 *          GetTotalWeight      가중치 뽑기를 위한 총 가중치를 구하여 totalWeight 변수에 저장하는 함수
 *          GetThreePlot        가중치를 적용하여 plotBuffer 내에서 3개의 증강을 뽑아 availPlotList 에 저장
 *          SelectPlot          주어진 3개의 증강에서 선택된 증강을 selPlotment 변수에 저장
 */
public class PlotManager : MonoBehaviour
{
    [SerializeField]
    PlotSO plotSO;

    [SerializeField]
    PlotApplier plotApplier;

    public List<Augment> plotBuffer = new List<Augment>();

    public List<Augment> availPlotList = new List<Augment>();

    public Augment selectedPlot;
    public List<Augment> activePlot = new List<Augment>();

    int totalWeight = 0;

    /* 함수 이름 : UpdatePlotBuffer
     * 함수 기능 : 해당 라운드에서 사용가능한 (조건을 충족한) 공작 리스트(plotBuffer)를 업데이트 하는 함수
     * 함수 파라미터 : int pol, 플레이어의 정치력을 입력받아 공작의 조건과 비교
     * 반환값 없음
     */
    public void UpdatePlotBuffer(Character player)
    {
        // 모든 공작에 대해 비교를 실행
        foreach (Augment plot in plotSO.plotList)
        {            
            // 플레이어 스탯이 공작의 활성화 조건을 만족하고 plotBuffer에 없다면 Add
            if (CheckConditions(plot, player) && !(plotBuffer.Contains(plot)))    // 조건을 만족하지만 plotBuffer에 없음
            {
                plotBuffer.Add(plot);
            }
            // 플레이어 스탯이 공작의 활성화 조건을 만족하지 못하는데 plotBuffer에 있다면 Remove
            else if (!CheckConditions(plot, player) && plotBuffer.Contains(plot))   // 조건을 만족 못하지만 plotBuffer에 있음
            {
                plotBuffer.Remove(plot);
            }
        }
    }
    
    bool CheckConditions(Augment plot, Character player)
    {
        foreach (var cond in plot.plotCondition)
        {
            // 비교 대상 스탯 매핑
            float statValue = 0f;   // 플레이어의 비교 대상 스탯을 저장
            switch (cond.statType)
            {
                case "hp":
                    statValue = player.hp;
                    break;
                case "influence":
                    statValue = player.influence;
                    break;
                case "piety":
                    statValue = player.piety;
                    break;
            }

            // 비교 연산자에 따라 case를 나누어 조건을 만족하지 못하면 false를 반환
            switch (cond.compareType)
            {   // '플레이어 스탯' '비교연산자' '비교 대상 조건 값'
                case ">=":  // 조건이 '이상' 일 때
                    if (statValue < cond.value) return false;
                    break;

                case "<=":  // 조건이 '이하' 일 때
                    if (statValue > cond.value) return false;
                    break;

                case ">":   // 조건이 '초과' 일 때
                    if (statValue <= cond.value) return false;
                    break;

                case "<":   // 조건이 '미만' 일 때
                    if (statValue >= cond.value) return false;
                    break;
            }            
        }
        return true;    // 모든 조건을 통과하면 true 반환
    }


    /* 함수 이름 : GetTotalWeight
     * 함수 기능 : 가중치 뽑기를 위한 총 가중치를 구하여 totalWeight 변수에 저장하는 함수
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void GetTotalWeight()
    {
        // 총 가중치 값 0으로 초기화
        totalWeight = 0;

        // plotBuffer에 있는 모든 증강의 가중치를 더하여 totalWeight에 저장
        foreach (Augment plot in plotBuffer)
        {
            totalWeight += plot.plotWeight;
        }
    }

    /* 함수 이름 : GetThreePlot
     * 함수 기능 : 가중치를 적용하여 plotBuffer 내에서 3개의 공작을 뽑아 availPlotList 에 저장
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void GetThreePlot()
    {
        float pivot;    // 뽑기를 위한 랜덤한 값
        float nowPivot; // 현재 pivot값, 현재 증강까지의 가중치들의 합

        // availPlotList 초기화
        availPlotList.Clear();

        // 뽑히지 않은 증강들을 저장하는 리스트 remaining
        List<Augment> remaining = new List<Augment>(plotBuffer);

        for (int i = 0; i < 3; i++)
        {
            if (remaining.Count == 0)
            {
                break;
            }

            pivot = Random.Range(0, totalWeight);   // 0부터 총 가중치 합 까지의 수 중 랜덤 값 지정
            nowPivot = 0;   // 현재 pivot 0으로 초기화

            foreach (Augment plot in remaining)
            {
                // 현재 증강의 가중치 값을 nowPivot에 더한다
                nowPivot += plot.plotWeight;

                // nowPivot 값이 랜덤하게 정한 pivot 값보다 크거나 같고 현재 증강이 availPlotList에 없다면 availPlotList에 Add 하고 foreach문 중단
                if (pivot <= nowPivot && !(availPlotList.Contains(plot)))    // 가중치 뽑기로 나온 증강이 availPlotList에 없음
                {
                    availPlotList.Add(plot);
                    remaining.Remove(plot);
                    totalWeight -= plot.plotWeight;
                    break;
                }
            }
        }
    }

    /* 함수 이름 : SelectPlot
     * 함수 기능 : 선택된 공작의 index값을 인자로 받아 해당 증강을 selectedPlot에 저장
     * 함수 파라미터 : 선택된 공작의 index 값 index, 카디널들의 정보 cardinals
     * 반환값 : 사용할 수 있는지 여부 bool 값
     */
    public bool SelectPlot(int index, List<Character> cardinals)
    {
        // 선택된 공작을 selectedPlot에 저장
        selectedPlot = availPlotList[index];
        
        // 선택한 증강을 사용하기 위한 경건함이 충분할 때와 부족할 때
        if (CanSelectPlot(selectedPlot, cardinals[0]))
        {
            // 경건함이 충분할 때 공작의 비용만큼 플레이어의 경건함을 감소
            cardinals[0].DecreasePiety(availPlotList[index].pietyCost);
            
            // 공작의 트리거에 따라 로직 실행
            switch (selectedPlot.trigger)
            {
                case TriggerType.OnSelect:  // 트리거가 OnSelect이면 즉시 적용
                    plotApplier.ApplyPlot(selectedPlot, cardinals);
                    break;

                default:    // OnSelect이 아니면 activePlot에 저장
                    activePlot.Add(selectedPlot);
                    break;
            }

            // true 리턴
            return true;

        }
        else
        {
            // 경건함이 부족할 때 false 리턴
            return false;
        }
    }

    /* 함수 이름 : CanSelectPlot
     * 함수 기능 : 선택된 공작을 사용하기 위한 경건함이 충분한지 확인하는 함수
     * 함수 파라미터 : 선택된 공작 selectedPlot, player 정보 player
     * 반환값 : 사용할 수 있는지 여부 bool 값
     */
    bool CanSelectPlot(Augment selectedPlot, Character player)
    {
        if (player.piety - selectedPlot.pietyCost >= 0)
        {
            // 경건함이 충분하면 true를 리턴
            return true;
        }
        else
        {
            // 경건함이 부족하면 false를 리턴
            return false;
        }
    }

    /* 함수 이름 : CheckTrigger
     * 함수 기능 : 게임내의 특정 상황 발생 시 트리거를 받아 activePlot 에서 트리거가 일치하는 공작들을 적용하는 함수
     * 함수 파라미터 : 카디널들의 정보 cardinals, 게임 내에서 발생한 상황 정보 trigger
     * 반환값 : 없음
     */
    public void CheckTrigger(List<Character> cardinals, TriggerType trigger)
    {
        foreach (Augment plot in activePlot)
        if (plot.trigger == trigger)
        {
            plotApplier.ApplyPlot(plot, cardinals);
        }
    }

}
