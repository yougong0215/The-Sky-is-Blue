using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class DayAndNight : MonoBehaviour
    {
        [SerializeField] private float secondPerRealTimeSecond; //게임 내 1분 = 현실 1시간

        private bool isNight = false;

        void Start()
        {

        }

        void Update()
        {
            transform.Rotate(Vector3.right, 0.1f * secondPerRealTimeSecond * Time.deltaTime);
        }
    }
}
