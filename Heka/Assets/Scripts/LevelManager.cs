using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public List<Transform> objectSpawnPoints;
    public List<Transform> slotSpawnPoints;
    public List<DragDrop> objects;
    public GameObject[] slots;
    public GameObject reloadButton;

    private void Start()
    {
        ShufflePositions();
    }

    public void ShufflePositions()
    {
        DragDrop.ResetCorrectCount();

        List<Transform> availableObjectSpawns = new List<Transform>(objectSpawnPoints);
        foreach (DragDrop obj in objects)
        {
            int randomIndex = Random.Range(0, availableObjectSpawns.Count);
            Transform randomSpawn = availableObjectSpawns[randomIndex];
            obj.ResetPosition(randomSpawn.position);
            availableObjectSpawns.RemoveAt(randomIndex);
        }

        List<Transform> availableSlotSpawns = new List<Transform>(slotSpawnPoints);
        foreach (GameObject slot in slots)
        {
            int randomIndex = Random.Range(0, availableSlotSpawns.Count);
            Transform randomSpawn = availableSlotSpawns[randomIndex];
            slot.transform.position = randomSpawn.position;
            availableSlotSpawns.RemoveAt(randomIndex);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;

            Collider2D collider = Physics2D.OverlapPoint(mousePos);
            if (collider != null && collider.gameObject == reloadButton)
            {
                ShufflePositions();
            }
        }
    }
}
