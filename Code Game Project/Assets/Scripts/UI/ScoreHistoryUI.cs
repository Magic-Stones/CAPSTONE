using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreHistoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _page1;
    [SerializeField] private GameObject _page2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Page_1()
    {
        _page1.SetActive(true);
        _page2.SetActive(false);
    }

    public void Page_2()
    {
        _page2.SetActive(true);
        _page1.SetActive(false);
    }
}
