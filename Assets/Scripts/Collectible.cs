using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public static event Action CollectibleGathered;
    public static int CollectiblesGathered { get; set; } = 0;
    new Renderer renderer;
    new Collider collider;

    private void Awake()
    {
        renderer= GetComponent<Renderer>();
        collider = GetComponent<Collider>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CollectiblesGathered++;
        CollectibleGathered?.Invoke();
        renderer.enabled = false;
        collider.enabled = false;
    }
}
