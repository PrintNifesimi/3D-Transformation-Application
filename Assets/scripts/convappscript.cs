using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;

public class convappscript : MonoBehaviour
{
    // Start is called before the first frame update
    Button rxAddBtn;
    VisualElement rxVE;
    Button ryAddBtn;
    VisualElement ryVE;
    Button rzAddBtn;
    VisualElement rzVE;
    Button txyzAddBtn;
    VisualElement txyzVE;
    Button mAddBtn;
    VisualElement MVE;
    Button braceAddBtn;
    VisualElement BraceVE;
    VisualElement secA_2;
    VisualElement secA_1;
    VisualElement UI_cont;
    CloneUtilityHover Clone;
    VisualElement VE;
    List<FieldHandler> UtilityArr = new List<FieldHandler>();
    List<String> setArr = new List<String>();
    
    
    //Field Handler
        private TextField drField;
        private Button expressionBtn;
        private Button valueBtn;
        private Button radiansBtn;
        private Button degreeBtn;
        private TextField[] TextFields= new TextField[16];
        private int secA_2ChildCount;
        private Label[] Labels= new Label[16];
         

    
    public static int count =1;
    private void Update() {
        if(Input.GetKeyUp(KeyCode.Return)){
            
            FieldHandler.drFieldfocus(0);
            drField.Blur();
            
        }
    }
    void Start()
    {

        var root = GetComponent<UIDocument>().rootVisualElement;
        
        rxAddBtn = root.Q<Button>("Rx-add-btn");
        rxVE = root.Q<VisualElement>("Sec-a-Rx");
        ryAddBtn = root.Q<Button>("Ry-add-btn");
        ryVE = root.Q<VisualElement>("Sec-a-Ry");
        rzAddBtn = root.Q<Button>("Rz-add-btn");
        rzVE = root.Q<VisualElement>("Sec-a-Rz");
        txyzAddBtn = root.Q<Button>("txyz-add-btn");
        txyzVE = root.Q<VisualElement>("Sec-a-txyz");
        mAddBtn = root.Q<Button>("M-add-btn");
        MVE = root.Q<VisualElement>("Sec-a-M");
        braceAddBtn = root.Q<Button>("brace-add-btn");
        BraceVE = root.Q<VisualElement>("Sec-a-brace");
        secA_2 = root.Q<VisualElement>("slots");
            secA_2ChildCount = secA_2.childCount;
        secA_1 = root.Q<VisualElement>("section-a");
        UI_cont = root.Q<VisualElement>("Ui-cont");

        //Field Handler
            drField =root.Q<TextField>("DGfield");
            expressionBtn = root.Q<Button>("ExpressionBtn");
            valueBtn=root.Q<Button>("ValueBtn");
            radiansBtn = root.Q<Button>("Radians");
            degreeBtn = root.Q<Button>("Degrees");

        

        //Disabled text fields
            for(int i=0;i<16;i++){
                TextFields[i]=root.Q<TextField>("TextField-"+(i+1));
                TextFields[i].focusable=false;
            }

        disablefields();
        
        
        //Labels
        for (int i=0;i<16;i++){
                Labels[i]=root.Q<Label>("Label-"+(i+1));
                
            }
        
        
        ///
        rxVE?.RegisterCallback<PointerDownEvent>(ev=>drag(1));
        ryVE?.RegisterCallback<PointerDownEvent>(ev=>drag(2));
        rzVE?.RegisterCallback<PointerDownEvent>(ev=>drag(3));
        txyzVE?.RegisterCallback<PointerDownEvent>(ev=>drag(4));
      //  MVE?.RegisterCallback<PointerDownEvent>(ev=>drag(5));
        BraceVE?.RegisterCallback<PointerDownEvent>(ev=>drag(6));


        rxAddBtn?.RegisterCallback<ClickEvent>(ev=> cloneUtility(1));
        ryAddBtn?.RegisterCallback<ClickEvent>(ev=> cloneUtility(2));
        rzAddBtn?.RegisterCallback<ClickEvent>(ev=> cloneUtility(3));
        txyzAddBtn?.RegisterCallback<ClickEvent>(ev=> cloneUtility(4));
       // mAddBtn?.RegisterCallback<ClickEvent>(ev=> cloneUtility(5));
        braceAddBtn?.RegisterCallback<ClickEvent>(ev=> cloneUtility(6));

        radiansBtn?.RegisterCallback<ClickEvent>(ev=>degreesORRadians(1));
       // degreeBtn?.RegisterCallback<ClickEvent>(ev=>degreesORRadians(-1));
       // expressionBtn?.RegisterCallback<ClickEvent>(ev=> FieldHandler.evHandler(true,expressionBtn,valueBtn));
        //valueBtn?.RegisterCallback<ClickEvent>(ev=> FieldHandler.evHandler(false,valueBtn,expressionBtn));
        
        
        
    }

    
    public void cloneUtility(int caseNum){
        if(UtilityData.check(caseNum,count)==true){return;}
        VisualElement CloneVe = new VisualElement();
        Label LabelClone = new Label();
        Toggle togglebtn = new Toggle();
        //Button emptyBtn = new Button();
        CloneVe.AddToClassList("seca-VE-conts-add");
        CloneVe.focusable = true;
        LabelClone.AddToClassList("sec-a-text");
        LabelClone.enableRichText = true;
        togglebtn.AddToClassList("utility-btn-add");
        //togglebtn.value = checked;
        togglebtn.label="";
        togglebtn.text="";
        togglebtn.focusable =false;
        //emptyBtn.text="";
        CloneVe.Add(LabelClone);
        CloneVe.Add(togglebtn);
        CloneVe.BringToFront();
        //CloneVe.Add(emptyBtn);
        
        switch(caseNum){
            case 1:
                LabelClone.text = "<size=75>R</size><color=red>x</color>";
                //FieldHandler rxBtnField = new FieldHandler(CloneVe,1);
                //rxBtnField.innitStart();
                break;
            case 2:
                LabelClone.text = "<size=75>R</size><color=#008000ff>y</color>";
                //FieldHandler ryBtnField = new FieldHandler(CloneVe,2);
                break;
            case 3:
                LabelClone.text = "<size=75>R</size><color=#0000ffff>z</color>";
                //FieldHandler rzBtnField = new FieldHandler(CloneVe,3);
                break;
            case 4:
                LabelClone.text = "<size=75>t</size><color=red>x<color=#008000ff>y<color=#0000ffff>z";
                //FieldHandler txyzBtnField = new FieldHandler(CloneVe,4);
                //disablefields();
                break;
            case 5:
                LabelClone.text = "<size=75>M</size>";
                //FieldHandler mBtnField = new FieldHandler(CloneVe,5);
                break;
            case 6:
                count++;
                LabelClone.text ="{"+count+"}";
                
                break;

        }
        CloneVe.style.position=Position.Absolute;
        CloneVe.style.left = 339;
        CloneVe.style.top = 29;
        secA_2.Add(CloneVe);



       

        
        FieldHandler fH = new FieldHandler(CloneVe,caseNum,
                                        drField,
                                        expressionBtn,
                                        valueBtn,
                                        TextFields
        
                                                            );
        
        
      
        
        if(caseNum<4){
            DragDrop ddr = new DragDrop(CloneVe,secA_2,false,0);
            UtilityArr.Add(fH);
            setArr.Add(caseNum+"");
            enablefields();
        }
        else if (caseNum == 4)
        {
            UtilityArr.Add(fH);
            setArr.Add(caseNum + "");
            disablefields();
        }
        else if(caseNum==6){
            setArr.Add("{"+count+"}");
            disablefields();
        }
        
        MatrixCalculator MC = new MatrixCalculator(UtilityArr,count,setArr,Labels,CloneVe);
        CloneVe.Focus();
    }
    private void drag(int switchNum){
        if(UtilityData.check(switchNum,count)==true){return;}
        switch(switchNum){
            case 1:
               Clone = new CloneUtilityHover(rxVE);
                break;
            case 2:
                Clone = new CloneUtilityHover(ryVE);
                break;
            case 3:
               Clone = new CloneUtilityHover(rzVE);
                break;
            case 4:
                Clone = new CloneUtilityHover(txyzVE);
                
                break;
            case 5:
                Clone = new CloneUtilityHover(MVE);
                
                break;
            case 6:
                count++;
                Clone = new CloneUtilityHover(BraceVE,count);
                
                break;
        }

        VE =Clone.cloneProcess(switchNum);
        if(switchNum==4){
            DragDrop ddr = new DragDrop(VE,secA_2,true,4);
        }

        VE.style.visibility = Visibility.Visible;
        
        FieldHandler fH = new FieldHandler(VE,switchNum,
                                        drField,
                                        expressionBtn,
                                        valueBtn,
                                        TextFields
        
                                                            );
        

        if(switchNum<4){
            DragDrop ddr_6 = new DragDrop(VE,secA_2,true,switchNum);
            UtilityArr.Add(fH);
            setArr.Add(switchNum+"");
            enablefields();
        }
        else if (switchNum == 4)
        {
            UtilityArr.Add(fH);
            setArr.Add(switchNum + "");
            disablefields();
        }
        else if(switchNum==6){
            setArr.Add("{"+count+"}");
            DragDrop ddr_c = new DragDrop(VE,secA_2,true,switchNum);
            disablefields();
        }
        MatrixCalculator MC = new MatrixCalculator(UtilityArr,count,setArr,Labels,VE);
        
    }

    private void degreesORRadians(int val){
        
        if(val==1){
            
            drField.value =Math.Round((int.Parse(drField.text)*Mathf.PI)/180,4) + " ";
        }
        if(val ==-1){
            
            drField.value =Math.Round((double.Parse(drField.text)*1/Mathf.PI*180))+"";
        }
    }
    public static int getSetCount(){
        return count;
    }
    
    public void disablefields()
    {
        drField.focusable = false;
        degreeBtn.SetEnabled(false);
        radiansBtn.SetEnabled(false);
        expressionBtn.SetEnabled(false);
        valueBtn.SetEnabled(false);
        BraceVE.SetEnabled(false);
        braceAddBtn.SetEnabled(false);
    }
    public void enablefields()
    {
        drField.focusable = true;
        degreeBtn.SetEnabled(true);
        radiansBtn.SetEnabled(true);
        expressionBtn.SetEnabled(true);
        valueBtn.SetEnabled(true);
        BraceVE.SetEnabled(true);
        braceAddBtn.SetEnabled(true);
    }

    

}