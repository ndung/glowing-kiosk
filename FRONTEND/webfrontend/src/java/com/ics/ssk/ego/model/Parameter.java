package com.ics.ssk.ego.model;

public class Parameter implements java.io.Serializable {

    private static final long serialVersionUID = -1587222627604768350L;
    private String parameterName;
    private String value;

    public Parameter() {
    }

    public Parameter(String parameterName) {
        this.parameterName = parameterName;
    }

    public Parameter(String parameterName, String value) {
        this.parameterName = parameterName;
        this.value = value;
    }

    public String getParameterName() {
        return this.parameterName;
    }

    public void setParameterName(String parameterName) {
        this.parameterName = parameterName;
    }

    public String getValue() {
        return this.value;
    }

    public void setValue(String value) {
        this.value = value;
    }
}
