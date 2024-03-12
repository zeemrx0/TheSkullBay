using System;
using System.Collections;
using LNE.Combat.Loots;
using UnityEngine;

namespace LNE.Combat
{
  public class HealthPresenter : MonoBehaviour
  {
    public event Action OnDie;

    [SerializeField]
    private float _maxHealth = 100;

    [SerializeField]
    private float _currentHealth;

    private HealthView _view;

    private void Awake()
    {
      _view = GetComponent<HealthView>();
    }

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
        StartDie();
      }
      _view.SetHealthSliderValue(_currentHealth / _maxHealth);
    }

    private void StartDie()
    {
      if (TryGetComponent(out SpawnLootOnDeath trophySpawner))
      {
        trophySpawner.SpawnTrophy();
      }

      _view.ShowOnDieVFX();
      float time = _view.PlayOnDieAudioClip();

      OnDie?.Invoke();

      StartCoroutine(DieCoroutine(time));
    }

    protected virtual IEnumerator DieCoroutine(float delayTime)
    {
      TryGetComponent(out Collider c);
      if (c != null)
      {
        c.enabled = false;
      }
      foreach (Transform child in transform)
      {
        child.gameObject.SetActive(false);
      }

      yield return new WaitForSeconds(delayTime);

      Destroy(gameObject);

      yield return null;
    }
  }
}
