using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using View.UI;

namespace View.Managers
{
    public class GameManagerView : MonoBehaviour
    {
        [SerializeField] private PlayerManagerView playerManagerView;
        [SerializeField] private GameCanvasView gameCanvasView;
        [SerializeField] private PlayerEndLapView playerEndLapView;
        [SerializeField] private GhostManagerView ghostManagerView;
        [SerializeField] private Transform startTransform;

        private const float StartRacingTime = 3f;
        private int currentLap = 0;
        private Coroutine timerCoroutine;
        public int MaxRacingLaps { get; private set; } = 3;
        
        private void Start()
        {
            playerManagerView.SetMove(false);
            playerEndLapView.SubscribeOnPlayerEndLap(OnPlayerEndLap);
            gameCanvasView.SetRaceLapText($"{currentLap}", $"{MaxRacingLaps}");
        }

        public void StartRacingTimer()
        {
            if (timerCoroutine != null)
            {
                StopCoroutine(timerCoroutine);
                timerCoroutine = null;
            }
            
            playerManagerView.GetRecorder().Record();
            ghostManagerView.StartReplay();
            
            gameCanvasView.SetRaceLapText($"{currentLap}", $"{MaxRacingLaps}");
            gameCanvasView.SetActiveTimer(true);
            
            timerCoroutine = StartCoroutine(StartRacingTimerCoroutine());
        }
        
        private void OnPlayerEndLap()
        {
            currentLap++;
            if (currentLap >= MaxRacingLaps)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                return;
            }
            
            playerManagerView.SetMove(false);
            ghostManagerView.StopReplay();
            ghostManagerView.ClearNodeFrames();
            
            playerManagerView.SetIdentityPosition(startTransform);
            playerManagerView.SetIdentityRotation();
            SetStartPosition(ghostManagerView.GetPlayerManager().GetPlayerGameObject());

            var playerRecorder = playerManagerView.GetRecorder();
            var playerRootNode = playerRecorder.GetRootNode();
            
            playerRecorder.StopRecording();
            ghostManagerView.CopyGhostNode(playerRootNode);
            playerRecorder.ClearNodeFrames();
            
            StartRacingTimer();
        }

        private IEnumerator StartRacingTimerCoroutine()
        {
            var startTime = StartRacingTime;
            
            while (startTime >= 0)
            {
                gameCanvasView.SetTimerText($"{startTime}");
                yield return new WaitForSecondsRealtime(1f);
                startTime -= 1;
            }
            
            gameCanvasView.SetActiveTimer(false);
            playerManagerView.SetMove(true);
        }
        
        private void SetStartPosition(GameObject moveGameObject)
        {
            moveGameObject.transform.rotation = Quaternion.identity;
            moveGameObject.transform.localRotation = Quaternion.identity;
            moveGameObject.transform.position = startTransform.position;
        }
    }
}