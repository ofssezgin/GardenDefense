using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public static EnemyMovement main;

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float rotateSpeed = 5f;

    private Transform target;
    private int pathIndex = 0;

    private float baseSpeed;

    private void Start() {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];
    }
    
    private void Awake() {
        main = this;
    }

    private void Update() {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f){
            pathIndex++;

            if(pathIndex == LevelManager.main.path.Length){
                EnemySpawner.onEnemyDestroy.Invoke();
                if(LevelManager.main.baseHealth != 0){
                    LevelManager.main.baseHealth--;
                }
                Destroy(gameObject);
                return;
            } else {
                target = LevelManager.main.path[pathIndex];
            }
        }
    }

    private void FixedUpdate() {
    Vector2 direction = (target.position - transform.position).normalized;
    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 90f;
    Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime); 
    rb.velocity = direction * moveSpeed;
}

    public void UpdateSpeed(float newSpeed) {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed() {
        moveSpeed = baseSpeed;
    }
    
}