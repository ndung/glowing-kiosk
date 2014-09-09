/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.device.controller;

import com.ics.ssk.ego.dao.ParameterDao;
import com.ics.ssk.ego.device.SocketServerProvider;

/**
 *
 * @author ndung
 */
public class PrinterController {
    
    public static final String DEVICE_CODE = "02";
    
    /**
    SocketConnectionServer server;    
    public void setServer(SocketConnectionServer server) {
        this.server = server;
    }*/
    
    ParameterDao parameterDao;    

    public void setParameterDao(ParameterDao parameterDao) {
        this.parameterDao = parameterDao;
    }
    
    SocketServerProvider server;
    
    public void setServer(SocketServerProvider server) {
        this.server = server;
    }   
    
    @SuppressWarnings({"SleepWhileInLoop", "CallToThreadDumpStack"})
    public boolean printReceipt(String message, String trxId) {
        try{
            String command = "01";
            server.transmit("RQ".concat(DEVICE_CODE).concat(command).concat("  ").concat(trxId).concat(message));
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterDao.getParameter("device.timeout").getValue());
            String msgFromDevice = null;
            while (System.currentTimeMillis() < timeout) {                
                msgFromDevice = server.getDeviceMessageMap().remove(DEVICE_CODE + command + trxId);
                if (msgFromDevice!=null){
                    break;
                }
                Thread.sleep(10);
            }
            if (msgFromDevice != null) {            
                String rc = msgFromDevice.substring(6, 8);
                if (rc.equals("00")) {
                    return true;
                }            
            }   
        }catch(Exception e){
            e.printStackTrace();
        }
        return false;
    }
}
