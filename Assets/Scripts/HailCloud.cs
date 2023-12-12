using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HailCloud : MonoBehaviour
{
    [SerializeField] private GameObject HailballPF;
    [SerializeField] private Vector2 SpawnSize = Vector2.one * 10;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(SpawnHailBall), 0.0f, 0.1f);
        SpawnHailBall();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnHailBall()
    {
        GameObject hailBallGameObject = Instantiate(HailballPF, GetRandomPos(), Quaternion.identity);
        
        Destroy(hailBallGameObject, 4.0f);
    }
    Vector3 GetRandomPos(){

        //Randomizing the position
        float x = Random.Range(-SpawnSize.x, SpawnSize.x);
        float z = Random.Range(-SpawnSize.y, SpawnSize.y);

        Vector3 position = transform.position;
        position.x += x;
        position.z += z;

        return position;
    }
    private void OnDrawGizmosSelected(){

        Gizmos.color = Color.red;

        Vector3 Size = new Vector3(SpawnSize.x, 0.1f, SpawnSize.y);


        Gizmos.DrawWireCube(transform.position, Size*2);

    }
    
    
}
