using LNE.Inputs;
using LNE.Utilities.Constants;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace LNE.Combat.Abilities
{
  public class AbilityButton
    : MonoBehaviour,
      IPointerDownHandler,
      IPointerUpHandler,
      IDragHandler
  {
    [SerializeField]
    private FixedJoystick _joystick;

    [SerializeField]
    private GameObject _icon;

    [SerializeField]
    private GameObject _overlay;

    [SerializeField]
    private GameObject _cooldownTimeText;

    private AbilityData _abilityData;
    private PlayerWatercraftAbilitiesPresenter _playerWatercraftAbilitiesPresenter;
    private PlayerInputPresenter _playerInputPresenter;
    private IObjectPool<Projectile> _projectilePool;

    private AbilityModel _abilityModel;

    private bool _isPerforming = false;

    public void Init(
      AbilityData abilityData,
      PlayerWatercraftAbilitiesPresenter playerWatercraftAbilitiesPresenter,
      IObjectPool<Projectile> projectilePool,
      PlayerInputPresenter playerInputPresenter
    )
    {
      _abilityData = abilityData;
      _playerWatercraftAbilitiesPresenter = playerWatercraftAbilitiesPresenter;
      _projectilePool = projectilePool;
      _playerInputPresenter = playerInputPresenter;
    }

    private void Start()
    {
      _abilityModel = new AbilityModel();
    }

    public void SetIconActive(bool active)
    {
      _icon.SetActive(active);
    }

    public void SetIcon(Sprite icon)
    {
      _icon.GetComponent<Image>().sprite = icon;
    }

    public void SetCooldownTime(float remainingTime, float initialTime)
    {
      _overlay.GetComponent<Image>().fillAmount =
        initialTime == 0 ? 0 : (remainingTime / initialTime);

      _cooldownTimeText.GetComponent<TextMeshProUGUI>().text =
        initialTime == 0 ? "" : Mathf.Ceil(remainingTime).ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
      if (
        _playerWatercraftAbilitiesPresenter.GetAbilityCooldownRemainingTime(
          _abilityData
        ) > 0
      )
      {
        return;
      }

      _isPerforming = true;

      _joystick.gameObject.SetActive(true);
      _joystick.OnPointerDown(eventData);

      _abilityModel = new AbilityModel();
      _abilityData.Perform(
        _playerWatercraftAbilitiesPresenter,
        _playerInputPresenter,
        _joystick,
        _projectilePool,
        _abilityModel
      );
    }

    public void OnPointerUp(PointerEventData eventData)
    {
      if (!_isPerforming)
      {
        return;
      }

      _joystick.OnPointerUp(eventData);

      _abilityModel.IsPerformed = true;

      _joystick.gameObject.SetActive(false);
      _isPerforming = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      if (!_isPerforming)
      {
        return;
      }

      _joystick.OnDrag(eventData);
    }
  }
}
