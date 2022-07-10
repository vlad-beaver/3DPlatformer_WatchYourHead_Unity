using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PuzzleTriggerHandler : PuzzleComponent
    {
        [SerializeField]
        private PuzzleComponent _puzzleComponent = null;

        [SerializeField]
        private bool _onTriggerEnter;

        [SerializeField]
        private bool _onTriggerExit;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !_onTriggerEnter)
            {
                return;
            }

            Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player") || !_onTriggerExit)
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
            _puzzleComponent?.Activate();
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
            _puzzleComponent?.Deactivate();
            PuzzleSystem?.Notify(
                this,
                new PuzzleEventArgs<bool>()
                {
                    Value = IsActive,
                });
        }
    }
}
