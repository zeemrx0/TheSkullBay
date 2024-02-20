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
        () => {
          OnTargetAcquired(abilityModel);
        }
      );
    }

    public void OnTargetAcquired(AbilityModel abilityModel){
      
    }
  }
}
