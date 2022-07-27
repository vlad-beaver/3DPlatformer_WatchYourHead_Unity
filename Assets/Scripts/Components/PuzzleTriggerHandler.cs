using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PuzzleTriggerHandler : PuzzleComponent
    {
        [SerializeField]
        private List<PuzzleComponent> _puzzleComponents = null;

        private bool OnTriggerEnterEnabled => _onTriggerEnter && IsEnable;

        [SerializeField]
        private bool _onTriggerEnter;

        private bool OnTriggerExitEnabled => _onTriggerExit && IsEnable;

        [SerializeField]
        private bool _onTriggerExit;

        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PuzzleEntity>() ==  null || !OnTriggerEnterEnabled)
            {
                return;
            }

            Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<PuzzleEntity>() ==  null || !OnTriggerExitEnabled)
            {
                return;
            }

            int colliders = Physics
                .OverlapBox(_collider.bounds.center, _collider.bounds.size * 0.5f, Quaternion.identity)
                .Select(_ => _.GetComponent<PuzzleEntity>())
                .Count(_ => _ != null);
            if (colliders != 0)
            {
                return;
            }

            Deactivate();
        }

        public override void Activate()
        {
            if (IsActive)
            {
                return;
            }

            base.Activate();
            _puzzleComponents.ForEach(_ => _.Activate());
            PuzzleSystem?.Notify(
                this,
                new PuzzleEventArgs<bool>()
                {
                    Value = IsActive,
                });
        }

        public override void Deactivate()
        {
            if (!IsActive)
            {
                return;
            }

            base.Deactivate();
            _puzzleComponents.ForEach(_ => _.Deactivate());
            PuzzleSystem?.Notify(
                this,
                new PuzzleEventArgs<bool>()
                {
                    Value = IsActive,
                });
        }
    }
}
