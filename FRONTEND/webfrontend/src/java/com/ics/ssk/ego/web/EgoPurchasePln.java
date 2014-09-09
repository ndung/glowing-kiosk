package com.ics.ssk.ego.web;

import com.ics.ssk.ego.converter.NumberTypeConverter;
import com.ics.ssk.ego.device.controller.PrinterController;
import com.ics.ssk.ego.iso8583.PackagerFactory;
import com.ics.ssk.ego.iso8583.SocketClientProvider;
import com.ics.ssk.ego.iso8583.processor.InquiryPurchasePlnProcessor;
import com.ics.ssk.ego.iso8583.processor.Processor;
import com.ics.ssk.ego.iso8583.processor.PurchaseAdvicePlnProcessor;
import com.ics.ssk.ego.iso8583.processor.PurchasePlnProcessor;
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
import com.ics.ssk.ego.util.SelectModel;
import com.ics.ssk.ego.util.StringUtil;
import java.text.SimpleDateFormat;
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
import net.sourceforge.stripes.validation.SimpleError;
import net.sourceforge.stripes.validation.Validate;
import org.jpos.iso.ISOMsg;

@UrlBinding("/egopurchasepln.html")
public class EgoPurchasePln extends BaseActionBean {

    public static final String VIEW1 = "/pages/ego/pln_prepaid/inqpurchase.jsp";
    public static final String VIEW2 = "/pages/ego/pln_prepaid/infopurchase.jsp";    
    public static final String VIEW3 = "/pages/ego/pln_prepaid/okpurchase.jsp";
    
    private ParameterManager parameterManager;
    private EgoManager egoManager;
    private ResponseCodeManager responseCodeManager;
    private CounterFactoryManager counterFactoryManager;
    private AdvertisementManager advertisementManager;
    @Validate(required = true, converter = NumberTypeConverter.class, on = "inquiry")
    private String customerId;
    private String productId;
    private MessageUtil message;
    private String id;    
    private String suffix;
    private Map<String, MessageUtil> sessionTable;
    private Map<String, Object[]> messageTable;
    private SocketClientProvider client;    
    private PrinterController printerController;
//    @Validate(required = true, converter = NumberTypeConverter.class, on = "inquiry", minvalue = 20000)
    private String amount;

    public String getCustomerId() {
        return customerId;
    }

    public void setCustomerId(String customerId) {
        this.customerId = customerId;
    }

    public String getProductId() {
        return productId;
    }

    public void setProductId(String productId) {
        this.productId = productId;
    }

    public String getId() {
        return id;
    }

    public void setId(String id) {
        this.id = id;
    }

    public void setAmount(String amount) {
        this.amount = amount;
    }

    public String getAmount() {
        return amount;
    }
    
    public String getSuffix() {
        return suffix;
    }

    public void setSuffix(String suffix) {
        this.suffix = suffix;
    }

    public MessageUtil getMessage() {
        return message;
    }

    public void setMessage(MessageUtil message) {
        this.message = message;
    }

    @SpringBean("counterFactoryManager")
    public void setCounterFactoryManager(CounterFactoryManager counterFactoryManager) {
        this.counterFactoryManager = counterFactoryManager;
    }

    @SpringBean("responseCodeManager")
    public void setResponseCodeManager(ResponseCodeManager responseCodeManager) {
        this.responseCodeManager = responseCodeManager;
    }

    @SpringBean("parameterManager")
    public void setParameterManager(ParameterManager parameterManager) {
        this.parameterManager = parameterManager;
    }

    @SpringBean("egoManager")
    public void setEgoManager(EgoManager egoManager) {
        this.egoManager = egoManager;
    }

