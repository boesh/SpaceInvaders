using UnityEngine;

public class BarrierController : MonoBehaviour
{
    [SerializeField] private int hp = 3;
    public void GetDamage(int damage)
    {
        hp -= damage;
        if (hp < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
