using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject[] Stages;

    public int stageIndex;
    public int stagePoint;
    public int totalPoint;

    public void NextStage()
    {
        if (stageIndex < Stages.Length)
        {
            //스테이지 변경
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            //점수 계산
            totalPoint += stagePoint;
            stagePoint = 0;
        }
        else
        {
            //클리어
            Debug.Log("클리어");
        }
    }
}
