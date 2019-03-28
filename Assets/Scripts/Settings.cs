using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    [SerializeField]
    private GameObject closeIcon, openIcon;

    [SerializeField]
    private Color hazardColor, collectibleColor, doorColor;

    [Tooltip("The panel that contains all the toggles. Used for select all functionality.")]
    [SerializeField]
    GameObject subPanel;

    public static bool IsFloatingNumbersEnabled { get; private set; } = false;
    public static bool IsColorCodingEnabled { get; private set; } = false;
    public static bool IsGameOverTextEnabled {get; private set;} = false;
    public static bool IsFlashAfterTakingDamageEnabled { get; private set; } = false;
    public static bool IsPlayerDeathAnimationEnabled { get; private set; } = false;
    public static bool IsTextHudEnabled { get; private set; } = false;
    public static bool IsHpIconsEnabled { get; private set; } = false;
    public static bool IsSoundEffectsEnabled { get; private set; } = false;
    public static bool IsDoorTextEnabled { get; private set; } = false;
    public static bool IsDoorArrowEnabled { get; private set; } = false;

    public static event Action<bool> IsColorCodingEnabledToggled, IsHpIconsEnabledToggled,
        IsTextHudEnabledToggled, IsDoorTextEnabledToggled, IsDoorArrowEnabledToggled;

    private bool menuIsOn = false;
    private Animator animator;
    private Toggle[] toggles;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        AdjustVolume();
        toggles = subPanel.GetComponentsInChildren<Toggle>();
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
    public void FlashAfterTakingDamageToggled()
    {
        IsFlashAfterTakingDamageEnabled = !IsFlashAfterTakingDamageEnabled;
    }
    public void PlayerDeathAnimationToggled()
    {
        IsPlayerDeathAnimationEnabled = !IsPlayerDeathAnimationEnabled;
    }
    public void TextHudToggled()
    {
        IsTextHudEnabled = !IsTextHudEnabled;
        IsTextHudEnabledToggled?.Invoke(IsTextHudEnabled);
    }
    public void HpIconsToggled()
    {
        IsHpIconsEnabled = !IsHpIconsEnabled;
        IsHpIconsEnabledToggled?.Invoke(IsHpIconsEnabled);
    }
    public void DoorTextToggled()
    {
        IsDoorTextEnabled = !IsDoorTextEnabled;
        IsDoorTextEnabledToggled?.Invoke(IsDoorTextEnabled);
    }
    public void DoorArrowToggled()
    {
        IsDoorArrowEnabled = !IsDoorArrowEnabled;
        IsDoorArrowEnabledToggled?.Invoke(IsDoorArrowEnabled);
    }
    public void SoundEffectsToggled()
    {
        IsSoundEffectsEnabled = !IsSoundEffectsEnabled;
        AdjustVolume();
    }

    private static void AdjustVolume()
    {
        AudioListener.volume = IsSoundEffectsEnabled ? 1 : 0;
    }

    public void SelectAllPressed()
    {
        foreach (var toggle in toggles)
        {
            toggle.isOn = true;
        }
    }
    public void DeselectAllPressed()
    {
        foreach (var toggle in toggles)
        {
            toggle.isOn = false;
        }
    }

    public static void ResetSettings()
    {
        IsFloatingNumbersEnabled = false;
        IsColorCodingEnabled = false;
        IsGameOverTextEnabled = false;
        IsPlayerDeathAnimationEnabled = false;
        IsFlashAfterTakingDamageEnabled = false;
        IsHpIconsEnabled = false;
        IsTextHudEnabled = false;
        IsSoundEffectsEnabled = false;
        IsDoorTextEnabled = false;
        IsDoorArrowEnabled = false;
    }
}
