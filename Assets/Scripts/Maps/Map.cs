using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Map : MonoBehaviour
{

    public int xSize = 100;
    public int ySize = 100;
    public GameObject tilePrefab;
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < xSize; ++x){
            for(int y = 0; y < ySize; ++y){
                GameObject tile = Instantiate(tilePrefab, new Vector3(x,y,0), Quaternion.identity);
                tile.transform.SetParent(this.transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
