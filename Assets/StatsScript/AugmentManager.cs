using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.HableCurve;

/* 클래스 이름 : AugmentManager
 * 클래스 기능 : 인게임에서의 증강 관련 데이터, 함수 관리
 * 필드 :     augmentSO   AugmentSO 형식으로 전체 증강의 리스트를 저장
 *          augmentBuffer       현재 조건을 만족하여 사용할 수 있는 증강의 리스트
 *          availAugmentList    augmentBuffer 에서 가중치 뽑기를 통해 뽑은 증강 3개를 저장하는 리스트
 *          selAugment          선택된 증강을 저장하는 변수
 *          totalWeight         augmentBuffer 내의 증강들의 총 가중치 합을 구하는 변수
 *          
 * 매서드 : UpdateAugBuffer    해당 라운드에서 사용가능한 (조건을 충족한) 증강 리스트(augmentBuffer)를 업데이트 하는 함수
 *          GetTotalWeight     가중치 뽑기를 위한 총 가중치를 구하여 totalWeight 변수에 저장하는 함수
 *          GetThreeAugment    가중치를 적용하여 augmentBuffer 내에서 3개의 증강을 뽑아 availAugmentList 에 저장
 *          SelectAugment      주어진 3개의 증강에서 선택된 증강을 selAugment 변수에 저장
 */
public class AugmentManager : MonoBehaviour
{
    [SerializeField]
    AugmentSO augmentSO;

    public List<Augment> augmentBuffer = new List<Augment>();

    public List<Augment> availAugmentList = new List<Augment>();

    public Augment selAugment = new Augment();

    int totalWeight = 0;

    /* 함수 이름 : UpdateAugBuffer
     * 함수 기능 : 해당 라운드에서 사용가능한 (조건을 충족한) 증강 리스트(augmentBuffer)를 업데이트 하는 함수
     * 함수 파라미터 : int pol, 플레이어의 정치력을 입력받아 증강의 조건과 비교
     * 반환값 없음
     */
    public void UpdateAugBuffer(int pol)
    {
        // 모든 증강에 대해 비교를 실행
        foreach (Augment augment in augmentSO.augmentList)
        {            
            // 인자로 전달받은 정치력이 증강의 조건보다 높고 augmentBuffer에 없다면 Add
            if (pol >= augment.augPolCond && !(augmentBuffer.Contains(augment)))    // 조건을 만족하지만 augmentBuffer에 없음
            {
                augmentBuffer.Add(augment);
            }            
            // 인자로 전달받은 정치력이 증강의 조건보다 낮고 augmentBuffer에 있다면 Remove
            else if (pol < augment.augPolCond && augmentBuffer.Contains(augment))   // 조건을 만족 못하지만 augmentBuffer에 있음
            {
                augmentBuffer.Remove(augment);
            }

        }
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

        // augmentBuffer에 있는 모든 증강의 가중치를 더하여 totalWeight에 저장
        foreach (Augment augment in augmentBuffer)
        {
            totalWeight += augment.augWeight;
        } 
    }

    /* 함수 이름 : GetThreeAugment
     * 함수 기능 : 가중치를 적용하여 augmentBuffer 내에서 3개의 증강을 뽑아 availAugmentList 에 저장
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void GetThreeAugment()
    {
        float pivot;    // 뽑기를 위한 랜덤한 값
        float nowPivot; // 현재 pivot값, 현재 증강까지의 가중치들의 합

        // availAugmentList 초기화
        availAugmentList.Clear();

        // 3개의 증강을 저장할 때 까지 반복
        while (true)
        {
            pivot = Random.Range(0, totalWeight);   // 0부터 총 가중치 합 까지의 수 중 랜덤 값 지정
            nowPivot = 0;   // 현재 pivot 0으로 초기화

            // augmentBuffer 내의 모든 증강에 대해 실행
            foreach (Augment augment in augmentBuffer)
            {
                // 현재 증강의 가중치 값을 nowPivot에 더한다
                nowPivot += augment.augWeight;

                // nowPivot 값이 랜덤하게 정한 pivot 값보다 크거나 같고 현재 증강이 availAugmentList에 있다면 foreach문 중단
                if (pivot <= nowPivot && availAugmentList.Contains(augment))    // 가중치 뽑기로 나온 증강이 이미 availAugmentList에 있음
                {
                    break;
                }
                // nowPivot 값이 랜덤하게 정한 pivot 값보다 크거나 같고 현재 증강이 availAugmentList에 없다면 availAugmentList에 Add 하고 foreach문 중단
                else if (pivot <= nowPivot && !(availAugmentList.Contains(augment)))    // 가중치 뽑기로 나온 증강이 availAugmentList에 없음
                {
                    availAugmentList.Add(augment);
                    break;
                }
            }

            // 증강을 3개 뽑았으면 while문 중단
            if (availAugmentList.Count >= 3)
            {
                break;
            }

        }
    }

    /* 함수 이름 : SelectAugment
     * 함수 기능 : 선택된 증강의 index값을 인자로 받아 해당 증강을 selAugment에 저장
     * 함수 파라미터 : int index, 선택된 증강의 index을 전달받는다
     * 반환값 : 없음
     */
    public void SelectAugment(int index)
    {
        selAugment = availAugmentList[index];
    }

}
