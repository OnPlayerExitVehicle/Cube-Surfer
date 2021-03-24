using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private int plumbobCount;
    [SerializeField] private Text plumbobText;
    [SerializeField] private Text levelText;
    [SerializeField] private Image progressBar;
    [SerializeField] private RectTransform followerTransform;
    [SerializeField] private float followerEnd;
    [SerializeField] private Transform character;
    [SerializeField] private float endPoint;
    [SerializeField] private Text finishText;
    [SerializeField] private Text finishCountText;
    [SerializeField] private GameObject plumbobSymbol;

    private float followerStart;
    private float startPoint;

    void Awake()
    {
        ResetPlumbobCount();
        RegisterEvents();

        levelText.text = (SceneManager.GetActiveScene().buildIndex + 1).ToString();
        progressBar.fillAmount = 0f;
        followerStart = followerTransform.anchoredPosition.x;
        startPoint = character.position.x;

    }

    void Update()
    {
        progressBar.fillAmount = Math.Abs((startPoint - character.position.x) / (startPoint - endPoint));
        followerTransform.anchoredPosition = new Vector2(Mathf.Lerp(followerStart, followerEnd, progressBar.fillAmount), followerTransform.anchoredPosition.y);
    }

    private void RegisterEvents()
    {
        EventManager.OnPlumbobCollected += AddPlumbob;
        EventManager.OnPlumbobCountReset += ResetPlumbobCount;
        EventManager.OnLevelFinished += FinishLevel;
        EventManager.OnLevelFailed += FailLevel;
    }

    private void AddPlumbob()
    {
        plumbobCount++;
        UpdateText();
    }

    private void ResetPlumbobCount()
    {
        plumbobCount = 0;
        UpdateText();
    }

    private void UpdateText()
    {
        plumbobText.text = plumbobCount.ToString();
    }

    private void FinishLevel(int multiplier)
    {
        Debug.Log($"{plumbobCount} * {multiplier} = {plumbobCount * multiplier}");
        plumbobSymbol.SetActive(true);
        finishText.text = "Level Finished!";
        finishCountText.text = "x " + plumbobCount * multiplier;
    }

    private void FailLevel()
    {
        Debug.Log($"Level Failed!");
        finishText.text = "Level Failed!";
    }


}
