using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public Vector3 m_target;
    public float speed;
    public float damage = 10.0f;



    // Update is called once per frame
    void Update()
    {
        // transform.position += transform.forward * Time.deltaTime * 300f;// The step size is equal to speed times frame time.
        float step = speed * Time.deltaTime;

        if (m_target != null)
        {
            if (transform.position == m_target)
            {
                //DealDamage();
                return;
            }
            transform.position = Vector3.MoveTowards(transform.position, m_target, step);
        }

    }

    public void setTarget(Vector3 target)
    {
        m_target = target;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Player.Instance.ModifyHealth(damage);
            //Debug.Log("damage!");
            Destroy(gameObject);
        }
        else if (!col.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
    }
}
