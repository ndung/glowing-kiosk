package com.ics.ssk.ego.util;

import java.io.Serializable;

public class SelectModel implements Serializable {

    private static final long serialVersionUID = 7761410033559386013L;
    private Object value;
    private String description;

    public SelectModel() {
        // TODO Auto-generated constructor stub
    }

    public SelectModel(Object value, String description) {
        this.value = value;
        this.description = description;
    }

    public void setValue(Object value) {
        this.value = value;
    }

    public Object getValue() {
        return value;
    }

    public void setDescription(String description) {
        this.description = description;
    }

    public String getDescription() {
        return description;
    }
}
