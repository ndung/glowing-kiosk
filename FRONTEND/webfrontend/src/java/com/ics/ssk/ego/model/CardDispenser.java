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
public class CardDispenser implements Serializable{
    
    private String deviceCode;
    private int currentAmount;
    private int maxAmount;

    public String getDeviceCode() {
        return deviceCode;
    }

    public void setDeviceCode(String deviceCode) {
        this.deviceCode = deviceCode;
    }

    public int getCurrentAmount() {
        return currentAmount;
    }

    public void setCurrentAmount(int currentAmount) {
        this.currentAmount = currentAmount;
    }

    public int getMaxAmount() {
        return maxAmount;
    }

    public void setMaxAmount(int maxAmount) {
        this.maxAmount = maxAmount;
    }
    
    
}
