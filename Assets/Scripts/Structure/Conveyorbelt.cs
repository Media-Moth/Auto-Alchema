using UnityEngine;
using System.Collections.Generic;

public class Conveyorbelt : MonoBehaviour
{
    List<Rigidbody> inContactRigids;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        inContactRigids = new List<Rigidbody>();   
    }
    void OnCollisionEnter(Collision collision)
    {
        Rigidbody rigid = collision.Rigidbody;
        if (rigid != null && inContactRigids.Contains(rigid))
        {
            inContactRigids.Add(rigid);
        }
    }
    void OnCollisionExit(Collision collision)
    {
        Rigidbody rigid = collision.Rigidbody;
        if (rigid != null && inContactRigids.Contains(rigid))
        {
            inContactRigids.Remove(rigid);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (Rigidbody rigid in inContactRigids)
        {
            rigid.MovePosition(rigid.transform.position + new Vector3(1,0,0) * Time.deltaTime);
        }
    }
}
