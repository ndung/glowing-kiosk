package com.ics.ssk.ego.util;

import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.text.NumberFormat;

public class StringUtil {

    public static String ALIGN_RIGHT = "right";
    public static String ALIGN_LEFT = "left";

    public static String padding(String s, int length, String alignment) {
        if (alignment.equalsIgnoreCase("right")) {
            return String.format("%1$#" + length + "s", s);
        } else {
            return String.format("%1$-" + length + "s", s);
        }
    }

    public static String addLeadingZeroes(int i, int length) {
        return String.format("%0" + length + "d", i);
    }

    public static String addLeadingZeroes(double i, int length) {
        return addLeadingZeroes(doubleToString(i), length);
    }

    public static String addLeadingZeroes(String i, int length) {
        String nol = "";
        for (int j = 0; j < length - i.length(); j++) {
            nol = nol + "0";
        }
        return nol + i;
    }

    public static String numberFormat(double i) {
        NumberFormat formatter = new DecimalFormat("###,###,###");
        return formatter.format(i);
    }

    public static String numberFormat(Double i, int len) {
        NumberFormat formatter = new DecimalFormat("###,###,###");
        return rightSpacePad(formatter.format(i), len);
    }

    public static String doubleToString(double i) {
        NumberFormat formatter = new DecimalFormat("#########");
        return formatter.format(i);
    }

    public static String numberFormat(String i) {
        NumberFormat formatter = new DecimalFormat("###,###,###");
        return formatter.format(i);
    }

    public static String numberFormat(Object i) {
        NumberFormat formatter = new DecimalFormat("###,###,###");
        return formatter.format(i);
    }

    public static String rightSpacePad(String argStr, int argLength) {
        String result = argStr;
        if (argLength != -1) {
            while (result.length() < argLength) {
                result = result.concat(" ");
            }
        }
        return result;
    }

    public static String leftZeroPad(String argNum, int argLength) {
        String result = argNum;
        while (result.length() < argLength) {
            result = "0".concat(result);
        }
        return result;
    }

    public static String removeLeftZeroPad(String argNum) {
        String result = argNum;
        int j = 0;
        for (int i = 0; i < argNum.length(); i++) {
            if (argNum.charAt(i) == '0') {
                j = j + 1;
            } else {
                result = argNum.substring(j);
                break;
            }
        }
        return result;
    }
    public static final int RIGHT_JUSTIFIED = 0;
    public static final int LEFT_JUSTIFIED = 1;

    public static String pad(String s, char c, int length, int justified) {
        StringBuilder buf = new StringBuilder(s);

        if (RIGHT_JUSTIFIED == justified) {
            for (int i = 0; i < length - s.length(); i++) {
                buf.insert(0, c);
            }
        } else if (LEFT_JUSTIFIED == justified) {
            if (s.length() <= length) {
                for (int i = 0; i < length - s.length(); i++) {
                    buf.append(c);
                }
            } else {
                buf.append(s.substring(0, length));
            }
        }
        return buf.toString();
    }

    public static String forPln(String i) {
        String nol = "";
        for (int j = 0; j < i.length(); j++) {
            nol += i.substring(j, j + 1);
            if (((j + 1) % 4 == 0 && (j + 1) < i.length())) {
                nol += " ";
            }
        }
        return nol;
    }

    public static String numberFormat2Decimal(double i, int decimal) {
        String x = "";
        if (decimal > 0) {
            for (int j = 0; j < decimal; j++) {
                x += "0";
            }
            x = "." + x;
        }

        DecimalFormat formatter = new DecimalFormat("###,###,##0" + x);
        DecimalFormatSymbols dfs = new DecimalFormatSymbols();
        dfs.setGroupingSeparator('.');
        dfs.setDecimalSeparator(',');
        formatter.setDecimalFormatSymbols(dfs);
        String result = formatter.format(i);
//        while ((result.endsWith("0")&& result.contains(",")) ||result.endsWith(",")){
//            result = result.substring(0,result.length()-1);
//        }
        return result;
    }

    public static String addHiddenValue(String i, int length) {
        String nol = "";
        for (int j = 0; j < length - i.length(); j++) {
            nol = nol + "X";
        }
        return i + nol;
    }
}
