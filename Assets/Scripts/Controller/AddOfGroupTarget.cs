using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cinemachine {
    public class AddOfGroupTarget : MonoBehaviour
    {
        public CinemachineTargetGroup test;
        public CinemachineVirtualCamera Camera;
        public bool activeOneCamera;

        void Start()
        {
            test.AddMember(gameObject.transform, 1, 0);
            activel();
        }

        void activel()
        {
            if (activeOneCamera)
            {
                test.gameObject.SetActive(false);
                Camera.Follow = gameObject.transform;
                Camera.LookAt = gameObject.transform;
            }
        }

        public void RemoveMe()
        {
            if (test)
            {
                test.RemoveMember(gameObject.transform);
                if (Camera)
                {
                    Camera.Follow = null;
                    Camera.LookAt = null;
                }
                test = null;
            }
        }

        private void OnDisable()
        {
            if (test)
            {
                test.RemoveMember(gameObject.transform);
            }
        }
    }
}
