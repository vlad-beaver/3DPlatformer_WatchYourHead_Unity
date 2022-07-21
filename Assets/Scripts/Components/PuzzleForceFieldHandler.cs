using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

public class PuzzleForceFieldHandler : PuzzleComponent
{
    [SerializeField]
    private Animator _characterAnimator;
    [SerializeField]
    private Animator _displayAnimator;
    [SerializeField]
    private Transform _checkpointRobot;
    [SerializeField]
    private Rigidbody _head;
    [SerializeField]
    private Transform _checkpointHead;
    private int _isHavingHeadHash;

    private void Awake()
    {

    }

    public override void Activate()
    {
        base.Activate();
        _isHavingHeadHash = Animator.StringToHash("isHavingHead");
        bool _isHavingHead = _characterAnimator.GetBool(_isHavingHeadHash);
        if (_isHavingHead)
        {
            Debug.Log("!!!Access approved!!!\nRobot is having head");
        }
        else
        {
            // Turn on display animation
            _displayAnimator.Play("OnComplete");
            // Spawn player and head
            PuzzlePlayer.Instance.transform.position = _checkpointRobot.position;
            _head.transform.position = _checkpointHead.position;
            Debug.Log("!!!Access denied!!!\nRobot is not having head or body");
        }

    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
