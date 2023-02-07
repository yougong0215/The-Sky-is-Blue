using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace JH
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class SelectableUnit : MonoBehaviour
    {
        private NavMeshAgent Agent;
        [SerializeField]
        private SpriteRenderer SelectionSprite;

        private void Awake()
        {
            SelectManager.Instance.AvailableUnit.Add(this);
            Agent = GetComponent<NavMeshAgent>();
        }

        public void MoveTo(Vector3 Position)
        {
            Agent.SetDestination(Position);
        }

        public void OnSelected()
        {
            SelectionSprite.gameObject.SetActive(true);
        }

        public void OnDeselected()
        {
            SelectionSprite.gameObject.SetActive(false);
        }
    }
}
