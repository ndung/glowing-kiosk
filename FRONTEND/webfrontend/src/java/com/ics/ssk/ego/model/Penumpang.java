package com.ics.ssk.ego.model;

import java.io.Serializable;

public class Penumpang implements Serializable {

    private static final long serialVersionUID = -8350443384970021412L;
    private String nama;
    private String identitas;
    private String tipe;
    private String seat;

    public String getNama() {
        return nama;
    }

    public void setNama(String nama) {
        this.nama = nama;
    }

    public String getIdentitas() {
        return identitas;
    }

    public void setIdentitas(String identitas) {
        this.identitas = identitas;
    }

    public String getTipe() {
        return tipe;
    }

    public void setTipe(String tipe) {
        this.tipe = tipe;
    }

    public String getSeat() {
        return seat;
    }

    public void setSeat(String seat) {
        this.seat = seat;
    }
}
