using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpawner : MonoBehaviour
{
    public List<GameObject> particles;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnParticle()
    {
        int rand = Random.Range(0, 5);
        Instantiate(particles[rand], transform.position, Quaternion.identity);
    }
}
