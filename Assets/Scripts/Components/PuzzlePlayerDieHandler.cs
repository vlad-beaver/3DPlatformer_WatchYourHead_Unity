using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components
{
    public class PuzzlePlayerDieHandler : PuzzleComponent
    {
        [SerializeField]
        private AudioSource _deathSound;

        public override void Activate()
        {
            base.Activate();
            _deathSound.Play();
            PuzzlePlayer.Instance.Kill();
        }
    }
}
