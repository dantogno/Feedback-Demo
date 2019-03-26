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
    }

    public void TakeDamage()
    {
        if (!isWaitingOnDamageCooldown && !GameManager.IsGameOver)
        {
            HitPoints--;
            Debug.Log($"Took damage. Hitpoints remaining: {HitPoints}");
            if (HitPoints < 1)
                GameManager.Lose();
            else
                StartCoroutine(DoDamageCooldown());
        }
    }

    private IEnumerator DoDamageCooldown()
    {
        isWaitingOnDamageCooldown = true;
        yield return new WaitForSeconds(damageCooldown);
        isWaitingOnDamageCooldown = false;
    }
}
