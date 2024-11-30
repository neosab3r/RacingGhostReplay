using UnityEngine;

namespace View.Managers
{
    public class PlayerRecorderManagerView : MonoBehaviour
    {
        [SerializeField] private GhostNodeView rootNodeView;
        private ReplayRecorderManager replayRecorderManager;
        private bool isInitialized = false;
        
        public void Initialize()
        {
            if (rootNodeView == null)
            {
                Debug.LogError($"{nameof(PlayerRecorderManagerView)} has empty root node view. Initialize cannot continue.");
                return;
            }

            replayRecorderManager = new ReplayRecorderManager();
            rootNodeView.Init();
            isInitialized = true;
        }
        
        public void Record()
        {
            if (replayRecorderManager.IsStarted)
            {
                Debug.LogError($"{nameof(ReplayRecorderManager)} has already started recording.");
                return;
            }
            
            replayRecorderManager.StartRecording(rootNodeView);
        }

        public void StopRecording()
        {
            replayRecorderManager.StopRecording();
        }
        
        private void FixedUpdate()
        {
            if (replayRecorderManager.IsStarted)
            {
                replayRecorderManager.Execute();
            }
        }

        public GhostNodeView GetRootNode()
        {
            return rootNodeView;
        }

        public void ClearNodeFrames()
        {
            rootNodeView.ClearFrames();
        }
    }
}