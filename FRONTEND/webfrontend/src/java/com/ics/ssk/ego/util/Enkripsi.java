package com.ics.ssk.ego.util;

import net.sourceforge.stripes.util.CryptoUtil;

public class Enkripsi {

    public Enkripsi() {
    }

    public static void main(String[] args) {
        String x = decrypt("TGR");
        System.out.println(x);
    }

    public static String encrypt(String str) {
        return CryptoUtil.encrypt(str);
    }

    public static String decrypt(String str) {
        return CryptoUtil.decrypt(str);
    }
}