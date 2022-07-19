using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System;
public static class UtilityData
{
    public static List<Dictionary<string,double>> dataSaver = new List<Dictionary<string,double>>();
    public static string[] Utilities ={"Rx","Ry","Rz","txyz"};
    private static Dictionary<string,double> utilityDict = new Dictionary<string, double>();
    
    public static bool check(int id,int count){
        if(count == 1 || id==6){dataSaver.Add(new Dictionary<string,double>());}
        if(id == 6){return false;}
        Debug.Log(count-1+" "+dataSaver.Count);
        if(dataSaver[count-1].ContainsKey(Utilities[id-1])==true){
            return true;
        }else{
            return false;
            
        }

    }
    public static void addUtility(int id,int count,double degree){
        
        
        dataSaver[count-1].Add(Utilities[id-1],degree);
    }

    public static double getUtilityAngle(int count,int id){
        return dataSaver[count-1][Utilities[id-1]];
    }








}

