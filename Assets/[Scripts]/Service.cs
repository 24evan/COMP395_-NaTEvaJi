using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
    public GameObject customerGO = null;

    public float ServiceTimeInMin = 3f; // 1/Mu
    public float ServiceRateInCustomersPerMin; // Mu  (0.3333 -> 1/3)
       
    private bool isSimulationRunning;


    List<GameObject> queue; // = new List<GameObject>();
    GameObject ATM;
    
    // Start is called before the first frame update
    void Awake()
    {
        ServiceRateInCustomersPerMin = 1f / ServiceTimeInMin;
        ArrivalProcess arrivalProcess = GameObject.FindGameObjectWithTag("Entrance").GetComponent<ArrivalProcess>();
        queue = arrivalProcess.queue;
        isSimulationRunning = arrivalProcess.isSimulationRunning;
        ATM = GameObject.FindGameObjectWithTag("ATM");

    }
    private void Start()
    {
        StartCoroutine(GenerateServiceTimes());

    }


    IEnumerator GenerateServiceTimes()
    {
        while (isSimulationRunning)
        {
            while (customerGO != null)
            {
                
                float nextServiceTimeInMin = Utilities.GetExpDistValue(ServiceRateInCustomersPerMin); //mu
                float nextServiceTimeInSec = nextServiceTimeInMin * 60f;
                //  Destroy(carGO, nextServiceTimeInSec);
                print("nextServiceTimeInSec=" + nextServiceTimeInSec);
                yield return new WaitForSeconds(nextServiceTimeInSec);
            }
            if (customerGO == null)
            {
                if (queue.Count > 0)
                {
                    customerGO = queue[0];
                    customerGO.transform.position = ATM.transform.position;
                    Vector3 customerGOposition = customerGO.transform.position;
                    customerGOposition.z = customerGOposition.z - 1f;
                    queue.RemoveAt(0);

                }
            }
            yield return null;
        }

    }
}
