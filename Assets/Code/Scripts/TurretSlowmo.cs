using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TurretSlowmo : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private LayerMask enemyMask;

    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private float aps = 4f; // attacks per second
    [SerializeField] private float freezeTime = 1f;

    private float timeUntilFire;

    private void Update() {
        timeUntilFire += Time.deltaTime;

        if(timeUntilFire >= 1f / aps) {
            FreezeEnemies();
            timeUntilFire = 0f;
        }
    }

    private void FreezeEnemies() {

        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, (Vector2)
        transform.position, 0f, enemyMask);

        if(hits.Length > 0) {
            for(int i = 0; i < hits.Length; i++) {
                RaycastHit2D hit = hits[i];

                EnemyMovement em = hit.transform.GetComponent<EnemyMovement>();
                em.UpdateSpeed(0.5f);

                StartCoroutine(ResetEnemySpeed(em));
            }
        }
    }

    private IEnumerator ResetEnemySpeed(EnemyMovement em) {
        yield return new WaitForSeconds(freezeTime);

        em.ResetSpeed();
    }
    
}
