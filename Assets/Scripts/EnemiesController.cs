using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiesController : MonoBehaviour
{
    [SerializeField] private EnemyController enemyPrefab;
    [SerializeField] private Transform columnPrefab;
    [SerializeField] private Vector2 startPos;

    private List<Transform> columns;
    private List<EnemyController> enemies;
    private List<EnemyController> enemiesFrontline;

    [SerializeField] private float timeBetweenEnemiesMove = .2f;
    [SerializeField] private float timeBetweenEnemyShots = 3f;

    private int currentColumnIndex = 0;

    private int currentEnemiesCount;
    private const int columnsCount = 5;
    public const int rowsCount = 11;
    private int enemiesFrontlineCount = 11;

    public bool movingRight = true;
    public bool moveDown = false;

    private IEnumerator MoveEnemies()
    {
        int i = 0;
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenEnemiesMove);

            if (moveDown)
            {
                if (i < columnsCount)
                {
                    columns[i].position += Vector3.down;
                    ++i;
                }
                else
                {
                    moveDown = false;
                    i = 0;
                }
            }
            else
            {
                if (movingRight)
                {
                    columns[currentColumnIndex].position += Vector3.right / 10f;
                }
                else
                {
                    columns[currentColumnIndex].position += Vector3.left / 10f;
                }
            }
            if (currentColumnIndex < columns.Count - 1)
            {
                ++currentColumnIndex;
            }
            else
            {
                currentColumnIndex = 0;
            }
        }
    }

    private IEnumerator Fire()
    {
        for (; ; )
        {
            yield return new WaitForSeconds(timeBetweenEnemyShots);
            int randomIndex = Random.Range(0, enemiesFrontlineCount);
            enemiesFrontline[randomIndex].Fire();
        }
    }

    public void ChangeEnemyInFrontline(int rowIndex, int index)
    {
        if (enemies[index].inFrontline)
        {
            if (index + 1 % columnsCount != 0 && index + 1 < enemies.Count)
            {

                enemiesFrontline[rowIndex] = enemies[index + 1];
                enemiesFrontline[rowIndex].inFrontline = true;
            }
            else
            {
                --enemiesFrontlineCount;
                 for (int i = rowIndex; i < rowsCount; ++i)
                {
                    for (int j = index / rowIndex; j < columnsCount; ++j)
                    {
                        enemies[i * j].rowIndex--;
                    }
                }
                enemiesFrontline.RemoveAt(rowIndex);
            }
        }
    }

    private void IncreaseEnemySpeed()
    {
        if (currentEnemiesCount > 10)
        {
            timeBetweenEnemiesMove -= 0.002f;
        }
        else if (currentEnemiesCount < 5)
        {
            timeBetweenEnemiesMove -= 0.01f;
        }
        else if (currentEnemiesCount < 3)
        {
            timeBetweenEnemiesMove -= 0.03f;
        }
        else
        {
            timeBetweenEnemiesMove = 0.005f;
        }
    }

    public void DecreaseEnemyCount()
    {
        --currentEnemiesCount;

        if(currentEnemiesCount == 0)
        {
            Settings.Instance.Win();
        }
        IncreaseEnemySpeed();
    }

    private void SpawnEnemies()
    {
        int index = 0;
        for (int i = 0; i < rowsCount; ++i)
        {
            for (int j = 0; j < columnsCount; ++j)
            {
                if (i == 0)
                {

                    columns.Add(Instantiate(columnPrefab, transform));
                }

                EnemyController enemy = Instantiate(enemyPrefab, columns[j]);

                enemy.transform.position = new Vector3(i - rowsCount / 2f + .5f + startPos.x, j + startPos.y);
                enemy.EnemySettings(this, Random.Range(1, 3), i, index);
                if (j == 0)
                {
                    enemy.inFrontline = true;
                }
                index++;

                enemies.Add(enemy);
            }
        }
    }

    private void AddFrontline()
    {
        for (int i = 0; i < rowsCount; ++i)
        {
            enemiesFrontline.Add(enemies[i * columnsCount]);
        }
    }

    private void Awake()
    {
        currentColumnIndex = 0;
        enemiesFrontlineCount = rowsCount;

        columns = new List<Transform>();
        enemies = new List<EnemyController>();
        enemiesFrontline = new List<EnemyController>();
        SpawnEnemies();
        AddFrontline();
    }

    private void Start()
    {
        StartCoroutine(MoveEnemies());
        StartCoroutine(Fire());

        currentEnemiesCount = enemies.Count;
    }
}
