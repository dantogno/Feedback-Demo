using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Door : MonoBehaviour
{
    [Tooltip("Use this color when color coding is turned on.")]
    [SerializeField]
    private Color color;

    [Tooltip("Door only works if player has collected this many.")]
    [SerializeField]
    private int numberCollectiblesRequired = 0;

    [Tooltip("The actual collider that blocks the player, not the trigger!")]
    [SerializeField]
    private Collider2D doorCollider;

    [SerializeField]
    private AudioClip lockedAudioClip, winAudioClip;

    [SerializeField]
    private SpriteRenderer arrowRenderer;

    private new SpriteRenderer renderer;
    private AudioSource audioSource;
    private Text text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.Play();
        GameManager.Win();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.Play();
    }
    private void Awake()
    {
        if (numberCollectiblesRequired == 0)
            UnlockDoor();
        renderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = lockedAudioClip;
        text = GetComponentInChildren<Text>();
        text.text = $"Need {numberCollectiblesRequired}";
        text.enabled = false;
        arrowRenderer.enabled = false;
    }

    private void UnlockDoor()
    {
        doorCollider.enabled = false;
        audioSource.clip = winAudioClip;
        text.text = "Unlocked";
        arrowRenderer.enabled = Settings.IsDoorArrowEnabled;
    }
    private void OnIsColorCodingEnabledToggled(bool isOn)
    {
        renderer.color = isOn ? color : Color.white;
        arrowRenderer.color = isOn ? color : Color.white;
    }

    private void OnIsDoorTextEnabledToggled(bool isOn)
    {
        text.enabled = isOn;
    }
    private void OnCollectibleGathered()
    {
        text.text = $"Need {numberCollectiblesRequired - Collectible.CollectiblesGathered}";
        if (Collectible.CollectiblesGathered >= numberCollectiblesRequired)
            UnlockDoor();
    }
    private void OnIsDoorArrowEnabledToggled(bool isOn)
    {
        arrowRenderer.enabled = isOn && Collectible.CollectiblesGathered >= numberCollectiblesRequired;
    }
    private void OnEnable()
    {
        Settings.IsDoorArrowEnabledToggled += OnIsDoorArrowEnabledToggled;
        Settings.IsDoorTextEnabledToggled += OnIsDoorTextEnabledToggled;
        Settings.IsColorCodingEnabledToggled += OnIsColorCodingEnabledToggled;
        Collectible.CollectibleGathered += OnCollectibleGathered;
    }
    private void OnDisable()
    {
        Settings.IsDoorArrowEnabledToggled -= OnIsDoorArrowEnabledToggled;
        Settings.IsDoorTextEnabledToggled -= OnIsDoorTextEnabledToggled;
        Settings.IsColorCodingEnabledToggled -= OnIsColorCodingEnabledToggled;
        Collectible.CollectibleGathered -= OnCollectibleGathered;
    }
}
