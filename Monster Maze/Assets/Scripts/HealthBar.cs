using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider slider;
    public Vector3 healthBarPos;
    public Gradient gradient;
    public Image fill;

    public void SetHealth(float health, float maxHealth)
    {
        slider.gameObject.SetActive(health < maxHealth && health > 0);
        slider.value = health;
        slider.maxValue = maxHealth;

        fill.color = gradient.Evaluate(slider.normalizedValue);
    }

    // Update is called once per frame
    void Update()
    {
        slider.transform.position = Camera.main.WorldToScreenPoint(transform.parent.position 
            + healthBarPos);
    }
}
