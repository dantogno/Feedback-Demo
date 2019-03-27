using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collectible : MonoBehaviour
{
    [Tooltip("Use this color when color coding is turned on.")]
    [SerializeField]
    private Color color;

    private GameObject floatingNumbersCanvas;
    public static event Action CollectibleGathered;
    public static int CollectiblesGathered { get; set; } = 0;
    private new SpriteRenderer renderer;
    private bool isCollected;
    private Text floatingText;

    private void Awake()
    {
        renderer= GetComponent<SpriteRenderer>();
        floatingText = GetComponentInChildren<Text>(true);
        floatingNumbersCanvas = GetComponentInChildren<Canvas>(true).gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isCollected)
        {
            isCollected = true;
            CollectiblesGathered++;
            CollectibleGathered?.Invoke();
            renderer.enabled = false;
            floatingText.text = CollectiblesGathered.ToString();
            if (Settings.IsFloatingNumbersEnabled)
                floatingNumbersCanvas.SetActive(true);
        }
    }

    private void OnIsColorCodingEnabledToggled(bool isOn)
    {
        renderer.color = isOn ? color : Color.white;
    }
    private void OnEnable()
    {
        Settings.IsColorCodingEnabledToggled += OnIsColorCodingEnabledToggled;
    }
    private void OnDisable()
    {
        Settings.IsColorCodingEnabledToggled -= OnIsColorCodingEnabledToggled;
    }
}
