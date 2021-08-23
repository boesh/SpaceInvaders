using System.Collections.Generic;
using UnityEngine;

public class BulletPool : Singleton<BulletPool>
{
    private List<BulletController> pool;
    [SerializeField] private BulletController prefab;

    BulletPool()
    {
        this.pool = new List<BulletController>();
    }

    public void AddObjectToPool()
    {
        pool.Add(Instantiate(prefab, transform));
    }

    public BulletController GetObjectFromPool()
    {

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeSelf)
            {
                pool[i].gameObject.SetActive(true);
                return pool[i];
            }
        }

        AddObjectToPool();
        return pool[pool.Count - 1];
    }
}
