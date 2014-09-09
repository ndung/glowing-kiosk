/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */

package com.ics.ssk.ego.scheduler;

import com.ics.ssk.ego.dao.DeviceDao;
import com.ics.ssk.ego.model.CardDispenser;
import com.ics.ssk.ego.model.Device;
import com.ics.ssk.ego.model.SmartPayout;
import com.ics.ssk.ego.util.StringUtil;
import java.util.List;

/**
 *
 * @author ICS Team
 */
public class DeviceStatusJob implements Executable{

    private DeviceDao deviceInventoryDao;    
    
    @Override
    public void execute() throws Exception {
        List<Device> list1 = deviceInventoryDao.getDeviceStatus();
        StringBuilder sb1 = new StringBuilder(1024);
        sb1.append(StringUtil.pad(String.valueOf(list1.size()), '0', 2, StringUtil.RIGHT_JUSTIFIED));
        for (Device d : list1){
            sb1.append(d.getDeviceCode());
            sb1.append(d.getDeviceStatus());
        }
        
        StringBuilder sb2 = new StringBuilder(1024);
        List<SmartPayout> list2 = deviceInventoryDao.getSmartPayoutInventory();
        sb2.append(StringUtil.pad(String.valueOf(list2.size()), '0', 2, StringUtil.RIGHT_JUSTIFIED));
        int temp = 0;
        for (SmartPayout sp : list2){
            sb2.append(StringUtil.pad(sp.getDeviceCode(), ' ', 2, StringUtil.LEFT_JUSTIFIED));
            sb2.append(StringUtil.pad(String.valueOf(sp.getIdx()), ' ', 2, StringUtil.LEFT_JUSTIFIED));
            sb2.append(StringUtil.pad(String.valueOf(sp.getDenom()), '0', 12, StringUtil.RIGHT_JUSTIFIED));
            sb2.append(StringUtil.pad(String.valueOf(sp.getMaxPayoutNote()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
            sb2.append(StringUtil.pad(String.valueOf(sp.getCurrentPayoutNote()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
            sb2.append(StringUtil.pad(String.valueOf(sp.getCurrentCashboxNote()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
            sb2.append(StringUtil.pad(String.valueOf(sp.getCurrentRouting()), ' ', 1, StringUtil.LEFT_JUSTIFIED));
            temp = temp + (sp.getCurrentCashboxNote()*sp.getDenom()) +(sp.getCurrentPayoutNote()*sp.getDenom());
        }
       
        StringBuilder sb3 = new StringBuilder(1024);
        List<CardDispenser> list3 = deviceInventoryDao.getCardDispenserInventory();
        sb3.append(StringUtil.pad(String.valueOf(list3.size()), '0', 2, StringUtil.RIGHT_JUSTIFIED));
        for (CardDispenser cd : list3){
            sb3.append(StringUtil.pad(cd.getDeviceCode(), ' ', 2, StringUtil.LEFT_JUSTIFIED));
            sb3.append(StringUtil.pad(String.valueOf(cd.getCurrentAmount()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
        }
       
    }

    public void setDeviceInventoryDao(DeviceDao deviceInventoryDao) {
        this.deviceInventoryDao = deviceInventoryDao;
    }
       
}
