using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivalProcess : MonoBehaviour
{
    public GameObject customerPrefab;

    public GameObject customerSpawnPlace;

    public float interArrivalTimeInMin = 5f; //1/Lambda
    public float arrivalRateInCustomersPerMin; //Lambda

    public bool isSimulationRunning = true;

    public List<GameObject> queue = new List<GameObject>();


    // Start is called before the first frame update
    void Start()
    {
        arrivalRateInCustomersPerMin = 1 / interArrivalTimeInMin;
        StartCoroutine(GenerateArrivals());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator GenerateArrivals()
    {
        while (isSimulationRunning)
        {
            GameObject customerGO = Instantiate(customerPrefab, customerSpawnPlace.transform.position, Quaternion.identity);

            queue.Add(customerGO);

            float nextArrivalTimeInMin = Utilities.GetExpDistValue(arrivalRateInCustomersPerMin);
            float nextArrivalTimeInSec = nextArrivalTimeInMin * 60f;
            print("nextArrivalTimeInSec=" + nextArrivalTimeInSec);
            yield return new WaitForSeconds(nextArrivalTimeInSec);
        }

        yield return null;
    }
}