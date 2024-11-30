using UnityEngine;

namespace View.Managers
{
    public class ReplayRecorderManager : IGhostManager
    {
        public GhostNodeView RootGhostNodeView { get; protected set; }
        public bool IsStarted { get; protected set; }
        private float startRecordTime = 0f;

        public void StartRecording(GhostNodeView rootNode)
        {
            RootGhostNodeView = rootNode;
            Debug.Log($"Starting recording for {RootGhostNodeView.gameObject.name}");
            startRecordTime = 0f;
            IsStarted = true;
        }

        public void StopRecording()
        {
            IsStarted = false;
            Debug.Log($"Stop recording for {RootGhostNodeView.gameObject.name}. Time = {startRecordTime}");
        }

        public void Execute()
        {
            startRecordTime += Time.smoothDeltaTime * 1000;
            RootGhostNodeView.RecordFrame(Time.smoothDeltaTime);
        }
    }
}