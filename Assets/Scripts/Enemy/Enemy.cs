using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float startingSpeed;
    private float moveSpeed;
    [SerializeField] private float maxHealth;
    private float currentHealth;
    [SerializeField] private int moneyDropped;
    [SerializeField] private GameObject deathEffect;
    [SerializeField] private float deathEffectDuration;

    private EnemyHealthBar healthBar;

    private float lifeTime;
    public float LifeTime { get => lifeTime; }
    public float MoveSpeed { get => moveSpeed; set {  moveSpeed = value; } }
    public float StartingSpeed { get => startingSpeed; set {  startingSpeed = value; } }

    private void Start()
    {
        healthBar = GetComponentInChildren<EnemyHealthBar>();

        moveSpeed = startingSpeed;
        currentHealth = maxHealth;

        healthBar.UpdateHealthBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        lifeTime += Time.deltaTime * moveSpeed;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        healthBar.UpdateHealthBar(currentHealth, maxHealth);

        if (currentHealth <= 0)
            Die();
    }

    public void Slow(float amount)
    {
        moveSpeed = startingSpeed * (1f - amount);
    }

    private void Die()
    {
        PlayerStats.Money += moneyDropped;
        WaveSpawner.EnemiesAlive--;
        
        GameObject deathParticles = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathParticles, deathEffectDuration);

        Destroy(gameObject);
    }
}
