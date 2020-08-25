using UnityEngine;
using System.Collections;

/*Count time and create new cactus for us*/
public class PrefabsSpawner : MonoBehaviour {

    private float nextSpawnPointTime = 0;
    public Transform prefabToSpawn;
    public AnimationCurve spawnCurve;
    public float curveLengthInSeconds = 30f;
    private float startTime;
    //randomness to curve
    public float jitter = 0.25f;


	// Use this for initialization
	void Start () {
        startTime = Time.time;
	
	}
	
	// Update is called once per frame
	void Update () {

        if (Time.time > nextSpawnPointTime)
        {
            //Quaternion.identity - pretty much a zero value for rotation. 
            Instantiate(prefabToSpawn, transform.position, Quaternion.identity);
            //nextSpawnPointTime = Time.time + spawnRateSeconds + Random.Range(0, randomDelay);

            //curve value between 0 and 1 on x axis. 
            float curvePos = (Time.time - startTime) / curveLengthInSeconds;

            if (curvePos > 1f)
            {
                curvePos = 1f;
                startTime = Time.time;
            }
            //evaluate> horisontal axis on the graph.
            //22:18!!!!
            nextSpawnPointTime = Time.time + spawnCurve.Evaluate(curvePos) + Random.Range(-jitter, jitter);
        }
	
	}
}
