/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.dao;

import com.ics.ssk.ego.model.CardDispenser;
import com.ics.ssk.ego.model.Device;
import com.ics.ssk.ego.model.SmartPayout;
import java.util.List;

/**
 *
 * @author ndung
 */
public interface DeviceDao {
    
    public List<SmartPayout> getSmartPayoutInventory();
    public SmartPayout getSmartPayoutInventory(String deviceCode, int denom);
    public void updateCurrentRouting(String deviceCode, int denom, int routing);
    public List<SmartPayout> getNonZeroSmartPayoutInventory(String deviceCode);
    public void updateSmartPayoutInventory(String deviceCode, int denom, int currentPayoutNote, String flag);
    public void updateCashboxInventory(String deviceCode, int denom, int currentCashNote);
    public void insertSmartPayoutHistory(String deviceCode, int denom, String command, int payout, int cashbox);
    public void insertSmartPayoutHistory(String deviceCode, int denom, String command, int payout, int cashbox, String stan);
    public void updateSmartPayoutHistory(String stan, String command, String deviceCode, int denom);    
    public void emptySmartPayout(String deviceCode);
    public void clearSmartPayout(String deviceCode);
    public void floatSmartPayout(String deviceCode, int denom, int currentPayoutNote);

    public void updateDeviceStatus(String deviceCode, String deviceStatusCode);
    public String getDeviceStatus(String deviceCode);
    public List<Device> getDeviceStatus();

    public void updateCardDispenserInventory(String deviceCode, int cardAmount);
    public List<CardDispenser> getCardDispenserInventory();
    
    public void executeSql(String sql);
    public List<String> getBackupTableData(final String table, String sql, final String kioskId);    
}
