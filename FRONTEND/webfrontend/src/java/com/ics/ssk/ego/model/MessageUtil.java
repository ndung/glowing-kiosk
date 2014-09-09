package com.ics.ssk.ego.model;

import com.ics.ssk.ego.util.DateTimeUtil;
import java.io.Serializable;
import java.util.Date;
import java.util.Map;

public class MessageUtil implements Serializable {

    private static final long serialVersionUID = 6496236114648486909L;
    private int step;
    private String status;
    private String prefix;    
    private String customerId;
    private String kioskId;
    private String kioskLocation;
    private String additional1;
    private String additional2;
    private String additional3;
    private int jumlahTagihan;
    private int jumlahTunggakan;
    private String stan;
    private String refNumber;
    private String billerId;
    private String productId;
    private String productCode;
    private String productName;
    private String billerName;    
    private double amount;
    private double fee;
    private double pinalty;
    private double price;
    private String voucherNumber1;
    private String voucherNumber2;
    private String voucherNumber3;
    private double price1;
    private double price2;
    private double price3;
    private String tahunBulanTag1;
    private String tahunBulanTag2;
    private String tahunBulanTag3;
    private String tahunBulanTag4;
    private Date date;
    private String bit48;
    private String bit63;   
    private Boolean cancelStatus;
    private String additional4;
    private String additional5;
    private String additional6;
    private double kwh;
    private double ppn;
    private double ppj;
    private double angsuran;
    private String pesan;    
    private String suffix;
    private int total;
    private Map<String, String> maps;

    public String getPrefix() {
        return prefix;
    }

    public void setPrefix(String prefix) {
        this.prefix = prefix;
    }

    public String getCustomerId() {
        return customerId;
    }

    public void setCustomerId(String customerId) {
        this.customerId = customerId;
    }   

    public String getKioskId() {
        return kioskId;
    }

    public void setKioskId(String kioskId) {
        this.kioskId = kioskId;
    }

    public String getKioskLocation() {
        return kioskLocation;
    }

    public void setKioskLocation(String kioskLocation) {
        this.kioskLocation = kioskLocation;
    }

    public String getBillerName() {
        return billerName;
    }

    public void setBillerName(String billerName) {
        this.billerName = billerName;
    }   
    
    public String getStan() {
        return stan;
    }

    public void setStan(String stan) {
        this.stan = stan;
    }

    public double getAmount() {
        return amount;
    }

    public void setAmount(double amount) {
        this.amount = amount;
    }

    public void setDate(Date date) {
        this.date = date;
    }

    public Date getDate() {
        return date;
    }

    public String getBit7() {
        return DateTimeUtil.convertDateToStringCustomized(date, DateTimeUtil.YYMMDDHHMMSS).substring(2);
    }

    public String getBit12() {
        return DateTimeUtil.convertDateToStringCustomized(date, DateTimeUtil.HHMMSS);
    }

    public String getBit13() {
        return DateTimeUtil.convertDateToStringCustomized(date, DateTimeUtil.YYMMDD).substring(2);
    }

    public String getProductId() {
        return productId;
    }

    public void setProductId(String productId) {
        this.productId = productId;
    }

    public String getBillerId() {
        return billerId;
    }

    public void setBillerId(String billerId) {
        this.billerId = billerId;
    }

    public String getBit48() {
        return bit48;
    }

    public void setBit48(String bit48) {
        this.bit48 = bit48;
    }

    public String getBit63() {
        return bit63;
    }

    public void setBit63(String bit63) {
        this.bit63 = bit63;
    }

    public void setPrice(double price) {
        this.price = price;
    }

    public double getPrice() {
        return price;
    }

    public String getVoucherNumber1() {
        if (voucherNumber1 == null) {
            voucherNumber1 = "";
        }
        return voucherNumber1;
    }

    public void setVoucherNumber1(String voucherNumber1) {
        this.voucherNumber1 = voucherNumber1;
    }

    public String getVoucherNumber2() {
        return voucherNumber2;
    }

    public void setVoucherNumber2(String voucherNumber2) {
        this.voucherNumber2 = voucherNumber2;
    }

