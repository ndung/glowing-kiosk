package com.ics.ssk.ego.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.InputStream;
import org.apache.commons.net.ftp.FTP;
import org.apache.commons.net.ftp.FTPClient;
import org.apache.log4j.Logger;

public class FtpUtil {

    private static Logger log = Logger.getLogger(FtpUtil.class);

    public FtpUtil() {

    }

    @SuppressWarnings("CallToThreadDumpStack")
    public static boolean sendFileToFTPServer(InputStream fis, String fileName, String host, int port, String username, String password) {

        FTPClient client = new FTPClient();

        boolean status = true;

        try {

            client.connect(host, port);

            log.info("FTP connect to " + host + ":" + port);            
            if (client.login(username, password)) {

                log.info("FTP login to user " + username + " success");
                client.setFileType(FTP.BINARY_FILE_TYPE);
                if (client.storeFile(fileName, fis)) {

                    log.info("FTP send file " + fileName + " success");

                    fis.close();

                } else {

                    log.info("FTP send file " + fileName + " failed");

                    status = false;

                }

                client.logout();

                log.info("FTP logout");

            } else {

                log.info("FTP login to user " + username + " failed");

                status = false;

            }

            return status;

        } catch (Exception e) {
            e.printStackTrace();
            log.error(e);

            return false;

        }

    }

    public static boolean downloadFileFromFTPServer(String fileName, String host, int port, String username, String password) {

        FTPClient client = new FTPClient();

        boolean status = true;        

        try {
            FileOutputStream fos = new FileOutputStream(fileName);

            client.connect(host, port);

            log.info("FTP connect to " + host + ":" + port);

            if (client.login(username, password)) {

                log.info("FTP login to user " + username + " success");

                if (client.retrieveFile("/" + fileName, fos)) {

                    log.info("FTP download file " + fileName + " success");

                    fos.close();

                } else {

                    log.info("FTP download file " + fileName + " failed");

                    status = false;

                }

                client.logout();

                log.info("FTP logout");

            } else {

                log.info("FTP login to user " + username + " failed");

                status = false;

            }

            return status;

        } catch (Exception e) {

            log.error(e);

            return false;

        }

    }

    public static boolean sendFile(File file, String ip, int port, String username, String password) {

        try {

            FileInputStream fis = new FileInputStream(file);

            return sendFileToFTPServer(fis, file.getName(), ip, port, username, password);

        } catch (Exception e) {

            log.error(e);

            return false;

        }

    }

    public static boolean deleteFileFromFTPServer(String nameFile, String host, int port, String username, String password) {

        FTPClient client = new FTPClient();

        boolean status = true;

        try {

            client.connect(host, port);

            log.info("FTP connect to " + host + ":" + port);

            if (client.login(username, password)) {

                log.info("FTP login to user " + username + " success");

                if (client.deleteFile(nameFile)) {

                    log.info("FTP send file " + nameFile + " success");

                } else {

                    log.info("FTP send file " + nameFile + " failed");

                    status = false;

                }

                client.logout();

                log.info("FTP logout");

            } else {

                log.info("FTP login to user " + username + " failed");

                status = false;

            }

            return status;

        } catch (Exception e) {

            log.error(e);

            return false;

        }

    }

    public static void main(String[] hattaKampret) {
        deleteFileFromFTPServer("drivez.log", "10.255.20.113", 21, "test", "test123");
    }
}
