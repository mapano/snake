using System;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

class UiController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI score;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button pauseButton;

    public void Init(Action onRestartClicked, Action onPauseClicked)
    {
        restartButton.onClick.AddListener(() => onRestartClicked());
        pauseButton.onClick.AddListener(() => onPauseClicked());
        SetScore(0);
    }

    public void SetScore(int score)
    {
        this.score.text = score.ToString();
    }
}