package com.ics.ssk.ego.web;

import com.ics.ssk.ego.converter.NumberTypeConverter;
import com.ics.ssk.ego.device.controller.PrinterController;
import com.ics.ssk.ego.iso8583.PackagerFactory;
import com.ics.ssk.ego.iso8583.SocketClientProvider;
import com.ics.ssk.ego.iso8583.processor.InquiryPurchaseTelcoProcessor;
import com.ics.ssk.ego.iso8583.processor.Processor;
import com.ics.ssk.ego.iso8583.processor.PurchaseTelcoProcessor;
import com.ics.ssk.ego.manager.AdvertisementManager;
import com.ics.ssk.ego.manager.CounterFactoryManager;
import com.ics.ssk.ego.manager.EgoManager;
import com.ics.ssk.ego.manager.ParameterManager;
import com.ics.ssk.ego.manager.ResponseCodeManager;
import com.ics.ssk.ego.model.Advertisement;
import com.ics.ssk.ego.model.EgoProduct;
import com.ics.ssk.ego.model.LogEgo;
import com.ics.ssk.ego.model.MessageUtil;
import com.ics.ssk.ego.model.ResponseCode;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.StringUtil;
import edu.emory.mathcs.backport.java.util.Arrays;
import java.text.SimpleDateFormat;
import java.util.Date;
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
import net.sourceforge.stripes.validation.SimpleError;
import net.sourceforge.stripes.validation.Validate;
import org.apache.log4j.Logger;
import org.jpos.iso.ISOMsg;

@UrlBinding("/egopurchasetelco.html")
public class EgoPurchaseTelco extends BaseActionBean {
    
    Logger logger = Logger.getLogger(EgoPurchaseTelco.class);

    public static final String VIEW1 = "/pages/ego/telco_prepaid/inqpurchase.jsp";
    public static final String VIEW2 = "/pages/ego/telco_prepaid/infopurchase.jsp";    
    public static final String VIEW3 = "/pages/ego/telco_prepaid/okpurchase.jsp";    
    
    private ParameterManager parameterManager;
    private ResponseCodeManager responseCodeManager;
    private EgoManager egoManager;
    private CounterFactoryManager counterFactoryManager;
    private AdvertisementManager advertisementManager;
    
    private String prefix;
    @Validate(required = true, converter = NumberTypeConverter.class, on = "inquiry")
    private String customerId;
    private String productId;
    private MessageUtil message;
    private String id;
    private Map<String, MessageUtil> sessionTable;    
    private Map<String, Object[]> messageTable;
    
    private SocketClientProvider client;    
    private PrinterController printerController;

    public void setProductId(String productId) {
        this.productId = productId;
    }

    public String getProductId() {
        return productId;
    }

    public String getCustomerId() {
        return customerId;
    }

    public void setCustomerId(String customerId) {
        this.customerId = customerId;
    }

    public String getPrefix() {
        return prefix;
    }

    public void setPrefix(String prefix) {
        this.prefix = prefix;
    }

    public MessageUtil getMessage() {
        return message;
    }

    public void setMessage(MessageUtil message) {
        this.message = message;
    }

    public void setId(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
    }

    @SpringBean("counterFactoryManager")
    public void setCounterFactoryManager(CounterFactoryManager counterFactoryManager) {
        this.counterFactoryManager = counterFactoryManager;
    }

    @SpringBean("responseCodeManager")
    public void setResponseCodeManager(ResponseCodeManager responseCodeManager) {
        this.responseCodeManager = responseCodeManager;
    }

    @SpringBean("egoManager")
    public void setEgoManager(EgoManager egoManager) {
        this.egoManager = egoManager;
    }

    @SpringBean("advertisementManager")
    public void setAdvertisementManager(AdvertisementManager advertisementManager) {
        this.advertisementManager = advertisementManager;
    }
        
    @SpringBean("parameterManager")
    public void setParameterManager(ParameterManager parameterManager) {
        this.parameterManager = parameterManager;
    }

    @SpringBean("sessionTable")
    public void setSessionTable(Map<String, MessageUtil> sessionTable) {
        this.sessionTable = sessionTable;
    }

    @SpringBean("messageTable")
    public void setMessageTable(Map<String, Object[]> messageTable) {
        this.messageTable = messageTable;
    }  

    @SpringBean("socketClientProvider")
    public void setSocketClientProvider(SocketClientProvider socketClientProvider) {
        this.client = socketClientProvider;
    }

