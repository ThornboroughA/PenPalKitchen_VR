using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISpin : MonoBehaviour
{
    [SerializeField] private bool spin = true;

    [SerializeField] private float speed = 0.01f;
    private float delta = 0.03f;
    [SerializeField] private bool invert;
    [SerializeField] private float height = 1.3f;


    private void Update()
    {
        if (spin)
        {
            transform.Rotate(Vector3.down * (0.15f)); //why is * time.deltatime not working here?
        } else
        {
            float y = Mathf.PingPong(speed * Time.time, delta);
            Vector3 pos = new Vector3(transform.position.x,y + height, transform.position.z);

                transform.position = pos;
        }
    }

}
