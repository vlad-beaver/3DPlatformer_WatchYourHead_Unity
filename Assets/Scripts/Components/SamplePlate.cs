using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class SamplePlate : PuzzleInteractable
    {
        [SerializeField]
        private int _reward;

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<PuzzlePlayer>() == null)
            {
                return;
            }

            Activate();
        }

        public override void Activate()
        {
            PuzzleSystem.Notify(
                this,
                new PuzzleEventArgs<int>()
                {
                    Value = _reward,
                });
            _animator.SetTrigger(ActivateClip);
        }

        public override void Deactivate()
        {
            _animator.SetTrigger(DeactivateClip);
        }
    }
}
