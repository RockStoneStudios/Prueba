using UnityEngine;

public class Bullet : MonoBehaviour
{
    private float speed = 3f;
    private Vector3 direction;

    public void setDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    void FixedUpdate()
    {
        transform.position += direction * speed * Time.deltaTime;
        speed += 1f;

        Collider[] targets = Physics.OverlapSphere(transform.position, 0.5f);
        foreach (var item in targets)
        {
            if (item.CompareTag("Enemy"))
            {
                Destroy(item.gameObject);     // Elimina enemigo
                Destroy(gameObject);          // Elimina bala
                break;
            }
        }
    }
}
