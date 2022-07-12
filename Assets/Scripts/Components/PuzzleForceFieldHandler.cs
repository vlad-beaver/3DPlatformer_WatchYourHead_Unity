using Assets.Scripts.Infrastructure.Abstractables;
using UnityEngine;

public class PuzzleForceFieldHandler : PuzzleComponent
{
    [SerializeField]
    private CharacterController _character;
    [SerializeField]
    private Animator _characterAnimator;
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
            PuzzlePlayer.Instance.Kill();
            Debug.Log("!!!Access denied!!!\nRobot is not having head or body");
        }

    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
