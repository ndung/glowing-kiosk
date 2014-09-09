package com.ics.ssk.ego.web;

import com.ics.ssk.ego.iso8583.PackagerFactory;
import com.ics.ssk.ego.iso8583.SocketClientProvider;
import com.ics.ssk.ego.iso8583.processor.ProcessorKAI;
import com.ics.ssk.ego.iso8583.processor.ticket.InquiryKAIBookingProcessor;
import com.ics.ssk.ego.manager.CounterFactoryManager;
import com.ics.ssk.ego.manager.ParameterManager;
import com.ics.ssk.ego.manager.ResponseCodeManager;
import com.ics.ssk.ego.model.Jadwal;
import com.ics.ssk.ego.model.Penumpang;
import com.ics.ssk.ego.model.ResponseCode;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.StringUtil;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;
import java.util.Map;
import net.sourceforge.stripes.action.After;
import net.sourceforge.stripes.action.Before;
import net.sourceforge.stripes.action.DefaultHandler;
import net.sourceforge.stripes.action.DontValidate;
import net.sourceforge.stripes.action.ForwardResolution;
import net.sourceforge.stripes.action.LocalizableMessage;
import net.sourceforge.stripes.action.RedirectResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.action.UrlBinding;
import net.sourceforge.stripes.integration.spring.SpringBean;
import net.sourceforge.stripes.validation.LocalizableError;
import net.sourceforge.stripes.validation.SimpleError;
import net.sourceforge.stripes.validation.Validate;
import net.sourceforge.stripes.validation.ValidationErrors;
import net.sourceforge.stripes.validation.ValidationMethod;
import org.jpos.iso.ISOMsg;

@UrlBinding("/egokaipassenger.html")
public class KAIPassenger extends BaseActionBean {

    private String VIEW = "/pages/ego/kai/passenger.jsp";
    private String dewasaNama1;
    private String dewasaNama2;
    private String dewasaNama3;
    private String dewasaNama4;
    private String dewasaKtp1;
    private String dewasaKtp2;
    private String dewasaKtp3;
    private String dewasaKtp4;
    private String dewasaDate1;
    private String dewasaDate2;
    private String dewasaDate3;
    private String dewasaDate4;
    private String anakNama1;
    private String anakNama2;
    private String anakNama3;
    private String anakDate1;
    private String anakDate2;
    private String anakDate3;
    private String bayiNama1;
    private String bayiNama2;
    private String bayiNama3;
    private String bayiNama4;
    private String bayiDate1;
    private String bayiDate2;
    private String bayiDate3;
    private String bayiDate4;
    private String contactPhone;
    
    /**
    @Validate(required = true)
    private String recaptcha_challenge_field;
    @Validate(required = true)
    private String recaptcha_response_field;

    public String getRecaptcha_challenge_field() {
        return recaptcha_challenge_field;
    }

    public void setRecaptcha_challenge_field(String recaptcha_challenge_field) {
        this.recaptcha_challenge_field = recaptcha_challenge_field;
    }

    public String getRecaptcha_response_field() {
        return recaptcha_response_field;
    }

    public void setRecaptcha_response_field(String recaptcha_response_field) {
        this.recaptcha_response_field = recaptcha_response_field;
    }*/

    public String getDetik() {
        Date dateEnd = (Date) getMaps().get("DATE_END");
        long detik = DateTimeUtil.selisihWaktu(dateEnd, new Date());
        return String.valueOf(detik);
    }

    public String getDewasaDate1() {
        return dewasaDate1;
    }

    public void setDewasaDate1(String dewasaDate1) {
        this.dewasaDate1 = dewasaDate1;
    }

    public String getDewasaDate2() {
        return dewasaDate2;
    }

    public void setDewasaDate2(String dewasaDate2) {
        this.dewasaDate2 = dewasaDate2;
    }

    public String getDewasaDate3() {
        return dewasaDate3;
    }

    public void setDewasaDate3(String dewasaDate3) {
        this.dewasaDate3 = dewasaDate3;
    }

    public String getDewasaDate4() {
        return dewasaDate4;
    }

    public void setDewasaDate4(String dewasaDate4) {
        this.dewasaDate4 = dewasaDate4;
    }

    public String getAnakDate1() {
        return anakDate1;
    }

    public void setAnakDate1(String anakDate1) {
        this.anakDate1 = anakDate1;
    }

