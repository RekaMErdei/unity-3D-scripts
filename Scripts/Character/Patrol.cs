using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.AI;

namespace RPG.Character
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField] private GameObject splineGameObject;
        [SerializeField] private float walkDuration = 3f;
        [SerializeField] private float pauseDuration = 2f;

        private SplineContainer splineCmp;
        private NavMeshAgent agentCmp;

        private float splinePosition = 0f;
        private float splineLength = 0f;
        private float lengthWalked = 0f;
        private float walkTime = 0f;
        private float pauseTime = 0f;
        private bool isWalking = true;

        private void Awake()
        {
            if (splineGameObject == null)
            {
                Debug.LogWarning($"{name} does not have a spline.");
            }

            splineCmp = splineGameObject.GetComponent<SplineContainer>();
            splineLength = splineCmp.CalculateLength();
            agentCmp = GetComponent<NavMeshAgent>();
        }

        public Vector3 GetNextPosition()
        {
            return splineCmp.EvaluatePosition(splinePosition);
        }

        public void CalculateNextPosition()
        {
            walkTime += Time.deltaTime;

            if (walkTime > walkDuration)
            {
                isWalking = false;
            }

            if (!isWalking)
            {
                pauseTime += Time.deltaTime;

                if (pauseTime < pauseDuration)
                {
                    return;
                }

                ResetTimers();
            }

            lengthWalked += Time.deltaTime * agentCmp.speed;

            if (lengthWalked > splineLength)
            {
                lengthWalked = 0f;
            }

            splinePosition = Mathf.Clamp01(lengthWalked / splineLength);
        }

        public void ResetTimers()
        {
            pauseTime = 0f;
            walkTime = 0f;
            isWalking = true;
        }

        public Vector3 GetFartherOutPosition()
        {
            float tempSplinePosition = splinePosition + 0.02f;

            if (tempSplinePosition >= 1)
            {
                // tempSplinePosition = tempSplinePosition - 1;
                tempSplinePosition -= 1;
            }

            return splineCmp.EvaluatePosition(tempSplinePosition);
        }
    }
}

// Debug.LogWarning($"{name} does not have a spline."); -- Using Dollar sign to allow the string interpolation to check every enemy have a spline added or not
// splineCmp.EvaluatePosition(0); -- Zero represents the starting point on the spline, 1 represents the ending point on the spline
// Mathf.Clamp01 method prevents the value from being below 0or above 1
// if (!isWalking) -- ! means NOT isWalking
// crtl+D -- select all similar names method/ vareables... (at first double click on the wished selectable)
// GetFartherOutPosition() necessary that partolling enemies can rotate to the walking direction

/*
        public void CalculateNextPosition()
        {
            splinePosition += Time.deltaTime;
            // splinePosition = splinePosition +Time.deltaTime;

            if (splinePosition > 1f)
            {
                splinePosition = 0f;
            }

            print(splinePosition);
        }
*/        