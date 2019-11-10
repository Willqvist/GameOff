using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereCollisionHandler : MonoBehaviour
{
    private int collisions = 0;
    private GameObject holding;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setHolding(GameObject holding) {
        this.holding = holding;
    }

    public bool isColliding() {
        return collisions > 0;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (holding != null && !holding.Equals(collision.gameObject) && collision.gameObject.layer == LayerMask.NameToLayer("Planet Object")) {
            collisions++;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (holding != null && !holding.Equals(collision.gameObject) && collision.gameObject.layer == LayerMask.NameToLayer("Planet Object"))
        {
            collisions--;
        }
    }
}
