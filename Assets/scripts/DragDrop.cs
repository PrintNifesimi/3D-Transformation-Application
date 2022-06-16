using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;


public class DragDrop : PointerManipulator
{

    public DragDrop(VisualElement target, VisualElement parentReal,bool cloneBool,int caseNum){
        this.target = target;
        root= target.parent;
        this.parentReal = parentReal;
        this.cloneBool = cloneBool;
        this.caseNum=caseNum;
        //this.fieldHandler = fh;
    }
    
    protected override void RegisterCallbacksOnTarget()
    {
        target.RegisterCallback<PointerDownEvent>(PointerDownHandler);
        target.RegisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.RegisterCallback<PointerUpEvent>(PointerCaptureOutHandler);
        //target.RegisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    protected override void UnregisterCallbacksFromTarget()
    {
        target.UnregisterCallback<PointerDownEvent>(PointerDownHandler);
        target.UnregisterCallback<PointerMoveEvent>(PointerMoveHandler);
        target.UnregisterCallback<PointerUpEvent>(PointerCaptureOutHandler);
        //target.UnregisterCallback<PointerCaptureOutEvent>(PointerCaptureOutHandler);
    }

    private Vector2 targetStartPosition { get; set; }

    private Vector3 pointerStartPosition { get; set; }

    private bool enabled { get; set; }
    private int caseNum { get; set; }

    private VisualElement root{ get; }

    private VisualElement parentReal{ get; }
    
    private bool cloneBool { get; set; }
    //private FieldHandler fieldHandler;
    private void PointerDownHandler(PointerDownEvent evt)
    {
        
        targetStartPosition = target.transform.position;
        pointerStartPosition = evt.position;
        target.CapturePointer(evt.pointerId);
        enabled = true;
    }

    private void PointerMoveHandler(PointerMoveEvent evt)
    {
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            Vector3 pointerDelta = evt.position - pointerStartPosition;
           
            //target.transform.position = new Vector2(
                //Mathf.Clamp(targetStartPosition.x + pointerDelta.x, 0, root.panel.visualTree.worldBound.width),
                //Mathf.Clamp(targetStartPosition.y + pointerDelta.y, 0, root.panel.visualTree.worldBound.height));
            

            target.transform.position = new Vector2(targetStartPosition.x+pointerDelta.x,targetStartPosition.y+pointerDelta.y);
            
        }
        
    }

    private void PointerCaptureOutHandler(PointerUpEvent evt)
    {
        
        if (enabled && target.HasPointerCapture(evt.pointerId))
        {
            target.ReleasePointer(evt.pointerId);
        }

//start2
        if (enabled)
        {
            VisualElement slotsContainer = parentReal.Q<VisualElement>("slots");
            UQueryBuilder<VisualElement> allSlots =
                slotsContainer.Query<VisualElement>(className: "slot");
            UQueryBuilder<VisualElement> overlappingSlots =
                allSlots.Where(OverlapsTarget);
            VisualElement closestOverlappingSlot =
                FindClosestSlot(overlappingSlots);
            Vector3 closestPos = Vector3.zero;
            if (closestOverlappingSlot != null)
            {
                closestPos = RootSpaceOfSlot(closestOverlappingSlot);
            if (cloneBool){    
            switch(caseNum){
                    case 1:
                    closestPos = new Vector2(closestPos.x-179, closestPos.y+210);
                    break;

                    case 2:
                    closestPos = new Vector2(closestPos.x-401, closestPos.y+210);
                    break;
                    
                    case 3:
                    closestPos = new Vector2(closestPos.x-620, closestPos.y+210);
                    break;

                    case 4:
                    closestPos = new Vector2(closestPos.x-841, closestPos.y+210);
                    break;

                    case 5:
                    closestPos = new Vector2(closestPos.x-1063, closestPos.y+210);
                    break;

                    case 6:
                    closestPos = new Vector2(closestPos.x-1459, closestPos.y+210);
                    break;
                }} 
                else if(caseNum==0){
                    closestPos = new Vector2(closestPos.x - 340, closestPos.y - 32);
                } 
            }
            
            if (closestOverlappingSlot!=null){
                target.transform.position=closestPos;
                //UnregisterCallbacksFromTarget();
            }else{
               // root.Remove(target);
                target.transform.position = targetStartPosition;
                //fieldHandler.clearTextfields();
                //fieldHandler.setDrfield(false);
                
            }

            enabled = false;
        }


    }

///START ///START
private bool OverlapsTarget(VisualElement slot)
    {
        return target.worldBound.Overlaps(slot.worldBound);
    }

    private VisualElement FindClosestSlot(UQueryBuilder<VisualElement> slots)
    {
        List<VisualElement> slotsList = slots.ToList();
        float bestDistanceSq = float.MaxValue;
        VisualElement closest = null;
        foreach (VisualElement slot in slotsList)
        {
            Vector3 displacement =
                RootSpaceOfSlot(slot) - target.transform.position;
            float distanceSq = displacement.sqrMagnitude;
            if (distanceSq < bestDistanceSq)
            {
                bestDistanceSq = distanceSq;
                closest = slot;
            }
        }
        return closest;
    }

    private Vector3 RootSpaceOfSlot(VisualElement slot)
    {
        Vector2 slotWorldSpace = slot.parent.LocalToWorld(slot.layout.position);
        return root.WorldToLocal(slotWorldSpace);
    }




}