using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChasingPlayer : MonoBehaviour
{

    public Transform target;
    public float speed;
    private Vector3 offset;
    // Use this for initialization
    void Start()
    {
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target, Vector3.up);
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(gameObject);
            SceneManager.LoadScene("gameOver");
        }
    }
}
