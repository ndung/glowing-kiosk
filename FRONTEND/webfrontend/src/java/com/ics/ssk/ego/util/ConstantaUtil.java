package com.ics.ssk.ego.util;

import java.util.ArrayList;
import java.util.List;

public class ConstantaUtil {

    public static String ALL_PARAM = "ALL_PARAM";
    public static String WEB_INF_LOCATION;
    public static String WEB_CONTENT_LOCATION;
    public static String LINK_SESSION = "LINK_SESSION";
    public static String USER_SESSION = "USER_SESSION";
    public static String IKLAN_SESSION = "IKLAN SESSION";
    public static Integer TIME_SESSION = 50000;

    public List<SelectModel> getYesOrNo() {
        List<SelectModel> list = new ArrayList<SelectModel>();
        list.add(new SelectModel("Y", "Yes"));
        list.add(new SelectModel("N", "No"));
        return list;
    }

    public List<SelectModel> getActiveOrDeactive() {
        List<SelectModel> list = new ArrayList<SelectModel>();
        list.add(new SelectModel(1, "Active"));
        list.add(new SelectModel(0, "De-Active"));
        return list;
    }

    public List<SelectModel> getLevels() {
        List<SelectModel> list = new ArrayList<SelectModel>();
        list.add(new SelectModel("MD", "Master Dealer"));
        list.add(new SelectModel("SD", "Sub Dealer"));
        list.add(new SelectModel("RS", "Reseller"));
        return list;
    }

    public List<SelectModel> getPageSizes() {
        List<SelectModel> list = new ArrayList<SelectModel>();
        list.add(new SelectModel(5, "5"));
        list.add(new SelectModel(10, "10"));
        list.add(new SelectModel(15, "15"));
        list.add(new SelectModel(20, "20"));
        list.add(new SelectModel(25, "25"));
        list.add(new SelectModel(30, "30"));
        list.add(new SelectModel(35, "35"));
        list.add(new SelectModel(40, "40"));
        list.add(new SelectModel(45, "45"));
        list.add(new SelectModel(50, "50"));
        return list;
    }

    public List<SelectModel> getPaymentOptions() {
        List<SelectModel> list = new ArrayList<SelectModel>();
        list.add(new SelectModel("IPAYMU", "IPAYMU"));
        list.add(new SelectModel("PAYPAL", "PAYPAL"));
        list.add(new SelectModel("TRANSFER", "TRANSFER"));
        return list;
    }

    public List<SelectModel> getEgoPaymentOptions() {
        List<SelectModel> list = new ArrayList<SelectModel>();
        list.add(new SelectModel("IPAYMU", "IPAYMU"));
        list.add(new SelectModel("PAYPAL", "PAYPAL"));
        return list;
    }
}
