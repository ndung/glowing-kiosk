package com.ics.ssk.ego.util;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.FileReader;
import java.io.FileWriter;
import java.io.IOException;
import java.io.InputStream;
import java.io.OutputStream;
import java.io.Reader;
import java.io.Writer;
import java.util.Calendar;

public class FileUtil {

    private static final int BUFFER_SIZE = 1024;

    public static String readTextFile(String filename) throws IOException {
        return readTextFile(new FileReader(filename));
    }

    public static String readTextFile(File file) throws IOException {
        return readTextFile(new FileReader(file));
    }

    public static String readTextFile(Reader reader) throws IOException {
        try {
            StringBuilder sb = new StringBuilder();
            char[] text = new char[BUFFER_SIZE];
            int n;
            while ((n = reader.read(text, 0, BUFFER_SIZE)) > 0) {
                sb.append(text, 0, n);
            }
            return sb.toString();
        } finally {
            if (reader != null) {
                reader.close();
            }
        }
    }

    public static String readFile(Reader reader) throws IOException {
        try {
            StringBuilder sb = new StringBuilder();
            char[] text = new char[BUFFER_SIZE];
            int n;
            while ((n = reader.read(text, 0, BUFFER_SIZE)) > 0) {
                sb.append(text, 0, n);
            }
            return sb.toString();
        } finally {
            if (reader != null) {
                reader.close();
            }
        }
    }

    public static void writeTextFile(String filename, String contents)
            throws IOException {
        writeTextFile(filename, contents, false);
    }

    public static void writeTextFile(String filename, String contents,
            boolean append) throws IOException {
        writeTextFile(new FileWriter(filename, append), contents);
    }

    public static void writeTextFile(File file, String contents)
            throws IOException {
        writeTextFile(file, contents, false);
    }

    public static void writeTextFile(File file, String contents, boolean append)
            throws IOException {
        writeTextFile(new FileWriter(file, append), contents);
    }

    public static void writeTextFile(Writer writer, String contents)
            throws IOException {
        try {
            char[] text = contents.toCharArray();
            writer.write(text);
        } finally {
            if (writer != null) {
                writer.close();
            }
        }
    }

    public static void moveFile(String src, String dest) {
        moveFile(new File(src), new File(dest));
    }

    public static void moveFile(String src, File dest) {
        moveFile(new File(src), dest);
    }

    public static void moveFile(File src, String dest) {
        moveFile(src, new File(dest));
    }

    public static void moveFile(File src, File dest) {
        if (dest.isFile()) {
            dest.delete();
        }
        src.renameTo(dest);
    }

    public static void moveDir(String src, String dest, int field, int amount) throws IOException {
        File source = new File(src);
        File destination = new File(dest);        
        copyDirectory(source, destination, field, amount);
        delete(source, field, amount);        
    }

    public static void copyDirectory(File sourceDir, File destDir, int field, int amount)
            throws IOException {

        if (!destDir.exists()) {
            destDir.mkdir();
        }

        File[] children = sourceDir.listFiles();

        for (File sourceChild : children) {

            String name = sourceChild.getName();
            File destChild = new File(destDir, name);

            if (sourceChild.isDirectory()) {
                copyDirectory(sourceChild, destChild, field, amount);
            } else {
                if (field != 0 && amount != 0 ){
                    Calendar cal = Calendar.getInstance();
                    cal.roll(field, amount);                
                    if (sourceChild.lastModified()<cal.getTimeInMillis()){
                        copyFile(sourceChild, destChild);
                    }
                }
                else{
                    copyFile(sourceChild, destChild);
                }
            }
        }
    }

    public static void copyFile(File source, File dest) throws IOException {
        if (!dest.exists()) {
            dest.createNewFile();
        }
        InputStream in = null;
        OutputStream out = null;
        try {
            in = new FileInputStream(source);
            out = new FileOutputStream(dest);
            byte[] buf = new byte[1024];
            int len;

            while ((len = in.read(buf)) > 0) {
                out.write(buf, 0, len);
            }
        } finally {
            in.close();
            out.close();
        }
    }

    public static void delete(File resource, int field, int amount) throws IOException {
        if (resource.isDirectory()) {
            File[] childFiles = resource.listFiles();
            for (File child : childFiles) {                
                delete(child, field, amount);
            }
        }
        if (!resource.isDirectory()) {
            if (field != 0 && amount != 0 ){
                Calendar cal = Calendar.getInstance();
                cal.roll(field, amount);                
                if (resource.lastModified()<cal.getTimeInMillis()){
                    resource.delete();
                }
            }
            else{
                resource.delete();
            }
        }
    }

    public static void main(String[] args) throws IOException {
       String a = readTextFile("C:/Cash_Dispenser.jpg");
        System.out.println(a);
    }
}
