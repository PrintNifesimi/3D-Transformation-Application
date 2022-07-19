using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class FieldHandler
{
    private VisualElement target;
    
    private TextField[] TextFields;

    private TextField drField;
    
    
    private Button expressionBtn;
    private Button valueBtn;
    private static Boolean evOnOff=false;
    private String cos;
    private String sin;
    private String nSin;
    private List<string> valuesClone= new List<string>(16);
    private double[,] utilityValues;
    
    private VisualElement root;
    private Button degreeBtn;
    private Button radiansBtn;
    private int[] res = new int[3];
    private static Boolean drFieldFocus=true;
    private int setCount = convappscript.getSetCount();
    
    public FieldHandler(VisualElement target, int identifier,TextField drField,Button expressionBtn,Button valueBtn,TextField[] textFields){
        
        this.target=target;
        this.id=identifier;
        this.drField = drField;
        this.expressionBtn = expressionBtn;
        this.valueBtn = valueBtn;
        this.TextFields = textFields;
        root = target.parent.parent;

        
        
        innitStart();
        
    }
    private static int identifier;
    private int id;
    

    public void innitStart(){
        
        
        identifier = id;
        drFieldfocus(1);
        for(int i=0;i<16;i++){
            valuesClone.Add("placeholder");
        }
        
        setDrfield(true);
        //expressionBtn?.RegisterCallback<ClickEvent>(ev=> evHandler(expressionBtn));
        //valueBtn?.RegisterCallback<ClickEvent>(ev=> evHandler(valueBtn));
        if(identifier!=4){
        clearTextfields();
        //target?.RegisterCallback<ClickEvent>(ev=> viewUtilityData());
        disableTextFields();}
        
        if(identifier==4){
            clearTextfields();
            //valueHandler();   
        }
        degreeBtn = root.Q<Button>("Degrees");
        radiansBtn = root.Q<Button>("Radians");

        //degreeBtn?.RegisterCallback<ClickEvent>(ev => valueHandler());
        //radiansBtn?.RegisterCallback<ClickEvent>(ev => valueHandler());
        
        drField?.RegisterCallback<BlurEvent>(ev=>valueHandler());
        expressionBtn?.RegisterCallback<ClickEvent>(ev=> ChangeToExp(true,expressionBtn,valueBtn));
        valueBtn?.RegisterCallback<ClickEvent>(ev=> ChangeToExp(false,valueBtn,expressionBtn));
        
        ChangeToExp(false,valueBtn,expressionBtn);
        //getTextfieldValues();
       
        
    }
    
    
    public  void ChangeToExp(Boolean TorF, Button btn,Button btnOther){

        btn.style.backgroundColor = new Color(0.3098039f, 0.3098039f, 0.3098039f);
        btn.style.color=new Color(1,1,1);
        if(TorF){
            evOnOff=true;
           
            btnOther.style.backgroundColor = new Color(0.6705f,0.6705f,0.6705f);

            btnOther.style.color = new Color(0.5176f,0.5176f,0.5176f);
            
        }else{
            evOnOff =false;
           // btnOther.style.backgroundColor = Color.white;
            
            btnOther.style.backgroundColor = new Color(0.6705f,0.6705f,0.6705f);
            btnOther.style.color = new Color(0.5176f,0.5176f,0.5176f);
        }

        valueHandler();
    }

    private void valueHandler(){
        
        
        if(identifier!=4){enableTextFields();disableTextFields();setDrfield(true);}
        if (drField.value!="" && drFieldFocus){
            
            if(id<=4){UtilityData.addUtility(identifier,convappscript.getSetCount(),double.Parse(drField.text));}
            
            double radValue=0;
            
            try{         
                radValue = Math.Round(degTORad(int.Parse(drField.text)),3);     
            }catch{
                radValue = Math.Round(double.Parse(drField.text),3);
                        }
            cos = Math.Round(Mathf.Cos((float)radValue),4)+"";
            sin = Math.Round(Mathf.Sin((float)radValue),4)+"";
            nSin = -Math.Round(Mathf.Sin((float)radValue),4)+"";
            
            switch(identifier){
                
                case 1:
                {
                    
                    TextFields[0].value="1.0000";
                    TextFields[0].focusable=false;
                    TextFields[1].value="0.0000";
                    TextFields[1].focusable=false;
                    TextFields[2].value="0.0000";
                    TextFields[2].focusable=false;
                    TextFields[4].value="0.0000";
                    TextFields[4].focusable=false;
                    TextFields[8].value="0.0000";
                    TextFields[8].focusable=false;
                    if(evOnOff){
                        TextFields[5].value = "cos "+radValue;
                        valuesClone[5]=cos;
                        TextFields[6].value= "sin "+radValue;
                        valuesClone[6]=sin;
                        TextFields[9].value="-sin "+radValue;
                        valuesClone[9]=nSin;
                        TextFields[10].value="cos "+radValue;
                        valuesClone[10]=cos;
                        
                    }else if(evOnOff == false){
                        TextFields[5].value = cos;
                        
                        TextFields[6].value= sin;
                        
                        TextFields[9].value=nSin;
                        
                        TextFields[10].value=cos;
                        
                    }
                        TextFields[5].focusable=true;
                        TextFields[6].focusable=true;
                        TextFields[10].focusable=true;
                        TextFields[9].focusable=true;
                    
                break;
                }

                case 2:
                {
                    
                    TextFields[1].value="0.0000";
                    TextFields[1].focusable=false;
                    TextFields[9].value="0.0000";
                    TextFields[9].focusable=false;
                    TextFields[4].value="0.0000";
                    TextFields[4].focusable=false;
                    TextFields[5].value="1.0000";
                    TextFields[5].focusable=false;
                    TextFields[6].value= "0.0000";
                    TextFields[6].focusable=false;
                    if(evOnOff){
                        TextFields[0].value = "cos "+radValue;
                        valuesClone[0]=cos;
                        TextFields[8].value= "sin "+radValue;
                        valuesClone[8]=sin;
                        TextFields[2].value="-sin "+radValue;
                        valuesClone[2]=nSin;
                        TextFields[10].value="cos "+radValue;
                        valuesClone[10]=cos;
                    }else if(evOnOff==false){
                        TextFields[0].value = cos;
                        
                        TextFields[8].value= sin;
                        
                        TextFields[2].value=nSin;
                        
                        TextFields[10].value=cos; 
                        
                    }
                        TextFields[0].focusable=true;
                        TextFields[8].focusable=true;
                        TextFields[2].focusable=true;
                        TextFields[10].focusable=true;
                    
                break;
                }

                case 3:
                {
                    
                    TextFields[2].value="0.0000";
                    TextFields[2].focusable=false;
                    TextFields[6].value="0.0000";
                    TextFields[6].focusable=false;
                    TextFields[8].value="0.0000";
                    TextFields[8].focusable=false;
                    TextFields[10].value="1.0000";
                    TextFields[10].focusable=false;
                    TextFields[9].value= "0.0000";
                    TextFields[9].focusable=false;
                    if(evOnOff){
                        TextFields[0].value = "cos "+radValue;
                        valuesClone[0]=cos;
                        TextFields[1].value= "sin "+radValue;
                        valuesClone[1]=sin;
                        TextFields[4].value="-sin "+radValue;
                        valuesClone[4]=nSin;
                        TextFields[5].value="cos "+radValue;
                        valuesClone[5]=cos;
                    }else if(evOnOff==false){
                        TextFields[0].value = cos;
                        
                        TextFields[1].value= sin;
                        
                        TextFields[4].value=nSin;
                        
                        TextFields[5].value=cos;
                        
                    }
                        TextFields[0].focusable=true;
                        TextFields[1].focusable=true;
                        TextFields[4].focusable=true;
                        TextFields[5].focusable=true;


                break;
                }

            }
           getTextfieldValues();
           
        }else if(identifier==4){
            
            for(int i=0;i<TextFields.Length;i++){
                        if(i%5==0){
                            TextFields[i].value="1";
                        }else{
                            TextFields[i].value="0";
                        }
                        if(i<15 &&i>11){
                            if(i==12){
                                TextFields[i].SetEnabled(true);
                                TextFields[i].focusable=true;}
                        TextFields[i].value="";}
                        else{
                            TextFields[i].SetEnabled(false);
                        }
                       
                    }
                    
                    TextFields[12]?.RegisterCallback<BlurEvent>(ev=>txyzHandler(12));
                    //txyzHandler(12);
        }
        
       
       if(identifier!=4){target?.RegisterCallback<ClickEvent>(ev=> viewUtilityData());} 
        
        
        
    }
    
    
    public void txyzHandler(int tID){
        
        
        if(tID==14 && TextFields[tID].value!="" && int.TryParse(TextFields[tID].value,out res[tID-12])){
            getTextfieldValues();
            MatrixCalculator.externalManipulation(getUtilityValues());
            ObjectProps.boolStart = true;
            
            
        }
        if(TextFields[tID].value!="" && int.TryParse(TextFields[tID].value,out res[tID-12])&&tID!=14){
            TextFields[tID+1].SetEnabled(true);
            TextFields[tID+1].focusable=true;
            TextFields[tID+1]?.RegisterCallback<BlurEvent>(ev=>txyzHandler(tID+1));
       }
    }

    private static double degTORad(int deg){
        return (deg*Math.PI)/180;
    }
    private static double degTORad(double deg){
        return (deg*Math.PI)/180;
    }

    public double[,] getTextfieldValues(){
        
        utilityValues = new double[4,4] {
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0},
            {0,0,0,0}
        };
        Boolean cond = identifier==1;
        
        if(identifier==4){
            
            int count =0;
            for(int i=0;i<4;i++){
                for(int j=0;j<4;j++){
                    utilityValues[j,i]=double.Parse(TextFields[count].value);
                count++;
                }
                
            }
        }
        
        else{
        int count =0;
        for(int i=0;i<4;i++){
            for(int j=0;j<4;j++){
                if(TextFields[count].text==""){
                    if(i==3&&j==3){
                    utilityValues[j,i]=1;
                    }else if(cond&&i==0&&j==0){
                        utilityValues[j,i]=1;
                    }
                    else{
                        utilityValues[j,i]=0;
                    }
                }else{
                    try{
                        utilityValues[j,i]=Math.Round(double.Parse(TextFields[count].text),4);
                        
                    }catch{
                        
                        utilityValues[j,i]=Math.Round(double.Parse(valuesClone[count]),4);
                    }
                }
                count++;
            }
        }}
        
        return utilityValues;

    }
    

    private void disableTextFields(){
        //clearTextfields();
        if(identifier<=3){
            int count=3;
            while(count<16){
                TextFields[count].SetEnabled(false);
                
               
                if(count>=11){
                    count+=1;
                }else{
                    count+=4;
                }
            }
        }
    }

    public void clearTextfields(){
        
        drField.value="";
        foreach(TextField x in TextFields){
            x.value="";
            //x.SetEnabled(false);
        }
    }

    public void setDrfield(Boolean drBool){
        drField.focusable=drBool;
        
    }

    public String getDrField(){
        
        return drField.text;
    }
    
   public double[,] getUtilityValues(){
       return utilityValues;
   }

   public void enableTextFields(){
       for(int i=0;i<TextFields.Length;i++){
           TextFields[i].SetEnabled(true);
       }
   }

   public static void drFieldfocus(int x){
       if(x==0){
           drFieldFocus = false;
       }else{
           drFieldFocus=true;
       }
   }

   private void viewUtilityData(){
        if(id==6){clearTextfields();
        return;}
        double angle =UtilityData.getUtilityAngle(setCount,id);
        drField.value=angle+"";
        
        double radValue=0;
            
            try{         
                radValue = Math.Round(degTORad(angle),3);     
            }catch{
                radValue = Math.Round(angle,3);
                        }
            cos = Math.Round(Mathf.Cos((float)radValue),4)+"";
            sin = Math.Round(Mathf.Sin((float)radValue),4)+"";
            nSin = -Math.Round(Mathf.Sin((float)radValue),4)+"";
            
            switch(id){
                
                case 1:
                {
                    
                    TextFields[0].value="1.0000";
                    TextFields[0].focusable=false;
                    TextFields[1].value="0.0000";
                    TextFields[1].focusable=false;
                    TextFields[2].value="0.0000";
                    TextFields[2].focusable=false;
                    TextFields[4].value="0.0000";
                    TextFields[4].focusable=false;
                    TextFields[8].value="0.0000";
                    TextFields[8].focusable=false;
                    if(evOnOff){
                        TextFields[5].value = "cos "+radValue;
                        valuesClone[5]=cos;
                        TextFields[6].value= "sin "+radValue;
                        valuesClone[6]=sin;
                        TextFields[9].value="-sin "+radValue;
                        valuesClone[9]=nSin;
                        TextFields[10].value="cos "+radValue;
                        valuesClone[10]=cos;
                        
                    }else if(evOnOff == false){
                        TextFields[5].value = cos;
                        
                        TextFields[6].value= sin;
                        
                        TextFields[9].value=nSin;
                        
                        TextFields[10].value=cos;
                        
                    }
                        TextFields[5].focusable=true;
                        TextFields[6].focusable=true;
                        TextFields[10].focusable=true;
                        TextFields[9].focusable=true;
                    
                break;
                }

                case 2:
                {
                    
                    TextFields[1].value="0.0000";
                    TextFields[1].focusable=false;
                    TextFields[9].value="0.0000";
                    TextFields[9].focusable=false;
                    TextFields[4].value="0.0000";
                    TextFields[4].focusable=false;
                    TextFields[5].value="1.0000";
                    TextFields[5].focusable=false;
                    TextFields[6].value= "0.0000";
                    TextFields[6].focusable=false;
                    if(evOnOff){
                        TextFields[0].value = "cos "+radValue;
                        valuesClone[0]=cos;
                        TextFields[8].value= "sin "+radValue;
                        valuesClone[8]=sin;
                        TextFields[2].value="-sin "+radValue;
                        valuesClone[2]=nSin;
                        TextFields[10].value="cos "+radValue;
                        valuesClone[10]=cos;
                    }else if(evOnOff==false){
                        TextFields[0].value = cos;
                        
                        TextFields[8].value= sin;
                        
                        TextFields[2].value=nSin;
                        
                        TextFields[10].value=cos; 
                        
                    }
                        TextFields[0].focusable=true;
                        TextFields[8].focusable=true;
                        TextFields[2].focusable=true;
                        TextFields[10].focusable=true;
                    
                break;
                }

                case 3:
                {
                    
                    TextFields[2].value="0.0000";
                    TextFields[2].focusable=false;
                    TextFields[6].value="0.0000";
                    TextFields[6].focusable=false;
                    TextFields[8].value="0.0000";
                    TextFields[8].focusable=false;
                    TextFields[10].value="1.0000";
                    TextFields[10].focusable=false;
                    TextFields[9].value= "0.0000";
                    TextFields[9].focusable=false;
                    if(evOnOff){
                        TextFields[0].value = "cos "+radValue;
                        valuesClone[0]=cos;
                        TextFields[1].value= "sin "+radValue;
                        valuesClone[1]=sin;
                        TextFields[4].value="-sin "+radValue;
                        valuesClone[4]=nSin;
                        TextFields[5].value="cos "+radValue;
                        valuesClone[5]=cos;
                    }else if(evOnOff==false){
                        TextFields[0].value = cos;
                        
                        TextFields[1].value= sin;
                        
                        TextFields[4].value=nSin;
                        
                        TextFields[5].value=cos;
                        
                    }
                        TextFields[0].focusable=true;
                        TextFields[1].focusable=true;
                        TextFields[4].focusable=true;
                        TextFields[5].focusable=true;


                break;
                }

            }
   }

}
