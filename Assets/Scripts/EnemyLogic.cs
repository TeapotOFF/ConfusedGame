using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyLogic : MonoBehaviour
{
    private AudioSource _zombyAudioSource;
    [SerializeField] private AudioClip _zombyAudioClip;
    [SerializeField] private GameObject _startRayPoint;
    [SerializeField] private GameObject _playerTarget;
    [SerializeField] private GameObject _player;
    private float _viewDistant = 15f;
    private Animator _animator;
    [SerializeField] private AnimationClip _dyingAnimation;
    private NavMeshAgent nav;
    [Range(0, 360)] public float viewAngle = 90f;
    private bool _inAttackArea = false;
    private bool _isAttacking = true;
    private bool _isDying = false;
    private bool _isAngry;
    public int _health = 3;
    private float _distantToPlayer;
    private float _attackDistant = 1f;
    private float _unsafeDistance = 4f;

    void Start()
    {
        _zombyAudioSource = GetComponent<AudioSource>();
        _animator = gameObject.GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (!_isDying)
        {
            Dying();
            _distantToPlayer = Vector3.Distance(_startRayPoint.transform.position, _playerTarget.transform.position);
            DrawView();
            EnemyView();
            CheckState();
        }
        else
            StartCoroutine(GameObjectDelete());
    }
    public void TakeDamege()
    {
        _health -= 1;
    }
    private void MoveToPlayer()
    {
        if (gameObject)
        {
            if (_distantToPlayer > _viewDistant)
            {
                _animator.SetBool("isRunning", false);
                _isAngry = false;
                nav.enabled = false;
            }
            if (_distantToPlayer <= _viewDistant)
            {
                if (!_zombyAudioSource.isPlaying)
                _zombyAudioSource.PlayOneShot(_zombyAudioClip);
                _animator.SetBool("isRunning", true);
                nav.enabled = true;
                nav.SetDestination(_playerTarget.transform.position);
            }
        }
    }
    private void CheckState()
    {
        if (_isAngry && !_inAttackArea)
        {
            MoveToPlayer();
        }
        else if (_inAttackArea && _isAttacking)
        {
            nav.enabled = false;
            _animator.SetBool("isRunning", false);
            _isAttacking = false;
            StartCoroutine(AttackCooldown());
        }
    }
    private void EnemyView()
    {
        if (_distantToPlayer <= _unsafeDistance && !_isDying)
        {
            _isAngry = true;
        }
        float angle = Vector3.Angle(_startRayPoint.transform.forward, _playerTarget.transform.position - _startRayPoint.transform.position);
        RaycastHit hit;
        if (Physics.Raycast(_startRayPoint.transform.position, _playerTarget.transform.position - _startRayPoint.transform.position, out hit, _viewDistant))
        {
            if (angle < viewAngle / 2f && _distantToPlayer <= _viewDistant && hit.collider.gameObject.tag == "Player" && !_isDying)
            {
                _isAngry = true;
            }
         
            if (angle < viewAngle / 2f && _distantToPlayer <= _attackDistant && hit.collider.gameObject.tag == "Player")
            {
                _inAttackArea = true;
            }
            else 
            {
                _inAttackArea = false;
            }
        } 
    }
    private void DrawView()
    {
        Vector3 left = _startRayPoint.transform.position + Quaternion.Euler(new Vector3(0, viewAngle / 2f, 0)) * (_startRayPoint.transform.forward * _viewDistant);
        Vector3 right = _startRayPoint.transform.position + Quaternion.Euler(-new Vector3(0, viewAngle / 2f, 0)) * (_startRayPoint.transform.forward * _viewDistant);
        Debug.DrawLine(_startRayPoint.transform.position,left, Color.red);
        Debug.DrawLine(_startRayPoint.transform.position, right, Color.red);
        Debug.DrawRay(_startRayPoint.transform.position, _playerTarget.transform.position - _startRayPoint.transform.position, Color.green);
    }
    private void Dying()
    {
        if (_health == 0)
        {
            _isDying = true;    
            _isAngry = false;
            nav.enabled = false;
            _animator.SetBool("isRunning", false);
            _animator.SetTrigger("isDying");
            gameObject.GetComponent<Rigidbody>().isKinematic = true;
            gameObject.GetComponent<CapsuleCollider>().enabled = false;   
        }
    }
    IEnumerator GameObjectDelete()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }
    IEnumerator AttackCooldown()
    {
        _player.GetComponent<Player>().TakeDamage();
        _animator.SetTrigger("isAttack");
        yield return new WaitForSeconds((_dyingAnimation.length - 1f) / 2);
        _isAttacking = true;
    }
}

