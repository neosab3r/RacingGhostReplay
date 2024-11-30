using TMPro;
using UnityEngine;

namespace View.UI
{
    public class GameCanvasView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI StartTimerText;
        [SerializeField] private TextMeshProUGUI RaceLapText;

        public void SetTimerText(string text)
        {
            StartTimerText.text = text;
        }

        public void SetActiveTimer(bool active)
        {
            StartTimerText.gameObject.SetActive(active);
        }

        public void SetRaceLapText(string currentLap, string maxLaps)
        {
            RaceLapText.text = $"Круг {currentLap}/{maxLaps}";
        }
    }
}