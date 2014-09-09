/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.device;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.io.IOException;
import java.net.Socket;
import java.util.Map;
import org.apache.log4j.Logger;

/**
 *
 * @author ICS Team
 */
public class SocketHandler extends Thread {

    public static final int HEADER_LENGTH = 4;
    private Logger logger = Logger.getLogger(getName());
    private Socket socket;
    private DataInputStream reader;
    private DataOutputStream writer;
    public boolean active = true;
    private Map<String, String> deviceMessageMap;    
    
    public SocketHandler(Socket socket, Map<String, String> deviceMessageMap) throws IOException {

        super("SocketHandler (" + socket.getInetAddress().getHostAddress() + ")");

        this.socket = socket;        
        this.reader = new DataInputStream(socket.getInputStream());
        this.writer = new DataOutputStream(socket.getOutputStream());
        this.deviceMessageMap = deviceMessageMap;
    }

    /**
     * The main loop of this thread. Read incoming message from socket's
     * InputStream, process it, and send back the response.
     *
     *	@Override
     * (non-Javadoc)
     * @see java.lang.Thread#run()
     */
    @Override
    @SuppressWarnings("CallToThreadDumpStack")
    public void run() {
        try {
            while (true) {
                String inMsg = readMessage();
                logger.info("In message=[" + inMsg + "]");

                String msgType = inMsg.substring(0, 2);
                String deviceCode = inMsg.substring(2, 4);
                String commandCode = inMsg.substring(4, 6);                
                                
                if (deviceCode.equals("02") && commandCode.equals("10")){                                        
                                        
                }
                
                if (msgType.equals("NQ")){
                    //echo message
                    String outMsg = inMsg.replace("NQ", "NS") + "00";
                    if (commandCode.equals("01")){                        

                        //send device status to server
                        /**Message msg = new Message();
                        msg.setParam(new HashMap<String, Object>());
                        msg.setTransid(stanDao.getStan());
                        msg.setValue("device_code",deviceCode);
                        String deviceStatus = inMsg.substring(6,8);
                        msg.setValue("device_status",deviceStatus);
                        deviceInventoryDao.updateDeviceStatus(deviceCode, deviceStatus);*/
                        //provider.execute("inquiryDeviceStatus",msg);
                    }
                    //writeMessage(outMsg);
                }else{            
                    String trxId = inMsg.substring(8,14);
                    deviceMessageMap.put(deviceCode+commandCode+trxId, inMsg);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        } finally {
            try {
                if (reader != null) {
                    reader.close();
                }
                if (writer != null) {
                    writer.close();
                }
                if (socket != null) {
                    socket.close();
                }
            } catch (IOException e) {
                e.printStackTrace();
            }
        }
    }

    public String readMessage() throws IOException {
        StringBuilder msg = new StringBuilder("");
        byte[] bLen = new byte[HEADER_LENGTH];
        reader.readFully(bLen, 0, bLen.length);        
        int len = Integer.parseInt(new String(bLen), 10);        
        byte[] data = new byte[len];
        
        if (len > 0) {
            reader.readFully(data, 0, len);
        }

        msg.append(new String(data));

        return msg.toString();
    }

    public void writeMessage(String message) throws IOException {
        String h = String.valueOf(message.length());
        while (h.length() < HEADER_LENGTH) {
            h = ("0" + h);
        }
        message = h+message;
        logger.info("Out message=["+message+"]");
        byte[] msg = message.getBytes();
        writer.write(msg, 0, msg.length);
        writer.flush();
    }

}
