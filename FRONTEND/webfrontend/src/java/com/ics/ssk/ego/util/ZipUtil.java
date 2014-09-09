/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Enumeration;
import java.util.zip.ZipEntry;
import java.util.zip.ZipFile;
import java.util.zip.ZipOutputStream;
import org.apache.log4j.Logger;

/**
 *
 * @author ndung
 */
public class ZipUtil {

    private static final Logger log = Logger.getLogger(ZipUtil.class);

    public static void zip(String inputFile, String outputFile) throws IOException {
        File file = new File(inputFile);
        if (file.exists()) {
            int level = 9;

            FileOutputStream fout = new FileOutputStream(outputFile);
            ZipOutputStream zout = new ZipOutputStream(fout);
            zout.setLevel(level);
            ZipEntry ze = new ZipEntry(inputFile);
            FileInputStream fin = new FileInputStream(inputFile);
            try {
                zout.putNextEntry(ze);
                for (int c = fin.read(); c != -1; c = fin.read()) {
                    zout.write(c);
                }
            } finally {
                fin.close();
            }
            zout.close();
        } else {
            log.error("File " + inputFile + " is not exists. zip file is aborted...");
        }
    }

    public static void unzip(String file) throws IOException {
        ZipFile zf = new ZipFile(file);

        File f = new File(file);
        if (f.exists()) {
            Enumeration e = zf.entries();
            while (e.hasMoreElements()) {
                ZipEntry ze = (ZipEntry) e.nextElement();
                FileOutputStream fout = new FileOutputStream(ze.getName());
                InputStream in = zf.getInputStream(ze);
                for (int c = in.read(); c != -1; c = in.read()) {
                    fout.write(c);
                }
                in.close();
                fout.close();
            }
        } else {
            log.error("File " + file + " is not exists. unzip file is aborted...");
        }
    }
}
