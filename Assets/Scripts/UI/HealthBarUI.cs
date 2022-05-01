using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    public GameObject healthUI;

    public Transform healthPoint;

    public bool Visible;

    Image healthBar;
    Transform UIbar;
    Transform cam;

    CharacterStats currentStats;

    void Awake()
    {
        currentStats = GetComponent<CharacterStats>();

        currentStats.healthBarwithAttack += updateHealthbar;

    }

    void OnEnable()
    {
        cam = Camera.main.transform;

        foreach (Canvas canvas in FindObjectsOfType<Canvas>())
        {
            if (canvas.renderMode == RenderMode.WorldSpace)
            {
                UIbar = Instantiate(healthUI, canvas.transform).transform;
                healthBar = UIbar.GetChild(0).GetComponent<Image>();
                UIbar.gameObject.SetActive(Visible);
            }
        }
    }

    private void updateHealthbar(int currentHealth, int maxHealth)
    {
        if (currentHealth <= 0)
        {
            Destroy(UIbar.gameObject);
        }
        UIbar.gameObject.SetActive(true);

        float sliderPercent = (float)currentHealth / maxHealth;
        healthBar.fillAmount = sliderPercent;
    }

    void LateUpdate()
    {
        if(UIbar != null)
        {
            UIbar.position = healthPoint.position;
            UIbar.forward = -cam.forward;
        }
    }
}

