using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public void RestartGame()
    {
        StartCoroutine(RestartGameCorutine());
    }

    IEnumerator RestartGameCorutine()
    {
        yield return new WaitForSeconds(7.0f);
        SceneManager.LoadScene(0);
    }
}
