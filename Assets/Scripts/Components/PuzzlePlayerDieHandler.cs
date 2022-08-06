using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Linq;

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
            InputSystem.DisableAllEnabledActions();
            PuzzlePlayer.Instance.GetComponent<Animator>().Play("Dead");
            _deathSound.Play();
            PuzzlePlayer.Instance.Kill();
            StartCoroutine(DieEffects());
        }

        IEnumerator DieEffects()
        {
            yield return new WaitForSeconds(0.5f);
            //FindObjectsOfType<ParticleSystem>().ToList().ForEach(ps => ps.Stop());
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            SceneManager.LoadScene("PlayerDie");
        }
    }
}
