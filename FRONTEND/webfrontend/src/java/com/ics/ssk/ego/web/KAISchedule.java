package com.ics.ssk.ego.web;

import com.ics.ssk.ego.iso8583.processor.ProcessorKAI;
import com.ics.ssk.ego.model.Jadwal;
import com.ics.ssk.ego.model.Kereta;
import com.ics.ssk.ego.util.DateTimeUtil;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import net.sourceforge.stripes.action.After;
import net.sourceforge.stripes.action.Before;
import net.sourceforge.stripes.action.DefaultHandler;
import net.sourceforge.stripes.action.DontValidate;
import net.sourceforge.stripes.action.ForwardResolution;
import net.sourceforge.stripes.action.RedirectResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.action.UrlBinding;

@UrlBinding("/egokaischedule.html")
public class KAISchedule extends BaseActionBean {

    private String VIEW = "/pages/ego/kai/schedule.jsp";
    private List<Kereta> jadwals;
    private String asal;
    private String code;
    private String asalName;
    private String tujuan;
    private String tujuanName;
    private String anak;
    private String dewasa;
    private String infant;
    private String tanggal;
    private Map<String, Object> maps;

    public void setJadwals(List<Kereta> jadwals) {
        this.jadwals = jadwals;
    }

    public List<Kereta> getJadwals() {
        return jadwals;
    }

    public void setTanggal(String tanggal) {
        this.tanggal = tanggal;
    }

    public String getTanggal() {
        return tanggal;
    }

    public void setAsal(String asal) {
        this.asal = asal;
    }

    public String getAsal() {
        return asal;
    }

    public void setTujuan(String tujuan) {
        this.tujuan = tujuan;
    }

    public String getTujuan() {
        return tujuan;
    }

    public void setAnak(String anak) {
        this.anak = anak;
    }

    public String getAnak() {
        return anak;
    }

    @SuppressWarnings("unchecked")
    public Map<String, Object> getMaps() {
        return (Map<String, Object>) sessionManager.getSession(getContext().getRequest(), "KAI_SCHEDULLE");
    }

    public String getDetik() {
        Date dateEnd = (Date) getMaps().get("DATE_END");
        long detik = DateTimeUtil.selisihWaktu(dateEnd, new Date());
        return String.valueOf(detik);
    }

    public void setDewasa(String dewasa) {
        this.dewasa = dewasa;
    }

    public String getDewasa() {
        return dewasa;
    }

    public void setInfant(String infant) {
        this.infant = infant;
    }

    public String getInfant() {
        return infant;
    }

