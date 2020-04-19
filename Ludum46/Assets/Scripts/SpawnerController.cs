using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerController : MonoBehaviour
{
    public int spawnerid;
    public float cooldown;
    public float cooldowntimer;
    public GameObject enemy;
    public List<List<GameObject>> wavelist;
    public int wavecount = 0;
    private GameObject spawnlist;
    private SpawnList listscript;
    // Start is called before the first frame update
    void Start()
    {
        wavelist = new List<List<GameObject>>();
        spawnlist = GameObject.FindGameObjectWithTag("Spawnlist");
        listscript = spawnlist.GetComponent<SpawnList>();
        wavelist.Add(listscript.wave1);
        wavelist.Add(listscript.wave2);
        wavelist.Add(listscript.wave3);
        wavelist.Add(listscript.wave4);
        wavelist.Add(listscript.wave5);
        wavelist.Add(listscript.wave6);
        wavelist.Add(listscript.wave7);
        wavelist.Add(listscript.wave8);
        wavelist.Add(listscript.wave9);
        wavelist.Add(listscript.wave10);
        wavelist.Add(listscript.wave11);
        wavelist.Add(listscript.wave12);
        wavelist.Add(listscript.wave13);
        wavelist.Add(listscript.wave14);
        wavelist.Add(listscript.wave15);
        wavelist.Add(listscript.wave16);
        wavelist.Add(listscript.wave17);
        wavelist.Add(listscript.wave18);
        wavelist.Add(listscript.wave19);
        wavelist.Add(listscript.wave20);
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.GetHealth() > 0 && GameManager.GetFireHealth() > 0)
        {
            cooldowntimer += Time.deltaTime;
            if(cooldowntimer >= cooldown)
            {
                cooldowntimer = 0;
                Spawn(wavelist[wavecount]);
                wavecount++;
            }
        }
    }
    void Spawn(List<GameObject> wave)
    {
        if(wave[spawnerid] != null)
        {
            Instantiate(wave[spawnerid], transform.position, Quaternion.identity);
        }
    }
}
