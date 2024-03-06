using LNE.Core;
using UnityEngine;
using UnityEngine.UI;

namespace LNE.Combat
{
  public class HealthView : MonoBehaviour
  {
    [SerializeField]
    private Slider _slider;

    [SerializeField]
    private VFX _onDieVFX;

    [SerializeField]
    private AudioClip _onDieAudioClip;

    private AudioSource _audioSource;

    private void Awake()
    {
      _audioSource = GetComponent<AudioSource>();
    }

    public void SetHealthSliderValue(float value)
    {
      _slider.value = value;
    }

    public void ShowOnDieVFX()
    {
      Vector3? origin = TryGetComponent<Boat>(out Boat boat)
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
