package com.ics.ssk.ego.web;

import com.ics.ssk.ego.iso8583.processor.ProcessorKAI;
import com.ics.ssk.ego.model.Jadwal;
import com.ics.ssk.ego.util.DateTimeUtil;
import java.util.Date;
import java.util.Map;
import net.sourceforge.stripes.action.After;
import net.sourceforge.stripes.action.Before;
import net.sourceforge.stripes.action.DefaultHandler;
import net.sourceforge.stripes.action.DontValidate;
import net.sourceforge.stripes.action.ForwardResolution;
import net.sourceforge.stripes.action.RedirectResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.action.UrlBinding;

@UrlBinding("/egokaiconfirmation.html")
public class KAIConfirmation extends BaseActionBean {

    private String VIEW = "/pages/ego/kai/confirmation.jsp";
    private Jadwal jadwal;
    private Map<String, Object> maps;
    private String asal;
    private String asalName;
    private String tujuan;
    private String tujuanName;
    private String anak;
    private String dewasa;
    private String infant;

    public String getAnak() {
        return anak;
    }

    public void setAnak(String anak) {
        this.anak = anak;
    }

    public String getDewasa() {
        return dewasa;
    }

    public void setDewasa(String dewasa) {
        this.dewasa = dewasa;
    }

    public String getInfant() {
        return infant;
    }

    public void setInfant(String infant) {
        this.infant = infant;
    }

    public Map<String, Object> getMaps() {
        return maps;
    }

    public void setMaps(Map<String, Object> maps) {
        this.maps = maps;
    }

    public String getAsal() {
        return asal;
    }

    public void setAsal(String asal) {
        this.asal = asal;
    }

    public String getAsalName() {
        return asalName;
    }

    public void setAsalName(String asalName) {
        this.asalName = asalName;
    }

    public String getTujuan() {
        return tujuan;
    }

    public void setTujuan(String tujuan) {
        this.tujuan = tujuan;
    }

    public String getTujuanName() {
        return tujuanName;
    }

    public void setTujuanName(String tujuanName) {
        this.tujuanName = tujuanName;
    }

    public void setJadwal(Jadwal jadwal) {
        this.jadwal = jadwal;
    }

    public Jadwal getJadwal() {
        return jadwal;
    }

    @SuppressWarnings("unchecked")
    @Before
    @DontValidate
    public void startup() {
        maps = (Map<String, Object>) sessionManager.getSession(getContext().getRequest(), "KAI_SCHEDULLE");
        if (maps != null) {
            if (getJadwal() == null) {
                setJadwal((Jadwal) this.maps.get("PILIH_KAI"));
            }
            if (getAsal() == null) {
                setAsal((String) maps.get(ProcessorKAI.ORIGINAL));
            }
            if (getAsalName() == null) {
                setAsalName((String) maps.get(ProcessorKAI.ORIGINAL_NAME));
            }
            if (getTujuan() == null) {
                setTujuan((String) maps.get(ProcessorKAI.DESTINATION));
            }
            if (getTujuanName() == null) {
                setTujuanName((String) maps.get(ProcessorKAI.DESTINATION_NAME));
            }
            if (getDewasa() == null) {
                setDewasa((String) maps.get(ProcessorKAI.DEWASA));
            }
            if (getAnak() == null) {
                setAnak((String) maps.get(ProcessorKAI.ANAK));
            }
            if (getInfant() == null) {
                setInfant((String) maps.get(ProcessorKAI.BAYI));
            }
        }
    }

    @After
    @DontValidate
    public void endup() {
        sessionManager.setSession(getContext().getRequest(), "KAI_SCHEDULLE", maps);
    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {
        return new ForwardResolution(VIEW);
    }

    public Resolution booking() {
        return new RedirectResolution(KAIPassenger.class);
    }

    public Resolution home() {
        return new RedirectResolution(KAIInquiry.class);
    }

    @SuppressWarnings("unchecked")
    public Map<String, Object> getMapss() {
        return (Map<String, Object>) sessionManager.getSession(getContext().getRequest(), "KAI_SCHEDULLE");
    }

    public String getDetik() {
        Date dateEnd = (Date) getMapss().get("DATE_END");
        long detik = DateTimeUtil.selisihWaktu(dateEnd, new Date());
        return String.valueOf(detik);
    }
}
