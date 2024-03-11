using UnityEngine;

namespace LNE.Combat.Abilities
{
  public abstract class WatercraftAbilitiesView : MonoBehaviour
  {
    protected AudioSource _audioSource;

    protected virtual void Awake()
    {
      _audioSource = gameObject.AddComponent<AudioSource>();
    }

    public float PlayAudioClip(AudioClip audioClip)
    {
      _audioSource.PlayOneShot(audioClip);
      return audioClip.length;
    }
  }
}
