/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.iso8583.processor;

import com.ics.ssk.ego.model.MessageUtil;
import java.util.Map;
import org.jpos.iso.ISOException;
import org.jpos.iso.ISOMsg;

/**
 *
 * @author ndung
 */
public interface ProcessorKAI {

    public static String CODEBOOK = "CODEBOOK";
    public static String ORIGINAL = "ORIGINAL";
    public static String ID_DEWASA1 = "ID_DEWASA1";
    public static String ID_DEWASA2 = "ID_DEWASA2";
    public static String ID_DEWASA3 = "ID_DEWASA3";
    public static String ID_DEWASA4 = "ID_DEWASA4";
    public static String DATE_DEWASA1 = "DATE_DEWASA1";
    public static String DATE_DEWASA2 = "DATE_DEWASA2";
    public static String DATE_DEWASA3 = "DATE_DEWASA3";
    public static String DATE_DEWASA4 = "DATE_DEWASA4";
    public static String NAMA_DEWASA4 = "NAMA_DEWASA4";
    public static String NAMA_DEWASA3 = "NAMA_DEWASA3";
    public static String NAMA_DEWASA2 = "NAMA_DEWASA2";
    public static String NAMA_DEWASA1 = "NAMA_DEWASA1";
    public static String NAMA_BAYI4 = "NAMA_BAYI4";
    public static String NAMA_BAYI3 = "NAMA_BAYI3";
    public static String NAMA_BAYI2 = "NAMA_BAYI2";
    public static String NAMA_BAYI1 = "NAMA_BAYI1";
    public static String DATE_BAYI4 = "DATE_BAYI4";
    public static String DATE_BAYI3 = "DATE_BAYI3";
    public static String DATE_BAYI2 = "DATE_BAYI2";
    public static String DATE_BAYI1 = "DATE_BAYI1";
    public static String NAMA_ANAK3 = "NAMA_ANAK3";
    public static String NAMA_ANAK2 = "NAMA_ANAK2";
    public static String NAMA_ANAK1 = "NAMA_ANAK1";
    public static String DATE_ANAK3 = "DATE_ANAK3";
    public static String DATE_ANAK2 = "DATE_ANAK2";
    public static String DATE_ANAK1 = "DATE_ANAK1";
    public static String TELP_PEMESAN = "TELP_PEMESAN";
    public static String NAMA_PEMESAN = "NAMA_PEMESAN";
    public static String ALAMAT_PEMESAN = "ALAMAT_PEMESAN";
    public static String ORIGINAL_NAME = "ORIGINAL_NAME";
    public static String DESTINATION = "DESTINATION";
    public static String DESTINATION_NAME = "DESTINATION_NAME";
    public static String DATE = "DATE";
    public static String DATE_TRX = "DATE_TRX";
    public static String BANK_REF = "BANK_REF";
    public static String KIOSK_ID = "KIOSK_ID";
    public static String KIOSK_LOCATION = "KIOSK_LOCATION";
    public static String STAN = "STAN";
    public static String DEWASA = "DEWASA";
    public static String ANAK = "ANAK";
    public static String BAYI = "BAYI";
    public static String BIT48_1 = "BIT48_1";
    public static String BIT48_2 = "BIT48_2";
    public static String PENUMPANG = "PENUMPANG";

    public ISOMsg prepareRequest(Map<String, Object> map) throws ISOException;
}
