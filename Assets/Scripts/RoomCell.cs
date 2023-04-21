using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCell : MonoBehaviour
{
    public Behaviour[] behaviorsToEnable;
    public Renderer[] renderersToEnable;
    public bool activated = false;
    public SpriteRenderer[] masks;

    void Start() {
        foreach(SpriteRenderer mask in masks) {
            mask.enabled = true;
        }
        foreach(Behaviour activate in behaviorsToEnable) {
            activate.enabled = false;
        }
        foreach(Renderer activate in renderersToEnable) {
            activate.enabled = false;
        }
        
        if(activated) Activate();
    }

    public void Activate() {
        activated = true;
        
        foreach(SpriteRenderer mask in masks) {
            mask.enabled = false;
        }

        foreach(Behaviour activate in behaviorsToEnable) {
            activate.enabled = true;
        }
        foreach(Renderer activate in renderersToEnable) {
            activate.enabled = true;
        }
    }
}
