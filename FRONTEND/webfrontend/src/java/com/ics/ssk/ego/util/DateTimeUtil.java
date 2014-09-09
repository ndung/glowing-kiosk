package com.ics.ssk.ego.util;

import java.text.DateFormat;
import java.text.ParseException;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.Locale;

public class DateTimeUtil {

    public static String DDMMYYYY = "ddMMyyyy";
    public static String YYYYMMDDHHMMSS = "yyyyMMddHHmmss";
    public static String YYYYMMDDHHMMSSSSS = "yyyyMMddHHmmssSSS";
    public static String YYMMDDHHMMSS = "yyMMddHHmmss";
    public static String DD_MMM_YYYY = "dd MMM yyyy";
    public static String YYYYMMDD = "yyyyMMdd";
    public static String YYMMDD = "yyMMdd";
    public static String DDMMYY = "ddMMyy";
    public static String HHMMSS = "HHmmss";
    public static String REPORTDATE = "dd/MM/yyyy HH:mm:ss";
    public static String WEBDATE = "yyyy/MM/dd";
    public static String WEBDATEKAI = "dd/MM/yyyy";

    public static Date convertStringToDateCustomized(String date, String pattern) {
        DateFormat df = new SimpleDateFormat(pattern);
        Date result = null;

        if (date != null) {
            try {
                result = df.parse(date);
            } catch (ParseException e) {
                result = null;
                //e.printStackTrace();
            }
            return result;
        } else {
            return null;
        }
    }

    public static String convertDateToStringCustomized(Date date, String pattern) {
        try {
            if (date != null) {
                return new SimpleDateFormat(pattern).format(date);
            } else {
                return null;
            }
        } catch (Exception e) {
            return "";
        }
    }

    public static String convertDateToStringCustomized(Date date, String pattern, int amount) {
        try {
            date = getCustomDate(date, amount);
            return convertDateToStringCustomized(date, pattern);
        } catch (Exception e) {
            return "";
        }
    }

    public static Date maxDateWeb(String date) {
        return convertStringToDateCustomized(date + " 235959", WEBDATE + " HHmmss");
    }

    public static Date maxDateWeb(Date date) {
        String dateStr = convertDateToStringCustomized(date, WEBDATE);
        return convertStringToDateCustomized(dateStr + " 235959", WEBDATE + " HHmmss");
    }

    public static Date minDateWeb(String date) {
        return convertStringToDateCustomized(date + " 000000", WEBDATE + " HHmmss");
    }

    public static Date minDateWeb(Date date) {
        String dateStr = convertDateToStringCustomized(date, WEBDATE);
        return convertStringToDateCustomized(dateStr + " 000000", WEBDATE + " HHmmss");
    }

    public static String convertStringToStringFormaterCustomized(String date, String patternFrom, String patternTo) {
        try {
            Date date2 = convertStringToDateCustomized(date, patternFrom);
            return convertDateToStringCustomized(date2, patternTo);
        } catch (Exception e) {
            return "";
        }
    }

    public static String convertMonth(String value) {
        try {
            String tahun = "201" + value.substring(0, 1);
            String bulan = value.substring(1);

            if (bulan.equals("01")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("02")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("03")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("04")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("05")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("06")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("07")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("08")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("09")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("10")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("11")) {
                return "Januari " + tahun;
            }
            if (bulan.equals("12")) {
                return "Januari " + tahun;
            } else {
                return "";
            }
        } catch (Exception e) {
            return "";
        }
    }

    public static Date getCustomDate(Date date, int amount) {
        Calendar calendar = Calendar.getInstance();
        if (date != null) {
            calendar.setTime(date);
        }
        calendar.add(Calendar.DATE, amount);
        return calendar.getTime();
    }

    public static Date getPrevMonth(Date date) {
        Calendar calendar = Calendar.getInstance();
        if (date != null) {
            calendar.setTime(date);
        }
        calendar.add(Calendar.MONTH, -1);
        return calendar.getTime();
    }

    public static Date getCostumMinuteDate(Date date, int amount) {
        Calendar calendar = Calendar.getInstance();
        if (date != null) {
            calendar.setTime(date);
        }
        calendar.add(Calendar.MINUTE, amount);
        return calendar.getTime();
    }

    public static String hasilWaktu(Date waktubesar, Date waktukecil) {
        long second = selisihWaktu(waktubesar, waktukecil);
        long jam = second / 3600;
        long menit = (second % 3600) / 60;
        long detik = (second % 3600) % 60;
        return jam + " jam, " + menit + " menit, " + detik + " detik";
    }

    public static long selisihWaktu(Date waktubesar, Date waktukecil) {
        Calendar dablek = Calendar.getInstance();
        dablek.setTime(waktubesar);
        Long i1 = dablek.getTimeInMillis();
        Calendar dablek2 = Calendar.getInstance();
        dablek2.setTime(waktukecil);
        Long i2 = dablek2.getTimeInMillis();
        long result = (int) ((i1 - i2) / 1000);
        return result;
    }

    public static String convertIndoDate(Date date) {
        String dayOfWeek = new SimpleDateFormat("EEEE", Locale.ENGLISH).format(date);
        if (dayOfWeek.equals("Monday")) {
            dayOfWeek = "Senin";
        } else if (dayOfWeek.equals("Tuesday")) {
            dayOfWeek = "Selasa";
        } else if (dayOfWeek.equals("Wednesday")) {
            dayOfWeek = "Rabu";
        } else if (dayOfWeek.equals("Thursday")) {
            dayOfWeek = "Kamis";
        } else if (dayOfWeek.equals("Friday")) {
            dayOfWeek = "Jum'at";
        } else if (dayOfWeek.equals("Saturday")) {
            dayOfWeek = "Sabtu";
        } else if (dayOfWeek.equals("Sunday")) {
            dayOfWeek = "Minggu";
        }

        String dat = convertDateToStringCustomized(date, "ddMMyyyy");
        if (dat.substring(2, 4).equals("01")) {
            dat = dat.substring(0, 2) + " Januari " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("02")) {
            dat = dat.substring(0, 2) + " Februari " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("03")) {
            dat = dat.substring(0, 2) + " Maret " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("04")) {
            dat = dat.substring(0, 2) + " April " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("05")) {
            dat = dat.substring(0, 2) + " Mei " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("06")) {
            dat = dat.substring(0, 2) + " Juni " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("07")) {
            dat = dat.substring(0, 2) + " Juli " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("08")) {
            dat = dat.substring(0, 2) + " Agustus " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("09")) {
            dat = dat.substring(0, 2) + " September " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("10")) {
            dat = dat.substring(0, 2) + " Oktober " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("11")) {
            dat = dat.substring(0, 2) + " November " + dat.substring(4);
        } else if (dat.substring(2, 4).equals("12")) {
            dat = dat.substring(0, 2) + " Desember " + dat.substring(4);
        }

        return dayOfWeek + ", " + dat;
    }

    public static Date getCustomYears(Date date, int amount) {
        Calendar calendar = Calendar.getInstance();
        if (date != null) {
            calendar.setTime(date);
        }
        calendar.add(Calendar.YEAR, amount);
        return calendar.getTime();
    }
}
