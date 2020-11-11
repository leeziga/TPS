using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayHUD : MonoBehaviour
{
    [SerializeField] private Text MessageText = null;
    [SerializeField] private Text ScoreText = null;
    [SerializeField] private Text BulletText = null;
    [SerializeField] private Slider PlayerHealth = null;

    public void Initialize()
    {
        MessageText.text = string.Empty;
        BulletText.text = "";
    }

    public void SetGameplayHUDActive(bool shouldBeActive)
    {
        gameObject.SetActive(shouldBeActive);
    }

    public void UpdateScore(int currentScore)
    {
        ScoreText.text = currentScore.ToString();
    }

    public void UpdateMessageText(string message)
    {
        MessageText.text = message;
    }

    public void UpdateBulletText(string bulletText)
    {
        BulletText.text = bulletText;
    }

    public void UpdatePlayerHealth(float health)
    {
        PlayerHealth.value = health;
    }
}
