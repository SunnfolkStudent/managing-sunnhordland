using UnityEngine;
using UnityEngine.AI;

namespace NavMeshPlus_master.NavMeshPlus_master
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        private NavMeshAgent _agent;
        // Start is called before the first frame update
        private void Start()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        // Update is called once per frame
        void Update()
        {
            _agent.SetDestination(target.position);
        }
    }
}
