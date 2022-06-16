using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using System.Threading;

public class CloneUtilityHover
{
    private int caseNum;
    private VisualElement targetparent;
    private int count=1;
    private VisualElement target;
    

    public CloneUtilityHover(VisualElement target){
     targetparent=target.parent;
     this.target = target;
     
     
 }
    
    
  
    public VisualElement cloneProcess(int caseNum){
        
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
        //CloneVe.Add(emptyBtn);
        
        targetparent.Add(CloneVe);
        CloneVe.style.position=Position.Absolute;
        float x = target.resolvedStyle.left;
        float y = target.resolvedStyle.top;
        CloneVe.style.left=x;
        CloneVe.style.top=y;
        switch(caseNum){
            case 1:
                LabelClone.text = "<size=75>R</size><color=red>x</color>";
                //DragDrop ddr = new DragDrop(CloneVe);
                break;
            case 2:
                LabelClone.text = "<size=75>R</size><color=#008000ff>y</color>";
                break;
            case 3:
                LabelClone.text = "<size=75>R</size><color=#0000ffff>z</color>";
                break;
            case 4:
                LabelClone.text = "<size=75>t</size><color=red>x<color=#008000ff>y<color=#0000ffff>z";
                break;
            case 5:
                LabelClone.text = "<size=75>M</size>";
                break;
            case 6:
                count++;
                LabelClone.text ="{"+count+"}";
                break;

        }
        CloneVe.style.visibility = Visibility.Hidden;
        //veFinal = CloneVe;
        return CloneVe;

        
        
    }



}
