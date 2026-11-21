using UnityEngine;

namespace Game.scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class EnemyMovement : MonoBehaviour
	{
		public bool hidden= false;
		[Header("Enemy Settings")]
		[SerializeField] private Rigidbody2D _rigidbody;
		//[SerializeField] private enemyAnimation _enemyAnimation;
		
		[Header("Enemy Parametrs")]
		[SerializeField][Min(0f)] private float _moveSpeed = 4f;

		[Header("Waypoints")]

		[SerializeField] private GameObject[] waypoints; 
		[SerializeField] private Transform _Player_transform;
		[SerializeField] private bool _chase=false;
		private Vector2 _inputDirection;
		

		private void Awake()
		{
			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody2D>();
			}

			// if (_enemyAnimation == null)
			// {
			// 	_enemyAnimation = GetComponentInChildren<enemyAnimation>();
			// }

			_rigidbody.gravityScale = 0f;
			_rigidbody.freezeRotation = true;
		}

		private void FixedUpdate()
		{
			_rigidbody.linearVelocity = _inputDirection.normalized * _moveSpeed;
			Move();
		}

		private int _current_point=0;
		[SerializeField]private float maxDistance = 5f;
		
		
		async  void Move()
		{
			float sqrDistance = (waypoints[_current_point].transform.position - transform.position).sqrMagnitude;
			float sqrPlayerDistance = (_Player_transform.position - transform.position).sqrMagnitude;
			
			if (_chase)
			{
				transform.position = Vector2.MoveTowards(transform.position, _Player_transform.position, _moveSpeed * Time.deltaTime);
				if (sqrPlayerDistance > 200*(maxDistance) || hidden)
				{
					_chase=false;
					await Awaitable.WaitForSecondsAsync(2.5f);
				}
			}
			else
			{
				transform.position = Vector2.MoveTowards(transform.position, waypoints[_current_point].transform.position, _moveSpeed * Time.deltaTime);
				if (sqrDistance < maxDistance)
				{
					_current_point+=1;
					if(_current_point==waypoints.Length) _current_point=0;
				}
			}
			if (sqrPlayerDistance < 100*(maxDistance) && !hidden)
			{
				_chase=true;
			}
		}

		// public void OnMove(InputAction.CallbackContext context)
		// {
		// 	_inputDirection = context.ReadValue<Vector2>();
		// //	_enemyAnimation.AnimateMovement(_inputDirection);
		// }
		

		// private void OnDisable()
		// {
		// 	_inputDirection = Vector2.zero;

		// 	if (_rigidbody != null)
		// 	{
		// 		_rigidbody.linearVelocity = Vector2.zero;
		// 	}
		// }
	}
}
