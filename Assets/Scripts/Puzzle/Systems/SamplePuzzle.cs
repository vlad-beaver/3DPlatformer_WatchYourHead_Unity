using Assets.Scripts.Puzzle.Abstractes;
using Assets.Scripts.Puzzle.Components;
using UnityEngine;

namespace Assets.Scripts.Puzzle.Systems
{
    public class SamplePuzzle : PuzzleSystem
    {
        [SerializeField]
        private int _goal = 1;
        private int _currentGoal = 0;

        public override void Notify(PuzzleInteractable sender)
        {
            if (sender is not SamplePlate plate)
            {
                return;
            }

            if (++_currentGoal >= _goal)
            {
                Complete();
            }
        }
    }
}