    @SpringBean("advertisementManager")
    public void setAdvertisementManager(AdvertisementManager advertisementManager) {
        this.advertisementManager = advertisementManager;
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

    public List<SelectModel> getSuffixs() {
        List<SelectModel> list = new ArrayList<SelectModel>();
        list.add(new SelectModel("0", "No Meter"));
        list.add(new SelectModel("1", "No IDPEL"));
        return list;
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

    public Resolution inquiry() {
        String kioskId = parameterManager.getParameter("kiosk.id").getValue();
        String kioskLocation = parameterManager.getParameter("kiosk.location").getValue();        
        EgoProduct product = egoManager.getProduct("PLNPRE");

        message = new MessageUtil();

        message.setCustomerId(this.getCustomerId());        
        message.setBillerId(product.getIndustryCode() + product.getBillerCode());
        message.setDate(new Date());
        message.setAmount(Double.parseDouble(amount));
        message.setProductName(product.getProductDescription());
        message.setProductId(product.getProductCode());
        message.setProductCode(product.getId());        
        message.setKioskId(kioskId);
        message.setKioskLocation(kioskLocation);
        message.setStan(StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6));
        message.setRefNumber(DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD) + message.getStan());
        message.setTahunBulanTag1(DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD).substring(0, 4));

        Processor processor = new InquiryPurchasePlnProcessor();

        try {
            ISOMsg isoMsg = processor.prepareRequest(message);
            isoMsg.setPackager(PackagerFactory.getPackager());
            id = message.getStan();
            sessionTable.put(id, message);

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
                String bit48 = isoMsg.getString(48).replaceAll("&quot;", "\"");
                String name = bit48.substring(88, 113).trim();

                message.setBit48(bit48);
                message.setSuffix(name);
                message.setStep(0);
                message.setAdditional1(bit48.substring(0, 11).trim());
                message.setAdditional3(bit48.substring(11, 23).trim());
                message.setAdditional2(bit48.substring(140, 144).trim() + "/" + String.valueOf(Integer.parseInt(bit48.substring(144, 153))));
                message.setVoucherNumber1(bit48.substring(56, 88).trim());
                message.setPrice(message.getAmount());
                message.setPrice1(Double.parseDouble(bit48.substring(154, 164)) / (10 * Double.parseDouble(bit48.substring(153, 154))));
                message.setAdditional5(bit48.substring(120, 135).trim());
                message.setCurrentCashNote(0D);   
                sessionTable.put(id, message);
                messageTable.put(id, new Object[]{message,this});
                return new ForwardResolution(VIEW2).addParameter("id", id);
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new ForwardResolution(VIEW1);
    }

    public Resolution advice() {
        return exePayment();
    }
    
    public Resolution cetaku() {
        printReceipt("CETAK ULANG (CU)");        
        return new ForwardResolution(EgoWelcome.class);
    }

