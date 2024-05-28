using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;

public class EndMenu : MonoBehaviour
{
    [SerializeField] private Canvas endMenuCanvas;
    [SerializeField] private Canvas loadingMenu;
    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button nextEpochButton;
    private int sceneIndex;
    private void Start()
    {
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    private void OnEnable()
    {
        Debug.Log("========================================");
        PlayerController.Instance.InMenu = true;
        PlayerController.Instance.InSubMenuOpen = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
        MainTimeController.Instance.StellarTimeScale = 0;
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
        if (sceneIndex + 1 < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log(sceneIndex);
            Debug.Log(SceneManager.sceneCountInBuildSettings);
            nextEpochButton.onClick.AddListener(() => LoadSceneWithIndex(sceneIndex + 1));
        }
        else
        {
            nextEpochButton.gameObject.SetActive(false);
        }
    }

    public async void LoadSceneWithIndex(int index)
    {
        endMenuCanvas.gameObject.SetActive(false);
        loadingMenu.gameObject.SetActive(true);
        PlayerPrefs.SetInt("epoch", index);
        var scene = SceneManager.LoadSceneAsync(index);
        scene.allowSceneActivation = false;
        do
        {
            await Task.Delay(100);
        } while (scene.progress < 0.9f);
        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        loadingMenu.gameObject.SetActive(false);
    }
}
