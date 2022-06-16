using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectProps : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    double[,] my_marix;
    public static bool boolStart=false;
    public static Quaternion q = new Quaternion();
    
    // Start is called before the first frame update
    void Start()
    {
        obj.transform.localRotation = new Quaternion(0, 0, 0, 0);
       
        
    }

    // Update is called once per frame
   void Update()
    {
        my_marix = MatrixCalculator.finalCalculation;
        Vector3 translation = new Vector3((float)my_marix[0,3],(float)my_marix[1,3],(float)my_marix[2,3]);
        //obj.transform.localRotation = new Quaternion((float)my_marix[1, 1], (float)my_marix[1, 2], (float)my_marix[2, 2]);
        QuaternionFromMatrix(my_marix);
        //obj.transform.localRotation = new Quaternion(q.x,q.y,q.z,q.w);
        obj.transform.rotation = new Quaternion(q.x, q.y, q.z, q.w);
        while(obj.transform.position != translation && boolStart){
            boolStart = false;
            obj.transform.Translate(translation*Time.deltaTime,Space.World);
        }
        

    }
    public static Quaternion QuaternionFromMatrix(double[,] m)
    {
        // Adapted from: http://www.euclideanspace.com/maths/geometry/rotations/conversions/matrixToQuaternion/index.htm
        
        q.w = Mathf.Sqrt(Mathf.Max(0, 1 + (float)m[0, 0] + (float)m[1, 1] + (float)m[2, 2])) / 2;
        q.x = Mathf.Sqrt(Mathf.Max(0, 1 + (float)m[0, 0] - (float)m[1, 1] - (float)m[2, 2])) / 2;
        q.y = Mathf.Sqrt(Mathf.Max(0, 1 - (float)m[0, 0] + (float)m[1, 1] - (float)m[2, 2])) / 2;
        q.z = Mathf.Sqrt(Mathf.Max(0, 1 - (float)m[0, 0] - (float)m[1, 1] + (float)m[2, 2])) / 2;
        q.x *= Mathf.Sign(q.x * ((float)m[2, 1] - (float)m[1, 2]));
        q.y *= Mathf.Sign(q.y * ((float)m[0, 2] - (float)m[2, 0]));
        q.z *= Mathf.Sign(q.z * ((float)m[1, 0] - (float)m[0, 1]));
        return q;
    }
}