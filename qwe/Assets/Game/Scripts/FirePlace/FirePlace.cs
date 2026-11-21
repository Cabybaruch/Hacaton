using UnityEngine;
using Game.scripts; 

public class FirePlace : MonoBehaviour
{
    [SerializeField] private bool _fire_active = false;
    [SerializeField] private Transform _Player_transform;
    [SerializeField]private float maxDistance = 5f;
    [SerializeField] private EnemyMovement enemymovement;
    private void FixedUpdate()
    {
        float sqrPlayerDistance = (_Player_transform.position - transform.position).sqrMagnitude;
        if (sqrPlayerDistance < (maxDistance))
			{
                enemymovement.hidden=true;
				_fire_active=true;
			}
        else enemymovement.hidden=false;
    }
}
