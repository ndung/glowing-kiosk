/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.device;

import java.util.Map;
import org.apache.log4j.Logger;

/**
 *
 * @author lmanik
 */
public class RunnableSocketHandler implements Runnable {
    
    Logger logger = Logger.getLogger(RunnableSocketHandler.class);

    private Object message;
    private Map<String, String> deviceMessageMap;

    public RunnableSocketHandler(Object message, Map<String, String> deviceMessageMap) {
        this.message = message;
        this.deviceMessageMap = deviceMessageMap;
    }

    @Override
    public void run() {
        String inMsg = (String) message;
        logger.info("in message: " + inMsg);
        String msgType = inMsg.substring(0, 2);
        String deviceCode = inMsg.substring(2, 4);
        String commandCode = inMsg.substring(4, 6);

        if (deviceCode.equals("02") && commandCode.equals("10")) {
        }

        if (msgType.equals("NQ")) {
            //echo message
            String outMsg = inMsg.replace("NQ", "NS") + "00";
            if (commandCode.equals("01")) {
                //send device status to server
                /**
                 * Message msg = new Message(); msg.setParam(new HashMap<String,
                 * Object>()); msg.setTransid(stanDao.getStan());
                 * msg.setValue("device_code",deviceCode); String deviceStatus =
                 * inMsg.substring(6,8);
                 * msg.setValue("device_status",deviceStatus);
                 * deviceInventoryDao.updateDeviceStatus(deviceCode,deviceStatus);
                 */
                //provider.execute("inquiryDeviceStatus",msg);
            }
            //transmit(outMsg);                
        } else {
            String trxId = inMsg.substring(8, 14);
            deviceMessageMap.put(deviceCode + commandCode + trxId, inMsg);
        }
    }
}