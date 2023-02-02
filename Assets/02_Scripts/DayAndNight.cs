using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class DayAndNight : MonoBehaviour
    {
        [SerializeField] private float secondPerRealTimeSecond; //���� �� 1�� = ���� 1�ð�

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
