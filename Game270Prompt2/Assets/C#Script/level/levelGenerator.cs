using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class levelGenerator : MonoBehaviour
{
public GameObject[] wallCubes;
   public int gridX = 10;
   public int gridY = 4;
   public int gridZ = 20;
  
   void Start()
   {
       for (int y=0; y<gridY; ++y)
       {
           for (int x=0; x<gridX; ++x)
           {    
               int randIndex = Random.Range(0,wallCubes.Length);
               Vector3 pos = new Vector3(x * 3.2f, y * 3.2f, 0);
               Instantiate(wallCubes[randIndex], pos, Quaternion.identity);
               transform.rotation = Random.rotation;
           }
       }   

       for (int z=0; z<gridZ; ++z)
       {
           for (int x=0; x<gridX; ++x)
           {    
               int randIndex = Random.Range(0,wallCubes.Length);
               Vector3 pos = new Vector3(x * 3.2f, 0, z * 3.2f);
               Instantiate(wallCubes[randIndex], pos, Quaternion.identity);
               transform.rotation = Random.rotation;
           }
       }   

       for (int z=0; z<gridZ; ++z)
       {
           for (int y=0; y<gridY; ++y)
           {    
               int randIndex = Random.Range(0,wallCubes.Length);
               Vector3 pos = new Vector3(0, y * 3.2f, z * 3.2f);
               Instantiate(wallCubes[randIndex], pos, Quaternion.identity);
               transform.rotation = Random.rotation;
           }
       }    
   }

}
