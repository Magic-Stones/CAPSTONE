using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHistoryUI : MonoBehaviour
{
    [SerializeField] private Transform _scoreEntryContainer;
    private Transform _scoreEntryTemplate;
    private List<Transform> _scoreEntryTransforms;

    private ScoreHistoryData _scoreHistoryData = null;
    [SerializeField] private StageScoreData _stageScoreData;

    void Awake()
    {
        _scoreEntryTemplate = _scoreEntryContainer.GetChild(0);
        _scoreEntryTransforms = new List<Transform>();
    }

    // Start is called before the first frame update
    void Start()
    {
        _scoreEntryTemplate.gameObject.SetActive(false);

        UpdateScoreHistory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [Serializable]
    private class ScoreEntry
    {
        public int score;
        public int totalQuestions;
        public string dateTime;
    }

    private class ScoreHistoryData
    {
        public List<ScoreEntry> scoreEntries;
    }

    private void UpdateScoreHistory()
    {
        foreach (Transform child in _scoreEntryContainer)
        {
            if (child.name.Equals(_scoreEntryTemplate.name)) continue;
            Destroy(child.gameObject);
        }

        if (!_stageScoreData.isScoreDataUpdated)
        {
            _scoreHistoryData = new ScoreHistoryData();
            string json = JsonUtility.ToJson(_scoreHistoryData);
            PlayerPrefs.SetString("scoreHistory", json);
            PlayerPrefs.Save();
        }

        if (_stageScoreData.stageScores.Count > 0)
        {
            AddScoreEntry(_stageScoreData.stageScores[0].score, 
                _stageScoreData.stageScores[0].totalQuestions, 
                _stageScoreData.stageScores[0].dateTime);

            _stageScoreData.stageScores.Clear();
        }

        string jsonString = PlayerPrefs.GetString("scoreHistory");
        _scoreHistoryData = JsonUtility.FromJson<ScoreHistoryData>(jsonString);

        if (_scoreHistoryData == null) return;

        // Sort rank descending
        for (int i = 0; i < _scoreHistoryData.scoreEntries.Count; i++)
        {
            for (int j = 0; j < _scoreHistoryData.scoreEntries.Count; j++)
            {
                if (_scoreHistoryData.scoreEntries[j].score < _scoreHistoryData.scoreEntries[i].score)
                {
                    ScoreEntry tmp = _scoreHistoryData.scoreEntries[i];
                    _scoreHistoryData.scoreEntries[i] = _scoreHistoryData.scoreEntries[j];
                    _scoreHistoryData.scoreEntries[j] = tmp;
                }
            }
        }

        foreach (ScoreEntry entry in _scoreHistoryData.scoreEntries)
        {
            CreateScoreEntry(entry, _scoreEntryContainer, _scoreEntryTransforms);
        }
    }

    private void CreateScoreEntry(ScoreEntry scoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 50f;

        Transform entryTransform = Instantiate(_scoreEntryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0f, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        entryTransform.Find("Text - Ranking").GetComponent<TextMeshProUGUI>().text = rank.ToString();

        int score = scoreEntry.score;
        int totalQuestions = scoreEntry.totalQuestions;
        entryTransform.Find("Text - Score").GetComponent<TextMeshProUGUI>().text = $"{score}/{totalQuestions}";

        string dateTime = scoreEntry.dateTime;
        entryTransform.Find("Text - DateTime").GetComponent<TextMeshProUGUI>().text = dateTime;

        transformList.Add(entryTransform);
    }

    public void AddScoreEntry(int newScore, int newTotalQuestions, string newDateTime)
    {
        string json;
        if (!_stageScoreData.isScoreDataUpdated)
        {
            _scoreHistoryData = new ScoreHistoryData();
            json = JsonUtility.ToJson(_scoreHistoryData);
            PlayerPrefs.SetString("scoreHistory", json);
            PlayerPrefs.Save();
        }

        // Create score entry
        ScoreEntry entry = new ScoreEntry 
        { 
            score = newScore, 
            totalQuestions = newTotalQuestions, 
            dateTime = newDateTime 
        };

        // Load saved score entries
        string jsonString = PlayerPrefs.GetString("scoreHistory");
        _scoreHistoryData = JsonUtility.FromJson<ScoreHistoryData>(jsonString);

        // Save update score entries
        _scoreHistoryData.scoreEntries.Add(entry);

        if (_scoreHistoryData.scoreEntries.Count > 5)
        {
            for (int i = _scoreHistoryData.scoreEntries.Count; i > 10; i--)
            {
                _scoreHistoryData.scoreEntries.RemoveAt(5);
            }
        }

        json = JsonUtility.ToJson(_scoreHistoryData);
        PlayerPrefs.SetString("scoreHistory", json);
        PlayerPrefs.Save();

        _stageScoreData.isScoreDataUpdated = true;
    }

    public void ClearScoreHistory()
    {
        _stageScoreData.isScoreDataUpdated = false;

        UpdateScoreHistory();
    }
}
