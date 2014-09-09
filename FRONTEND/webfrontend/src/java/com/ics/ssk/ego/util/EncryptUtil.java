package com.ics.ssk.ego.util;

import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;

public class EncryptUtil {

    public static String MD5(String string) {
        StringBuilder hexString = new StringBuilder();
        String md5 = "";
        try {
            MessageDigest mdAlgorithm = MessageDigest.getInstance("MD5");
            mdAlgorithm.update(string.getBytes());
            byte[] digest = mdAlgorithm.digest();
            for (int i = 0; i < digest.length; i++) {
                string = Integer.toHexString(0xFF & digest[i]);
                if (string.length() < 2) {
                    string = "0" + string;
                }
                hexString.append(string);
            }
            md5 = hexString.toString();
        } catch (NoSuchAlgorithmException e) {
            md5 = string;
        }
        return md5;
    }
}
