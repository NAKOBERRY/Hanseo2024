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
            //�������� ����
            Stages[stageIndex].SetActive(false);
            stageIndex++;
            Stages[stageIndex].SetActive(true);
            //���� ���
            totalPoint += stagePoint;
            stagePoint = 0;
        }
        else
        {
            //Ŭ����
            Debug.Log("Ŭ����");
        }
    }
}
