using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EpochMenu : MonoBehaviour
{
    [SerializeField] private Transform currentMenu;
    [SerializeField] private Transform loadingMenu;

    public async void StartSimulationScene(int sceneIndex)
    {
        currentMenu.gameObject.SetActive(false);
        loadingMenu.gameObject.SetActive(true);
        var scene = SceneManager.LoadSceneAsync(sceneIndex);
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