    public String getVoucherNumber3() {
        return voucherNumber3;
    }

    public void setVoucherNumber3(String voucherNumber3) {
        this.voucherNumber3 = voucherNumber3;
    }

    public String getTahunBulanTag1() {
        return tahunBulanTag1;
    }

    public void setTahunBulanTag1(String tahunBulanTag1) {
        this.tahunBulanTag1 = tahunBulanTag1;
    }

    public String getTahunBulanTag2() {
        return tahunBulanTag2;
    }

    public void setTahunBulanTag2(String tahunBulanTag2) {
        this.tahunBulanTag2 = tahunBulanTag2;
    }

    public String getTahunBulanTag3() {
        return tahunBulanTag3;
    }

    public void setTahunBulanTag3(String tahunBulanTag3) {
        this.tahunBulanTag3 = tahunBulanTag3;
    }

    public String getRefNumber() {
        return refNumber;
    }

    public void setRefNumber(String refNumber) {
        this.refNumber = refNumber;
    }   

    public String getProductName() {
        return productName;
    }

    public void setProductName(String productName) {
        this.productName = productName;
    }

    public int getStep() {
        return step;
    }

    public void setStep(int step) {
        this.step = step;
    }

    public String getStatus() {
        return status;
    }

    public void setStatus(String status) {
        this.status = status;
    }

    public String getProductCode() {
        return productCode;
    }

    public void setProductCode(String productCode) {
        this.productCode = productCode;
    }
   
    public String getAdditional1() {
        return additional1;
    }

    public void setAdditional1(String additional1) {
        this.additional1 = additional1;
    }

    public String getAdditional2() {
        return additional2;
    }

    public void setAdditional2(String additional2) {
        this.additional2 = additional2;
    }

    public String getAdditional3() {
        return additional3;
    }

    public void setAdditional3(String additional3) {
        this.additional3 = additional3;
    }

    public double getFee() {
        return fee;
    }

    public void setFee(double fee) {
        this.fee = fee;
    }

    public int getJumlahTagihan() {
        return jumlahTagihan;
    }

    public void setJumlahTagihan(int jumlahTagihan) {
        this.jumlahTagihan = jumlahTagihan;
    }

    public int getJumlahTunggakan() {
        return jumlahTunggakan;
    }

    public void setJumlahTunggakan(int jumlahTunggakan) {
        this.jumlahTunggakan = jumlahTunggakan;
    }        

    public double getPrice2() {
        return price2;
    }

    public void setPrice2(double price2) {
        this.price2 = price2;
    }

    public double getPrice3() {
        return price3;
    }

    public void setPrice3(double price3) {
        this.price3 = price3;
    }

    public double getPrice1() {
        return price1;
    }

    public void setPrice1(double price1) {
        this.price1 = price1;
    }

    public double getPinalty() {
        return pinalty;
    }

    public void setPinalty(double pinalty) {
        this.pinalty = pinalty;
    }

    public String getTahunBulanTag4() {
        return tahunBulanTag4;
    }

    public void setTahunBulanTag4(String tahunBulanTag4) {
        this.tahunBulanTag4 = tahunBulanTag4;
    }
    
    private String paymentOption; 
    private Map<String, Integer> baMap;
    private Double currentCashNote;
    private Double changeAmount;
    private Double changeCashNote;
    private Double changeNotPaid;
    private Map<String, Integer> cdMap;
    
    public String getPaymentOption() {
        return paymentOption;
    }

    public void setPaymentOption(String paymentOption) {
        this.paymentOption = paymentOption;
    }

    public Map<String, Integer> getBaMap() {
        return baMap;
    }

    public void setBaMap(Map<String, Integer> baMap) {
        this.baMap = baMap;
    }        

    public Double getCurrentCashNote() {
        return currentCashNote;
    }

    public void setCurrentCashNote(Double currentCashNote) {
        this.currentCashNote = currentCashNote;
    }   

    public Double getChangeAmount() {
        return changeAmount;
    }

    public void setChangeAmount(Double changeAmount) {
        this.changeAmount = changeAmount;
    }

    public Double getChangeCashNote() {
        return changeCashNote;
    }

