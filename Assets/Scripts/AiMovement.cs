using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiMovement : MonoBehaviour
{
    public float Speed = 3f;
    public float rotSpeed = 100f;
    private bool movingAI = false;
    private bool movingRight = false;
    private bool movingLeft = false;
    private bool movingAround = false;
    // Start is called before the first frame update
    void Start()
    {

    }

   void Update()
    {

        if (movingAI == false)
        {
            StartCoroutine(AIMovement());
        }

        if(movingRight == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * rotSpeed);
        }

        if (movingLeft == true)
        {
            transform.Rotate(transform.up * Time.deltaTime * -rotSpeed);
        }

        if (movingAround == true)
        {
            transform.position += transform.forward * Speed * Time.deltaTime;
        }
    }

    IEnumerator AIMovement()
    {
        int rotTime = Random.Range(1, 3);
        int rotWait = Random.Range(1, 3);
        int rotLorR = Random.Range(0, 3);
        int walkWait = Random.Range(1, 3);
        int walkTime = Random.Range(1, 5);

        movingAI = true;

        yield return new WaitForSeconds(walkWait);

        movingAround = true;

        yield return new WaitForSeconds(walkTime);

        movingAround = false;

        yield return new WaitForSeconds(rotWait);

        if (rotLorR == 1)
        {
            movingRight = true;
            yield return new WaitForSeconds(rotTime);
            movingLeft = true;
        }

        if (rotLorR == 2)
        {
            movingLeft = true;
            yield return new WaitForSeconds(rotTime);
            movingRight = true;
        }

        movingAI = false;
    }
}