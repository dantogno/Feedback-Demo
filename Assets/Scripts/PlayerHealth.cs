using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    private int maxHitPoints = 3;

    [Tooltip("Period of invulnerability after taking damage.")]
    [SerializeField]
    private float damageCooldown = 1;

    private bool isWaitingOnDamageCooldown;
    private int hitPoints;
    private Animator animator;
    private static PlayerHealth instance;
    private int HitPoints
    {
        get { return hitPoints; }
        set { hitPoints = Mathf.Clamp(value, 0, maxHitPoints); }        
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
    }

    public void TakeDamage()
    {
        if (!isWaitingOnDamageCooldown && !GameManager.IsGameOver)
        {
            HitPoints--;
            Debug.Log($"Took damage. Hitpoints remaining: {HitPoints}");
            if (HitPoints < 1)
            {
                if (Settings.IsPlayerDeathAnimationEnabled)
                    animator.SetTrigger("playerDied");
                GameManager.Lose();
            }
            else
                StartCoroutine(DoDamageCooldown());
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
}
