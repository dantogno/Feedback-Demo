using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private GameObject closeIcon, openIcon;
    [SerializeField]
    private Color hazardColor, collectibleColor, doorColor;

    public static bool IsFloatingNumbersEnabled { get; private set; } = false;
    public static bool IsColorCodingEnabled { get; private set; } = false;
    public static bool IsGameOverTextEnabled {get; private set;} = false;
    public static bool IsFlashAfterTakingDamageEnabled { get; private set; } = false;
    public static bool IsPlayerDeathAnimationEnabled { get; private set; } = false;
    public static event Action<bool> IsColorCodingEnabledToggled;

    private bool menuIsOn = false;
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void SettingsMenuToggled()
    {
        menuIsOn = !menuIsOn;
        closeIcon.SetActive(menuIsOn);
        openIcon.SetActive(!menuIsOn);
        animator.SetBool("menuIsOn", menuIsOn);
    }
    public void FloatingNumbersToggled()
    {
        IsFloatingNumbersEnabled = !IsFloatingNumbersEnabled;
    }
    public void ColorCodingToggled()
    {
        IsColorCodingEnabled = !IsColorCodingEnabled;
        IsColorCodingEnabledToggled?.Invoke(IsColorCodingEnabled);
    }
    public void GameOverMessagesToggled()
    {
        IsGameOverTextEnabled = !IsGameOverTextEnabled;
    }
    public static void ResetSettings()
    {
        IsFloatingNumbersEnabled = false;
        IsColorCodingEnabled = false;
        IsGameOverTextEnabled = false;
    }
}
