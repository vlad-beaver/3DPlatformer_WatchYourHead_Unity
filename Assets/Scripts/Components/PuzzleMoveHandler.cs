using Assets.Scripts.Infrastructure.Abstractables;
using DG.Tweening;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PuzzleMoveHandler : PuzzleComponent
    {
        [SerializeField]
        private Transform _moveTo;
        private Vector3 _initialPosition;

        [SerializeField]
        private float _duration;

        private void Awake()
        {
            _initialPosition = transform.position;
        }

        public override void Activate()
        {
            base.Activate();
            transform
                .DOMove(_moveTo.position, _duration)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Yoyo);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            transform.DOKill();
            transform.DOMove(_initialPosition, _duration);
        }
    }
}
