using UnityEngine;
using UnityEngine.SceneManagement;

public class FimDeJogoScript : MonoBehaviour
{
    private const string PlayerTag = "Player";

    [SerializeField]
    private int Level;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag(PlayerTag))
        {
            return;
        }

        LoadConfiguredLevel();
    }

    private void LoadConfiguredLevel()
    {
        if (Level < 0 || Level >= SceneManager.sceneCountInBuildSettings)
        {
            Debug.LogWarning($"Scene index {Level} is not configured in Build Settings.", this);
            return;
        }

        SceneManager.LoadScene(Level);
    }
}
