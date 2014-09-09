package com.ics.ssk.ego.model;

public class Advertisement implements java.io.Serializable {

    public static String TYPE_HEADLINE = "HEADLINE";
    public static String TYPE_RECEIPT = "RECEIPT";
    
    public static String ID = "id";
    public static String TYPE = "type";
    public static String CONTENT = "content";
    public static String STATUS = "status";
    private static final long serialVersionUID = 1027949838064330543L;
    private Long id;
    private String type;
    private String content;
    private int status;

    public String getType() {
        return type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public String getContent() {
        return content;
    }

    public void setContent(String content) {
        this.content = content;
    }

    public void setId(Long id) {
        this.id = id;
    }

    public Long getId() {
        return id;
    }

    public void setStatus(int status) {
        this.status = status;
    }

    public int getStatus() {
        return status;
    }
}
