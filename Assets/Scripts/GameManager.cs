using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private int curStage = 0;
    public TextMeshProUGUI stageTMP;
    private int stageClear = 2;

    

    public void NextStage()
    {         
        SceneManager.LoadScene(++curStage);       
        stageTMP.text = "STAGE"+ curStage;
    }

    public void Clear()
    {        
        if(curStage == stageClear)
        {
            //씬 이동 혹은 데스 패널 생성 등,
        }
    }
}
