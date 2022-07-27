using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Assets.Scripts.Components
{
    public class PuzzlePlayerDieHandler : PuzzleComponent
    {
        [SerializeField]
        private AudioSource _deathSound;
        [SerializeField]
        private GameObject _deathScreen;

        public override void Activate()
        {
            base.Activate();
            _deathSound.Play();
            //PuzzlePlayer.Instance.Kill();
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SceneManager.LoadScene("PlayerDie");
        }
    }
}
