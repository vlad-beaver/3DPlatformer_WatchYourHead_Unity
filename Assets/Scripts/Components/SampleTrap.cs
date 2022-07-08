using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

namespace Assets.Scripts.Components
{
    public class SampleTrap : PuzzleTrap
    {
        [SerializeField]
        private bool _isActive;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.gameObject.Equals(PuzzlePlayer.Instance.gameObject) || !_isActive)
            {
                return;
            }

            GameManager.Instance.KillPlayer();
        }

        public override void Activate()
        {
            _isActive = true;
        }

        public override void Deactivate()
        {
            _isActive = false;
        }
    }
}
