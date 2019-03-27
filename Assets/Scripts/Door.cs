using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private new SpriteRenderer renderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       GameManager.Win();
    }

    private void Awake()
    {
        if (numberCollectiblesRequired == 0)
            UnlockDoor();
        renderer = GetComponent<SpriteRenderer>();
    }

    private void UnlockDoor()
    {
        doorCollider.enabled = false;
    }
    private void OnIsColorCodingEnabledToggled(bool isOn)
    {
        renderer.color = isOn ? color : Color.white;
    }

    private void OnCollectibleGathered()
    {
        if (Collectible.CollectiblesGathered >= numberCollectiblesRequired)
            UnlockDoor();
    }
    private void OnEnable()
    {
        Settings.IsColorCodingEnabledToggled += OnIsColorCodingEnabledToggled;
        Collectible.CollectibleGathered += OnCollectibleGathered;
    }
    private void OnDisable()
    {
        Settings.IsColorCodingEnabledToggled -= OnIsColorCodingEnabledToggled;
        Collectible.CollectibleGathered -= OnCollectibleGathered;
    }
}
