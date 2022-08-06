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
        [SerializeField]
        private bool isPress = false;

        private int _isJumpingHash;

        public override void Activate()
        {
            base.Activate();
            InputSystem.DisableAllEnabledActions();
            AnimationDieHandler();
            _deathSound.Play();
            PuzzlePlayer.Instance.Kill();
            StartCoroutine(DieEffects());
        }

        private void AnimationDieHandler()
        {
            _isJumpingHash = Animator.StringToHash("isJumping");
            bool isJumping = PuzzlePlayer.Instance.GetComponent<Animator>().GetBool(_isJumpingHash);
            // If having head
            if (PuzzlePlayer.Instance.HasHead.Value && !isPress)
            {
                PuzzlePlayer.Instance.GetComponent<Animator>().Play("DeadWithHead");
            }
            // If jumping or not on the ground
            else if (isJumping && !PuzzlePlayer.Instance.GetComponent<CharacterController>().isGrounded && !isPress)
            {
                PuzzlePlayer.Instance.GetComponent<Animator>().Play("DeadInJump");
            }
            // If trap is press
            else if (isPress && PuzzlePlayer.Instance.HasHead.Value)
            {
                return;
            }
            // If without head
            else
            {
                PuzzlePlayer.Instance.GetComponent<Animator>().Play("DeadWithoutHead");
            }
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
