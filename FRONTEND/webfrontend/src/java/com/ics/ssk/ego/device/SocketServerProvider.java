/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.device;

import com.ics.ssk.ego.dao.ParameterDao;
import com.ics.ssk.ego.iso8583.PrefixedStringCodecFactory;
import java.net.InetSocketAddress;
import java.util.Map;
import java.util.concurrent.Executor;
import org.apache.log4j.Logger;
import org.apache.mina.core.service.IoHandlerAdapter;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFactory;
import org.apache.mina.filter.codec.ProtocolCodecFilter;
import org.apache.mina.transport.socket.SocketSessionConfig;
import org.apache.mina.transport.socket.nio.NioSocketAcceptor;
import org.springframework.beans.factory.DisposableBean;
import org.springframework.beans.factory.InitializingBean;

/**
 *
 * @author ndung
 */
public class SocketServerProvider extends IoHandlerAdapter implements InitializingBean, DisposableBean {

    private ProtocolCodecFactory delimitingCodecFactory;
    private int socketBufferSize = 4 * 1024;
    private int port = 3400;
    private NioSocketAcceptor acceptor;
    private Logger logger = Logger.getLogger(SocketServerProvider.class);
    private IoSession session;
    private Executor executor;
    private ParameterDao parameterDao;
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
    public void afterPropertiesSet() throws Exception {
        /**
         * ThreadPoolTaskExecutor taskExecutor = new ThreadPoolTaskExecutor();
         * taskExecutor.setCorePoolSize(50); taskExecutor.setMaxPoolSize(50);
         * taskExecutor.setQueueCapacity(10); taskExecutor.afterPropertiesSet();
         * taskExecutor.initialize(); setTaskExecutor(taskExecutor);
         */
        this.port = Integer.parseInt(parameterDao.getParameter("device.host.port").getValue());
        acceptor = new NioSocketAcceptor();
        SocketSessionConfig sessionConfig = (SocketSessionConfig) acceptor.getSessionConfig();
        sessionConfig.setReceiveBufferSize(socketBufferSize);
        sessionConfig.setSendBufferSize(socketBufferSize);
        delimitingCodecFactory = new PrefixedStringCodecFactory();
        acceptor.getFilterChain().addLast("codec", new ProtocolCodecFilter(delimitingCodecFactory));
        acceptor.setHandler(this);
        acceptor.setCloseOnDeactivation(true);
        acceptor.bind(new InetSocketAddress(port));
        logger.info("Socket is listening on port: " + port);
    }

    @Override
    public void destroy() throws Exception {
        acceptor.unbind();
        logger.info("Shutdown");
    }

    @Override
    public void sessionOpened(IoSession session) throws Exception {
        logger.info("Session opened " + session.getId());
        this.session = session;
    }

    @Override
    public void sessionClosed(IoSession session) throws Exception {
        logger.info("Session closed " + session.getId());
        this.session = null;
    }
    
    @Override
    public void exceptionCaught(IoSession session, Throwable cause) throws Exception {
        if (session != null) {
            session.close(false);
        }
    }

    @Override
    public void messageReceived(IoSession session, final Object message) throws Exception {
        executor.execute(new RunnableSocketHandler(message, deviceMessageMap));
    }
    
    @SuppressWarnings("CallToThreadDumpStack")
    public void transmit(String message) {
        logger.info("out message: "+message);
        try {          
            if (session!=null){
                session.write(message);
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }

}
