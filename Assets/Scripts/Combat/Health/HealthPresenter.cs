using LNE.Combat.Trophies;
using UnityEngine;

namespace LNE.Combat
{
  public class HealthPresenter : MonoBehaviour
  {
    [SerializeField]
    private float _maxHealth = 100;

    [SerializeField]
    private float _currentHealth;

    [SerializeField]
    private HealthView _view;

    private void Start()
    {
      _currentHealth = _maxHealth;
    }

    public void TakeDamage(float damage)
    {
      _currentHealth -= damage;
      if (_currentHealth <= 0)
      {
        _currentHealth = 0;
        Die();
      }
      _view.SetHealthSliderValue(_currentHealth / _maxHealth);
    }

    private void Die()
    {
      if (TryGetComponent(out SpawnTrophyOnDeath trophySpawner))
      {
        trophySpawner.SpawnTrophy();
      }

      Destroy(gameObject);
    }
  }
}
