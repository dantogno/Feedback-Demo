using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHitPoints = 3;

    [Tooltip("Period of invulnerability after taking damage.")]
    [SerializeField]
    private float damageCooldown = 1;

    [Tooltip("These must be added in order with index 0 being the far left image.")]
    [SerializeField]
    private Image[] hitPointImages;

    [SerializeField]
    private AudioClip deathAudioClip, hitAudioClip;

    private bool isWaitingOnDamageCooldown;
    private int hitPoints;
    private Animator animator;
    private Canvas canvas;
    private AudioSource audioSource;
    private static PlayerHealth instance;
    public int HitPoints
    {
        get { return hitPoints; }
        private set { hitPoints = Mathf.Clamp(value, 0, maxHitPoints); }        
    }

    public static PlayerHealth Instance => instance;
    

    private void Awake()
    {
        if (instance == null)
            instance = GameObject.FindObjectOfType<PlayerHealth>();
        else
            Destroy(this);
        hitPoints = maxHitPoints;
        animator = GetComponent<Animator>();
        canvas = GetComponentInChildren<Canvas>(true);
        canvas.enabled = false;
        audioSource = GetComponent<AudioSource>();
    }

    public void TakeDamage()
    {
        if (!isWaitingOnDamageCooldown && !GameManager.IsGameOver)
        {
            HitPoints--;
            SetHpIconEnabledBasedOnCurrentHp();
            Debug.Log($"Took damage. Hitpoints remaining: {HitPoints}");
            if (HitPoints < 1)
            {
                if (Settings.IsPlayerDeathAnimationEnabled)
                    animator.SetTrigger("playerDied");
                audioSource.clip = deathAudioClip;
                audioSource.Play();
                GameManager.Lose();
            }
            else
            {
                audioSource.clip = hitAudioClip;
                audioSource.Play();
                StartCoroutine(DoDamageCooldown());
            }
        }
    }

    private void SetHpIconEnabledBasedOnCurrentHp()
    {
        for (int i = 0; i < hitPointImages.Length; i++)
        {
            if (HitPoints <= i)
                hitPointImages[i].enabled = false;
        }
    }

    private IEnumerator DoDamageCooldown()
    {
        isWaitingOnDamageCooldown = true;
        if (Settings.IsFlashAfterTakingDamageEnabled)
            animator.SetBool("shouldFlash", true);
        yield return new WaitForSeconds(damageCooldown);
        isWaitingOnDamageCooldown = false;
        animator.SetBool("shouldFlash", false);
    }

    private void OnHpIconsEnabledToggled(bool isOn)
    {
        canvas.enabled = isOn;
    }
    private void OnEnable()
    {
        Settings.IsHpIconsEnabledToggled += OnHpIconsEnabledToggled;
    }
    private void OnDisable()
    {
        Settings.IsHpIconsEnabledToggled -= OnHpIconsEnabledToggled;
    }
}