    public String getAnakDate2() {
        return anakDate2;
    }

    public void setAnakDate2(String anakDate2) {
        this.anakDate2 = anakDate2;
    }

    public String getAnakDate3() {
        return anakDate3;
    }

    public void setAnakDate3(String anakDate3) {
        this.anakDate3 = anakDate3;
    }

    public String getBayiDate1() {
        return bayiDate1;
    }

    public void setBayiDate1(String bayiDate1) {
        this.bayiDate1 = bayiDate1;
    }

    public String getBayiDate2() {
        return bayiDate2;
    }

    public void setBayiDate2(String bayiDate2) {
        this.bayiDate2 = bayiDate2;
    }

    public String getBayiDate3() {
        return bayiDate3;
    }

    public void setBayiDate3(String bayiDate3) {
        this.bayiDate3 = bayiDate3;
    }

    public String getBayiDate4() {
        return bayiDate4;
    }

    public void setBayiDate4(String bayiDate4) {
        this.bayiDate4 = bayiDate4;
    }
    
    private ParameterManager parameterManager;
    private ResponseCodeManager responseCodeManager;
    private CounterFactoryManager counterFactoryManager;
    private SocketClientProvider client;

    @SpringBean("parameterManager")
    public void setParameterManager(ParameterManager parameterManager) {
        this.parameterManager = parameterManager;
    }

    @SpringBean("responseCodeManager")
    public void setResponseCodeManager(ResponseCodeManager responseCodeManager) {
        this.responseCodeManager = responseCodeManager;
    }
    
    @SpringBean("counterFactoryManager")
    public void setCounterFactoryManager(CounterFactoryManager counterFactoryManager) {
        this.counterFactoryManager = counterFactoryManager;
    }
    
    @SpringBean("socketClientProvider")
    public void setClient(SocketClientProvider socketClientProvider) {
        this.client = socketClientProvider;
    }

    public String getContactPhone() {
        return contactPhone;
    }

    public void setContactPhone(String contactPhone) {
        this.contactPhone = contactPhone;
    }

    public String getDewasaKtp1() {
        return dewasaKtp1;
    }

    public void setDewasaKtp1(String dewasaKtp1) {
        this.dewasaKtp1 = dewasaKtp1;
    }

    public String getDewasaKtp2() {
        return dewasaKtp2;
    }

    public void setDewasaKtp2(String dewasaKtp2) {
        this.dewasaKtp2 = dewasaKtp2;
    }

    public String getDewasaKtp3() {
        return dewasaKtp3;
    }

    public void setDewasaKtp3(String dewasaKtp3) {
        this.dewasaKtp3 = dewasaKtp3;
    }

    public String getDewasaKtp4() {
        return dewasaKtp4;
    }

    public void setDewasaKtp4(String dewasaKtp4) {
        this.dewasaKtp4 = dewasaKtp4;
    }

    public String getAsal() {
        return (String) getMaps().get(ProcessorKAI.ORIGINAL);
    }

    public String getAsalName() {
        return (String) getMaps().get(ProcessorKAI.ORIGINAL_NAME);
    }

    public String getTujuan() {
        return (String) getMaps().get(ProcessorKAI.DESTINATION);
    }

    public String getTujuanName() {
        return (String) getMaps().get(ProcessorKAI.DESTINATION_NAME);
    }

    public String getAnak() {
        return (String) getMaps().get(ProcessorKAI.ANAK);
    }

    public String getDewasa() {
        return (String) getMaps().get(ProcessorKAI.DEWASA);
    }

    public String getInfant() {
        return (String) getMaps().get(ProcessorKAI.BAYI);
    }

    @SuppressWarnings("unchecked")
    public Map<String, Object> getMaps() {
        return (Map<String, Object>) sessionManager.getSession(getContext().getRequest(), "KAI_SCHEDULLE");
    }

    public String getDewasaNama1() {
        return dewasaNama1;
    }

    public void setDewasaNama1(String dewasaNama1) {
        this.dewasaNama1 = dewasaNama1;
    }

    public String getDewasaNama2() {
        return dewasaNama2;
    }

    public void setDewasaNama2(String dewasaNama2) {
        this.dewasaNama2 = dewasaNama2;
    }

    public String getDewasaNama3() {
        return dewasaNama3;
    }

    public void setDewasaNama3(String dewasaNama3) {
        this.dewasaNama3 = dewasaNama3;
    }

