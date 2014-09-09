package com.ics.ssk.ego.device;

import com.ics.ssk.ego.dao.ParameterDao;
import com.ics.ssk.ego.iso8583.PrefixedStringCodecFactory;
import java.net.InetSocketAddress;
import java.util.Map;
import java.util.concurrent.Executor;
import org.apache.log4j.Logger;
import org.apache.mina.core.future.ConnectFuture;
import org.apache.mina.core.future.IoFuture;
import org.apache.mina.core.future.IoFutureListener;
import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFactory;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.transport.socket.SocketSessionConfig;
import org.apache.mina.transport.socket.nio.NioSocketConnector;

public class SocketClientProvider extends IoHandlerAdapter {

    Logger LOGGER = Logger.getLogger(SocketClientProvider.class);
    private NioSocketConnector connector;
    private int socketBufferSize = 1 * 1024;
    private long reconnectDelay = 5000;
    private boolean keepReconnecting = true;
    private ParameterDao parameterDao;
    private String ipAddress;
    private Integer port;
    private IoSession session;
    private Executor executor;
    private Map<String, String> deviceMessageMap;
    
    public void setExecutor(Executor executor) {
        this.executor = executor;
    }

    public void setParameterDao(ParameterDao parameterDao) {
        this.parameterDao = parameterDao;
    }

    public void setDeviceMessageMap(Map<String, String> deviceMessageMap) {
        this.deviceMessageMap = deviceMessageMap;
    }

    public Map<String, String> getDeviceMessageMap() {
        return deviceMessageMap;
    }

    @Override
    public void messageReceived(IoSession session, final Object message) throws Exception {
        executor.execute(new RunnableSocketHandler(message, deviceMessageMap));
    }

    @Override
    public void sessionOpened(IoSession session) throws Exception {
        this.session = session;
    }

    @Override
    public void sessionClosed(IoSession session) throws Exception {
        LOGGER.info("Session null, closed");
        this.session = null;
        keepReconnecting = true;
        LOGGER.info("reconnecting");
        reconnectUntilSuccessful();
    }

    @Override
    public void exceptionCaught(IoSession session, Throwable cause) throws Exception {
        if (session != null) {
            session.close(false);
        }
    }

    public boolean start() {
        try {
            //this.ipAddress = parameterDao.getParameter("payout.host.addr").getValue();
            //this.port = Integer.parseInt(parameterDao.getParameter("payout.host.port").getValue());
            ProtocolCodecFactory delimitingCodecFactory = new PrefixedStringCodecFactory();
            connector = new NioSocketConnector();
            SocketSessionConfig sessionConfig = connector.getSessionConfig();
            sessionConfig.setReceiveBufferSize(socketBufferSize);
            sessionConfig.setSendBufferSize(socketBufferSize);
            connector.getFilterChain().addLast("codec", new ProtocolCodecFilter(delimitingCodecFactory));
            connector.setHandler(this);            
            connector.setDefaultRemoteAddress(new InetSocketAddress("localhost",11000));
            reconnectUntilSuccessful();
        } catch (Exception ex) {
            ex.printStackTrace();
            return false;
        }
        return true;
    }

    public void reconnectUntilSuccessful() {
        if (!keepReconnecting) {
            return;
        }

        LOGGER.info("connecting to "
                + connector.getDefaultRemoteAddress().getHostName() + ":"
                + connector.getDefaultRemoteAddress().getPort() + ".");

        final ConnectFuture connectFuture = connector.connect();
        connectFuture.addListener(new IoFutureListener<IoFuture>() {
            @Override
            public void operationComplete(IoFuture future) {
                if (!connectFuture.isConnected()) {
                    try {
                        Thread.sleep(reconnectDelay);
                    } catch (Exception e) {
                    }

                    reconnectUntilSuccessful();
                } else {
                    SocketClientProvider.this.session = connectFuture.getSession();
                    LOGGER.info("connected to " + connector.getDefaultRemoteAddress().getHostName() + ":"
                            + connector.getDefaultRemoteAddress().getPort() + ".");
                }
            }
        });
    }

    public boolean transmit(String message) {
        try {
            if (session != null) {
                LOGGER.info("outgoing message :"+message);
                session.write(message);
                return true;
            } else {
                return false;
            }
        } catch (Exception ex) {
            ex.printStackTrace();
            return false;
        }
    }
        
}
