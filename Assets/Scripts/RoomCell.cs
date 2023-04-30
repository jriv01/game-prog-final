using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCell : MonoBehaviour
{
    public MonoBehaviour[] behaviorsToEnable;
    public Renderer[] renderersToEnable;
    public bool activated = false;
    public SpriteRenderer[] masks;

    void Awake() {
        foreach(SpriteRenderer mask in masks) {
            mask.enabled = true;
        }
        foreach(MonoBehaviour activate in behaviorsToEnable) {
            activate.enabled = false;
        }
        foreach(Renderer activate in renderersToEnable) {
            activate.enabled = false;
        }
        
        if(activated) Activate();
    }

    public void Activate() {
        if(!activated) {
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
        activated = true;
    }
}
