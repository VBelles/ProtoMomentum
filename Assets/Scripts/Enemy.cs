using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

	private new Rigidbody rigidbody;
    private new BoxCollider collider;

    private Vector3 initialPosition;

    public float despawnTime = 1f;
    public float respawnTime = 5f;

    private int unitsPerHit = 3900;

    public delegate void HitAction(int units);
    public static event HitAction OnHit;

    void Awake ()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        initialPosition = GetComponent<Transform>().position;
    }

	void Update ()
    {
    }

    public void PropelledOnMe(Vector3 speedVector)
    {
        if (OnHit != null)
        {
            OnHit(unitsPerHit);
        }

        collider.enabled = false;
        rigidbody.velocity = speedVector;
        StartCoroutine(DespawnAndRespawn());
    }

    IEnumerator DespawnAndRespawn()
    {
        yield return new WaitForSeconds(despawnTime);
        MakeInactive();
        yield return new WaitForSeconds(respawnTime);
        MakeActive();
    }

    void MakeInactive()
    {
        GetComponent<MeshRenderer>().enabled = false;
        rigidbody.velocity = Vector3.zero;
    }

    void MakeActive()
    {
        collider.enabled = true;
        GetComponent<Transform>().position = initialPosition;
        GetComponent<MeshRenderer>().enabled = true;
    }
}
