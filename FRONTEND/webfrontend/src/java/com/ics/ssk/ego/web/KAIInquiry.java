package com.ics.ssk.ego.web;

import com.ics.ssk.ego.iso8583.PackagerFactory;
import com.ics.ssk.ego.iso8583.SocketClientProvider;
import com.ics.ssk.ego.iso8583.processor.ProcessorKAI;
import com.ics.ssk.ego.iso8583.processor.ticket.InquiryKAIScheduleProcessor;
import com.ics.ssk.ego.manager.BaseManager;
import com.ics.ssk.ego.manager.CounterFactoryManager;
import com.ics.ssk.ego.manager.ParameterManager;
import com.ics.ssk.ego.manager.ResponseCodeManager;
import com.ics.ssk.ego.model.KAIStationGroup;
import com.ics.ssk.ego.model.ResponseCode;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.ParameterDao;
import com.ics.ssk.ego.util.SelectModel;
import com.ics.ssk.ego.util.StringUtil;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
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
import net.sourceforge.stripes.validation.ValidationErrors;
import net.sourceforge.stripes.validation.ValidationMethod;
import org.jpos.iso.ISOMsg;

@UrlBinding("/egokaiinquiry.html")
public class KAIInquiry extends BaseActionBean {

    private String VIEW = "/pages/ego/kai/inquiry.jsp";
    private String date;
    private String asal;
    private String tujuan;
    private String anak;
    private String dewasa;
    private String infant;
    private ParameterManager parameterManager;
    private ResponseCodeManager responseCodeManager;
    private BaseManager baseManager;
    private CounterFactoryManager counterFactoryManager;
    private SocketClientProvider client;

    @SpringBean("baseManager")
    public void setBaseManager(BaseManager baseManager) {
        this.baseManager = baseManager;
    }
    
    @SpringBean("counterFactoryManager")
    public void setCounterFactoryManager(CounterFactoryManager counterFactoryManager) {
        this.counterFactoryManager = counterFactoryManager;
    }

    @SpringBean("parameterManager")
    public void setParameterManager(ParameterManager parameterManager) {
        this.parameterManager = parameterManager;
    }

    @SpringBean("responseCodeManager")
    public void setResponseCodeManager(ResponseCodeManager responseCodeManager) {
        this.responseCodeManager = responseCodeManager;
    }
    
    @SpringBean("socketClientProvider")
    public void setClient(SocketClientProvider socketClientProvider) {
        this.client = socketClientProvider;
    }

    public void setDate(String date) {
        this.date = date;
    }

    public String getDate() {
        return date;
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

    @Before
    @DontValidate
    public void startup() {
    }

    @After
    @DontValidate
    public void endup() {
    }

    @ValidationMethod
    public void cekLogin(ValidationErrors errors) {
        int dewasa = Integer.parseInt(this.dewasa);
        int anak = Integer.parseInt(this.anak);
        int infant = Integer.parseInt(this.infant);

        if (asal.equals(tujuan)) {
            errors.add("tujuan", new LocalizableError("asal.tujuan.sama", new Object[]{}));
        } else if (dewasa == 0) {
            errors.add("dewasa", new LocalizableError("dewasa.null", new Object[]{}));
        } else if (dewasa < infant) {
            errors.add("dewasa", new LocalizableError("infant.lebih.dewasa", new Object[]{}));
        } else if ((dewasa + anak) > 4) {
            errors.add("dewasa", new LocalizableError("dewasa.anak.lebih.4", new Object[]{}));
        }

    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {
        return new ForwardResolution(VIEW);
    }

    public List<SelectModel> getJumlahs() {
        List<SelectModel> respCodes = new ArrayList<SelectModel>();
        for (int i = 0; i < 5; i++) {
            respCodes.add(new SelectModel(i, String.valueOf(i)));
        }
        return respCodes;
    }

    @SuppressWarnings("unchecked")
    public List<KAIStationGroup> getStations() {
        ParameterDao parameterDao = new ParameterDao(KAIStationGroup.class);
        parameterDao.setOrders(ParameterDao.ORDER_ASC, "description");

        return baseManager.getList(parameterDao);
    }

    public List<SelectModel> getDates() {
        List<SelectModel> respCodes = new ArrayList<SelectModel>();
        Date date = getStartDate();
        for (int i = 0; i < 90; i++) {
            respCodes.add(new SelectModel(DateTimeUtil.convertDateToStringCustomized(date, DateTimeUtil.WEBDATE), DateTimeUtil.convertIndoDate(date).toUpperCase()));
            date = DateTimeUtil.getCustomDate(date, 1);
        }
        return respCodes;
    }

    private Date getStartDate() {
        Calendar calendar = Calendar.getInstance();
        calendar.add(Calendar.HOUR, 6);
        return calendar.getTime();
    }

    public Resolution tampilkan() {
        Date date = new Date();
        String stan = StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6);
        String kioskId = parameterManager.getParameter("kiosk.id").getValue();
        String kioskLocation = parameterManager.getParameter("kiosk.location").getValue();        
        Map<String, Object> maps = new HashMap<String, Object>();
        maps.put(ProcessorKAI.DATE_TRX, date);
        maps.put("DATE_END", DateTimeUtil.getCostumMinuteDate(new Date(), 10));
        maps.put(ProcessorKAI.DATE, DateTimeUtil.convertStringToDateCustomized(this.date, DateTimeUtil.WEBDATE));
        maps.put(ProcessorKAI.ORIGINAL, asal.split("#")[0]);
        maps.put(ProcessorKAI.ORIGINAL_NAME, asal.split("#")[1]);
        maps.put(ProcessorKAI.DESTINATION, tujuan.split("#")[0]);
        maps.put(ProcessorKAI.DESTINATION_NAME, tujuan.split("#")[1]);
        maps.put(ProcessorKAI.DEWASA, dewasa);
        maps.put(ProcessorKAI.ANAK, anak);        
        maps.put(ProcessorKAI.BAYI, infant);
        maps.put(ProcessorKAI.STAN, stan);
        maps.put(ProcessorKAI.BANK_REF, DateTimeUtil.convertDateToStringCustomized(date, DateTimeUtil.YYMMDD).concat(stan));        
        maps.put(ProcessorKAI.KIOSK_ID, kioskId);
        maps.put(ProcessorKAI.KIOSK_LOCATION, kioskLocation);
        
        ProcessorKAI processor = new InquiryKAIScheduleProcessor();
        try {
            ISOMsg isoMsg = processor.prepareRequest(maps);
            isoMsg.setPackager(PackagerFactory.getPackager());
            String key = isoMsg.getString(11) + isoMsg.getString(7);
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
                maps.put(ProcessorKAI.BIT48_1, isoMsg.getString(48));
                sessionManager.setSession(getContext().getRequest(), "KAI_SCHEDULLE", maps);
                return new RedirectResolution(KAISchedule.class);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new ForwardResolution(VIEW);
    }
    
    @DontValidate
    public Resolution kembali() {
        return new RedirectResolution(EgoWelcome.class);
    }
}
