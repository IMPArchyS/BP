using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas loadingMenu;
    private bool inMenu;
    private long timeScale;
    private void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !inMenu)
        {
            PauseSim();
        }
        else if (Input.GetKeyDown(KeyCode.Q) && inMenu)
        {
            ResumeSim();
        }
    }

    public async void BackToMenu()
    {
        pauseCanvas.gameObject.SetActive(false);
        loadingMenu.gameObject.SetActive(true);
        var scene = SceneManager.LoadSceneAsync(0);
        scene.allowSceneActivation = false;
        do
        {
            await Task.Delay(100);
        } while (scene.progress < 0.9f);
        await Task.Delay(1000);
        scene.allowSceneActivation = true;
        loadingMenu.gameObject.SetActive(false);
    }

    public void ResumeSim()
    {
        if (inMenu)
        {
            // Time.timeScale = 1;
            PlayerController.Instance.SetCursorBasedOnCam();
            MainTimeController.Instance.StellarTimeScale = timeScale;
            PlayerController.Instance.CanMove = true;
            pauseCanvas.gameObject.SetActive(false);
            inMenu = false;
        }
    }

    public void PauseSim()
    {
        if (!inMenu)
        {
            // Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            timeScale = MainTimeController.Instance.StellarTimeScale;
            MainTimeController.Instance.StellarTimeScale = 0;
            PlayerController.Instance.CanMove = false;
            pauseCanvas.gameObject.SetActive(true);
            inMenu = true;
        }
    }
}
