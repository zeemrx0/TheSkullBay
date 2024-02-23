using UnityEngine;

namespace LNE.Combat
{
  public class Health : MonoBehaviour
  {
    [SerializeField]
    private float _maxHealth = 100;

    [SerializeField]
    private float _currentHealth;

    private void Start()
    {
      _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
      _currentHealth -= damage;
      if (_currentHealth <= 0)
      {
        Die();
      }
    }

    private void Die()
    {
      Destroy(gameObject);
    }
  }
}
