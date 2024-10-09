using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragDrop : MonoBehaviour
{
    public GameObject CorrectSlot;
    private bool isMoving;
    private bool finish;
    private Vector3 resetPosition;
    private Vector3 offset;
    private static int correctCount = 0;
    private static int totalObjects = 3;

    void Start()
    {
        resetPosition = this.transform.localPosition;
    }

    void Update()
    {
        if (!finish)
        {
            if (isMoving)
            {
                Vector3 mousePos = Input.mousePosition;
                mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
                mousePos.z = 0;

                this.gameObject.transform.position = mousePos + offset;
            }
        }
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, Camera.main.nearClipPlane));
            mousePos.z = 0;

            offset = this.transform.position - mousePos;
            isMoving = true;
        }
    }

    private void OnMouseUp()
    {
        isMoving = false;

        if (Mathf.Abs(this.transform.localPosition.x - CorrectSlot.transform.localPosition.x) < 0.5f &&
            Mathf.Abs(this.transform.localPosition.y - CorrectSlot.transform.localPosition.y) < 0.5f)
        {
            this.transform.position = new Vector3(CorrectSlot.transform.position.x, CorrectSlot.transform.position.y, CorrectSlot.transform.position.z);
            finish = true;
            correctCount++;
            Debug.Log("correct slot");

            if (correctCount == totalObjects)
            {
                Debug.Log("congratulation circuit complete");
            }
        }
        else
        {
            StartCoroutine(SmoothReset());
            Debug.Log("wrong slot");
        }
    }

    private IEnumerator SmoothReset()
    {
        float time = 0;
        Vector3 startPos = this.transform.localPosition;

        while (time < 1)
        {
            time += Time.deltaTime / 0.5f;
            this.transform.localPosition = Vector3.Lerp(startPos, resetPosition, time);
            yield return null;
        }
    }

    public void ResetPosition(Vector3 newPosition)
    {
        this.transform.position = newPosition;
        resetPosition = newPosition;
        finish = false;
    }

    public static void ResetCorrectCount()
    {
        correctCount = 0;
    }
}
