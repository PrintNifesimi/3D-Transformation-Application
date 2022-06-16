using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MatrixCalculator
{
    private FieldHandler fieldHandler;
    private List<FieldHandler> utilityArr;
    private int setIdentifier;
    private static Boolean flag = false;
    public static double[,] finalCalculation = new double[,]{
        {0,0,0,0 },
        {0,0,0,0 },
        {0,0,0,0 },
        {0,0,0,0 }
    };
    private double[,] values = new double[4, 4];
    private List<String> setArr;
    private static Label[] MlabelsArr;
    private VisualElement target;
    private VisualElement root;
    private TextField drField;
    private Button degreeBtn;

    private Button radiansBtn;
    private TextField txyzRun;
    public MatrixCalculator(List<FieldHandler> utilityArr,int setIdentifier, List<String> setArr,Label[] labelsArr, VisualElement target){
        this.utilityArr = utilityArr;
        this.setIdentifier=setIdentifier;
        this.setArr = setArr;
        MlabelsArr = labelsArr;
        this.target = target;
        root = target.parent.parent;
        innitStart();
    }

    private void innitStart(){

        //target?.RegisterCallback<ClickEvent>(ev=>processMatrix());
        drField=root.Q<TextField>("DGfield");
        
        degreeBtn = root.Q<Button>("Degrees");
        radiansBtn = root.Q<Button>("Radians");
        drField?.RegisterCallback<BlurEvent>(ev=>processMatrix());
        
        //degreeBtn?.RegisterCallback<ClickEvent>(ev => processMatrix());
        //radiansBtn?.RegisterCallback<ClickEvent>(ev => processMatrix());


    }

    public void processMatrix(){
        List<FieldHandler> setList;
        int indexSet;
        if (utilityArr.Count != 0)
        {
            if (setIdentifier != 1 && setArr.Count! <= 1)
            {

                indexSet = setArr.IndexOf("{" + setIdentifier + "}");

                setList = utilityArr.GetRange(indexSet + 1, utilityArr.Count - 1);
            }
            else if (setIdentifier == 1)
            {

                setList = utilityArr;
            }
           
            else
            {
                
                setList = null;
            }

            if (setList != null)
            {

                foreach (FieldHandler utility in setList)
                {

                    values = utility.getTextfieldValues();
                    processUtility(utility);

                }


                utilityArr.Clear();
                setArr.Clear();
                degreeBtn.SetEnabled(false);
                radiansBtn.SetEnabled(false);

            }
        }
    }

    public void processUtility(FieldHandler utility){
        
        if(setIdentifier==1 && flag==false){
            finalCalculation = values;
            flag=true;
        }else{
            finalCalculation = multiply(finalCalculation, values);
        }
        utility = null;
        showCalculation();


        //setArr.Remove(setIdentifier.ToString());

    }
    public static void externalManipulation(double[,] txyzVal){
        
        if(flag==true){
            finalCalculation=multiply(finalCalculation,txyzVal);
        }else{
            finalCalculation=txyzVal;
        }
        showCalculation();
    }
    private static double[,] multiply(double[,]A,double[,]B){
        double[,] c = new double[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                c[i, j] = 0;
                for (int k = 0; k < 4; k++) c[i, j] += A[i, k] * B[k, j];
            }
        }
        return c;
    }
    

    private static void showCalculation(){
        int lCount=0;
        for(int i=0;i<4;i++){
            for(int j=0;j<4;j++){
                MlabelsArr[lCount].text=Math.Round(finalCalculation[j,i],4).ToString();
                lCount++;
            }
        }
    }

}