package com.ics.ssk.ego.model;

public class CounterFactory implements java.io.Serializable {
        
    public static String PAYMENT = "P";            
    
    private static final long serialVersionUID = 5889641571192343216L;
    private String type;
    private int value;

    public CounterFactory() {
    }

    public CounterFactory(String type, int value) {
        this.type = type;
        this.value = value;
    }

    public String getType() {
        return this.type;
    }

    public void setType(String type) {
        this.type = type;
    }

    public int getValue() {
        return this.value;
    }

    public void setValue(int value) {
        this.value = value;
    }
}