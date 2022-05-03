using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class PlayerInfoUI : MonoBehaviour
{
    TextMeshProUGUI level;
    Image health;
    Image exp;

    private void Awake()
    {
        Debug.Log(transform.GetChild(0).GetComponent<TextMeshProUGUI>());
        level = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        health = transform.GetChild(1).GetChild(0).GetComponent<Image>();
        exp = transform.GetChild(2).GetChild(0).GetComponent<Image>();
    }

    private void Update()
    {
        UpdateHealth();
        UpdateExp();
        UpdateLevel();
    }

    private void UpdateHealth()
    {
        float currentHealth = (float)GameManager.Instance.playerStats.CurrentHealth;
        float maxHealth = (float)GameManager.Instance.playerStats.MaxHealth;
        float healthPercent = currentHealth / maxHealth;
        health.fillAmount = healthPercent;
    }

    private void UpdateExp()
    {
        float currentExp = (float)GameManager.Instance.playerStats.characterStats.curExp;
        float maxExp = (float)GameManager.Instance.playerStats.characterStats.expBase;
        float healthPercent = currentExp/ maxExp;
        exp.fillAmount = healthPercent;
    }

    private void UpdateLevel()
    {
        level.text = "LV " + GameManager.Instance.playerStats.characterStats.curlevel;
    }
}
 