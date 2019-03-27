using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [Tooltip("Use this color when color coding is turned on.")]
    [SerializeField]
    private Color color;

    private new SpriteRenderer renderer;

    private void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            PlayerHealth.Instance.TakeDamage();
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
