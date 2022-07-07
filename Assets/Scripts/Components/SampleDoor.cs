using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class SampleDoor : PuzzleExit
    {
        private Animator _animator;

        private int ActivateClip => Animator.StringToHash(_activateClipName);
        [SerializeField]
        private string _activateClipName;

        private int DeactivateClip => Animator.StringToHash(_deactivateClipName);
        [SerializeField]
        private string _deactivateClipName;


        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public override void Activate()
        {
            _animator.SetTrigger(ActivateClip);
        }

        public override void Deactivate()
        {
            _animator.SetTrigger(DeactivateClip);
        }
    }
}
