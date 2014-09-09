package com.ics.ssk.ego.model;

import java.util.Date;
import java.util.Map;
import org.codehaus.jackson.map.ObjectMapper;

public class LogEgo implements java.io.Serializable {

    public static String RESPONSE = "response";
    public static String PRODUCTID = "product";
    public static String PRICE = "price";
    public static String PAYEEID = "payeeId";
    public static String VOUCHER = "voucher";
    public static String STAN = "stan";
    public static String DATE = "date";
    private static final long serialVersionUID = 5190806229643146146L;
    private Integer id;
    private String kioskId;
    private String stan;
    private Date date;
    private String product;
    private String payeeId;
    private String response;
    private double amount;
    private double price;
    private String voucher;
    private int tagihan;
    private String bulantahun;
    private String iso;
    private String paymentOption;
    private String refNumber;
    private String contentStruk;

    public LogEgo() {
    }

    public Integer getId() {
        return this.id;
    }

    public void setId(Integer id) {
        this.id = id;
    }

    public String getKioskId() {
        return kioskId;
    }

    public void setKioskId(String kioskId) {
        this.kioskId = kioskId;
    }
    
    public String getStan() {
        return this.stan;
    }

    public void setStan(String stan) {
        this.stan = stan;
    }

    public Date getDate() {
        return this.date;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public String getProduct() {
        return this.product;
    }

    public void setProduct(String product) {
        this.product = product;
    }

    public String getPayeeId() {
        if (payeeId.startsWith("00")) {
            payeeId = payeeId.substring(1);
        }
        return this.payeeId;
    }

    public void setPayeeId(String payeeId) {
        this.payeeId = payeeId;
    }

    public String getResponse() {
        return this.response;
    }

    public void setResponse(String response) {
        this.response = response;
    }

    public double getAmount() {
        return this.amount;
    }

    public void setAmount(double amount) {
        this.amount = amount;
    }

    public double getPrice() {
        return this.price;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public String getVoucher() {
        return this.voucher;
    }

    public void setVoucher(String voucher) {
        this.voucher = voucher;
    }

    public int getTagihan() {
        return this.tagihan;
    }

    public void setTagihan(int tagihan) {
        this.tagihan = tagihan;
    }

    public String getBulantahun() {
        return this.bulantahun;
    }

    public void setBulantahun(String bulantahun) {
        this.bulantahun = bulantahun;
    }

    public String getIso() {
        return this.iso;
    }

    public void setIso(String iso) {
        this.iso = iso;
    }

    public String getPaymentOption() {
        return paymentOption;
    }

    public void setPaymentOption(String paymentOption) {
        this.paymentOption = paymentOption;
    }

    public String getRefNumber() {
        return refNumber;
    }

    public void setRefNumber(String refNumber) {
        this.refNumber = refNumber;
    }

    public String getContentStruk() {
        return contentStruk;
    }

    public void setContentStruk(String contentStruk) {
        this.contentStruk = contentStruk;
    }
    
    @SuppressWarnings("CallToThreadDumpStack")
    public void setMaps(Map<String, String> maps) {
        ObjectMapper mapper = new ObjectMapper();
        try {
            this.contentStruk = mapper.writeValueAsString(maps);
        } catch (Exception e) {
            e.printStackTrace();
        }
    }

    @SuppressWarnings({"unchecked", "CallToThreadDumpStack"})
    public Map<String, String> getMaps() {
        ObjectMapper mapper = new ObjectMapper();
        try {
            return mapper.readValue(this.contentStruk, Map.class);
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }
    }    
}