    @SuppressWarnings("unchecked")
    @Before
    @DontValidate
    public void startup() {
        maps = (Map<String, Object>) sessionManager.getSession(getContext().getRequest(), "KAI_SCHEDULLE");
        if (maps != null) {
            if (getTanggal() == null) {
                setTanggal(DateTimeUtil.convertIndoDate((Date) maps.get(ProcessorKAI.DATE)));
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
            if (getJadwals() == null) {
                setJadwals(getLogicJadwal((String) maps.get(ProcessorKAI.BIT48_1)));
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
        if (maps == null) {
            return new RedirectResolution(KAIInquiry.class);
        } else {
            return new ForwardResolution(VIEW);
        }
    }

    @SuppressWarnings("unchecked")
    public Resolution booking() {
        Map<String, Jadwal> maps = (Map<String, Jadwal>) this.maps.get("JADWAL_KAI");
        Jadwal jadwalKai = maps.get(code);
        this.maps.put("PILIH_KAI", jadwalKai);
        return new RedirectResolution(KAIPassenger.class);
    }

    public Resolution home() {
        return new RedirectResolution(KAIInquiry.class);
    }

    public String getAsalName() {
        return asalName;
    }

    public void setAsalName(String asalName) {
        this.asalName = asalName;
    }

    public String getTujuanName() {
        return tujuanName;
    }

    public void setTujuanName(String tujuanName) {
        this.tujuanName = tujuanName;
    }

    private List<Kereta> getLogicJadwal(String bit48) {
        List<Kereta> keretas = new ArrayList<Kereta>();

        Map<String, Jadwal> maps = new TreeMap<String, Jadwal>();

        int num_schedulle = Integer.parseInt(bit48.substring(18, 20));
        int start = 20;
        int end = 20;
        for (int i = 0; i < num_schedulle; i++) {
            end = end + 8;
            String nomor = bit48.substring(start, end).trim();
            start = end;

            end = end + 20;
            String nama = bit48.substring(start, end).trim();
            start = end;

            end = end + 8;
            String tglTiba = bit48.substring(start, end).trim();
            start = end;

            end = end + 4;
            String berangkat = bit48.substring(start, end).trim();
            start = end;

            end = end + 4;
            String sampai = bit48.substring(start, end).trim();
            start = end;

            end = end + 1;
            int kelas_tersedia = Integer.parseInt(bit48.substring(start, end).trim());
            start = end;

            Kereta kereta = new Kereta();
            kereta.setDateStart(getTanggal());
            kereta.setDateEnd(DateTimeUtil.convertIndoDate(DateTimeUtil.convertStringToDateCustomized(tglTiba, DateTimeUtil.YYYYMMDD)));
            kereta.setKode(nomor);
            kereta.setName(nama);
            kereta.setTimeStart(berangkat.substring(0, 2) + ":" + berangkat.substring(2));
            kereta.setTimeEnd(sampai.substring(0, 2) + ":" + sampai.substring(2));

            List<Jadwal> jadwalKais = new ArrayList<Jadwal>();
            for (int j = 0; j < kelas_tersedia; j++) {


                end = end + 1;
                String subkelas = bit48.substring(start, end).trim();
                start = end;

                end = end + 3;
                int jumlah_bangku = Integer.parseInt(bit48.substring(start, end).trim());
                start = end;

                end = end + 1;
                //String kelas  = bit48.substring(start, end).trim();
                start = end;

                end = end + 6;
                double harga_dewasa = Integer.parseInt(bit48.substring(start, end).trim());
                start = end;

                end = end + 6;
                double harga_anak = Integer.parseInt(bit48.substring(start, end).trim());
                start = end;

                end = end + 6;
                double harga_bayi = Integer.parseInt(bit48.substring(start, end).trim());
                start = end;

                Jadwal kai = new Jadwal();
                kai.setSeat(jumlah_bangku);
                kai.setClazz(subkelas);
                kai.setId(nomor + "-" + subkelas);
                kai.setKode(nomor);
                kai.setName(nama);
                kai.setPriceAdult(harga_dewasa);
                kai.setPriceChild(harga_anak);
                kai.setPriceInfant(harga_bayi);
                kai.setDateStart(getTanggal());
                kai.setDateEnd(DateTimeUtil.convertIndoDate(DateTimeUtil.convertStringToDateCustomized(tglTiba, DateTimeUtil.YYYYMMDD)));
                kai.setTimeStart(berangkat.substring(0, 2) + ":" + berangkat.substring(2));
                kai.setTimeEnd(sampai.substring(0, 2) + ":" + sampai.substring(2));
                kai.setAvailable(0);
                if (jumlah_bangku >= (Integer.parseInt(getDewasa()) + Integer.parseInt(getAnak()))) {
                    kai.setAvailable(1);
                }
                maps.put(nomor + "-" + subkelas, kai);
                jadwalKais.add(kai);
            }
            if (jadwalKais.size() > 0) {
                kereta.setJadwalKais(jadwalKais);
                keretas.add(kereta);
            }

        }
        this.maps.put("JADWAL_KAI", maps);
        return keretas;
    }

    public String getCode() {
        return code;
    }

    public void setCode(String code) {
        this.code = code;
    }
    
    @DontValidate
    public Resolution kembali() {
        return new RedirectResolution(KAIInquiry.class);
    }
}
