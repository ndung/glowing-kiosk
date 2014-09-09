package com.ics.ssk.ego.util;

import java.io.DataInputStream;
import java.io.DataOutputStream;
import java.net.Socket;

public class SocketClient {

    private Socket clientSocket;
    private DataOutputStream outToServer;
    private DataInputStream inFromServer;

    public String sendMessage(String ipDest, int port, String message) {
        int delay = 0;
//    	int     port   = 8119;
        boolean reply = false;
        try {
            clientSocket = new Socket(ipDest, port);
            clientSocket.setSoTimeout(2 * 60 * 1000);
            inFromServer = new DataInputStream(clientSocket.getInputStream());
            outToServer = new DataOutputStream(clientSocket.getOutputStream());

            System.out.println("[" + message + "]");

            outToServer.write(message.getBytes());
            outToServer.flush();

            byte[] bLen = new byte[4];
            inFromServer.readFully(bLen, 0, bLen.length);
            int len = Integer.parseInt(new String(bLen), 10);
            byte[] data = new byte[len];
            if (len > 0) {
                inFromServer.readFully(data, 0, len);
                message = new String(bLen) + new String(data);
            } else {
                message = null;
            }

            System.out.println("HASIL : " + message);
            //message = inFromServer.readLine().toString();

            inFromServer.close();
            outToServer.close();
            clientSocket.close();

            return message;
        } catch (Exception e) {
            System.out.println(message);
            e.printStackTrace();
            return null;
        }
    }
}
