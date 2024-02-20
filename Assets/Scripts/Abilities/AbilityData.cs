using LNE.Inputs;
using UnityEngine;

namespace LNE.Abilities
{
  [CreateAssetMenu(
    fileName = "_AbilityData",
    menuName = "Abilities/Ability",
    order = 0
  )]
  public class AbilityData : ScriptableObject
  {
    [SerializeField]
    private TargetingStrategy _targetingStrategy;

    [SerializeField]
    private EffectStrategy _effectStrategy;

    public void Perform(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions
    )
    {
      AbilityModel abilityModel = new AbilityModel();

      _targetingStrategy.StartTargeting(
        playerBoatAbilitiesPresenter,
        playerInputActions,
        abilityModel,
        () =>
        {
          OnTargetAcquired(
            playerBoatAbilitiesPresenter,
            playerInputActions,
            abilityModel
          );
        }
      );
    }

    public void OnTargetAcquired(
      PlayerBoatAbilitiesPresenter playerBoatAbilitiesPresenter,
      PlayerInputActions playerInputActions,
      AbilityModel abilityModel
    )
    {
      _effectStrategy.StartEffect(
        playerBoatAbilitiesPresenter,
        playerInputActions,
        abilityModel
      );
    }
  }
}
