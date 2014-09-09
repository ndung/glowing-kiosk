/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.device;

import com.ics.ssk.ego.dao.ParameterDao;
import java.io.IOException;
import java.net.ServerSocket;
import java.util.Map;

/**
 *
 * @author ICS Team
 */
public class SocketConnectionServer {

    private Integer port;
    
    private SocketHandlerFactory handlerFactory;
    private ParameterDao parameterDao;
    
    private boolean stop;
    private SocketHandler socketHandler;
    Map<String, String> deviceMessageMap;
    
    ServerSocket serverSocket;
    
    public SocketConnectionServer() {        
    }

    public void setHandlerFactory(SocketHandlerFactory handlerFactory) {
        this.handlerFactory = handlerFactory;
    }

    public void setParameterDao(ParameterDao parameterDao) {
        this.parameterDao = parameterDao;
    }        
    
    

    /**
     * Starts a thread to listen any incoming connection. Every connection will
     * be handled in a new separated thread.
     *
     * @throws IOException
     */
    public void start() throws IOException {        
        stop = false;
        this.port = Integer.parseInt(parameterDao.getParameter("device.host.port").getValue());
        serverSocket = new ServerSocket(port);
        // fork here
        new Thread(new Runnable() {

            @SuppressWarnings("CallToThreadDumpStack")
            @Override
            public void run() {
                while (!stop) {
                    try {
                        // start new handler for every socket connection
                        System.out.println("receive new connection from device...");
                        socketHandler = handlerFactory.createHandler(serverSocket.accept());
                        System.out.println("socketHandler:"+socketHandler);
                        socketHandler.start();                        
                    } catch (IOException e) {
                        e.printStackTrace();
                    }
                }
            }
        }).start();
    }

    @SuppressWarnings("CallToThreadDumpStack")
    public void transmit(String message) {
        try {
            System.out.println("socketHandler:"+socketHandler);
            socketHandler.writeMessage(message);
        } catch (IOException ex) {
            ex.printStackTrace();
        }
    }

    /**
     * This method will stop the server to accept new connection, but not close
     * already handled ones.
     *
     */
    public void stop() throws IOException {
        stop = true;
        serverSocket.close();
    }

    public Map<String, String> getDeviceMessageMap() {
        return deviceMessageMap;
    }

    public void setDeviceMessageMap(Map<String, String> deviceMessageMap) {
        this.deviceMessageMap = deviceMessageMap;
    }
 
    
}
