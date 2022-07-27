using System.Runtime.CompilerServices;
using Assets.Scripts.Infrastructure.Abstractables;
using DG.Tweening;
using UnityEngine;

public class PuzzleForceFieldHandler : PuzzleComponent
{
    [SerializeField]
    private Animator _displayAnimator;
    [SerializeField]
    private Transform _checkpointPlayer;
    [SerializeField]
    private Rigidbody _head;
    [SerializeField]
    private Transform _checkpointHead;

    [SerializeField]
    [ColorUsage(true, true)]
    private Color _dangerColor;
    [SerializeField]
    [ColorUsage(true, true)]
    private Color _safeColor;

    private Renderer _renderer;
    private Collider _collider;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();

        PuzzlePlayer.Instance.HasHead.OnValueChanged += (oldValue, newValue) =>
            _renderer.material.DOColor(PuzzlePlayer.Instance.HasHead ? _dangerColor : _safeColor, "_Emission", 1f);
    }

    private void Update()
    {
        _collider.enabled = !PuzzlePlayer.Instance.HasHead;
    }

    public override void Activate()
    {
        base.Activate();


        if (PuzzlePlayer.Instance.HasHead)
        {
            return;
        }

        _displayAnimator.Play("OnComplete");

        var cached = _checkpointPlayer.position;
        cached.y = PuzzlePlayer.Instance.transform.position.y;
        PuzzlePlayer.Instance.transform.DOMove(cached, 1f);
        _head.DOMove(_checkpointHead.position, 1f);
    }
}
