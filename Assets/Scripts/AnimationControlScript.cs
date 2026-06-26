using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationControlScript : MonoBehaviour
{
    [SerializeField]
    private int nextSceneIndex = 1;

    public void TransicaoFinal()
    {
        LoadScene(nextSceneIndex);
    }

    private void LoadScene(int sceneIndex)
    {
        if (sceneIndex < 0 || sceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning($"Scene index {sceneIndex} is not configured in Build Settings.", this);
            return;
        }

        SceneManager.LoadScene(sceneIndex);
    }
}
