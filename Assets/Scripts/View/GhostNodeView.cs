using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace View
{
    public class GhostNodeView : MonoBehaviour
    {
        [SerializeField] private GameObject recordObject;
        public GhostNodeView Parent { get; protected set; }
        public List<ReplayFrame> Frames {get; protected set;}
        
        public List<GhostNodeView> childNodes = new();
        private Transform cachedObjectTransform;
        
        public void Init()
        {
            Frames = new List<ReplayFrame>();
            cachedObjectTransform = recordObject.transform;
            foreach (var frameNode in childNodes)
            {
                frameNode.Parent = this;
                frameNode.Init();
            }
        }

        public void CopyData(GhostNodeView newNodeView)
        {
            CopyFrames(newNodeView);
        }
        
        private void CopyFrames(GhostNodeView newNodeView)
        {
            Frames = newNodeView.Frames.ToList();
            for (var i = 0; i < newNodeView.childNodes.Count; i++)
            {
                var copyFrom = newNodeView.childNodes[i];
                var copyTo = childNodes[i];
                copyTo.CopyFrames(copyFrom);
            }
        }

        public void RecordFrame(float time)
        {
            var replayFrame = new ReplayFrame
            {
                Time = time,
                Position = cachedObjectTransform.localPosition,
                Rotation = cachedObjectTransform.localRotation
            };

            Frames.Add(replayFrame);

            foreach (var childNode in childNodes)
            {
                childNode.RecordFrame(time);
            }
        }

        public void ReplayFrame(int frameIndex, bool isChild = false)
        {
            var frame = Frames[frameIndex];
            if (isChild)
            {
                cachedObjectTransform.localPosition = Vector3.MoveTowards(cachedObjectTransform.localPosition, frame.Position, frame.Time);
                cachedObjectTransform.localRotation = Quaternion.RotateTowards(cachedObjectTransform.localRotation, frame.Rotation, frame.Time);
            }
            else
            {
                cachedObjectTransform.localPosition = frame.Position;
                cachedObjectTransform.localRotation = frame.Rotation;
            }
            
            foreach (var childNode in childNodes)
            {
                childNode.ReplayFrame(frameIndex, true);
            }
        }
        
        public void ClearFrames()
        {
            Frames.Clear();
            foreach (var childNode in childNodes)
            {
                childNode.ClearFrames();
            }
        }
    }
}