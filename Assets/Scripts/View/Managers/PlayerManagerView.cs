using Ashsvp;
using UnityEngine;

namespace View.Managers
{
    public class PlayerManagerView : MonoBehaviour
    {
        [SerializeField] private SimcadeVehicleController simcadeVehicleController;
        [SerializeField] private GameObject playerGameObject;
        [SerializeField] private PlayerRecorderManagerView playerRecorderManagerView;

        [SerializeField] private Rigidbody playerRigidbody;

        private void Awake()
        {
            playerRecorderManagerView.Initialize();
        }

        public void SetMove(bool isMove)
        {
            if (simcadeVehicleController == null)
            {
                return;
            }
            
            simcadeVehicleController.CanDrive = isMove;
            simcadeVehicleController.CanAccelerate = isMove;

            if (isMove)
            {
                simcadeVehicleController.rb.drag = 0f;
                simcadeVehicleController.rb.velocity = Vector3.zero;
            }
            else
            {
                //simcadeVehicleController.rb.drag = 1000f;
                simcadeVehicleController.rb.velocity = Vector3.zero;
            }
        }
        
        public void SetIdentityRotation()
        {
            simcadeVehicleController.enabled = false;
            playerRigidbody.MoveRotation(Quaternion.identity);
            playerRigidbody.rotation = Quaternion.identity;
            simcadeVehicleController.enabled = true;
        }

        public void SetIdentityPosition(Transform position)
        {
            simcadeVehicleController.enabled = false;
            playerRigidbody.MovePosition(position.position);
            simcadeVehicleController.enabled = true;
        }

        public PlayerRecorderManagerView GetRecorder()
        {
            return playerRecorderManagerView;
        }

        public GameObject GetPlayerGameObject()
        {
            return playerGameObject;
        }
    }
}