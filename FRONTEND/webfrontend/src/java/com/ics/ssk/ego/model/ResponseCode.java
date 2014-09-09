package com.ics.ssk.ego.model;

public class ResponseCode implements java.io.Serializable {

    public static String RESPONSE_CODE = "responseCode";
    public static String CATEGORY = "category";
    public static String RETRY_STATUS = "retryStatus";
    public static String DESCRIPTION = "description";
    public static String STATUS = "status";
    private static final long serialVersionUID = 2137641320881427140L;
    private String responseCode;
    private String description;
    private String category;
    private Integer status;
    private Integer retryStatus;

    public ResponseCode() {
    }

    public ResponseCode(String responseCode) {
        this.responseCode = responseCode;
    }

    public ResponseCode(String responseCode, String description) {
        this.responseCode = responseCode;
        this.description = description;
    }

    public String getResponseCode() {
        return this.responseCode;
    }

    public void setResponseCode(String responseCode) {
        this.responseCode = responseCode;
    }

    public String getDescription() {
        return this.description;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public Integer getStatus() {
        return this.status;
    }

    public void setStatus(Integer status) {
        this.status = status;
    }

    public Integer getRetryStatus() {
        return this.retryStatus;
    }

    public void setRetryStatus(Integer retryStatus) {
        this.retryStatus = retryStatus;
    }

    public void setCategory(String category) {
        this.category = category;
    }

    public String getCategory() {
        return category;
    }

    @Override
    public String toString() {
        String str = "";
        str += "RESPONSE CODE : " + responseCode + ", ";
        str += "DESCRIPTION : " + description;
        return str;
    }
}
