using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    [SerializeField] private string buttonOneScene;
    [SerializeField] private string buttonTwoScene;
    public void ButtonOne()
    {
        SceneManager.LoadScene(buttonOneScene);
    }
    public void ButtonTwo()
    {
        SceneManager.LoadScene(buttonTwoScene);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
