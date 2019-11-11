using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetWalkigEntity : PlanetEntity
{
    public Rigidbody rigidBody;
    public float moveSpeed = 5;
    public override void Start()
    {
        base.Start();
    }

    public override void Update()
    {
        base.Update();

        if (hasPlanet()) {
            rigidBody.isKinematic = false;
            
            Vector3 pos = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized;
            rigidBody.MovePosition(rigidBody.position + transform.TransformDirection(pos) * moveSpeed*Time.deltaTime);

            attract(this.transform,rigidBody);
        }
    }

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
