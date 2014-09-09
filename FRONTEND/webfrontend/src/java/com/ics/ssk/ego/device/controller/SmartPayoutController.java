/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.device.controller;

import com.ics.ssk.ego.dao.DeviceDao;
import com.ics.ssk.ego.dao.ParameterDao;
import com.ics.ssk.ego.device.SocketClientProvider;
import com.ics.ssk.ego.device.SocketServerProvider;
import com.ics.ssk.ego.model.SmartPayout;
import com.ics.ssk.ego.util.StringUtil;

/**
 *
 * @author ndung
 */
public class SmartPayoutController {

    public static final String DEVICE_CODE = "01";
    /**
    SocketConnectionServer server;    
    public void setServer(SocketConnectionServer server) {
        this.server = server;
    }*/
    SocketClientProvider server;
    ParameterDao parameterDao;
    DeviceDao deviceDao;  

    public void setServer(SocketClientProvider server) {
        this.server = server;
    }        

    public void setParameterDao(ParameterDao parameterDao) {
        this.parameterDao = parameterDao;
    }

    public void setDeviceDao(DeviceDao deviceDao) {
        this.deviceDao = deviceDao;
    }        
    
    public boolean isSSPStarted(){
        String deviceStatus = deviceDao.getDeviceStatus(DEVICE_CODE);
        if (deviceStatus.equals("00")){
            return true;
        }
        return false;
    }

