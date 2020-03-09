using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int currentSceneIndex;
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    public void Fail()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
    public void Succeed()
    {
        SceneManager.LoadScene(currentSceneIndex+1);
    }
}
