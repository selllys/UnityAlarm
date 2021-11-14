using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    private const string Speed = "Speed";

    [SerializeField] private float _moveSpeed = 15f;

    private Animator _animator;
    private SpriteRenderer _sprite;

    private void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector2 moveDirection = new Vector2(horizontalAxis, 0);
        transform.Translate(moveDirection * _moveSpeed * Time.deltaTime);

        _animator.SetFloat(Speed, Mathf.Abs(horizontalAxis));
        _sprite.flipX = horizontalAxis < 0;
    }
}