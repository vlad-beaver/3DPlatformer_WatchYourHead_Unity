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
    private Rigidbody _headAlive;
    [SerializeField]
    private Rigidbody _headDead;
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

    private bool _isForce = false;
    private Vector3 _checkPointPos;
    [SerializeField]
    private float _forceSpeed = 1.1f;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _collider = GetComponent<Collider>();

        PuzzlePlayer.Instance.HasHead.OnValueChanged += (_, newValue) =>
        {
            _collider.enabled = !newValue;
            _renderer.material.DOColor(newValue ? _safeColor : _dangerColor, "_Emission", 1f);
        };
        _checkPointPos = _checkpointPlayer.position;
        _checkPointPos.y = PuzzlePlayer.Instance.transform.position.y;
    }

    private void Update()
    {
        if (_isForce)
        {
            PuzzlePlayer.Instance.transform.position = Vector3.Lerp(PuzzlePlayer.Instance.transform.position, _checkPointPos, Time.fixedDeltaTime * _forceSpeed);
            Debug.Log(Vector3.Distance(PuzzlePlayer.Instance.transform.position, _checkPointPos));
            if (Vector3.Distance(PuzzlePlayer.Instance.transform.position, _checkPointPos) < 4.5f)
            {
                _isForce = false;
            }
        }
    }

    public override void Activate()
    {
        base.Activate();


        if (PuzzlePlayer.Instance.HasHead)
        {
            return;
        }

        _displayAnimator.Play("OnComplete");

        _isForce = true;
        //var cached = _checkpointPlayer.position;
        //cached.y = PuzzlePlayer.Instance.transform.position.y;
        //PuzzlePlayer.Instance.transform.position = cached;
        //PuzzlePlayer.Instance.transform.DOMove(cached, 1f);
        if (PuzzlePlayer.Instance.HeadDead)
        {
            _head = _headDead;
        }
        else
        {
            _head = _headAlive;
        }
        //_head.isKinematic = true;
        //_head.DOMove(_checkpointHead.position, 1f);
        //_head.isKinematic = false;
        _head.isKinematic = true;
        _head.transform.position = _checkpointHead.transform.position;
        _head.transform.rotation = _checkpointHead.transform.rotation;
        _head.isKinematic = false;
    }
}
