using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CameraMovement : MonoBehaviour
{
    public Transform target;
    public Transform rabbitend;
    //public Camera cam;
    public float smoothing;
    public float smoothing02;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public bool activated;
    public Transform target02; 
    [SerializeField] private Animator ChihiroCam;
    private InMemoryVariableStorage variableStorage;
    public AutomaticInteractions02 anotherscript;
    public AmusementPark amusementScript;

    public bool complicated;

    private Animator cameranchor;

    private void Awake()
    {
        //ChihiroCam.gameObject.GetComponent<Animator>().enabled = false;
        variableStorage = FindObjectOfType<Yarn.Unity.InMemoryVariableStorage>();
        complicated = false;
    }

    private void Start()
    {
        cameranchor = GetComponent<Animator>();
    }
    void Update()
    {
        variableStorage.TryGetValue("$activated", out activated);
        
        if (Input.GetKeyDown("space"))
        {
            Debug.Log("WeaponNum = " + complicated);
        }
    }

    private void FixedUpdate()
    {
        if (transform.position != target.position && !activated)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,
                minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,
                minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position,
                targetPosition, smoothing);
        }

        if (transform.position != target.position && activated)
        {
         transform.position = target02.transform.position + new Vector3(0, 1, -5);
         //ChihiroCam.gameObject.GetComponent<Animator>().enabled = true;
         //ChihiroCam.SetBool("chihirofollow", true);
        }

        if (transform.position != target.position && !anotherscript.enteredbox)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,
                minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,
                minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position,
                targetPosition, smoothing);
        }

        if (transform.position != target.position && anotherscript.enteredbox && !cameranchor.enabled && !complicated)
        {
            cameranchor.enabled = !cameranchor.enabled;
        }

        if (transform.position != target.position && complicated)
        {
            Vector3 targetPosition = new Vector3(target.position.x,
                target.position.y, transform.position.z);
            targetPosition.x = Mathf.Clamp(targetPosition.x,
                minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y,
                minPosition.y, maxPosition.y);
            transform.position = Vector3.Lerp(transform.position,
                targetPosition, smoothing);
        }
    }
}