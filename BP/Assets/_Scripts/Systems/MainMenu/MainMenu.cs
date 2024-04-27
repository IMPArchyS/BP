using System.Threading.Tasks;
using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Canvas mainMenu;
    [SerializeField] private Canvas optionsMenu;
    [SerializeField] private Canvas epochMenu;
    [SerializeField] private Canvas loadingMenu;
    [SerializeField] private Animator bg;

    private bool startup = true;

    private void Start()
    {
        if (startup)
        {
            bg.Play("BackgroundFadeIn");
            startup = false;
        }
    }

    public async void Play()
    {
        mainMenu.gameObject.SetActive(false);
        loadingMenu.gameObject.SetActive(true);
        var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        scene.allowSceneActivation = false;
        do
        {
            await Task.Delay(100);
        } while (scene.progress < 0.9f);
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
