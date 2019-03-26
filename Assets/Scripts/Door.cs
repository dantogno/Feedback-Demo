using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Tooltip("Door only works if player has collected this many.")]
    [SerializeField]
    private int numberCollectiblesRequired = 0;

    [Tooltip("The actual collider that blocks the player, not the trigger!")]
    [SerializeField]
    private Collider2D doorCollider;

    private void OnTriggerEnter2D(Collider2D collision)
    {
       GameManager.Win();
    }

    private void Awake()
    {
        if (numberCollectiblesRequired == 0)
            UnlockDoor();
    }

    private void UnlockDoor()
    {
        doorCollider.enabled = false;
    }
    private void OnCollectibleGathered()
    {
        if (Collectible.CollectiblesGathered >= numberCollectiblesRequired)
            UnlockDoor();
    }
    private void OnEnable()
    {
        Collectible.CollectibleGathered += OnCollectibleGathered;
    }
    private void OnDisable()
    {
        Collectible.CollectibleGathered -= OnCollectibleGathered;
    }
}
