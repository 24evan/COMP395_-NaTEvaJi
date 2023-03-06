using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ArrivalProcess : MonoBehaviour
{
    public GameObject customerPrefab;

    public GameObject customerSpawnPlace;

    public float interArrivalTimeInMin = 5f; //1/Lambda
    public float arrivalRateInCustomersPerMin; //Lambda

    public bool isSimulationRunning = true;
    public int timeScaling = 5;
    public TMP_Text arrivalTxt;
    public TMP_Text numberOfCustomers;
    public float speed = 1f;
    public GameObject initWaitingPoint;
    private GameObject customerGO;
    GameObject ATM;
    public List<GameObject> queue = new List<GameObject>();
    public List<GameObject> waypoints = new List<GameObject>();
    private float nextArrivalTimeInSec;
   


    // Start is called before the first frame update
    void Start()
    {
        ATM = GameObject.FindGameObjectWithTag("ATM");
        arrivalRateInCustomersPerMin = 1 / interArrivalTimeInMin;
        StartCoroutine(GenerateArrivals());

    }

    // Update is called once per frame
    void Update()
    {
        nextArrivalTimeInSec -= Time.deltaTime;
        arrivalTxt.text = nextArrivalTimeInSec.ToString("0.0");
        if (queue.Count > 0)
        { 
            numberOfCustomers.text = (queue.Count - 1).ToString();

                if (customerGO != queue[0])
                {                                 
                    customerGO.transform.position = Vector3.MoveTowards(customerGO.transform.position, initWaitingPoint.transform.position, speed * Time.deltaTime);
                } 
       
        }

        
            
    }

    IEnumerator GenerateArrivals()
    {
        while (isSimulationRunning)
        {
            customerGO = Instantiate(customerPrefab, customerSpawnPlace.transform.position, Quaternion.identity);
           

            queue.Add(customerGO);
            

            float nextArrivalTimeInMin = Utilities.GetExpDistValue(arrivalRateInCustomersPerMin);
             nextArrivalTimeInSec = nextArrivalTimeInMin * 60f/ timeScaling;
            print("Scaled_NextArrivalTimeInSec=" + nextArrivalTimeInSec);
            yield return new WaitForSeconds(nextArrivalTimeInSec);
        }

        yield return null;
    }
}