using System.Collections.Generic;
using LNE.Core;
using UnityEngine;
using UnityEngine.Pool;

namespace LNE.Combat.Abilities
{
  public abstract class BoatAbilitiesPresenter : MonoBehaviour
  {
    public string Id { get; protected set; }

    [SerializeField]
    protected List<AbilityData> _abilityDataList;

    protected Character _character;

    protected List<IObjectPool<Projectile>> _projectilePools =
      new List<IObjectPool<Projectile>>();

    protected BoatAbilitiesView _view;
    protected BoatAbilitiesModel _model;

    public Vector3 Origin => _character.Position;

    protected virtual void Awake()
    {
      Id = gameObject.GetInstanceID().ToString();
      _character = GetComponent<Character>();
    }

    protected virtual void Start()
    {
      _model = new BoatAbilitiesModel();

      for (int i = 0; i < _abilityDataList.Count; ++i)
      {
        AbilityData abilityData = _abilityDataList[i];

        _projectilePools.Add(abilityData.InitProjectilePool());
      }
    }

    protected virtual void Update()
    {
      _model.CoolDownAbilities();
    }

    public Vector3 FindAbilitySpawnPosition(string abilityName)
    {
      return _character.AbilitySpawnPosition;
    }

    public Vector3 GetCurrentVelocity()
    {
      return gameObject.GetComponent<Rigidbody>().velocity;
    }

    public float PlayAudioClip(AudioClip audioClip)
    {
      return _view.PlayAudioClip(audioClip);
    }
  }
}
