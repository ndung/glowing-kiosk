package com.ics.ssk.ego.model;

import java.io.Serializable;

public class Jadwal implements Serializable {

    private static final long serialVersionUID = 553492410278756349L;
    private String id;
    private String kode;
    private String name;
    private String dateStart;
    private String dateEnd;
    private String timeStart;
    private String timeEnd;
    private String clazz;
    private int available;
    private int seat;
    private double priceAdult;
    private double priceChild;
    private double priceInfant;

    public String getKode() {
        return kode;
    }

    public void setKode(String kode) {
        this.kode = kode;
    }

    public String getName() {
        return name;
    }

    public void setName(String name) {
        this.name = name;
    }

    public String getTimeStart() {
        return timeStart;
    }

    public void setTimeStart(String timeStart) {
        this.timeStart = timeStart;
    }

    public String getTimeEnd() {
        return timeEnd;
    }

    public void setTimeEnd(String timeEnd) {
        this.timeEnd = timeEnd;
    }

    public String getClazz() {
        if (clazz.equalsIgnoreCase("A") || clazz.equalsIgnoreCase("H") || clazz.equalsIgnoreCase("I") || clazz.equalsIgnoreCase("J")) {
            return "Eks (" + clazz + ")";
        } else if (clazz.equalsIgnoreCase("C") || clazz.equalsIgnoreCase("P") || clazz.equalsIgnoreCase("Q") || clazz.equalsIgnoreCase("S")) {
            return "Eko (" + clazz + ")";
        } else if (clazz.equalsIgnoreCase("B") || clazz.equalsIgnoreCase("K") || clazz.equalsIgnoreCase("N") || clazz.equalsIgnoreCase("O")) {
            return "Bis (" + clazz + ")";
        } else if (clazz.equalsIgnoreCase("X")) {
            return "Eks Promo (" + clazz + ")";
        } else if (clazz.equalsIgnoreCase("Y")) {
            return "Bis Promo (" + clazz + ")";
        } else if (clazz.equalsIgnoreCase("Z")) {
            return "Eko Promo (" + clazz + ")";
        } else {
            return "Eko (" + clazz + ")";
        }
    }

    public String getClaz() {
        return clazz;
    }

    public String getClazzName() {
        if (clazz.equalsIgnoreCase("A") || clazz.equalsIgnoreCase("H") || clazz.equalsIgnoreCase("I") || clazz.equalsIgnoreCase("J")) {
            return "Eksekutif";
        } else if (clazz.equalsIgnoreCase("C") || clazz.equalsIgnoreCase("P") || clazz.equalsIgnoreCase("Q") || clazz.equalsIgnoreCase("S")) {
            return "Ekonomi";
        } else if (clazz.equalsIgnoreCase("B") || clazz.equalsIgnoreCase("K") || clazz.equalsIgnoreCase("N") || clazz.equalsIgnoreCase("O")) {
            return "Bisnis";
        } else if (clazz.equalsIgnoreCase("X")) {
            return "Eksekutif Promo";
        } else if (clazz.equalsIgnoreCase("Y")) {
            return "Bisnis Promo";
        } else if (clazz.equalsIgnoreCase("Z")) {
            return "Ekonomi Promo";
        } else {
            return "Ekonomi";
        }
    }

    public void setClazz(String clazz) {
        this.clazz = clazz;
    }

    public int getAvailable() {
        return available;
    }

    public void setAvailable(int available) {
        this.available = available;
    }

    public double getPriceAdult() {
        return priceAdult;
    }

    public void setPriceAdult(double priceAdult) {
        this.priceAdult = priceAdult;
    }

    public double getPriceChild() {
        return priceChild;
    }

    public void setPriceChild(double priceChild) {
        this.priceChild = priceChild;
    }

    public double getPriceInfant() {
        return priceInfant;
    }

    public void setPriceInfant(double priceInfant) {
        this.priceInfant = priceInfant;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getDateStart() {
        return dateStart;
    }

    public void setDateStart(String dateStart) {
        this.dateStart = dateStart;
    }

    public String getDateEnd() {
        return dateEnd;
    }

    public void setDateEnd(String dateEnd) {
        this.dateEnd = dateEnd;
    }

    public int getSeat() {
        return seat;
    }

    public void setSeat(int seat) {
        this.seat = seat;
    }
}
