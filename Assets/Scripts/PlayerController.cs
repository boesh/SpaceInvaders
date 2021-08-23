using UnityEngine;

public class PlayerController : MonoBehaviour
{
    /// <summary>
    /// Settings
    /// </summary>
    [SerializeField, Range(.1f, 50f)] private float smooth = 10f;
    [SerializeField, Range(.1f, 5f)] private float moveSpeed = 10f;
    [SerializeField, Range(1f, 10, order = 5)] private int hp = 5;

    /// <summary>
    /// target postion
    /// </summary>
    private float targetPositionX;

    [SerializeField] private BulletPool bulletPool;

    /// <summary>
    /// Get inputs for set target player position, if target position in borders change player position
    /// </summary>
    private void PlayerInputs()
    {
        if (Input.GetKey(KeyCode.A))
        {
            if (targetPositionX > -Settings.Instance.borders)
            {
                targetPositionX -= moveSpeed * Time.fixedDeltaTime;
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            if (targetPositionX < Settings.Instance.borders)
            {
                targetPositionX += moveSpeed * Time.fixedDeltaTime;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }

    public void TakeDamage(int damage)
    {
        hp -= damage;
        if (hp < 1)
        {
            Settings.Instance.Lose();
        }
    }

    private void Fire()
    {
        BulletController bullet = bulletPool.GetObjectFromPool();
        bullet.transform.position = transform.position;
        bullet.bulletType = BulletType.Player;
    }

    private void Update()
    {
        PlayerInputs();
    }

    private void FixedUpdate()
    {
        transform.position += (new Vector3(targetPositionX, transform.position.y) - transform.position) * Time.deltaTime * smooth; /// smooth player moving
    }
}


