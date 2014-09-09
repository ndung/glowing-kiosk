/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.web;

import com.ics.ssk.ego.dao.DeviceDao;
import com.ics.ssk.ego.device.controller.SmartPayoutController;
import com.ics.ssk.ego.manager.CounterFactoryManager;
import com.ics.ssk.ego.manager.ParameterManager;
import com.ics.ssk.ego.model.Device;
import com.ics.ssk.ego.model.SmartPayout;
import com.ics.ssk.ego.util.FileUtil;
import com.ics.ssk.ego.util.FtpUtil;
import com.ics.ssk.ego.util.StringUtil;
import java.util.List;
import net.sourceforge.stripes.action.After;
import net.sourceforge.stripes.action.Before;
import net.sourceforge.stripes.action.DefaultHandler;
import net.sourceforge.stripes.action.DontValidate;
import net.sourceforge.stripes.action.RedirectResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.action.UrlBinding;
import net.sourceforge.stripes.integration.spring.SpringBean;

/**
 *
 * @author ndung
 */
@UrlBinding("/egosskcommand.html")
public class EgoSendCommand extends BaseActionBean {

    private String command;
    private String fileName;
    private SmartPayoutController smartPayoutController;
    private CounterFactoryManager counterFactoryManager;
    private ParameterManager parameterManager;
    private DeviceDao deviceDao;

    @SpringBean("smartPayoutController")
    @Override
    public void setSmartPayoutController(SmartPayoutController smartPayoutController) {
        this.smartPayoutController = smartPayoutController;
    }

    @SpringBean("counterFactoryManager")
    public void setCounterFactoryManager(CounterFactoryManager counterFactoryManager) {
        this.counterFactoryManager = counterFactoryManager;
    }

    @SpringBean("parameterManager")
    public void setParameterManager(ParameterManager parameterManager) {
        this.parameterManager = parameterManager;
    }

    @SpringBean("deviceDao")
    public void setDeviceDao(DeviceDao deviceDao) {
        this.deviceDao = deviceDao;
    }        

    public String getCommand() {
        return command;
    }

    public void setCommand(String command) {
        this.command = command;
    }

    @Before
    @DontValidate
    public void startup() {
    }

    @After
    @DontValidate
    public void endup() {
    }

    @DontValidate
    public Resolution back() {
        return new RedirectResolution(EgoWelcome.class);
    }

