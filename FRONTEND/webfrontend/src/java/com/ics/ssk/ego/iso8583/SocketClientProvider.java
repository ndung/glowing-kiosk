package com.ics.ssk.ego.iso8583;

import com.ics.ssk.ego.dao.ParameterDao;
import java.net.InetSocketAddress;
import java.util.Map;
import java.util.TreeMap;
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
import org.jpos.iso.ISOMsg;

public class SocketClientProvider extends IoHandlerAdapter {

    Logger LOGGER = Logger.getLogger(SocketClientProvider.class);
    private NioSocketConnector connector;
    private int socketBufferSize = 1 * 1024;
    private long reconnectDelay = 5000;
    private boolean keepReconnecting = true;
    private Map<String, ISOMsg> messageMap;
    private ParameterDao parameterDao;

    public SocketClientProvider() {
    }
    public IoSession session;

    @Override
    public void messageReceived(IoSession session, final Object message) throws Exception {
        LOGGER.info("message received : " + (String) message);
        ISOMsg isoMsg = new ISOMsg();
        isoMsg.setPackager(PackagerFactory.getPackager());
        isoMsg.unpack(((String) message).getBytes());
        messageMap.put(isoMsg.getString(11) + isoMsg.getString(7), isoMsg);
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
            ProtocolCodecFactory delimitingCodecFactory = new PrefixedStringCodecFactory();
            connector = new NioSocketConnector();
            SocketSessionConfig sessionConfig = connector.getSessionConfig();
            sessionConfig.setReceiveBufferSize(socketBufferSize);
            sessionConfig.setSendBufferSize(socketBufferSize);
            connector.getFilterChain().addLast("codec", new ProtocolCodecFilter(delimitingCodecFactory));
            connector.setHandler(this);
            connector.setDefaultRemoteAddress(new InetSocketAddress(parameterDao.getParameter("server.address").getValue(),
                    Integer.parseInt(parameterDao.getParameter("server.port").getValue())));
            messageMap = new TreeMap<String, ISOMsg>();
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
                LOGGER.info("transmitting message : ["+ message + "]");
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

    public Map<String, ISOMsg> getMessageMap() {
        return messageMap;
    }

    public void setParameterDao(ParameterDao parameterDao) {
        this.parameterDao = parameterDao;
    }
}