    public String getDewasaNama4() {
        return dewasaNama4;
    }

    public void setDewasaNama4(String dewasaNama4) {
        this.dewasaNama4 = dewasaNama4;
    }

    public String getAnakNama1() {
        return anakNama1;
    }

    public void setAnakNama1(String anakNama1) {
        this.anakNama1 = anakNama1;
    }

    public String getAnakNama2() {
        return anakNama2;
    }

    public void setAnakNama2(String anakNama2) {
        this.anakNama2 = anakNama2;
    }

    public String getAnakNama3() {
        return anakNama3;
    }

    public void setAnakNama3(String anakNama3) {
        this.anakNama3 = anakNama3;
    }

    public String getBayiNama1() {
        return bayiNama1;
    }

    public void setBayiNama1(String bayiNama1) {
        this.bayiNama1 = bayiNama1;
    }

    public String getBayiNama2() {
        return bayiNama2;
    }

    public void setBayiNama2(String bayiNama2) {
        this.bayiNama2 = bayiNama2;
    }

    public String getBayiNama3() {
        return bayiNama3;
    }

    public void setBayiNama3(String bayiNama3) {
        this.bayiNama3 = bayiNama3;
    }

    public String getBayiNama4() {
        return bayiNama4;
    }

    public void setBayiNama4(String bayiNama4) {
        this.bayiNama4 = bayiNama4;
    }

    public Jadwal getJadwal() {
        return (Jadwal) getMaps().get("PILIH_KAI");
    }

    @Before
    @DontValidate
    public void startup() {
    }

