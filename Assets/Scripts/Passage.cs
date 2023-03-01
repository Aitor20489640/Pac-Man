using UnityEngine;

public class Passage : MonoBehaviour
{
    public Transform connection;
    private void OnTriggerEnter2D(Collider2D col)
    {
        col.transform.position =  connection.position;
    }
}
