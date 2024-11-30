using System;
using UnityEngine;

namespace View
{
    public class PlayerEndLapView : MonoBehaviour
    {
        private int countToTrigger = 2;
        private event Action OnPlayerEndLap;
        private int count = 0;
        public void SubscribeOnPlayerEndLap(Action callback)
        {
            OnPlayerEndLap += callback;
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag.Equals("Player"))
            {
                count++;
                if (count >= countToTrigger)
                {
                    OnPlayerEndLap?.Invoke();
                    count = 0;
                }
            }
        }
    }
}