    @After
    @DontValidate
    public void endup() {
    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {
        if (getMaps() == null) {
            return new RedirectResolution(KAIInquiry.class);
        } else {
            return new ForwardResolution(VIEW);
        }
    }

    @DontValidate
    public Resolution home() {
        return new RedirectResolution(KAIInquiry.class);
    }

    @ValidationMethod
    public void cekInput(ValidationErrors errors) {
        int dewasa = Integer.parseInt(getDewasa());
        int anak = Integer.parseInt(getAnak());
        int infant = Integer.parseInt(getInfant());

        /**Date dateDewa = DateTimeUtil.getCustomYears(new Date(), -12);
        Date dateAnna = DateTimeUtil.getCustomYears(new Date(), -3);*/

        if (dewasa > 0) {
            if (getDewasaNama1() == null || getDewasaNama1().equals("") || getDewasaKtp1() == null || getDewasaKtp1().equals("") || getContactPhone() == null || getContactPhone().equals("")){// || getDewasaDate1() == null || getDewasaDate1().equals("")) {
                errors.add("dewasaNama1", new LocalizableError("input.dewasa.kurang", new Object[]{"1"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getDewasaDate1(), DateTimeUtil.WEBDATEKAI);                
                if (date.after(dateDewa)) {
                    errors.add("dewasaDate1", new LocalizableError("input.dewasa.date", new Object[]{"1"}));
                }(*/
            }
        }

        if (dewasa > 1) {
            if (getDewasaNama2() == null || getDewasaNama2().equals("") || getDewasaKtp2() == null || getDewasaKtp2().equals("")){// || getDewasaDate2() == null || getDewasaDate2().equals("")) {
                errors.add("dewasaNama2", new LocalizableError("input.dewasa.kurang", new Object[]{"2"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getDewasaDate2(), DateTimeUtil.WEBDATEKAI);
                if (date.after(dateDewa)) {
                    errors.add("dewasaDate2", new LocalizableError("input.dewasa.date", new Object[]{"2"}));
                }*/
            }
        }

        if (dewasa > 2) {
            if (getDewasaNama3() == null || getDewasaNama3().equals("") || getDewasaKtp3() == null || getDewasaKtp3().equals("")){// || getDewasaDate3() == null || getDewasaDate3().equals("")) {
                errors.add("dewasaNama3", new LocalizableError("input.dewasa.kurang", new Object[]{"3"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getDewasaDate3(), DateTimeUtil.WEBDATEKAI);
                if (date.after(dateDewa)) {
                    errors.add("dewasaDate3", new LocalizableError("input.dewasa.date", new Object[]{"3"}));
                }*/
            }
        }

        if (dewasa > 3) {
            if (getDewasaNama4() == null || getDewasaNama4().equals("") || getDewasaKtp4() == null || getDewasaKtp4().equals("")){// || getDewasaDate4() == null || getDewasaDate4().equals("")) {
                errors.add("dewasaNama4", new LocalizableError("input.dewasa.kurang", new Object[]{"4"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getDewasaDate4(), DateTimeUtil.WEBDATEKAI);
                if (date.after(dateDewa)) {
                    errors.add("dewasaDate4", new LocalizableError("input.dewasa.date", new Object[]{"4"}));
                }*/
            }
        }

        if (anak > 0) {
            if (getAnakNama1() == null || getAnakNama1().equals("")){// || getAnakDate1() == null || getAnakDate1().equals("")) {
                errors.add("anakNama1", new LocalizableError("input.anak.kurang", new Object[]{"1"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getAnakDate1(), DateTimeUtil.WEBDATEKAI);
                if (dateDewa.after(date)) {
                    errors.add("anakDate1", new LocalizableError("input.anak.date", new Object[]{"1"}));
                }*/
            }
        }

        if (anak > 1) {
            if (getAnakNama2() == null || getAnakNama2().equals("")){// || getAnakDate2() == null || getAnakDate2().equals("")) {
                errors.add("anakNama2", new LocalizableError("input.anak.kurang", new Object[]{"2"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getAnakDate2(), DateTimeUtil.WEBDATEKAI);
                if (dateDewa.after(date)) {
                    errors.add("anakDate2", new LocalizableError("input.anak.date", new Object[]{"2"}));
                }*/
            }
        }

        if (anak > 2) {
            if (getAnakNama3() == null || getAnakNama3().equals("")){// || getAnakDate3() == null || getAnakDate3().equals("")) {
                errors.add("anakNama3", new LocalizableError("input.anak.kurang", new Object[]{"3"}));
            } else {
                Date date = DateTimeUtil.convertStringToDateCustomized(getAnakDate3(), DateTimeUtil.WEBDATEKAI);
                /**if (dateDewa.after(date)) {
                    errors.add("anakDate3", new LocalizableError("input.anak.date", new Object[]{"3"}));
                }*/
            }
        }

        if (infant > 0) {
            if (getBayiNama1() == null || getBayiNama1().equals("")){// || getBayiDate1() == null || getBayiDate1().equals("")) {
                errors.add("bayiNama1", new LocalizableError("input.infant.kurang", new Object[]{"1"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getBayiDate1(), DateTimeUtil.WEBDATEKAI);
                if (dateAnna.after(date)) {
                    errors.add("bayiDate1", new LocalizableError("input.infant.date", new Object[]{"1"}));
                }*/
            }
        }

        if (infant > 1) {
            if (getBayiNama2() == null || getBayiNama2().equals("")){// || getBayiDate2() == null || getBayiDate2().equals("")) {
                errors.add("bayiNama1", new LocalizableError("input.infant.kurang", new Object[]{"2"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getBayiDate2(), DateTimeUtil.WEBDATEKAI);
                if (dateAnna.after(date)) {
                    errors.add("bayiDate2", new LocalizableError("input.infant.date", new Object[]{"2"}));
                }*/
            }
        }

        if (infant > 2) {
            if (getBayiNama3() == null || getBayiNama3().equals("")){// || getBayiDate3() == null || getBayiDate3().equals("")) {
                errors.add("bayiNama3", new LocalizableError("input.infant.kurang", new Object[]{"3"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getBayiDate3(), DateTimeUtil.WEBDATEKAI);
                if (dateAnna.after(date)) {
                    errors.add("bayiDate3", new LocalizableError("input.infant.date", new Object[]{"3"}));
                }*/
            }
        }

        if (infant > 3) {
            if (getBayiNama4() == null || getBayiNama4().equals("")){// || getBayiDate4() == null || getBayiDate4().equals("")) {
                errors.add("bayiNama4", new LocalizableError("input.infant.kurang", new Object[]{"4"}));
            } else {
                /**Date date = DateTimeUtil.convertStringToDateCustomized(getBayiDate4(), DateTimeUtil.WEBDATEKAI);
                if (dateAnna.after(date)) {
                    errors.add("bayiDate4", new LocalizableError("input.infant.date", new Object[]{"4"}));
                }*/
            }
        }
    }

    public Resolution booking() {
        String stan = StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6);
        String kioskId = parameterManager.getParameter("kiosk.id").getValue();
        String kioskLocation = parameterManager.getParameter("kiosk.location").getValue();        
        
        Map<String, Object> maps = getMaps();
        maps.put(ProcessorKAI.STAN, stan);
        maps.put(ProcessorKAI.BANK_REF, DateTimeUtil.convertDateToStringCustomized(new Date(), DateTimeUtil.YYMMDD).concat(stan));        
        maps.put(ProcessorKAI.KIOSK_ID, kioskId);
        maps.put(ProcessorKAI.KIOSK_LOCATION, kioskLocation);     
        
        List<Penumpang> penumpangs = new ArrayList<Penumpang>();
        if (getDewasaNama1() != null) {
            maps.put(ProcessorKAI.NAMA_DEWASA1, getDewasaNama1());
            maps.put(ProcessorKAI.ID_DEWASA1, getDewasaKtp1());
            maps.put(ProcessorKAI.DATE_DEWASA1, "19860601");
        }

        if (getDewasaNama2() != null) {
            maps.put(ProcessorKAI.NAMA_DEWASA2, getDewasaNama2());
        }
        if (getDewasaKtp2() != null) {
            maps.put(ProcessorKAI.ID_DEWASA2, getDewasaKtp2());
        }

        if (getDewasaNama3() != null) {
            maps.put(ProcessorKAI.NAMA_DEWASA3, getDewasaNama3());
        }
        if (getDewasaKtp3() != null) {
            maps.put(ProcessorKAI.ID_DEWASA3, getDewasaKtp3());
        }

        if (getDewasaNama4() != null) {
            maps.put(ProcessorKAI.NAMA_DEWASA4, getDewasaNama4());
        }
        if (getDewasaKtp4() != null) {
            maps.put(ProcessorKAI.ID_DEWASA4, getDewasaKtp4());
        }

        if (getAnakNama1() != null) {
            maps.put(ProcessorKAI.NAMA_ANAK1, getAnakNama1());
        }

        if (getAnakNama2() != null) {
            maps.put(ProcessorKAI.NAMA_ANAK2, getAnakNama2());
        }

        if (getAnakNama3() != null) {
            maps.put(ProcessorKAI.NAMA_ANAK3, getAnakNama3());
        }

        if (getBayiNama1() != null) {
            maps.put(ProcessorKAI.NAMA_BAYI1, getBayiNama1());
        }

        if (getBayiNama2() != null) {
            maps.put(ProcessorKAI.NAMA_BAYI2, getBayiNama2());
        }

        if (getBayiNama3() != null) {
            maps.put(ProcessorKAI.NAMA_BAYI3, getBayiNama3());
        }

        if (getBayiNama4() != null) {
            maps.put(ProcessorKAI.NAMA_BAYI4, getBayiNama4());
        }

        if (getContactPhone() != null) {
            maps.put(ProcessorKAI.TELP_PEMESAN, getContactPhone());
        }

        ProcessorKAI processor = new InquiryKAIBookingProcessor();
        try {
            ISOMsg isoMsg = processor.prepareRequest(maps);
            String key = isoMsg.getString(11) + isoMsg.getString(7);
            isoMsg.setPackager(PackagerFactory.getPackager());            
            client.transmit(new String(isoMsg.pack()));
            isoMsg = null;
            long timeout = System.currentTimeMillis() + Long.parseLong(parameterManager.getParameter("server.timeout").getValue());
            while (System.currentTimeMillis() < timeout) {
                isoMsg = client.getMessageMap().remove(key);
                if (isoMsg != null) {
                    break;
                }
                Thread.sleep(100);
            }

            if (isoMsg == null) {  
                getContext().getMessages().add(new LocalizableMessage("server.timeout"));
            } else if (!isoMsg.getString(39).equals("00")) {
                ResponseCode respCode = responseCodeManager.getRespCode(isoMsg.getString(39));
                if (respCode != null) {
                    getContext().getMessages().add(new SimpleError(respCode.getDescription()));
                } else {
                    getContext().getMessages().add(new SimpleError("Unknown Error"));
                }
            } else {

                String bit48 = isoMsg.getString(48);

                maps.put(ProcessorKAI.CODEBOOK, bit48.substring(73, 88).trim());

                int start = 124 + (104 * Integer.parseInt(getDewasa())) + (58 * (Integer.parseInt(getAnak()) + Integer.parseInt(getInfant())));


                if (getDewasaNama1() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas(getDewasaKtp1());
                    penumpang.setNama(getDewasaNama1());
                    penumpang.setTipe("Dewasa");
                    penumpang.setSeat(bit48.substring(start, start + 8).trim() + "-" + bit48.substring(start + 8, start + 10) + "/" + bit48.substring(start + 10, start + 14));
                    start = start + 14;
                    penumpangs.add(penumpang);

                }

                if (getDewasaNama2() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas(getDewasaKtp2());
                    penumpang.setNama(getDewasaNama2());
                    penumpang.setTipe("Dewasa");
                    penumpang.setSeat(bit48.substring(start, start + 8).trim() + "-" + bit48.substring(start + 8, start + 10) + "/" + bit48.substring(start + 10, start + 14));
                    start = start + 14;
                    penumpangs.add(penumpang);
                }

                if (getDewasaNama3() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas(getDewasaKtp3());
                    penumpang.setNama(getDewasaNama3());
                    penumpang.setTipe("Dewasa");
                    penumpang.setSeat(bit48.substring(start, start + 8).trim() + "-" + bit48.substring(start + 8, start + 10) + "/" + bit48.substring(start + 10, start + 14));
                    start = start + 14;
                    penumpangs.add(penumpang);
                }

                if (getDewasaNama4() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas(getDewasaKtp4());
                    penumpang.setNama(getDewasaNama4());
                    penumpang.setTipe("Dewasa");
                    penumpang.setSeat(bit48.substring(start, start + 8).trim() + "-" + bit48.substring(start + 8, start + 10) + "/" + bit48.substring(start + 10, start + 14));
                    start = start + 14;
                    penumpangs.add(penumpang);
                }

                if (getAnakNama1() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas("");
                    penumpang.setNama(getAnakNama1());
                    penumpang.setTipe("Anak");
                    penumpang.setSeat(bit48.substring(start, start + 8).trim() + "-" + bit48.substring(start + 8, start + 10) + "/" + bit48.substring(start + 10, start + 14));
                    start = start + 14;
                    penumpangs.add(penumpang);
                }

                if (getAnakNama2() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas("");
                    penumpang.setNama(getAnakNama2());
                    penumpang.setTipe("Anak");
                    penumpang.setSeat(bit48.substring(start, start + 8).trim() + "-" + bit48.substring(start + 8, start + 10) + "/" + bit48.substring(start + 10, start + 14));
                    start = start + 14;
                    penumpangs.add(penumpang);
                }

                if (getAnakNama3() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas("");
                    penumpang.setNama(getAnakNama3());
                    penumpang.setTipe("Anak");
                    penumpang.setSeat(bit48.substring(start, start + 8).trim() + "-" + bit48.substring(start + 8, start + 10) + "/" + bit48.substring(start + 10, start + 14));
                    start = start + 14;
                    penumpangs.add(penumpang);
                }

                if (getBayiNama1() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas("");
                    penumpang.setNama(getBayiNama1());
                    penumpang.setTipe("Infant");
                    penumpang.setSeat("");
                    penumpangs.add(penumpang);
                }

                if (getBayiNama2() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas("");
                    penumpang.setNama(getBayiNama2());
                    penumpang.setTipe("Infant");
                    penumpang.setSeat("");
                    penumpangs.add(penumpang);
                }

                if (getBayiNama3() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas("");
                    penumpang.setNama(getBayiNama3());
                    penumpang.setTipe("Infant");
                    penumpang.setSeat("");
                    penumpangs.add(penumpang);
                }

                if (getBayiNama4() != null) {
                    Penumpang penumpang = new Penumpang();
                    penumpang.setIdentitas("");
                    penumpang.setNama(getBayiNama4());
                    penumpang.setTipe("Infant");
                    penumpang.setSeat("");
                    penumpangs.add(penumpang);
                }

                maps.put(ProcessorKAI.PENUMPANG, penumpangs);
                maps.put(ProcessorKAI.BIT48_2, isoMsg.getString(48));
                sessionManager.setSession(getContext().getRequest(), "KAI_SCHEDULLE", maps);
                return new RedirectResolution(KAIPayment.class).addParameter("keySession", getKeySession());
            }
        } catch (Exception e) {
            e.printStackTrace();
        }

        return new ForwardResolution(VIEW);
    }
}
