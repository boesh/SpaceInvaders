using UnityEngine;

public enum BulletType
{
    Enemy,
    Player
}

public class BulletController : MonoBehaviour
{
    [SerializeField] private int damage = 1;
    [SerializeField] private float speed = 10f;
    [SerializeField] private float deathPoint = 10f;
    [SerializeField] public BulletType bulletType;

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "barrier")
        {
            if (bulletType == BulletType.Enemy)
            {
                other.gameObject.GetComponent<BarrierController>().GetDamage(damage);
            }
            gameObject.SetActive(false);
        }
        else if (other.transform.tag == "Player" && bulletType == BulletType.Enemy)
        {
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
            gameObject.SetActive(false);
        }
        else if (other.transform.tag == "enemy" && bulletType == BulletType.Player)
        {
            other.gameObject.GetComponent<EnemyController>().GetDamage(damage);
            gameObject.SetActive(false);
        }
    }

    private void LateUpdate()
    {
        if (transform.position.y > deathPoint)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (bulletType == BulletType.Player)
        {
            transform.position += Vector3.up * Time.deltaTime * speed;
        }
        else
        {
            transform.position -= Vector3.up * Time.deltaTime * speed;
        }
        
    }
}
