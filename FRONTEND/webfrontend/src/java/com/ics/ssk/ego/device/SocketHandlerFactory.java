/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.device;

import java.io.IOException;
import java.net.Socket;
import java.util.Map;

/**
 *
 * @author ICS Team
 */
public class SocketHandlerFactory {

    Map<String, String> deviceMessageMap;

    public void setDeviceMessageMap(Map<String, String> deviceMessageMap) {
        this.deviceMessageMap = deviceMessageMap;
    }        
    
    public SocketHandlerFactory(){

    }

    /**
     * Create a SocketHandler using given socket and a shared
     * <code>MessageHandler</code>.
     *
     * @param socket
     * @return
     * @throws IOException
     */
    public SocketHandler createHandler(Socket socket) throws IOException {
        return new SocketHandler(socket, deviceMessageMap);
    }

    
}