    public Resolution exePayment() {
        int decimal = 0;        
        
        Processor processor = new PurchasePlnProcessor();
        if (message.getStep() == 100) {
            processor = new PurchaseAdvicePlnProcessor();
        }

        try {
            ISOMsg isoMsg = processor.prepareRequest(message);
            isoMsg.setPackager(PackagerFactory.getPackager());            
            sessionTable.put(id, message);

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
                getContext().getMessages().add(new LocalizableMessage("server.timeout.check"));
            } else {
                if (isoMsg.getString(39).equals("00")) {
                    message.setStatus("Sukses");
                    String bit48 = isoMsg.getString(48).replaceAll("&quot;", "'");
                    decimal = Integer.parseInt(bit48.substring(230, 231).trim());
                    message.setVoucherNumber2(StringUtil.forPln(bit48.substring(241, 261).trim()));
                    message.setVoucherNumber3(bit48.substring(231, 241).trim());
                    message.setPesan(isoMsg.getString(62).trim().toUpperCase());
                    message.setKwh(Double.parseDouble(bit48.substring(231, 241).trim()) / (Math.pow(10, decimal)));
                    message.setPrice1(Double.parseDouble(bit48.substring(163, 173).trim()) / (Math.pow(10, decimal)));
                    message.setPrice2(Double.parseDouble(bit48.substring(174, 184).trim()) / (Math.pow(10, decimal)));
                    message.setPpn(Double.parseDouble(bit48.substring(185, 195).trim()) / (Math.pow(10, decimal)));
                    message.setPpj(Double.parseDouble(bit48.substring(196, 206).trim()) / (Math.pow(10, decimal)));
                    message.setAngsuran(Double.parseDouble(bit48.substring(207, 217).trim()) / (Math.pow(10, decimal)));
                    message.setPrice(message.getAmount() - message.getPrice1() - message.getPrice2() - message.getPpn() - message.getPpj() - message.getAngsuran());
                    message.setStep(0);
                    dispenseNote(id, message.getCdMap());
                    printReceipt("CETAK ASLI (CA)");
                } else if (isoMsg.getString(39).equals("50")) {
                    message.setStatus("Suspect");
                    message.setVoucherNumber2("TRANSAKSI SEDANG DIPROSES");
                    message.setPesan("TRANSAKSI SUSPECT");
                    message.setStep(100);
                    dispenseNote(id, message.getCdMap());
                    printReceipt("CETAK ASLI (CA)");
                } else {
                    String error = "UNKNOWN ERROR";
                    ResponseCode respCode = responseCodeManager.getRespCode(isoMsg.getString(39));
                    if (respCode != null) {
                        error = respCode.getDescription().toUpperCase();
                    }
                    message.setPesan(error);
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
        return new ForwardResolution(VIEW1);
    }
    
    private void printReceipt(String type){
        StringBuilder sb = new StringBuilder();
        sb.append("^BE    PT. INDO CIPTA GUNA ^C");
        Date date = new Date();
        sb.append("^SL    TANGGAL JAM     : ").append(new SimpleDateFormat("dd/MM/yyyy").format(date)).append(" ")
                .append(new SimpleDateFormat("HH:mm").format(new Date())).append(" ^C");        
        sb.append("^SL    KIOSK ID        : ").append(message.getKioskId()).append(" ^C");
        sb.append("^SL    LOKASI          : ").append(message.getKioskLocation()).append(" ^C");
        sb.append("^SL    NO REF          : ").append(message.getRefNumber()).append(" ^C");
        sb.append("^SL ^C");
        sb.append("^SE    STRUK PEMBELIAN LISTRIK PRABAYAR ^C");
        sb.append("^SE    ").append(type).append(" ^C");
        sb.append("^SL    NO METER        : ").append(message.getAdditional1()).append(" ^C");
        sb.append("^SL    IDPEL           : ").append(message.getAdditional3()).append(" ^C");
        sb.append("^SL    NAMA            : ").append(message.getSuffix().replaceAll("\"",  "\"\"\"")).append(" ^C");
        sb.append("^SL    TARIF/DAYA      : ").append(message.getAdditional2()).append("VA").append(" ^C");
        sb.append("^SL    JPA REF         : ").append(message.getVoucherNumber1()).append(" ^C");        
        sb.append("^SL    RP BAYAR        : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getAmount(),2),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");                
        sb.append("^SL    ADMIN BANK      : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPrice1(),2),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    METERAI         : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPrice2(),2),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    PPN             : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPpn(),2),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    PPJ             : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPpj(),2),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    ANGSURAN        : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getAngsuran(),2),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    RP STROOM/TOKEN : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPrice(),2),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    JUMLAH KWH      : ").append(StringUtil.numberFormat2Decimal(message.getKwh(), 2)).append(" ^C");
        sb.append("^SL    STROOM/TOKEN    : ^C");      
        sb.append("^SE    ").append(message.getVoucherNumber2()).append(" ^C");
        sb.append("^SL    PEMBAYARAN=============================^C");
        sb.append("^SL    UANG TUNAI      : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getCurrentCashNote(),2),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        if (message.getChangeAmount()!=null && message.getChangeAmount() != 0D){
            sb.append("^SL    UANG KEMBALI    : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeCashNote(),2),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");            
        }
        if (message.getChangeNotPaid()!=null && message.getChangeNotPaid() != 0D){            
            sb.append("^SL    TOTAL DONASI    : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeNotPaid(),2),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }
        sb.append("^SL ^C");        
        sb.append("^SE    ").append(message.getPesan().toUpperCase()).append(message.getAdditional5()).append(" ^C");        
        
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
        log.setPayeeId(message.getCustomerId());
        log.setPrice(message.getPrice());
        log.setProduct(message.getProductCode());
        log.setKioskId(message.getKioskId());
        log.setResponse(message.getStatus());
        log.setStan(message.getStan());
        log.setTagihan(1);
        log.setRefNumber(message.getRefNumber());
        log.setMaps(message.getMaps());
        log.setVoucher(message.getVoucherNumber1());

        return egoManager.saveLog(log);
    }
}
