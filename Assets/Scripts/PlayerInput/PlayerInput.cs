using UnityEngine.InputSystem;

namespace LNE.Inputs
{
  public class PlayerInputPresenter
  {
    public bool IsInitialized { get; private set; } = false;

    private PlayerInputActions _playerInputActions;

    public PlayerInputActions GetPlayerInputActions()
    {
      if (_playerInputActions == null)
      {
        _playerInputActions = new PlayerInputActions();
        _playerInputActions.Boat.Enable();
      }

      return _playerInputActions;
    }

    public void Init()
    {
      if (IsInitialized)
      {
        return;
      }

      _playerInputActions = new PlayerInputActions();
      _playerInputActions.Boat.Enable();

      // Use ` key instead of Esc when in editor
#if UNITY_EDITOR
      string keyboardBackQuotePath = "<Keyboard>/BackQuote";
      string keyboardEscapePath = "<Keyboard>/Escape";

      _playerInputActions.Boat.Cancel.ApplyBindingOverride(
        keyboardBackQuotePath,
        path: keyboardEscapePath
      );
#endif

      IsInitialized = true;
    }
  }
}
