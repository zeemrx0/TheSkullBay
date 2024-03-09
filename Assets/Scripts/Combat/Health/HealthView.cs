using LNE.Core;
using LNE.Utilities.Constants;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.Combat
{
  public class HealthView : MonoBehaviour
  {
    [SerializeField]
    private VFX _onDieVFX;

    [SerializeField]
    private AudioClip _onDieAudioClip;

    private Slider _slider;
    private AudioSource _audioSource;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();
      _slider = transform
        .GetComponentInChildren<Vehicle>()
        .transform.Find(GameObjectName.WatercraftCharacterInfoCanvas)
        .Find(GameObjectName.HealthBar)
        .GetComponent<Slider>();
    }

    public void SetHealthSliderValue(float value)
    {
      _slider.value = value;
    }

    public void ShowOnDieVFX()
    {
      Vector3? origin = TryGetComponent<Character>(out Character boat)
        ? boat.Position
        : null;

      VFX vfx = Instantiate(
        _onDieVFX,
        origin ?? transform.position,
        Quaternion.identity
      );

      Destroy(vfx.gameObject, vfx.Duration);
    }

    public float PlayOnDieAudioClip()
    {
      if (_onDieAudioClip != null)
      {
        _audioSource.PlayOneShot(_onDieAudioClip);
        return _onDieAudioClip.length;
      }

      return 0;
    }
  }
}
