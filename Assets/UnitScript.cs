using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitScript : MonoBehaviour
{
    public int owner;
    public bool selected;
    public float marchingSpeed;


    private Vector3 destination;
    private float angleOfUnit;


    void Start()
    {
        destination = this.transform.position;
    }


    void Update()
    {
        MoveUnitTowardDestination();
    }


    private void MoveUnitTowardDestination()
    {
        this.transform.position = Vector3.MoveTowards(this.transform.position, destination, marchingSpeed * Time.deltaTime);
    }


    private void FindNewAngle()
    {
        float x = destination.x - this.transform.position.x;
        float y = destination.y - this.transform.position.y;

        angleOfUnit = Mathf.Atan2(x, y) * Mathf.Rad2Deg;
        Debug.Log("Unit angle: " + angleOfUnit);
    }


    public void SetNewDestination(Vector3 newDestination)
    {
        if (Vector2.Distance(this.transform.position, newDestination) > 0.5f)
        {
            destination = newDestination;
            FindNewAngle();
            this.transform.rotation = Quaternion.Euler(0, 0, angleOfUnit);
        }
    }


    public void SetSelected(bool checkSelection)
    {
        selected = checkSelection;
        Debug.Log("Unit: Acquired destination");
    }


    public int GetOwner()
    {
        return owner;
    }
}
