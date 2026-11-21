using UnityEngine;
using UnityEngine.InputSystem;
using Game.Managers;

namespace Game.scripts
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class PlayerMovement : MonoBehaviour
	{
		[Header("Player Settings")]
		[SerializeField] private Rigidbody2D _rigidbody;
		[SerializeField] private PlayerAnimation _playerAnimation;
		
		[Header("Player Parametrs")]
		[SerializeField][Min(0f)] private float _moveSpeed = 4f;
		
		private Vector2 _inputDirection;

		private ArtifactItem _currentItem = null;
		private bool _isInteracting = false;
		private bool _canInteract = false;
		private bool _canMove = false;

		private void Awake()
		{
			if (_rigidbody == null)
			{
				_rigidbody = GetComponent<Rigidbody2D>();
			}

			if (_playerAnimation == null)
			{
				_playerAnimation = GetComponentInChildren<PlayerAnimation>();
			}

			_rigidbody.gravityScale = 0f;
			_rigidbody.freezeRotation = true;
			
			AllowMovement();
		}

		public void AllowMovement()
		{
			_canMove = true;
		}

		public void OnMove(InputAction.CallbackContext context)
		{
			_inputDirection = context.ReadValue<Vector2>();
			_playerAnimation.AnimateMovement(_inputDirection);
		}

		private void FixedUpdate()
		{
			_rigidbody.linearVelocity = _inputDirection.normalized * _moveSpeed;
		}

		private void OnDisable()
		{
			_inputDirection = Vector2.zero;

			if (_rigidbody != null)
			{
				_rigidbody.linearVelocity = Vector2.zero;
			}
		}
		
		private void OnTriggerEnter2D(Collider2D other)
		{
			ArtifactItem artifact = other.GetComponent<ArtifactItem>();
			if (artifact != null)
			{
				_currentItem = artifact;
				_canInteract = true;
				//Debug.Log(_currentItem);
				//Debug.Log(_currentItem.ArtifactName);
				//Debug.Log(_canInteract);
			}
		}
		private void OnTriggerExit2D(Collider2D other)
		{
			ArtifactItem artifact = other.GetComponent<ArtifactItem>();
			if (artifact != null)
			{
				_currentItem = null;
				_canInteract = false;
			}
		}
		

		public void SetInteracting(bool _isInteracting)
		{
			
			if (_canInteract & _isInteracting)
			{
				InteractionManager.Instance.InteractWithItem(_currentItem);
				_canMove = false;
			}
		}

		public void OnInteract(InputAction.CallbackContext context)
		{
			Debug.Log("OnInteract");
			_isInteracting = context.ReadValueAsButton();
			SetInteracting(_isInteracting);
			
		}
		
	}
}
