using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour, IEnemy {

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletMoveSpeed;
    [SerializeField] private int burstCount;
    [SerializeField] private int projectilesPerBurst;
    [SerializeField][Range(0, 359)] private float angleSpread;
    [SerializeField] private float startingDistance = 0.1f;
    [SerializeField] private float timeBetweenBursts;
    [SerializeField] private float restTime = 1f;
    [SerializeField] private bool stagger;
    [Tooltip("Stagger has to be enabled for oscillate to work properly.")]
    [SerializeField] private bool oscillate;
    [SerializeField] private bool spriteFlipInverted = false;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private bool isShooting = false;
    private AnimationEventHelper animationEventHelper;

    readonly int ATTACK_HASH = Animator.StringToHash("Attack");


    private void OnValidate() {
        if (oscillate) stagger = true;
        if (!oscillate) stagger = false;
        if (projectilesPerBurst < 1) projectilesPerBurst = 1;
        if (burstCount < 1) burstCount = 1;
        if (timeBetweenBursts < 0.1f) timeBetweenBursts = 0.1f;
        if (restTime < 0.1f) restTime = 0.1f;
        if (startingDistance < 0.1f) startingDistance = 0.1f;
        if (angleSpread == 0) projectilesPerBurst = 1;
        if (bulletMoveSpeed <= 0) bulletMoveSpeed = 0.1f;
    }

    private void Awake() {
        animationEventHelper = GetComponentInChildren<AnimationEventHelper>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start() {
        animationEventHelper.OnAttackPerformed += HandleAttackPerformed;
    }

    private void HandleAttackPerformed(object sender, System.EventArgs e) {
        StartCoroutine(ShootRoutine());
    }

    public void Attack() {
        if (isShooting) return;

        animator.SetTrigger(ATTACK_HASH);

        if (transform.position.x - Player.Instance.transform.position.x < 0) {
            spriteRenderer.flipX = !spriteFlipInverted ? false : true;
        }
        else {
            spriteRenderer.flipX = !spriteFlipInverted ? true : false;
        }
    }

    private IEnumerator ShootRoutine() {
        isShooting = true;

        float startAngle, currentAngle, angleStep, endAngle;
        float timeBetweenProjectiles = 0f;

        TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);

        if (stagger) {
            timeBetweenProjectiles = timeBetweenBursts / projectilesPerBurst;
        }

        for (int i = 0; i < burstCount; i++) {
            if (!oscillate) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }

            if (oscillate && i % 2 != 1) {
                TargetConeOfInfluence(out startAngle, out currentAngle, out angleStep, out endAngle);
            }
            else if (oscillate) {
                currentAngle = endAngle;
                endAngle = startAngle;
                startAngle = currentAngle;
                angleStep *= -1;
            }

            for (int j = 0; j < projectilesPerBurst; j++) {
                Vector2 pos = FindBulletSpawnPos(currentAngle);

                GameObject newBullet = Instantiate(bulletPrefab, pos, Quaternion.identity);
                newBullet.transform.right = newBullet.transform.position - transform.position;

                if (newBullet.TryGetComponent<Projectile>(out Projectile projectile)) {
                    projectile.UpdateMoveSpeed(bulletMoveSpeed);
                }

                currentAngle += angleStep;

                if (stagger) {
                    yield return new WaitForSeconds(timeBetweenProjectiles);
                }
            }

            currentAngle = startAngle;

            if (!stagger) {
                yield return new WaitForSeconds(timeBetweenBursts);
            }
        }

        yield return new WaitForSeconds(restTime);
        isShooting = false;
    }

    private void TargetConeOfInfluence(out float startAngle, out float currentAngle, out float angleStep, out float endAngle) {
        Vector2 targetDirection = Player.Instance.transform.position - transform.position;
        float targetAngle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
        startAngle = targetAngle;
        endAngle = targetAngle;
        currentAngle = targetAngle;
        float halfAngleSpread = 0f;
        angleStep = 0f;

        if (angleSpread != 0) {
            angleStep = angleSpread / (projectilesPerBurst - 1);
            halfAngleSpread = angleSpread / 2;
            startAngle = targetAngle - halfAngleSpread;
            endAngle = targetAngle + halfAngleSpread;
            currentAngle = startAngle;
        }
    }

    private Vector2 FindBulletSpawnPos(float currentAngle) {
        float x = transform.position.x + startingDistance * Mathf.Cos(currentAngle * Mathf.Deg2Rad);
        float y = transform.position.y + startingDistance * Mathf.Sin(currentAngle * Mathf.Deg2Rad);

        Vector2 pos = new Vector2(x, y);

        return pos;
    }
}
