using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    PlayerHealth player;
    [SerializeField] float damage = 40f;


    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<PlayerHealth>();
    }

    public void AttackHitEvent()
    {
        if (player == null) return;
        player.TakeDamage(damage);
    }
}
