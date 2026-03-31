using UnityEngine;

public class EnemyController : MonoBehaviour
{
         void Start()
    {
        TakeDamange(10);
    }

        public void TakeDamange(int damage)
    {
        Debug.Log("damage " + damage);
        return;
    }
}
