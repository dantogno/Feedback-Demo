using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHud : MonoBehaviour
{
    [SerializeField]
    private Text collectiblesText, healthText;

    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 0;
    }

    private void FixedUpdate()
    {
        collectiblesText.text = $"Collectibles Gathered: {Collectible.CollectiblesGathered}";
        healthText.text = $"Health: {PlayerHealth.Instance.HitPoints}";
    }

    private void OnIsTextHudEnabledToggled(bool isOn)
    {
        canvasGroup.alpha = isOn ? 1 : 0;
    }
    private void OnEnable()
    {
        Settings.IsTextHudEnabledToggled += OnIsTextHudEnabledToggled;
    }
    private void OnDisable()
    {
        Settings.IsTextHudEnabledToggled -= OnIsTextHudEnabledToggled;
    }
}
