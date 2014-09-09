package com.ics.ssk.ego.model;

import java.io.Serializable;
import java.util.List;

public class Kereta implements Serializable {

    private static final long serialVersionUID = 8811346382560478505L;
    private List<Jadwal> jadwals;
    private String kode;
    private String seat;
    private String name;
    private String dateStart;
    private String dateEnd;
    private String timeStart;
    private String timeEnd;

    public List<Jadwal> getJadwals() {
        return jadwals;
    }

    public void setJadwalKais(List<Jadwal> jadwals) {
        this.jadwals = jadwals;
    }

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

    public String getSeat() {
        return seat;
    }

    public void setSeat(String seat) {
        this.seat = seat;
    }
}
