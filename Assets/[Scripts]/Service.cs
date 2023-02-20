using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Service : MonoBehaviour
{
   // public GameObject customerGO = null;

    public float ServiceTimeInMin = 3f; // 1/Mu
    public float ServiceRateInCustomersPerMin; // Mu  (0.3333 -> 1/3)
    public float speed = 1f;

    public int timeScaling = 5; 
    private bool isSimulationRunning;
    private float nextServiceTimeInSec;
    private float currentServiceTime;
    public GameObject currentCustomer = null;
    private GameObject exitCustomer;
    private bool isInService = false;
    private bool isServiced = false;


    List<GameObject> queue; // = new List<GameObject>();
    GameObject ATM, exit;
    
    // Start is called before the first frame update
    void Awake()
    {
        ServiceRateInCustomersPerMin = 1f / ServiceTimeInMin;
        ArrivalProcess arrivalProcess = GameObject.FindGameObjectWithTag("Entrance").GetComponent<ArrivalProcess>();
        queue = arrivalProcess.queue;
        isSimulationRunning = arrivalProcess.isSimulationRunning;
        ATM = GameObject.FindGameObjectWithTag("ATM");
        exit = GameObject.FindGameObjectWithTag("Exit");

    }
    private void Start()
    {
        float nextServiceTimeInMin = Utilities.GetExpDistValue(ServiceRateInCustomersPerMin); //mu
        nextServiceTimeInSec = nextServiceTimeInMin * 60f;
        currentServiceTime = nextServiceTimeInSec / timeScaling;
        StartCoroutine(GenerateServiceTimes());

    }

    void Update()
    { //if first Customer in line exist move it towards ATM
        if(currentCustomer != null)
        {
            currentCustomer.transform.position = Vector3.MoveTowards(currentCustomer.transform.position, ATM.transform.position, speed * Time.deltaTime);

            //start countdown service time when customer reaches ATM
            if (currentCustomer.transform.position == ATM.transform.position)
            {
                if (isInService)
                {
                        if (currentServiceTime > 0)
                        {
                            currentServiceTime -= Time.deltaTime;
                        }
                        else   //when service time is run out current customer becomes exiting customer
                        {
                            print("currentServceTime = 0 ");
                            isInService = false;
                            exitCustomer = queue[0];
                            queue.RemoveAt(0);
                            isServiced = true;
                            currentCustomer = null;

                            if (queue.Count > 0)
                            {
                                currentCustomer = queue[0];
                                isInService = true;
                            }
                        }
                }
            }
        }
        else
        {
            if (queue.Count > 0)
            {
                currentCustomer = queue[0];               
            }
        }

        //if exiting customer is alreday serviced move it towards exit point and delete
        if (isServiced)
        {
           
            if (exitCustomer != null)
            {
                exitCustomer.transform.position = Vector3.MoveTowards(exitCustomer.transform.position, exit.transform.position, speed * Time.deltaTime);
                if (exitCustomer.transform.position == exit.transform.position )
                {
                    Destroy(exitCustomer);                    
                    isServiced = false;                    
                }
            }
            else
            {
                print("Exit customer = null");
            }                                                   
        }

    }


    IEnumerator GenerateServiceTimes() 
    {
        while (isSimulationRunning)
        {
           if (currentCustomer != null)
            {                             
                float nextServiceTimeInMin = Utilities.GetExpDistValue(ServiceRateInCustomersPerMin); //mu
                nextServiceTimeInSec = nextServiceTimeInMin * 60f;
                currentServiceTime = nextServiceTimeInSec / timeScaling;
                print("Scaled_NextServiceTimeInSec=" + currentServiceTime);  
                  isInService = true;     
                yield return new WaitForSeconds(currentServiceTime);
               

            }
            if (currentCustomer == null)
            {
                if (queue.Count > 0)
                {
                    currentCustomer = queue[0];
                   isInService = true;                  
                }
            }
            yield return null;
        }
          
    }
}
