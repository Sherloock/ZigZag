using UnityEngine;
using UnityEngine.SceneManagement;

public class ScorePanelHotkeys : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}
