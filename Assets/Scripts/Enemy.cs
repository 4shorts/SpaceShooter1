using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4f;

    private Player _player;
    private Animator _animator;
    private AudioSource _audioSource;
    private BoxCollider2D _boxCollider;

    private void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();
        _boxCollider = GetComponent<BoxCollider2D>();
        if ( _player == null )
        {
            Debug.LogError("The Player on enemy is NULL");
        }
        _animator = GetComponent<Animator>();
        if ( _animator == null )
        {
            Debug.LogError("Animator on Enemy is NULL");
        }
        if ( _audioSource == null )
        {
            Debug.LogError("AudioSource on Enemy is NULL");
        }
        if (_boxCollider == null )
        {
            Debug.LogError("The Enemy box collider is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -7f)
        {
            float randomX = Random.Range(-9f, 9f);
            transform.position = new Vector3(randomX, 7f, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
           
           if (player != null)
            {
                player.Damage();
            }
            _animator.SetTrigger("OnEnemyDeath");
            _speed = .5f;
            _audioSource.Play();
            _boxCollider.enabled = false;
            Destroy(this.gameObject, 2.8f);
            
        }

        if (other.tag == "Laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _animator.SetTrigger("OnEnemyDeath");
                _speed = .5f;
                _player.AddScore(10);
            }
            _boxCollider.enabled = false;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }
    }
}
