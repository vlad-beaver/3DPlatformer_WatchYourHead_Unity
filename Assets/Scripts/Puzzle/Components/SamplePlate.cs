using Assets.Scripts.Puzzle.Abstractes;
using UnityEngine;

namespace Assets.Scripts.Puzzle.Components
{
    public class SamplePlate : PuzzleInteractable
    {
        public override void Activate()
        {
            base.Activate();
            PuzzleSystem.Notify(this);
        }

        public override void Deactivate()
        {
            throw new System.NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            Activate();
        }
    }
}
