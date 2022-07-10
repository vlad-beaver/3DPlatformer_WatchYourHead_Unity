using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.Components
{
    [RequireComponent(typeof(Animator))]
    public class PuzzleAnimatorHandler : PuzzleComponent
    {
        private Animator _animator;

        private readonly int _onActiveParameterName = Animator.StringToHash("On Active");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public void ChangeOnActiveState(bool isActive)
            => _animator.SetBool(_onActiveParameterName, isActive);

        public override void Activate()
        {
            base.Activate();
            ChangeOnActiveState(IsActive);
        }

        public override void Deactivate()
        {
            base.Deactivate();
            ChangeOnActiveState(IsActive);
        }
    }
}
