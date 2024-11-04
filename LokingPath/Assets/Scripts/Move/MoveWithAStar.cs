using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Scripts.HeroHealth;
using Assets.Scripts.PathFinder;
using UnityEngine;

namespace Assets.Scripts.Move
{
    internal class MoveWithAStar : MonoBehaviour, IMovable
    {
        [SerializeField]
        [Header("MoveSpeed")]
        [Range(0, 1)] private float _moveSpeed;

        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private FinishTrigger.FinishTrigger _finishTrigger;
        private HeroHealth.HeroHealth _heroHealth;

        private IReadOnlyList<Vector3> _route;

        private bool _canMove;
        private int _routeIndex;

        private void OnEnable()
        {
            _heroHealth = GetComponent<HeroHealth.HeroHealth>();

            _pathCreator.PathFounded += Init;
            _finishTrigger.GameFinished += FinishMove;
            _heroHealth.OnHeroDeaded += FinishMove;
            _heroHealth.OnHeroStandOnDangerZone += StopMoving;
            _finishTrigger.OnHeroStandOnFinish += StopMoving;
        }

        private void OnDisable()
        {
            _pathCreator.PathFounded -= Init;
            _finishTrigger.GameFinished -= FinishMove;
            _heroHealth.OnHeroDeaded -= FinishMove;
            _heroHealth.OnHeroStandOnDangerZone -= StopMoving;
            _finishTrigger.OnHeroStandOnFinish -= StopMoving;

        }

        private async void Init(IReadOnlyList<Vector3> route)
        {
            _route = route;
            await Task.Delay(2000);
            _canMove = true;
        }

        private void FixedUpdate()
        {
            if(_canMove)
               Move();
        }

        public void Move()
        {
            if (this.transform.position == _route[_route.Count - 1])
            {
                return;
            }

            if (this.transform.position == _route[_routeIndex]) _routeIndex++;

            this.transform.position = Vector3.MoveTowards(this.transform.position, _route[_routeIndex], _moveSpeed);
        }

        private void FinishMove()
        {
            this.transform.position = _route[0];
            _routeIndex = 0;
        }

        private void StopMoving() => _canMove = false;

        private void OnDrawGizmos()
        {
            if(_route == null) return;

            for(int i = 0; i < _route.Count; i++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(_route[i] + Vector3.up, new Vector3(0.9f, 0.9f, 0.9f));
            }
        }
    }
}
