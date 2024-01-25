using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageSelection : MonoBehaviour
{
    [SerializeField] private StageData _stageData;

    [Space(10)]
    [SerializeField] private Button _stage2;
    [SerializeField] private Button _stage3;
    [SerializeField] private Button _stage4;
    [SerializeField] private Button _stage5;
    [SerializeField] private Button _stage6;

    int stg1 = 0;
    int stg2 = 0;
    int stg3 = 0;
    int stg4 = 0;
    int stg5 = 0;
    int stg6 = 0;

    // Start is called before the first frame update
    void Start()
    {
        /*_stage2.interactable = _stageData.isStage2Unlocked;
        _stage3.interactable = _stageData.isStage3Unlocked;
        _stage4.interactable = _stageData.isStage4Unlocked;
        _stage5.interactable = _stageData.isStage5Unlocked;
        _stage6.interactable = _stageData.isStage6Unlocked;*/

        SetSTG();
    }

    // Update is called once per frame
    void Update()
    {
        if (stg2 == 0)
        {
            _stage2.interactable = false;
        }
        else
        {
            _stage2.interactable = true;
        }

        if (stg3 == 0)
        {
            _stage3.interactable = false;
        }
        else
        {
            _stage3.interactable = true;
        }

        if (stg4 == 0)
        {
            _stage4.interactable = false;
        }
        else
        {
            _stage4.interactable = true;
        }

        if (stg5 == 0)
        {
            _stage5.interactable = false;
        }
        else
        {
            _stage5.interactable = true;
        }

        if (stg6 == 0)
        {
            _stage6.interactable = false;
        }
        else
        {
            _stage6.interactable = true;
        }
    }

    public void ResetAllStages()
    {
        /*_stageData.ResetAllStages();

        _stage2.interactable = _stageData.isStage2Unlocked;
        _stage3.interactable = _stageData.isStage3Unlocked;
        _stage4.interactable = _stageData.isStage4Unlocked;
        _stage5.interactable = _stageData.isStage5Unlocked;
        _stage6.interactable = _stageData.isStage6Unlocked;*/

        PlayerPrefs.SetInt("stage2", 0);
        PlayerPrefs.SetInt("stage3", 0);
        PlayerPrefs.SetInt("stage4", 0);
        PlayerPrefs.SetInt("stage5", 0);
        PlayerPrefs.SetInt("stage6", 0);
    }

    void SetSTG()
    {
        stg2 = PlayerPrefs.GetInt("stage2");
        stg3 = PlayerPrefs.GetInt("stage3");
        stg4 = PlayerPrefs.GetInt("stage4");
        stg5 = PlayerPrefs.GetInt("stage5");
        stg6 = PlayerPrefs.GetInt("stage6");
    }
}
