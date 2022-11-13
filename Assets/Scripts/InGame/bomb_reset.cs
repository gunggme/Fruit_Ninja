using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb_reset : MonoBehaviour
{
    public GameObject whole;
    public GameObject sliced;

    private Rigidbody itemRigidbody;
    private Collider itemCollider;
    private ParticleSystem itemEffect;

    public int point = 0;

    private void Awake()
    {
        itemCollider = GetComponent<Collider>();
        itemRigidbody = GetComponent<Rigidbody>();
        itemEffect = GetComponentInChildren<ParticleSystem>();
       
    }

    private void Slice(Vector3 direcion, Vector3 position, float force)
    {
        

        itemCollider.enabled = false;
        whole.SetActive(false);

        sliced.SetActive(true);
        itemEffect.Play();

        
        float angle = Mathf.Atan2(direcion.y, direcion.x) * Mathf.Rad2Deg;
        sliced.transform.rotation = Quaternion.Euler(0f, 0f, angle);

        Rigidbody[] slices = sliced.GetComponentsInChildren<Rigidbody>();

        foreach (Rigidbody slice in slices)
        {
            slice.velocity = itemRigidbody.velocity;
            slice.AddForceAtPosition(direcion * force, position, ForceMode.Impulse);
        }

        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Spawner call = GameObject.Find("Spawner").GetComponent<Spawner>();
            call.bombChance = 0.05f;
            Blade blade = other.GetComponent<Blade>();
            Slice(blade.direction, blade.transform.position, blade.sliceForce);
        } 
    }
}
