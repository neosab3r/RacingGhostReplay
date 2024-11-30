using UnityEngine;
using UnityEngine.UI;
using View.Managers;

namespace View.UI
{
    public class PreGameCanvasView : MonoBehaviour
    {
        [SerializeField] private Button startGameButton;
        [SerializeField] private GameCanvasView gameCanvasView;
        [SerializeField] private GameManagerView gameManagerView;

        private void Awake()
        {
            startGameButton.onClick.AddListener(OnStartGame);
        }

        private void OnStartGame()
        {
            gameCanvasView.gameObject.SetActive(true);
            gameManagerView.StartRacingTimer();
            gameObject.SetActive(false);
        }
    }
}