using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace dummy
{
    public class CameraFollower : MonoBehaviour
    {
        private Transform target;
        private Vector3 offset;
        [Range(1, 10)]
        [SerializeField]
        private float smoothFactor;
        // Start is called before the first frame update
        private void Awake()
        {
            offset = new Vector3(0, 0, -10);
            target = GameObject.Find("player").transform;
        }

        private void FixedUpdate()
        {
            Follow();
        }

        private void Follow()
        {
            var targetPosition = target.position + offset;
            var smoothedMove = Vector3.Lerp(transform.position, targetPosition, smoothFactor * Time.fixedDeltaTime);

            transform.position = smoothedMove;
        }

        private void OnDrawGizmosSelected()
        {
            
        }
    }
}