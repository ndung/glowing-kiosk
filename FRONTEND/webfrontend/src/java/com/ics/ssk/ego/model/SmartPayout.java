/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.model;

import java.io.Serializable;

/**
 *
 * @author ndung
 */
public class SmartPayout implements Serializable{
    
    private String deviceCode;
    private int idx;
    private int denom;    
    private int maxPayoutNote;
    private int currentPayoutNote;
    private int currentCashboxNote;
    private int currentRouting;
    private String description;

    public String getDeviceCode() {
        return deviceCode;
    }

    public void setDeviceCode(String deviceCode) {
        this.deviceCode = deviceCode;
    }

    public int getIdx() {
        return idx;
    }

    public void setIdx(int idx) {
        this.idx = idx;
    }

    public int getDenom() {
        return denom;
    }

    public void setDenom(int denom) {
        this.denom = denom;
    }

    public int getMaxPayoutNote() {
        return maxPayoutNote;
    }

    public void setMaxPayoutNote(int maxPayoutNote) {
        this.maxPayoutNote = maxPayoutNote;
    }

    public int getCurrentPayoutNote() {
        return currentPayoutNote;
    }

    public void setCurrentPayoutNote(int currentPayoutNote) {
        this.currentPayoutNote = currentPayoutNote;
    }

    public int getCurrentCashboxNote() {
        return currentCashboxNote;
    }

    public void setCurrentCashboxNote(int currentCashboxNote) {
        this.currentCashboxNote = currentCashboxNote;
    }

    public int getCurrentRouting() {
        return currentRouting;
    }

    public void setCurrentRouting(int currentRouting) {
        this.currentRouting = currentRouting;
    }

    public String getDescription() {
        return description;
    }

    public void setDescription(String description) {
        this.description = description;
    }
    
    
}
