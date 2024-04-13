using UnityEngine;
using UnityEngine.AI;

namespace People__Students_
{
    public class AgentMovement : MonoBehaviour
    {
        private Vector3 _target;
        private NavMeshAgent _agent;
        private Camera _camera;
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }
        private void Start()
        {
            _camera = Camera.main;
        }
        
        private void Update()
        {
            SetTargetPosition();
            SetAgentPosition();
        }

        private void SetAgentPosition()
        {
            _agent.SetDestination(new Vector3(_target.x, _target.y, transform.position.z));
        }

        private void SetTargetPosition()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _target = _camera.ScreenToWorldPoint(Input.mousePosition);
            }
        }
    }
}
