using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Animations;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas optionsMenu;
    [SerializeField] private Canvas epochMenu;
    [SerializeField] private Canvas loadingMenu;
    [SerializeField] private Animator bg;

    private bool startup = true;
    private float target;

    private void Start()
    {
        if (startup)
        {
            bg.Play("BackgroundFadeIn");
            startup = false;
        }
    }
    private void Update()
    {
    }

    public async void Play()
    {
        mainMenu.gameObject.SetActive(false);
        loadingMenu.gameObject.SetActive(true);
        target = 0;
        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        scene.allowSceneActivation = false;
        do
        {
            await Task.Delay(100);
            target = scene.progress;
        } while (scene.progress < 0.9f);
        target = 1;
        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        loadingMenu.gameObject.SetActive(false);
    }

    public void DisableDelayed(GameObject g)
    {
        StartCoroutine(WaitForAnimationDisable(g));
    }
    public void EnableDelayed(GameObject g)
    {
        StartCoroutine(WaitForAnimationEnable(g));
    }

    private IEnumerator WaitForAnimationEnable(GameObject g)
    {
        yield return new WaitForSeconds(1f);
        if (g != null)
            g.SetActive(true);
    }

    public void Exit()
    {
        StartCoroutine(WaitForAnimationDisable(null));
        Application.Quit();
        Debug.Log("Quit");
    }

    private IEnumerator WaitForAnimationDisable(GameObject g)
    {
        yield return new WaitForSeconds(1f);
        if (g != null)
            g.SetActive(false);
    }
}
