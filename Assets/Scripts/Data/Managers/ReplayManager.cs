using UnityEngine;

namespace View.Managers
{
    public class ReplayManager : IGhostManager
    {
        public GhostNodeView RootGhostNodeView { get; protected set; }
        public bool IsStarted { get; protected set; }
        private float startReplayTime = 0f;
        private int currentFrameIndex = 0;
        private int maxFrames = 0;

        public void Start(GhostNodeView rootNode)
        {
            RootGhostNodeView = rootNode;
            Debug.Log($"Starting Replay for {RootGhostNodeView.gameObject.name}");
            startReplayTime = 0f;
            currentFrameIndex = 0;
            maxFrames = RootGhostNodeView.Frames.Count;
            IsStarted = true;
        }

        public void Stop()
        {
            IsStarted = false;
            Debug.Log($"Stop replay for {RootGhostNodeView.gameObject.name}. Time = {startReplayTime}");
        }

        public void Execute()
        {
            if (currentFrameIndex < maxFrames)
            {
                startReplayTime += Time.smoothDeltaTime * 1000;
                RootGhostNodeView.ReplayFrame(currentFrameIndex);
                currentFrameIndex++;
            }
        }
    }
}