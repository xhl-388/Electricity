using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private int currentSceneIndex;
    public AudioClip succeedAudio;
    public AudioClip finalSuceedAudio;
    public AudioClip failAudio;
    private AudioSource audioSource;
    private GameObject playerA;
    private GameObject playerB;
    IEnumerator LoadNextScene()
    {
        yield return new WaitForSeconds(3f);
        if (currentSceneIndex != 6)
        {
            audioSource.PlayOneShot(succeedAudio);
        }
        else
        {
            audioSource.PlayOneShot(finalSuceedAudio);
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
    IEnumerator Restart()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(currentSceneIndex);
    }
    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        audioSource = GetComponent<AudioSource>();
        playerA = GameObject.Find("Player_A");
        playerB = GameObject.Find("Player_B");
    }
    public void Fail()
    {
        audioSource.PlayOneShot(failAudio);
        playerA.GetComponent<Player_A>().cantControl = true;
        playerB.GetComponent<Player_B>().cantControl = true;
        playerA.GetComponent<Animator>().SetBool("jump", false);
        playerA.GetComponent<Animator>().SetFloat("speed", 0f);
        playerA.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        playerB.GetComponent<Animator>().SetBool("jump_B", false);
        playerB.GetComponent<Animator>().SetFloat("speed_B", 0f);
        playerB.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
        StartCoroutine(Restart());
    }
    public void Succeed()
    {
        StartCoroutine(LoadNextScene());
    }
}
