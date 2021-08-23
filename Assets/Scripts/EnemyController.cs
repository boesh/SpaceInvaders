using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] EnemiesController enemiesController;
    private int deathCost;
    private int hp;
    public int rowIndex;
    public int index;
    public bool inFrontline = false;

    public void EnemySettings(EnemiesController enemies, int _hp, int _rowIndex, int _index)
    {
        enemiesController = enemies;
        hp = _hp;
        deathCost = 10 * hp;
        rowIndex = _rowIndex;
        index = _index;
    }

    public void GetDamage(int damage)
    {
        hp -= damage;
        if (hp < 1)
        {
            Settings.Instance.UpdateScore(deathCost);
            enemiesController.DecreaseEnemyCount();
            enemiesController.ChangeEnemyInFrontline(rowIndex, index);
            gameObject.SetActive(false);
        }
    }

    public void Fire()
    {
        BulletController bullet = BulletPool.Instance.GetObjectFromPool();
        bullet.transform.position = transform.position;
        bullet.gameObject.SetActive(true);
        bullet.bulletType = BulletType.Enemy;

    }

    private void FixedUpdate()
    {

        if (enemiesController.movingRight && transform.TransformPoint(transform.position).x > (Settings.Instance.borders))
        {
            enemiesController.movingRight = false;
            if (!enemiesController.moveDown)
            {
                enemiesController.moveDown = true;

            }
        }
        else if (!enemiesController.movingRight && -transform.TransformPoint(transform.position).x > (Settings.Instance.borders))
        {
            enemiesController.movingRight = true;

            if (!enemiesController.moveDown)
            {
                enemiesController.moveDown = true;
            }
        }
        if (transform.TransformPoint(transform.position).y < -3.3f)
        {
            Settings.Instance.Lose();
        }
    }
}