    @SpringBean("printerController")
    public void setPrinterController(PrinterController printerController) {
        this.printerController = printerController;
    }
            
    @Before
    @DontValidate
    public void startup() {        
        if (id != null) {
            message = sessionTable.remove(id);            
        }        
    }

    @After
    @DontValidate   
    public void endup() {
    }

    public Resolution purchase() {
        return new ForwardResolution(VIEW1);
    }

    @DontValidate
    public Resolution cancel() {
        logger.info("cancel payment...");        
        messageTable.put(id, new Object[]{message, null});
        return new RedirectResolution(EgoCashCancel.class).addParameter("id", id);
    }

    @DontValidate
    public Resolution back() {
        return new RedirectResolution(EgoWelcome.class);
    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {
        return new ForwardResolution(VIEW1);
    }

    @SuppressWarnings({"CallToThreadDumpStack", "SleepWhileInLoop"})
    public Resolution inquiry() {        
        String kioskId = parameterManager.getParameter("kiosk.id").getValue();        
        String kioskLocation = parameterManager.getParameter("kiosk.location").getValue();        
        EgoProduct product = egoManager.getProduct(this.getProductId());
        message = new MessageUtil();

        prefix = customerId.substring(0, 3);
        String[] threeDigitsAreaCode = {"021", "022", "024", "031", "061"};
        if (!Arrays.asList(threeDigitsAreaCode).contains(prefix)) {
            prefix = customerId.substring(0, 4);
            customerId = customerId.substring(4);
        } else {
            prefix = "0" + prefix;
            customerId = customerId.substring(3);
        }

        message.setPrefix(prefix);
        message.setCustomerId(customerId);
        message.setBillerId(product.getIndustryCode() + product.getBillerCode());
        message.setBillerName(product.getBillerDescription());        
        message.setDate(new Date());
        message.setAmount(product.getAmount());
        message.setProductName(product.getProductDescription());
        message.setProductId(product.getProductCode());
        message.setProductCode(this.getProductId());
        message.setKioskId(kioskId);
        message.setKioskLocation(kioskLocation);
        message.setStan(StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6));
        message.setRefNumber(DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD) + message.getStan());
        message.setTahunBulanTag1(DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD).substring(0, 4));
        message.setStep(0);

        Processor processor = new InquiryPurchaseTelcoProcessor();

        try {            

            ISOMsg isoMsg = processor.prepareRequest(message);            
            isoMsg.setPackager(PackagerFactory.getPackager());            
            id = message.getStan();
            sessionTable.put(id, message);            
            String key = isoMsg.getString(11)+isoMsg.getString(7);            
            client.transmit(new String(isoMsg.pack()));
            isoMsg = null;
            long timeout = System.currentTimeMillis()+Long.parseLong(parameterManager.getParameter("server.timeout").getValue());
            while (System.currentTimeMillis() < timeout) {                   
                isoMsg = client.getMessageMap().remove(key);
                if (isoMsg!=null){
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
                message.setPrice(Double.parseDouble(isoMsg.getString(48).substring(28)));
                message.setCurrentCashNote(0D);                
                sessionTable.put(id, message);
                messageTable.put(id, new Object[]{message,this});
                return new ForwardResolution(VIEW2).addParameter("id", id);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new ForwardResolution(VIEW1).addParameter("id", id);
    }

    @DontValidate
    @SuppressWarnings({"SleepWhileInLoop", "CallToThreadDumpStack"})
    public Resolution exePayment() {        
        Processor processor = new PurchaseTelcoProcessor();

        try {            
            ISOMsg isoMsg = processor.prepareRequest(message);
            isoMsg.setPackager(PackagerFactory.getPackager());            
            sessionTable.put(id, message);            
            String key = isoMsg.getString(11)+isoMsg.getString(7);
            client.transmit(new String(isoMsg.pack()));
            isoMsg = null;
            long timeout = System.currentTimeMillis()+Long.parseLong(parameterManager.getParameter("server.timeout").getValue());
            while (System.currentTimeMillis() < timeout) {  
                isoMsg = client.getMessageMap().remove(key);
                if (isoMsg!=null){
                    break;
                }
                Thread.sleep(100);
            }
            
            if (isoMsg == null) {
                getContext().getMessages().add(new LocalizableMessage("server.timeout.check"));
            } else {
                if (isoMsg.getString(39).equals("00")) {
                    message.setStatus("Sukses");
                    message.setVoucherNumber1(isoMsg.getString(48).substring(48).trim());
                    dispenseNote(id, message.getCdMap());
                    printReceipt();
                } else if (isoMsg.getString(39).equals("50")) {                    
                    message.setStatus("Suspect");
                    dispenseNote(id, message.getCdMap());
                    printReceipt();
                } else {
                    message.setStatus("Gagal");                    
                    dispenseNote(id, message.getBaMap());
                }

                saveLog(message, new String(isoMsg.pack()));

                id = DateTimeUtil.convertDateToStringCustomized(new Date(), DateTimeUtil.YYYYMMDDHHMMSSSSS);
                sessionTable.put(id, message);
                return new ForwardResolution(VIEW3).addParameter("id", id);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new ForwardResolution(VIEW1).addParameter("id", id);
    }
            
    private void printReceipt(){        
        StringBuilder sb = new StringBuilder();
        sb.append("^BE    PT. INDO CIPTA GUNA ^C");        
        sb.append("^SL    TANGGAL JAM   : ").append(new SimpleDateFormat("dd/MM/yyyy").format(new Date())).append(" ")
                .append(new SimpleDateFormat("HH:mm").format(new Date())).append(" ^C");   
        sb.append("^SL    KIOSK ID      : ").append(message.getKioskId()).append(" ^C");
        sb.append("^SL    LOKASI        : ").append(message.getKioskLocation()).append(" ^C");
        sb.append("^SL    NO REF        : ").append(message.getRefNumber()).append(" ^C");
        sb.append("^SL ^C");
        sb.append("^SE    PEMBELIAN VOUCHER ").append(message.getBillerName().toUpperCase()).append(" ^C");
        sb.append("^SL    NO HANDPHONE  : ").append(message.getPrefix()).append(message.getCustomerId()).append(" ^C");
        sb.append("^SL    NO SERI       : ").append(message.getVoucherNumber1()).append(" ^C");
        //sb.append("^SL    TANGGAL BELI  : ").append(new SimpleDateFormat("dd/MM/yyyy HH:mm").format(message.getDate())).append(" ^C");
        sb.append("^SL    DENOMINASI    : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getAmount(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    TOTAL HARGA   : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPrice(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");        
        sb.append("^SL    PEMBAYARAN===========================^C");
        sb.append("^SL    UANG TUNAI    : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getCurrentCashNote(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        if (message.getChangeAmount()!=null && message.getChangeAmount() != 0D){
            sb.append("^SL    UANG KEMBALI  : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeCashNote(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");            
        }
        if (message.getChangeNotPaid()!=null && message.getChangeNotPaid() != 0D){            
            sb.append("^SL    TOTAL DONASI  : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeNotPaid(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }
        sb.append("^SL ^C");
        if (message.getStatus().equals("Sukses")){
            sb.append("^SE    PULSA ANDA AKAN BERTAMBAH SECARA OTOMATIS ^C");
            sb.append("^SE    TERIMA KASIH ^C");
        }
        if (message.getStatus().equals("Suspect")){
            sb.append("^SE    TRANSAKSI SEDANG DIPROSES ^C");
            sb.append("^SE    LAKUKAN CEK STATUS ^C");
        }
        if (advertisementManager.getReceipt()!=null){
            for (Advertisement advertisement : advertisementManager.getReceipt()){
                sb.append("^SE    ").append(advertisement.getContent()).append(" ^C");
            }
        }
        sb.append("^SL ^C").append("^SL ^C").append("^X");
        boolean printed = printerController.printReceipt(sb.toString(), message.getStan());        
    }

    private boolean saveLog(MessageUtil message, String iso) {
        LogEgo log = new LogEgo();
        log.setAmount(message.getAmount());
        log.setBulantahun(message.getTahunBulanTag1());
        log.setDate(message.getDate());
        log.setIso(iso);
        log.setPayeeId(message.getPrefix() + message.getCustomerId());
        log.setPrice(message.getPrice());
        log.setProduct(message.getProductCode());
        log.setKioskId(message.getKioskId());
        log.setResponse(message.getStatus());
        log.setStan(message.getStan());
        log.setTagihan(1);
        log.setVoucher(message.getVoucherNumber1());
        log.setPaymentOption(message.getPaymentOption());      
        log.setRefNumber(message.getRefNumber());
        return egoManager.saveLog(log);
    }
}
