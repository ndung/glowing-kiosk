package com.ics.ssk.ego.model;

public class EgoProduct implements java.io.Serializable {

    public static String TYPE = "type";
    public static String ID = "id";
    private static final long serialVersionUID = -3854620688261625968L;
    private String id;
    private String billerCode;
    private String productCode;
    private String industryCode;
    private int type;    
    private Double amount;
    private String billerDescription;
    private String productDescription;

    public EgoProduct() {
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getBillerCode() {
        return billerCode;
    }

    public void setBillerCode(String billerCode) {
        this.billerCode = billerCode;
    }

    public String getProductCode() {
        return productCode;
    }

    public void setProductCode(String productCode) {
        this.productCode = productCode;
    }

    public String getIndustryCode() {
        return industryCode;
    }

    public void setIndustryCode(String industryCode) {
        this.industryCode = industryCode;
    }

    public int getType() {
        return type;
    }

    public void setType(int type) {
        this.type = type;
    }

    public Double getAmount() {
        return amount;
    }

    public void setAmount(Double amount) {
        this.amount = amount;
    }

    public String getBillerDescription() {
        return billerDescription;
    }

    public void setBillerDescription(String billerDescription) {
        this.billerDescription = billerDescription;
    }

    public String getProductDescription() {
        return productDescription;
    }

    public void setProductDescription(String productDescription) {
        this.productDescription = productDescription;
    }
   
}
