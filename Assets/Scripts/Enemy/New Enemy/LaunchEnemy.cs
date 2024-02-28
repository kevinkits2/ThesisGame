using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchEnemy : MonoBehaviour, IEnemy {

    [SerializeField] private int projectileAmount = 1;
    [SerializeField] private float projectileRange = 5f;
    [SerializeField] private bool aimAtPlayer = true;
    [SerializeField] private GameObject grapeProjectilePrefab;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private AnimationEventHelper animationEventHelper;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");


    private void OnValidate() {
        if (aimAtPlayer) projectileAmount = 1;
    }

    private void Awake() {
        animator = GetComponentInChildren<Animator>(); 
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animationEventHelper = GetComponentInChildren<AnimationEventHelper>();
    }

    private void Start() {
        animationEventHelper.OnAttackPerformed += HandleAttackPerfromed;
    }

    private void HandleAttackPerfromed(object sender, System.EventArgs e) {
        if (aimAtPlayer) SpawnProjectile();
        else SpawnProjectiles();
    }

    public void Attack() {
        animator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - Player.Instance.transform.position.x < 0) {
            spriteRenderer.flipX = false;
        }
        else {
            spriteRenderer.flipX = true;
        }
    }

    public void SpawnProjectile() {
        GameObject newProjectile = Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
        LaunchProjectile launchProjectile = newProjectile.GetComponent<LaunchProjectile>();

        launchProjectile.Init(Player.Instance.transform.position);
    }

    public void SpawnProjectiles() {
        float startAngle, currentAngle, angleStep;
        angleStep = 360 / projectileAmount;

        if (projectileAmount % 2 == 1) {
            startAngle = angleStep / projectileAmount;
        }
        else {
            startAngle = 0f;
        }
        
        currentAngle = startAngle;

        for (int i = 0; i < projectileAmount; i++) {
            GameObject newProjectile = Instantiate(grapeProjectilePrefab, transform.position, Quaternion.identity);
            LaunchProjectile launchProjectile = newProjectile.GetComponent<LaunchProjectile>();

            Vector2 targetDirection = transform.position - 
                Quaternion.AngleAxis(currentAngle, Vector3.forward) * Vector3.right * projectileRange;

            launchProjectile.Init(targetDirection);

            currentAngle += angleStep;
        } 
    }
}