    public void setChangeCashNote(Double changeCashNote) {
        this.changeCashNote = changeCashNote;
    }

    public Double getChangeNotPaid() {
        return changeNotPaid;
    }

    public void setChangeNotPaid(Double changeNotPaid) {
        this.changeNotPaid = changeNotPaid;
    }

    public Map<String, Integer> getCdMap() {
        return cdMap;
    }

    public void setCdMap(Map<String, Integer> cdMap) {
        this.cdMap = cdMap;
    }
    
    private Integer loop;

    public Integer getLoop() {
        return loop;
    }

    public void setLoop(Integer loop) {
        this.loop = loop;
    }

    @Override
    public String toString() {
        return "MessageUtil{" + "step=" + step + ", status=" + status + ", prefix=" + prefix + ", customerId=" + customerId + ", kioskId=" + kioskId + ", kioskLocation=" + kioskLocation + ", additional1=" + additional1 + ", additional2=" + additional2 + ", additional3=" + additional3 + ", jumlahTagihan=" + jumlahTagihan + ", stan=" + stan + ", refNumber=" + refNumber + ", billerId=" + billerId + ", productId=" + productId + ", productCode=" + productCode + ", productName=" + productName + ", billerName=" + billerName + ", amount=" + amount + ", fee=" + fee + ", pinalty=" + pinalty + ", price=" + price + ", voucherNumber1=" + voucherNumber1 + ", voucherNumber2=" + voucherNumber2 + ", voucherNumber3=" + voucherNumber3 + ", price1=" + price1 + ", price2=" + price2 + ", price3=" + price3 + ", tahunBulanTag1=" + tahunBulanTag1 + ", tahunBulanTag2=" + tahunBulanTag2 + ", tahunBulanTag3=" + tahunBulanTag3 + ", tahunBulanTag4=" + tahunBulanTag4 + ", date=" + date + ", bit48=" + bit48 + ", bit63=" + bit63 + ", paymentOption=" + paymentOption + ", baMap=" + baMap + ", currentCashNote=" + currentCashNote + ", changeAmount=" + changeAmount + ", changeCashNote=" + changeCashNote + ", changeNotPaid=" + changeNotPaid + ", cdMap=" + cdMap + ", loop=" + loop + '}';
    }

    public Boolean getCancelStatus() {
        return cancelStatus;
    }

    public void setCancelStatus(Boolean cancelStatus) {
        this.cancelStatus = cancelStatus;
    }

    public String getAdditional4() {
        return additional4;
    }

    public void setAdditional4(String additional4) {
        this.additional4 = additional4;
    }

    public String getAdditional5() {
        return additional5;
    }

    public void setAdditional5(String additional5) {
        this.additional5 = additional5;
    }

    public String getAdditional6() {
        return additional6;
    }

    public void setAdditional6(String additional6) {
        this.additional6 = additional6;
    }

    public double getKwh() {
        return kwh;
    }

    public void setKwh(double kwh) {
        this.kwh = kwh;
    }

    public double getPpn() {
        return ppn;
    }

    public void setPpn(double ppn) {
        this.ppn = ppn;
    }

    public double getPpj() {
        return ppj;
    }

    public void setPpj(double ppj) {
        this.ppj = ppj;
    }

    public double getAngsuran() {
        return angsuran;
    }

    public void setAngsuran(double angsuran) {
        this.angsuran = angsuran;
    }

    public String getPesan() {
        return pesan;
    }

    public void setPesan(String pesan) {
        this.pesan = pesan;
    }

    public String getSuffix() {
        return suffix;
    }

    public void setSuffix(String suffix) {
        this.suffix = suffix;
    }           

    public Map<String, String> getMaps() {
        return maps;
    }

    public void setMaps(Map<String, String> maps) {
        this.maps = maps;
    }

    public int getTotal() {
        return total;
    }

    public void setTotal(int total) {
        this.total = total;
    }
    
    private Map<String, Object> mapsKAI;

    public Map<String, Object> getMapsKAI() {
        return mapsKAI;
    }

    public void setMapsKAI(Map<String, Object> mapsKAI) {
        this.mapsKAI = mapsKAI;
    }   
    
}
