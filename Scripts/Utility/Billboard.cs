using UnityEngine;

namespace RPG.Utility
{
    public class Billboard : MonoBehaviour
    {
        private GameObject cam;

        private void Awake()
        {
            cam = GameObject.FindGameObjectWithTag(Constants.CAMERA_TAG);
        }

        private void LateUpdate()
        {
            Vector3 cameraDirection = transform.position + cam.transform.forward;
            transform.LookAt(cameraDirection);
        }

    }
}

// LateUpdate -- called after all Update functions have been called!!!