/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.scheduler;

import com.ics.ssk.ego.dao.DeviceDao;
import com.ics.ssk.ego.manager.ParameterManager;
import com.ics.ssk.ego.util.FileUtil;
import com.ics.ssk.ego.util.FtpUtil;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.List;
import java.util.zip.ZipEntry;
import java.util.zip.ZipOutputStream;
import org.apache.log4j.Logger;

/**
 *
 * @author ICS Team
 */
public class LogBackupJob implements Executable {

    Logger log = Logger.getLogger(this.getClass());
    private SimpleDateFormat sdf = new SimpleDateFormat("yyyy-MM-dd");
    private ParameterManager parameterManager;
    private DeviceDao deviceDao;

    public void setParameterManager(ParameterManager parameterManager) {
        this.parameterManager = parameterManager;
    }

    public void setDeviceDao(DeviceDao deviceDao) {
        this.deviceDao = deviceDao;
    }

    @SuppressWarnings("CallToThreadDumpStack")
    @Override
    public void execute() throws Exception {
        Calendar cal = Calendar.getInstance();
        cal.setTime(new Date());
        cal.add(Calendar.DATE, -1);

        //send device log file
        String deviceLogFileName = parameterManager.getParameter("device.log.file.name").getValue();
        FileUtil.copyFile(new File(deviceLogFileName), new File(deviceLogFileName + "." + sdf.format(cal.getTime())));
        sendLogFile(deviceLogFileName + "." + sdf.format(cal.getTime()));
        FileUtil.writeTextFile(deviceLogFileName, "");

        //send core log file
        String serverLogFileName = parameterManager.getParameter("server.log.file.name").getValue();
        sendLogFile(serverLogFileName + sdf.format(cal.getTime()));
                
        sendLogTable("ssp_history");                        
        sendLogTable("log_ego");               
    }

    @SuppressWarnings("CallToThreadDumpStack")
    public File getDailyBackupTransactionFilePath(String file, Date date) {
        String dirPath = parameterManager.getParameter("backup.daily.path").getValue() + "/";
        File filePath = new File(dirPath);
        if (!filePath.exists()) {
            filePath.mkdirs();
        }

        String fileName = dirPath + file +"."+ sdf.format(date) + ".zip";

        filePath = new File(fileName);

        return filePath;
    }

    public void sendLogFile(String fileName) throws FileNotFoundException, IOException {
        String host = parameterManager.getParameter("ftp.server.host").getValue();
        int port = Integer.parseInt(parameterManager.getParameter("ftp.server.port").getValue());
        String username = parameterManager.getParameter("ftp.server.username").getValue();
        String password = parameterManager.getParameter("ftp.server.password").getValue();

        File file = new File(fileName + ".zip");
        ZipOutputStream os = new ZipOutputStream(new FileOutputStream(file));
        os.putNextEntry(new ZipEntry(file.getName().replace(".zip", "")));
        FileInputStream in = new FileInputStream(file.getAbsolutePath().replace(".zip", ""));
        byte[] buffer = new byte[1024];
        int len;
        while ((len = in.read(buffer)) > 0) {
            os.write(buffer, 0, len);
        }
        in.close();        
        os.closeEntry();
        os.close();

        log.info("send log file : " + file.getAbsolutePath());

        boolean sent = FtpUtil.sendFile(file, host, port, username, password);

        while (sent == false) {
            sent = FtpUtil.sendFile(file, host, port, username, password);
        }
    }

    @SuppressWarnings("CallToThreadDumpStack")
    public void sendLogTable(String tableName) throws FileNotFoundException, IOException {
        Calendar cal = Calendar.getInstance();
        cal.setTime(new Date());
        cal.add(Calendar.DATE, -1);
        
        String host = parameterManager.getParameter("ftp.server.host").getValue();
        int port = Integer.parseInt(parameterManager.getParameter("ftp.server.port").getValue());
        String username = parameterManager.getParameter("ftp.server.username").getValue();
        String password = parameterManager.getParameter("ftp.server.password").getValue();
        String kioskId = parameterManager.getParameter("kiosk.id").getValue();
        String query = "select * from " + tableName;
        List<String> list = deviceDao.getBackupTableData(tableName, query, kioskId);

        File file = getDailyBackupTransactionFilePath(tableName, cal.getTime());
        ZipOutputStream os = new ZipOutputStream(new FileOutputStream(file));
        os.putNextEntry(new ZipEntry(file.getName().replace(".zip", ".sql")));

        try {
            for (int j = 0; j < list.size(); j++) {
                os.write(list.get(j).getBytes());
                os.write("\n".getBytes());
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        os.closeEntry();
        os.close();

        boolean sent = FtpUtil.sendFile(file, host, port, username, password);
        while (sent == false) {
            sent = FtpUtil.sendFile(file, host, port, username, password);
        }
        if (sent){
            deviceDao.executeSql("truncate table "+tableName);            
        }
    }

}