    @DontValidate
    @DefaultHandler
    @SuppressWarnings("CallToThreadDumpStack")
    public Resolution view() {
        if (command.equals("00")) {
            final boolean restart = restartKiosk();
            return new RedirectResolution(EgoBlockScreen.class);
        } 
        else if (command.equals("01")) {
            final boolean shutdown = shutdownKiosk();            
            return new RedirectResolution(EgoBlockScreen.class);
        } 
        else if (command.equals("02")) {
            final boolean empty = emptySmartPayout();
            return new RedirectResolution(EgoBlockScreen.class);
        } 
        else if (command.equals("03")) {
            List<SmartPayout> list = deviceDao.getSmartPayoutInventory();
            StringBuilder sb = new StringBuilder(1024);
            sb.append(StringUtil.pad(String.valueOf(list.size()), '0', 2, StringUtil.RIGHT_JUSTIFIED));
            int temp = 0;
            for (SmartPayout sp : list) {
                sb.append(StringUtil.pad(String.valueOf(sp.getDenom()), '0', 12, StringUtil.RIGHT_JUSTIFIED));
                sb.append(StringUtil.pad(String.valueOf(sp.getCurrentCashboxNote()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
                temp = temp + (sp.getCurrentCashboxNote() * sp.getDenom());
            }
            //TO DO:
            //processing settlement            
            final boolean empty = emptyCashbox();            
            if (empty){
                deviceDao.clearSmartPayout(SmartPayoutController.DEVICE_CODE);
                deviceDao.emptySmartPayout(SmartPayoutController.DEVICE_CODE);
            }
            return new RedirectResolution(EgoBlockScreen.class);
        } 
        else if (command.equals("04")) {
            StringBuilder sb = new StringBuilder(1024);
            List<SmartPayout> list = deviceDao.getSmartPayoutInventory();
            sb.append(StringUtil.pad(String.valueOf(list.size()), '0', 2, StringUtil.RIGHT_JUSTIFIED));
            int temp = 0;
            for (SmartPayout sp : list) {
                sb.append(StringUtil.pad(sp.getDeviceCode(), ' ', 2, StringUtil.LEFT_JUSTIFIED));
                sb.append(StringUtil.pad(String.valueOf(sp.getIdx()), ' ', 2, StringUtil.LEFT_JUSTIFIED));
                sb.append(StringUtil.pad(String.valueOf(sp.getDenom()), '0', 12, StringUtil.RIGHT_JUSTIFIED));
                sb.append(StringUtil.pad(String.valueOf(sp.getMaxPayoutNote()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
                sb.append(StringUtil.pad(String.valueOf(sp.getCurrentPayoutNote()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
                sb.append(StringUtil.pad(String.valueOf(sp.getCurrentCashboxNote()), '0', 3, StringUtil.RIGHT_JUSTIFIED));
                sb.append(StringUtil.pad(String.valueOf(sp.getCurrentRouting()), ' ', 1, StringUtil.LEFT_JUSTIFIED));
                temp = temp + (sp.getCurrentCashboxNote() * sp.getDenom()) + (sp.getCurrentPayoutNote() * sp.getDenom());
            }
            //TO DO:
            //processing send ssp inventory  
            final boolean sent = true;
            return new RedirectResolution(EgoWelcome.class);
        } 
        else if (command.equals("05")) {
            //just block screen
            return new RedirectResolution(EgoBlockScreen.class);
        } 
        else if (command.equals("06")) {
            //just unblock screen 
            return new RedirectResolution(EgoWelcome.class);
        } 
        else if (command.equals("07")) {
            boolean download = downloadFile(fileName);
            if (download) {
                String path = parameterManager.getParameter("ftp.download.path").getValue();
                if (fileName.contains(".war")) {
                    FileUtil.moveFile(path + fileName, parameterManager.getParameter("tomcat.webapps.dir").getValue() + fileName);
                    restartKiosk();
                } else if (fileName.contains(".exe")) {
                    FileUtil.moveFile(path + fileName, parameterManager.getParameter("device.app.dir").getValue() + fileName);
                    restartKiosk();
                }
            }
            return new RedirectResolution(EgoBlockScreen.class);
        } 
        else if (command.equals("08")) {
            downloadFile(fileName);
            String path = parameterManager.getParameter("ftp.download.path").getValue();
            if (fileName.contains(".sql")) {
                updateTable(path + fileName);
                restartKiosk();
            }
            return new RedirectResolution(EgoBlockScreen.class);
        } 
        else if (command.equals("09")) {
            StringBuilder sb = new StringBuilder(1024);
            List<Device> list = deviceDao.getDeviceStatus();
            sb.append(StringUtil.pad(String.valueOf(list.size()), '0', 2, StringUtil.RIGHT_JUSTIFIED));
            for (Device d : list){
                sb.append(StringUtil.pad(d.getDeviceCode(), ' ', 2, StringUtil.LEFT_JUSTIFIED));
                sb.append(StringUtil.pad(d.getDeviceStatus(), ' ', 2, StringUtil.LEFT_JUSTIFIED));
            }
            //TO DO:
            //processing send device status
            return new RedirectResolution(EgoWelcome.class);
        }
        return null;
    }

    @SuppressWarnings("CallToThreadDumpStack")
    public boolean restartKiosk() {
        try {
            String cmd = "shutdown -r -f";
            Runtime.getRuntime().exec(cmd);
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
        return true;
    }

    @SuppressWarnings("CallToThreadDumpStack")
    public boolean shutdownKiosk() {
        try {
            Runtime.getRuntime().exec("shutdown -s -f");
        } catch (Exception e) {
            e.printStackTrace();
            return false;
        }
        return true;
    }

    public boolean emptySmartPayout() {
        //empty smartpayout
        String trxId = StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6);
        List<SmartPayout> list = deviceDao.getSmartPayoutInventory();
        for (SmartPayout sp : list) {
            if (sp.getCurrentRouting() == 1) {
                boolean route = smartPayoutController.setNoteRoute(trxId, String.valueOf(sp.getIdx()), "0");
                if (route) {
                    deviceDao.updateCurrentRouting(SmartPayoutController.DEVICE_CODE, sp.getDenom(), 0);
                }
            }
        }

        deviceDao.emptySmartPayout(SmartPayoutController.DEVICE_CODE);
        return smartPayoutController.emptySSP(trxId);
    }

    public boolean emptyCashbox() {
        //clear smartpayout
        String trxId = StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6);
        return smartPayoutController.clearSSP(trxId);
    }

    public boolean downloadFile(String fileName) {
        String host = parameterManager.getParameter("ftp.server.host").getValue();
        int port = Integer.parseInt(parameterManager.getParameter("ftp.server.port").getValue());
        String username = parameterManager.getParameter("ftp.server.username").getValue();
        String password = parameterManager.getParameter("ftp.server.password").getValue();

        boolean received = FtpUtil.downloadFileFromFTPServer(fileName, host, port, username, password);
        int i = 0;
        while (i < 3 && received == false) {
            i = i + 1;
            received = FtpUtil.downloadFileFromFTPServer(fileName, host, port, username, password);
            if (received == true) {
                return true;
            }
        }
        return false;
        //FtpUtil.deleteFileFromFTPServer(fileName, host, port, username, password);
    }

    @SuppressWarnings("CallToThreadDumpStack")
    public void updateTable(String fileName) {
        try {
            String fileContent = FileUtil.readTextFile(fileName);
            String[] rows = fileContent.split("\n");
            for (int i = 0; i < rows.length; i++) {
                deviceDao.executeSql(rows[i]);
            }
        } catch (Exception ex) {
            ex.printStackTrace();
        }
    }
}