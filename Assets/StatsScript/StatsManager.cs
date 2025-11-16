using System.Collections.Generic;
using UnityEngine;

// 캐릭터 능력치 클래스
public class Character
{
    public int piety;   // 경건함
    public int pol;     // 정치력
    public float hp;    // 체력

    // 입력 받은 값으로 능력치 초기화
    public Character(int piety, int pol, float hp)
    {
        this.piety = piety;
        this.pol = pol;
        this.hp = hp;
    }
}

public class StatsManager : MonoBehaviour
{
    // 후보자들의 능력치를 저장하는 리스트
    public List<Character> characters = new List<Character>();

    // 기도 관련 변수
    public int pietyPrayChange; // 기도로 인한 경건함 변화량
    public int hpPrayChange;    // 기도로 인한 체력 변화량
    public int hpPrayRate;      // 기도로 인한 체력 회복 확률

    // 연설 관련 변수
    public int polSpeechChange; // 연설로 인한 정치력 변화량
    public int polSpeechRate;   // 연설로 인한 정치력 변화 확률


    /* 함수 이름 : SetStats()
     * 함수 기능 : 각 캐릭터들의 능력치를 초기화 한다. 경건함과 정치력은 지정한 범위 내의 랜덤한 값, 체력은 100 으로 초기화
     *            기도와 연설로 인한 능력치 변화량을 초기화
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void SetStats()
    {
        // 0번 인덱스 플레이서 능력치
        characters.Add(new Character(Random.Range(25, 35), Random.Range(25, 35), 100f));
        // 1~3번 인덱스 상대 후보 능력치
        characters.Add(new Character(Random.Range(25, 35), Random.Range(25, 35), 100f));
        characters.Add(new Character(Random.Range(25, 35), Random.Range(25, 35), 100f));
        characters.Add(new Character(Random.Range(25, 35), Random.Range(25, 35), 100f));

        // 기도 관련 변수 초기화
        pietyPrayChange = 10;   // 기도로 인한 경건함 변화량
        hpPrayChange = 10;      // 기도로 인한 체력 회복량
        hpPrayRate = 80;        // 체력 회복 확률
        
        // 연설 관련 변수 초기화
        polSpeechChange = 10;   // 연설로 인한 정치력 변화량
        polSpeechRate = 70;     // 정치력 변화 확률

    }

    /* 함수 이름 : Pray()
     * 함수 기능 : 기도를 수행하여 플레이어의 경건함을 지정한 값만큼 변화시킨다. 체력은 일정 확률을 적용시켜 회복
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void Pray()
    {
        characters[0].piety += pietyPrayChange;     // 경건함을 변화

        if (Random.Range(0f, 100f) <= hpPrayRate)   // 체력이 회복될 확률 적용
        {
            characters[0].hp += hpPrayChange;       // 확률에 들어오면 체력 회복
        }

    }

    /* 함수 이름 : Speech()
     * 함수 기능 : 연설 수행 시 일정 확률에 따라 플레이어의 정치력을 변화시킨다
     * 함수 파라미터 : 없음
     * 반환값 없음
     */
    public void Speech()
    {
        if (Random.Range(0f, 100f) <= polSpeechRate)    // 정치력이 변화할 확률 적용
        {
            characters[0].pol += polSpeechChange;       // 확률에 들어오면 정치력 변화
        }
    }

    


    // 구글 시트와 연동 대비용
    /*
    void SetItemSO(string tsv)
    {
        string[] row = tsv.Split('\n');
        int rowSize = row.Length;
        int columnSize = row[0].Split('\t').Length;

        for (int i = 0; i < rowSize; i++)
        {
            string[] column = row[i].Split('\t');
            for (int j = 0; j < columnSize; j++)
            {
                Augment targetAugment = augmentSO.augments[i];

                targetAugment.augID = column[0];
                targetAugment.augName = column[1];
                targetAugment.augDesc = column[2];
                targetAugment.augType = column[3];
                targetAugment.augTarget = column[4];
                targetAugment.hpAugChange = int.Parse(column[5]);
                targetAugment.polAugChange = int.Parse(column[6]);
                targetAugment.pietyAugChange = int.Parse(column[7]);
                targetAugment.hpCost = int.Parse(column[8]);
                targetAugment.polCost = int.Parse(column[9]);
                targetAugment.pietyCost = int.Parse(column[10]);
                targetAugment.augDuration = int.Parse(column[11]);
                targetAugment.augWeight = int.Parse(column[12]);
            }
        }
    }
    */
}
