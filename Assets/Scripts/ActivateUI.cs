using UnityEngine;

public class ActivateUI : MonoBehaviour
{
    [SerializeField] GameObject _uiToLoad;

    public void LoadUI()
    {
        _uiToLoad.SetActive(true);
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
    }
}
