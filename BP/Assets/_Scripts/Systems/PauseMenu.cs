using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Threading.Tasks;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    #region Properties
    [SerializeField] private Canvas pauseCanvas;
    [SerializeField] private Canvas loadingMenu;
    private bool inPauseMenu = false;
    private long timeScale;
    #endregion
    private void Update()
    {
        PauseGame();
    }

    private void PauseGame()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !inPauseMenu) PauseSim();
        else if (Input.GetKeyDown(KeyCode.Q) && inPauseMenu) ResumeSim();
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
        if (inPauseMenu)
        {
            // set cursor
            PlayerController.Instance.SetCursorBasedOnCam();
            // resume time scale
            MainTimeController.Instance.StellarTimeScale = timeScale;
            // disable canvas and enable player movement
            PlayerController.Instance.InMenu = false;
            pauseCanvas.gameObject.SetActive(false);
            inPauseMenu = false;
        }
    }

    public void PauseSim()
    {
        if (!inPauseMenu)
        {
            // set the cursor to visible
            Cursor.lockState = CursorLockMode.Confined;
            Cursor.visible = true;
            // pause the time scale
            timeScale = MainTimeController.Instance.StellarTimeScale;
            MainTimeController.Instance.StellarTimeScale = 0;
            // set canvas active and disable player movement
            PlayerController.Instance.InMenu = true;
            pauseCanvas.gameObject.SetActive(true);
            inPauseMenu = true;
        }
    }
}
