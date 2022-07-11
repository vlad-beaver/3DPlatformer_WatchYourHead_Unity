using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class PuzzleTriggerHandler : PuzzleComponent
    {
        [SerializeField]
        private List<PuzzleComponent> _puzzleComponents = null;

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
