using UnityEngine;

namespace View.Managers
{
    public class GhostManagerView : MonoBehaviour
    {
        [SerializeField] private GhostNodeView rootNodeView;
        [SerializeField] private PlayerManagerView playerManagerView;
        private ReplayManager replayManager;
        private bool isInitialized = false;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            if (rootNodeView == null)
            {
                Debug.LogError($"{nameof(GhostManagerView)} has empty root node view. Initialize cannot continue.");
                return;
            }

            rootNodeView.Init();
            replayManager = new ReplayManager();
            isInitialized = true;
        }

        public PlayerManagerView GetPlayerManager()
        {
            return playerManagerView;
        }

        public void CopyGhostNode(GhostNodeView newRootNode)
        {
            Debug.Log($"Copy Replay frames from {newRootNode.gameObject.name} to {rootNodeView.gameObject.name}");
            rootNodeView.CopyData(newRootNode);
        }

        public void StartReplay()
        {
            if (replayManager.IsStarted)
            {
                Debug.LogError($"{nameof(ReplayManager)} has already started replay.");
                return;
            }

            replayManager.Start(rootNodeView);
        }

        public void StopReplay()
        {
            replayManager.Stop();
        }

        private void FixedUpdate()
        {
            if (replayManager.IsStarted)
            {
                replayManager.Execute();
            }
        }
        
        public void ClearNodeFrames()
        {
            rootNodeView.ClearFrames();
        }
    }
}