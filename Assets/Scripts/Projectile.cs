using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private GameObject particleOnHitPrefabVFX;
    [SerializeField] private float moveSpeed = 22f;
    [SerializeField] private bool isEnemyProjectile = false;
    [SerializeField] private float projectileRange = 10f;

    private Vector3 startPosition;


    private void Start() {
        startPosition = transform.position;
    }

    private void Update() {
        MoveProjectile();
        DetectFireDistance();
    }

    public void UpdateProjectileRange(float projectileRange) {
        this.projectileRange = projectileRange;
    }

    public void UpdateMoveSpeed(float moveSpeed) {
        this.moveSpeed = moveSpeed;
    }

    private void MoveProjectile() {
        transform.Translate(Vector2.right * (moveSpeed * Time.deltaTime));
    }

    private void DetectFireDistance() {
        if (Vector3.Distance(transform.position, startPosition) > projectileRange) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.isTrigger) return;

        EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
        //Indestructible indestructible = collision.gameObject.GetComponent<Indestructible>();
        PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();

        /*if (indestructible) {
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }*/
        if (player && isEnemyProjectile) {
            player?.TakeDamage(1, transform);
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        else if (enemyHealth && !isEnemyProjectile) {
            Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
            Destroy(gameObject);
        }


        /*if (!collision.isTrigger && (enemyHealth || indestructible || player)) {
            if ((player && isEnemyProjectile) || (enemyHealth && !isEnemyProjectile)) {
                player?.TakeDamage(1, transform);
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
            else {
                Instantiate(particleOnHitPrefabVFX, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }*/
    }
}
