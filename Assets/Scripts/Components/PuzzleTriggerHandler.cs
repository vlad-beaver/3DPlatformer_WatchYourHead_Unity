using System.Collections.Generic;
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

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player") || !OnTriggerEnterEnabled)
            {
                return;
            }

            Activate();
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player") || !OnTriggerExitEnabled)
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
