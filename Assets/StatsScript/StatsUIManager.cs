using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class StatsUIManager : MonoBehaviour
{
    public GameManager gameManager;

    public TextMeshProUGUI[] charStatsText = new TextMeshProUGUI[4];

    public TextMeshProUGUI[] augmentNames = new TextMeshProUGUI[3];
    public TextMeshProUGUI[] augmentDescs = new TextMeshProUGUI[3];

    public GameObject augSelectUI;
    public GameObject actionState;
    public Button[] actionButtons = new Button[3];
    public TextMeshProUGUI actionText;

    public TextMeshProUGUI dayTimeText;

    // 3초 동안 게임을 일시정지하고 선택한 행동 정보를 표시한 후 게임을 재개하는 코루틴
    IEnumerator WaitForSecond()
    {
        Time.timeScale = 0f;    // 게임의 시간을 멈춤

        actionState.SetActive(true);    // actionState 오브젝트를 활성화하여 UI 표시

        yield return new WaitForSecondsRealtime(3f);    // 실제 시간 기준으로 3초 대기

        actionState.SetActive(false);   // 3초 후 actionState UI 비활성화

        Time.timeScale = 1.0f;  // 게임의 시간을 다시 정상속도로 설정
    }

    /* 함수 이름 : OnClickPray
     * 함수 기능 : Pray 버튼이 클릭 됐을 때 호출하여 actionText를 "Praying..." 으로 설정하고 WaitForSecond 코루틴 호출
     *              gameManager의 OnPrayAction을 호출하여 Pray 실행
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void OnClickPray()
    {
        actionText.text = "Praying...";

        StartCoroutine("WaitForSecond");

        gameManager.OnPrayAction();
    }

    /* 함수 이름 : OnClickSpeech
     * 함수 기능 : Speech 버튼이 클릭 됐을 때 호출하여 actionText를 "Speaking..." 으로 설정하고 WaitForSecond 코루틴 호출
     *              gameManager의 OnSpeechAction을 호출하여 Speech 실행
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void OnClickSpeech()
    {
        actionText.text = "Speaking...";

        StartCoroutine("WaitForSecond");

        gameManager.OnSpeechAction();
    }

    /* 함수 이름 : OnClickAugment
     * 함수 기능 : Augment 버튼이 클릭 됐을 때 호출하여 게임의 시간을 멈추고 3개의 증강을 띄운다
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void OnClickAugment()
    {
        Time.timeScale = 0f;

        augSelectUI.SetActive(true);

        foreach (var item in actionButtons)
        {
            item.interactable = false;
        }
    }

    /* 함수 이름 : OnClickSelectAugment
     * 함수 기능 : 세가지 증강 중 하나를 선택했을 때 호출. 왼쪽부터 인덱스 0, 1, 2 로 선택한 증강에 따라 인덱스값을 인자로 전달
     *              증강 선택 UI를 끄고 actionText를 "Conducting Operations..." 으로 설정하고 WaitForSecond 코루틴 호출
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void OnClickSelectAugment(int index)
    {
        gameManager.OnSelectAugment(index);

        augSelectUI.SetActive(false);

        foreach (var item in actionButtons)
        {
            item.interactable = true;
        }

        actionText.text = "Conducting Operations...";

        StartCoroutine("WaitForSecond");
    }

    /* 함수 이름 : OnClickBack
     * 함수 기능 : 증강 선택 화면에서 back 버튼을 누르면 증강 선택 화면을 끄고 각 행동 버튼을 활성화한 후 게임 시간을 정상으로 복구
     * 함수 파라미터 : 없음
     * 반환값 : 없음
     */
    public void OnClickBack()
    {
        augSelectUI.SetActive(false);

        foreach (var item in actionButtons)
        {
            item.interactable = true;
        }

        Time.timeScale = 1f;
    }

    /* 함수 이름 : SetAugmentUI
     * 함수 기능 : 증강 선택 화면에서 선택 가능한 3가지 증강의 정보를 UI로 전달
     * 함수 파라미터 : List<Augment> availAugmentList, 이번 턴에 선택 가능한 세 가지 증강을 인자로 받음
     * 반환값 : 없음
     */
    public void SetAugmentUI(List<Augment> availAugmentList)
    {
        for (int i = 0; i < 3; i++)
        {
            augmentNames[i].text = availAugmentList[i].augName;
            augmentDescs[i].text = availAugmentList[i].augDesc;
        }
    }

    /* 함수 이름 : SetDayTime
     * 함수 기능 : 날짜시간 정보를 인자로 전달 받아 UI로 전달
     * 함수 파라미터 : string dayTime, 날짜시간 정보를 저장한 string 타입을 인자로 전달 받음
     * 반환값 : 없음
     */
    public void SetDayTime(string dayTime)
    {
        dayTimeText.text = dayTime;
    }

    /* 함수 이름 : SetStatsInfo
     * 함수 기능 : 캐릭터들의 스탯 정보를 받아 UI로 전달
     * 함수 파라미터 : List<Character> characters, 캐릭터들의 스탯을 저장핞 리스트를 인자로 받음
     * 반환값 : 없음
     */
    public void SetStatsInfo(List<Character> characters)
    {
        for (int index = 0; index < 4; index++)
        {
            if (index == 0)
            {
                charStatsText[index].text = ($"Player\n  hp : {characters[index].hp}\n  piety : {characters[index].piety}\n  pol : {characters[index].pol}");

            }
            else
            {
                charStatsText[index].text = ($"Papabilis{index}\n  hp : {characters[index].hp}\n  piety : {characters[index].piety}\n  pol : {characters[index].pol}");
            }
            
        }
    }
}

    