    @SuppressWarnings("SleepWhileInLoop")
    public boolean runSSP(String trxId) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 01 (run ssp)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         */
        boolean result = true;
        String command = "01";        
        server.transmit("RQ".concat(DEVICE_CODE).concat(command).concat("  ").concat(trxId));        
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            while (System.currentTimeMillis() < timeout) {                                                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (msgFromDevice == null) {
                deviceDao.updateDeviceStatus(DEVICE_CODE, "50");
                result = false;
            } else {
                String rc = msgFromDevice.substring(6, 8);
                if (!rc.equals("00")) {
                    result = false;
                }
                deviceDao.updateDeviceStatus(DEVICE_CODE, rc);
            }
        } catch (Exception ex) {
            result = false;
        }
        return result;
    }

    @SuppressWarnings({"SleepWhileInLoop", "CallToThreadDumpStack"})
    public String acceptNote(String trxId)  {
        System.out.println("acceptNote...0");
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 02 (accept note)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456
         * - flag        : 14-15 = 0/1 (0: payout, 1: cashbox)
         * - amount      : 15-27 = 000000000000
         */
        try{            
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());
            String command = "02";        
            while (System.currentTimeMillis() < timeout) {                                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);                
            }
            String inMsg = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (inMsg != null) {                           
                String flag = inMsg.substring(14, 15);
                String amount = StringUtil.removeLeftZeroPad(inMsg.substring(15, 27));
                if (flag.equals("0")) {
                    deviceDao.insertSmartPayoutHistory(DEVICE_CODE, Integer.parseInt(amount), "accept", 1, 0);
                    deviceDao.updateSmartPayoutInventory(DEVICE_CODE, Integer.parseInt(amount), 1, flag);
                    SmartPayout sp = deviceDao.getSmartPayoutInventory(DEVICE_CODE, Integer.parseInt(amount));
                    if (sp.getCurrentPayoutNote() > sp.getMaxPayoutNote()) {
                        if (sp.getCurrentRouting() == 0) {
                            server.transmit("RQ" + DEVICE_CODE + "09" + "  " + sp.getIdx() + "1");
                            deviceDao.updateCurrentRouting(DEVICE_CODE, Integer.parseInt(amount), 1);
                        }
                    } else {
                        if (sp.getCurrentRouting() == 1) {
                            server.transmit("RQ" + DEVICE_CODE + "09" + "  " + sp.getIdx() + "0");
                            deviceDao.updateCurrentRouting(DEVICE_CODE, Integer.parseInt(amount), 0);
                        }
                    }
                } else if (flag.equals("1")) {
                    deviceDao.insertSmartPayoutHistory(DEVICE_CODE, Integer.parseInt(amount), "accept", 0, 1);
                    deviceDao.updateSmartPayoutInventory(DEVICE_CODE, Integer.parseInt(amount), 1, flag);
                }
                return amount;
            }
        }catch(Exception e){
            e.printStackTrace();
        }
        return null;
    }

    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public boolean dispenseNote(String trxId, String amount) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 03 (dispense note)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         * - amount      : 14-26 = 000000000000
         */
        boolean result = true;        
        String command = "03";
        StringBuilder sb = new StringBuilder(1024);
        sb.append("RQ").append(DEVICE_CODE).append(command).append("  ").append(trxId).append(StringUtil.leftZeroPad((String) amount, 12));
        server.transmit(sb.toString());
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            System.out.println("server.getDeviceMessageMap():"+server.getDeviceMessageMap());
            while (System.currentTimeMillis() < timeout) {                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);    
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (msgFromDevice == null) {
                result = false;
            } else {                
                String rc = msgFromDevice.substring(6, 8);                
                if (rc.equals("00")) {
                    deviceDao.updateSmartPayoutInventory(DEVICE_CODE, Integer.parseInt(amount), -1, "0");
                    deviceDao.insertSmartPayoutHistory(DEVICE_CODE, Integer.parseInt(amount), "dispense", -1, 0, trxId);
                } else {
                    result = false;
                }

            }
        } catch (Exception ex) {
            ex.printStackTrace();
            result = false;
        }
        return result;
    }

    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public boolean stopSSP(String trxId) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 04 (stop ssp)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         */
        boolean result = true;        
        String command = "04";
        server.transmit("RQ".concat(DEVICE_CODE).concat(command).concat("  ").concat(trxId));
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            while (System.currentTimeMillis() < timeout) {                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);    
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (msgFromDevice == null) {
                deviceDao.updateDeviceStatus(DEVICE_CODE, "50");
                result = false;
            } else {
                String rc = msgFromDevice.substring(6, 8);                
                if (rc.equals("00")) {
                    deviceDao.updateDeviceStatus(DEVICE_CODE, "02");
                } else {
                    result = false;
                    deviceDao.updateDeviceStatus(DEVICE_CODE, rc);
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            result = false;
        }
        return result;
    }
    
    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public boolean resetSSP(String trxId) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 05 (reset ssp)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         */
        boolean result = true;        
        String command = "05";
        server.transmit("RQ".concat(DEVICE_CODE).concat(command).concat("  ").concat(trxId));
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            while (System.currentTimeMillis() < timeout) {                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);    
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (msgFromDevice == null) {
                deviceDao.updateDeviceStatus(DEVICE_CODE, "50");
                result = false;
            } else {
                String rc = msgFromDevice.substring(6, 8);                
                if (rc.equals("00")) {
                    deviceDao.updateDeviceStatus(DEVICE_CODE, "02");
                } else {
                    result = false;
                    deviceDao.updateDeviceStatus(DEVICE_CODE, rc);
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            result = false;
        }
        return result;
    }
    
    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public boolean emptySSP(String trxId) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 06 (empty ssp)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         */
        boolean result = true;        
        String command = "06";
        server.transmit("RQ".concat(DEVICE_CODE).concat(command).concat("  ").concat(trxId));
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            System.out.println("timeout:"+timeout);
            while (System.currentTimeMillis() < timeout) {                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);    
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            System.out.println("msgFromDevice:"+msgFromDevice);
            if (msgFromDevice == null) {
                deviceDao.updateDeviceStatus(DEVICE_CODE, "50");
                result = false;
            } else {
                String rc = msgFromDevice.substring(6, 8);                
                if (rc.equals("00")) {
                    System.out.println("1");
                    deviceDao.updateDeviceStatus(DEVICE_CODE, "02");
                    System.out.println("2");
                    deviceDao.emptySmartPayout(DEVICE_CODE);  
                    System.out.println("3");
                    result = stopSSP(trxId);                    
                    System.out.println("4");
                } else {
                    result = false;
                    deviceDao.updateDeviceStatus(DEVICE_CODE, rc);
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            result = false;
        }
        return result;
    }        
    
    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public boolean floatNote(String trxId, String denom, String amount) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 07 (float note)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         * - denom       : 14-26 = 000000000000
         * - amount      : 26-38 = 000000000000
         */
        boolean result = true;        
        String command = "07";
        StringBuilder sb = new StringBuilder(1024);
        sb.append("RQ").append(DEVICE_CODE).append(command).append("  ").append(trxId).
                append(StringUtil.leftZeroPad((String) denom, 12)).
                append(StringUtil.leftZeroPad((String) amount, 12));
        server.transmit(sb.toString());
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            System.out.println("server.getDeviceMessageMap():"+server.getDeviceMessageMap());
            while (System.currentTimeMillis() < timeout) {                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);    
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (msgFromDevice == null) {
                result = false;
            } else {                
                String rc = msgFromDevice.substring(6, 8);                
                if (rc.equals("00")) {
                    deviceDao.floatSmartPayout(DEVICE_CODE, Integer.parseInt(denom), Integer.parseInt(amount));
                } else {
                    result = false;
                }

            }
        } catch (Exception ex) {
            ex.printStackTrace();
            result = false;
        }
        return result;
    }
    
    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public boolean clearSSP(String trxId) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 08 (clear ssp)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         */
        boolean result = true;        
        String command = "08";
        server.transmit("RQ".concat(DEVICE_CODE).concat(command).concat("  ").concat(trxId));
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            while (System.currentTimeMillis() < timeout) {                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);    
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (msgFromDevice == null) {
                deviceDao.updateDeviceStatus(DEVICE_CODE, "50");
                result = false;
            } else {
                String rc = msgFromDevice.substring(6, 8);                
                if (rc.equals("00")) {
                    deviceDao.updateDeviceStatus(DEVICE_CODE, "02");
                    deviceDao.clearSmartPayout(DEVICE_CODE);                    
                } else {
                    result = false;
                    deviceDao.updateDeviceStatus(DEVICE_CODE, rc);
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            result = false;
        }
        return result;
    }
    
    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public boolean setNoteRoute(String trxId, String index, String indexRoute) {
        /**
         * USAGE: 
         * - message type: 0-2   = RQ (request)
         * - device code : 2-4   = 01 (smart payout)
         * - command code: 4-6   = 09 (set note route)
         * - resp. code  : 6-8   = 00 (success)
         * - stan        : 8-14  = 123456         
         * - index no.   : 14-15 = 1/2/3/4/5/...
         * - index route : 15-16 = 0/1
         */
        boolean result = true;        
        String command = "09";
        server.transmit("RQ".concat(DEVICE_CODE).concat(command).concat("  ").concat(trxId).concat(index).concat(indexRoute));
        try {
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());            
            while (System.currentTimeMillis() < timeout) {                
                if (server.getDeviceMessageMap().get(DEVICE_CODE + command + trxId)!=null){
                    break;
                }
                Thread.sleep(100);    
            }
            String msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
            if (msgFromDevice == null) {
                deviceDao.updateDeviceStatus(DEVICE_CODE, "50");
                result = false;
            } else {
                String rc = msgFromDevice.substring(6, 8);                
                if (rc.equals("00")) {                   
                    deviceDao.updateDeviceStatus(DEVICE_CODE, "02");
                } else {
                    result = false;
                    deviceDao.updateDeviceStatus(DEVICE_CODE, rc);
                }
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            result = false;
        }
        return result;
    }
}
