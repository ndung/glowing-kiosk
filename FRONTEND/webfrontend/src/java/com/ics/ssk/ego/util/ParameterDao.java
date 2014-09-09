package com.ics.ssk.ego.util;

import java.io.Serializable;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

@SuppressWarnings("rawtypes")
public class ParameterDao implements Serializable {

    public static String ORDER_ASC = "0";
    public static String ORDER_DESC = "1";
    private static final long serialVersionUID = 4871816771696340651L;
    private Map<String, Object> equals;
    private Map<String, List> ors;
    private Map<String, Object> likes;
    private Map<String, Object> notEquals;
    private Map<String, Object> gratherThan;
    private Map<String, Object> lessThan;
    private Map<String, Object> gratherEqualsThan;
    private Map<String, Object> lessEqualsThan;
    private Map<String, List> in;
    private Map<String, List> notIn;
    private Map<String, Object[]> between;
    private List<String[]> orders;
    private int offside;
    private int maxRows;
    private Class clazz;
    private List<String> isNull;
    private List<String> isNotNull;

    public ParameterDao(Class clazz, int offside, int maxRows) {
        this.equals = new HashMap<String, Object>();
        this.ors = new HashMap<String, List>();
        this.likes = new HashMap<String, Object>();
        this.notEquals = new HashMap<String, Object>();
        this.in = new HashMap<String, List>();
        this.notIn = new HashMap<String, List>();
        this.between = new HashMap<String, Object[]>();
        this.gratherThan = new HashMap<String, Object>();
        this.gratherEqualsThan = new HashMap<String, Object>();
        this.lessThan = new HashMap<String, Object>();
        this.lessEqualsThan = new HashMap<String, Object>();
        this.orders = new ArrayList<String[]>();
        this.isNotNull = new ArrayList<String>();
        this.isNull = new ArrayList<String>();
        this.offside = offside;
        this.maxRows = maxRows;
        this.setClazz(clazz);
    }

    public ParameterDao(Class clazz) {
        this.equals = new HashMap<String, Object>();
        this.ors = new HashMap<String, List>();
        this.likes = new HashMap<String, Object>();
        this.notEquals = new HashMap<String, Object>();
        this.in = new HashMap<String, List>();
        this.notIn = new HashMap<String, List>();
        this.between = new HashMap<String, Object[]>();
        this.gratherThan = new HashMap<String, Object>();
        this.gratherEqualsThan = new HashMap<String, Object>();
        this.lessThan = new HashMap<String, Object>();
        this.lessEqualsThan = new HashMap<String, Object>();
        this.orders = new ArrayList<String[]>();
        this.isNotNull = new ArrayList<String>();
        this.isNull = new ArrayList<String>();
        this.offside = 0;
        this.maxRows = 0;
        this.setClazz(clazz);
    }

    public Map<String, Object> getEquals() {
        return equals;
    }

    public void setEquals(String fieldClass, Object value) {
        this.equals.put(fieldClass, value);
    }

    public Map<String, List> getOrs() {
        return ors;
    }

    public void setOrs(String fieldClass, List value) {
        this.ors.put(fieldClass, value);
    }

    public Map<String, Object> getLikes() {
        return likes;
    }

    public void setLikes(String fieldClass, Object value) {
        this.likes.put(fieldClass, value);
    }

    public Map<String, Object> getNotEquals() {
        return notEquals;
    }

    public void setNotEquals(String fieldClass, Object value) {
        this.notEquals.put(fieldClass, value);
    }

    public Map<String, List> getIn() {
        return in;
    }

    public void setIn(String fieldClass, List value) {
        this.in.put(fieldClass, value);
    }

    public void setIn(String fieldClass, Object[] values) {
        List<Object> objects = new ArrayList<Object>();
        for (Object object : values) {
            objects.add(object);
        }
        this.in.put(fieldClass, objects);
    }

    public Map<String, List> getNotIn() {
        return notIn;
    }

    public void setNotIn(String fieldClass, Object[] values) {
        List<Object> objects = new ArrayList<Object>();
        for (Object object : values) {
            objects.add(object);
        }
        this.notIn.put(fieldClass, objects);
    }

    public void setNotIn(String fieldClass, List value) {
        this.notIn.put(fieldClass, value);
    }

    public Map<String, Object[]> getBetween() {
        return between;
    }

    public void setBetween(String fieldClass, Object startValue, Object endValue) {
        this.between.put(fieldClass, new Object[]{startValue, endValue});
    }

    public int getOffside() {
        return offside;
    }

    public void setOffside(int offside) {
        this.offside = offside;
    }

    public int getMaxRows() {
        return maxRows;
    }

    public void setMaxRows(int maxRows) {
        this.maxRows = maxRows;
    }

    public void setGratherThan(String fieldClass, Object value) {
        this.gratherThan.put(fieldClass, value);
    }

    public Map<String, Object> getGratherThan() {
        return gratherThan;
    }

    public void setLessThan(String fieldClass, Object value) {
        this.lessThan.put(fieldClass, value);
    }

    public Map<String, Object> getLessThan() {
        return lessThan;
    }

    public void setGratherEqualsThan(String fieldClass, Object value) {
        this.gratherEqualsThan.put(fieldClass, value);
    }

    public Map<String, Object> getGratherEqualsThan() {
        return gratherEqualsThan;
    }

    public void setLessEqualsThan(String fieldClass, Object value) {
        this.lessEqualsThan.put(fieldClass, value);
    }

    public Map<String, Object> getLessEqualsThan() {
        return lessEqualsThan;
    }

    public void setOrders(String typeOrder, String fieldClass) {
        String[] strings = new String[]{typeOrder, fieldClass};
        this.orders.add(strings);
    }

    public List<String[]> getOrders() {
        return orders;
    }

    public Class getClazz() {
        return clazz;
    }

    public void setClazz(Class clazz) {
        this.clazz = clazz;
    }

    public List<String> getIsNull() {
        return isNull;
    }

    public void setIsNull(String isNull) {
        this.isNull.add(isNull);
    }

    public List<String> getIsNotNull() {
        return isNotNull;
    }

    public void setIsNotNull(String isNotNull) {
        this.isNotNull.add(isNotNull);
    }
}
