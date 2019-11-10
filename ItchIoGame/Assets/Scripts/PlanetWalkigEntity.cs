using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWalkigEntity : PlanetEntity
{
    // Start is called before the first frame update
    public Rigidbody rigidBody;
    public float moveSpeed = 5;
    public virtual void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    public void Update()
    {
        base.Update();
        if (hasPlanet()) {
            rigidBody.isKinematic = false;
            
            Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(pos) * moveSpeed*Time.deltaTime);

            attract(this.transform,rigidBody);
        }
    }
    /*
    public override void SetColor(Color color)
    {
        //base.setColor();

    }
    */
    private void attract(Transform ptransform, Rigidbody body) {
        Vector3 up = (ptransform.position - planet.transform.position).normalized;
        Vector3 localUp = ptransform.up;
        body.AddForce(up * planet.gravity);

        Quaternion rot = Quaternion.FromToRotation(localUp,up) * ptransform.rotation;
        transform.rotation = Quaternion.Slerp(ptransform.rotation, rot, 50 * Time.deltaTime);
    }
    public override void OnPlace()
    {
        base.OnPlace();
        this.rigidBody.isKinematic = true;
    }
}
