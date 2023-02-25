using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JH
{
    public class Attack : MonoBehaviour
    {
        [SerializeField] Transform UnitBody;
        [SerializeField] float range = 0f;
        [SerializeField] LayerMask layermask = 0;
        [SerializeField] float speenSpeed = 0f;
        [SerializeField] float fireRate = 0f;
        float currentFireRate;

        Transform target = null;

        private void SearchEnemy()
        {
            Collider[] cols = Physics.OverlapSphere(transform.position, range, layermask);
            Transform shortestTarget = null;

            if (cols.Length > 0)
            {
                float shortestDistance = Mathf.Infinity;
                foreach (Collider colTarget in cols)
                {
                    float distance = Vector3.SqrMagnitude(transform.position - colTarget.transform.position);
                    if (shortestDistance > distance)
                    {
                        shortestDistance = distance;
                        shortestTarget = colTarget.transform;
                    }
                }
            }

            target = shortestTarget;
        }

        void Start()
        {
            currentFireRate = fireRate;
            InvokeRepeating("SearchEnemy", 0f, 0.5f);
        }

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                Quaternion lookRotarion = Quaternion.LookRotation(target.position);
                Vector3 euler = Quaternion.RotateTowards(UnitBody.rotation, lookRotarion, speenSpeed * Time.deltaTime).eulerAngles;
                UnitBody.rotation = Quaternion.Euler(0, euler.y, 0);

                Quaternion fireRotation = Quaternion.Euler(0, lookRotarion.eulerAngles.y, 0);
                if (Quaternion.Angle(UnitBody.rotation, fireRotation) < 5f)
                {
                    currentFireRate -= Time.deltaTime;
                    if (currentFireRate <= 0)
                    {
                        currentFireRate = fireRate;
                        Debug.Log("발사");
                    }
                }
                Debug.Log("적 감지");
            }
        }
    }
}
