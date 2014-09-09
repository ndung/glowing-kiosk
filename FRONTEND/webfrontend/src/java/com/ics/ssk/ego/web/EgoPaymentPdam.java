package com.ics.ssk.ego.web;

import com.ics.ssk.ego.converter.NumberTypeConverter;
import com.ics.ssk.ego.device.controller.PrinterController;
import com.ics.ssk.ego.iso8583.PackagerFactory;
import com.ics.ssk.ego.iso8583.SocketClientProvider;
import com.ics.ssk.ego.iso8583.processor.InquiryPaymentPdamProcessor;
import com.ics.ssk.ego.iso8583.processor.PaymentPdamProcessor;
import com.ics.ssk.ego.iso8583.processor.Processor;
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

@UrlBinding("/egopaymentpdam.html")
public class EgoPaymentPdam extends BaseActionBean {

    Logger logger = Logger.getLogger(EgoPaymentPdam.class);
    public static final String VIEW1 = "/pages/ego/pdam_postpaid/inqpayment.jsp";
    public static final String VIEW2 = "/pages/ego/pdam_postpaid/infopayment.jsp";
    public static final String VIEW3 = "/pages/ego/pdam_postpaid/okpayment.jsp";
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

        Processor processor = new InquiryPaymentPdamProcessor();
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
                String bit48 = isoMsg.getString(48);
                message.setBit48(bit48);
                message.setAmount(Double.parseDouble(isoMsg.getString(4)));
                message.setFee(Double.parseDouble(bit48.substring(73, 81)));
                message.setAdditional1(bit48.substring(27, 57).trim().toUpperCase());
                message.setJumlahTagihan(Integer.parseInt(bit48.substring(59, 61)));
                message.setTahunBulanTag1(DateTimeUtil.convertStringToStringFormaterCustomized(bit48.substring(15, 27).substring(0, 6) + "01", DateTimeUtil.YYYYMMDD, DateTimeUtil.DD_MMM_YYYY).substring(3).toUpperCase());
                message.setTahunBulanTag2(DateTimeUtil.convertStringToStringFormaterCustomized(bit48.substring(15, 27).substring(6) + "01", DateTimeUtil.YYYYMMDD, DateTimeUtil.DD_MMM_YYYY).substring(3).toUpperCase());
                message.setPrice(message.getAmount() + message.getFee());
                message.setCurrentCashNote(0D);
                sessionTable.put(id, message);
                messageTable.put(id, new Object[]{message, this});
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
        Processor processor = new PaymentPdamProcessor();
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
                    String bit48 = isoMsg.getString(48);
                    message.setStatus("Sukses");
                    message.setVoucherNumber1(bit48.substring(76, 108).trim());
                    message.setAdditional2(DateTimeUtil.convertStringToStringFormaterCustomized(bit48.substring(128, 142), DateTimeUtil.YYYYMMDDHHMMSS, "yyyy-MM-dd HH:mm:ss"));
                    dispenseNote(id, message.getCdMap());
                    printReceipt("CETAK ASLI (CA)");
                } else if (isoMsg.getString(39).equals("50")) {
                    message.setStatus("Suspect");
                    message.setPesan("TRX SUSPECT. SILAHKAN MENGHUBUNGI CUSTOMER SERVICE.");
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
        return new ForwardResolution(VIEW1).addParameter("id", id);
    }

    private void printReceipt(String type) {
        StringBuilder sb = new StringBuilder();
        sb.append("^BE    PT. INDO CIPTA GUNA ^C");
        Date date = new Date();
        sb.append("^SL    TANGGAL JAM   : ").append(new SimpleDateFormat("dd/MM/yyyy").format(date)).append(" ")
                .append(new SimpleDateFormat("HH:mm").format(new Date())).append(" ^C");
        sb.append("^SL    KIOSK ID      : ").append(message.getKioskId()).append(" ^C");
        sb.append("^SL    LOKASI        : ").append(message.getKioskLocation()).append(" ^C");
        sb.append("^SL    NO REF        : ").append(message.getRefNumber()).append(" ^C");
        sb.append("^SL ^C");
        sb.append("^SE    STRUK PEMBAYARAN TAGIHAN ").append(this.message.getProductName().toUpperCase()).append(" ^C");
        sb.append("^SE    ").append(type).append(" ^C");
        sb.append("^SL    NO REKENING   : ").append(this.message.getCustomerId()).append(" ^C");
        sb.append("^SL    BL / TH       : ").append(this.message.getTahunBulanTag1().toUpperCase()).append("-").append(this.message.getTahunBulanTag2().toUpperCase()).append(" ^C");
        sb.append("^SL    NAMA          : ").append(this.message.getAdditional1()).append(" ^C");
        //sb.append("^SL    TGL LUNAS     : ").append(this.message.getAdditional2()).append(" ^C");
        //sb.append("^SL    JPA REF       : ").append(this.message.getVoucherNumber1()).append(" ^C");
        sb.append("^SL    TOTAL TAGIHAN : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(this.message.getAmount(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SE    PDAM MENYATAKAN STRUK INI SEBAGAI ^C");
        sb.append("^SE    BUKTI PEMBAYARAN YANG SAH ^C");
        sb.append("^SL    BIAYA ADMIN   : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getFee(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    TOTAL BAYAR   : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPrice(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");        
        sb.append("^SL    PEMBAYARAN===========================^C");
        sb.append("^SL    UANG TUNAI    : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getCurrentCashNote(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        if (message.getChangeAmount() != null && message.getChangeAmount() != 0D) {
            sb.append("^SL    UANG KEMBALI  : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeCashNote(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }
        if (message.getChangeNotPaid() != null && message.getChangeNotPaid() != 0D) {
            sb.append("^SL    TOTAL DONASI  : Rp. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeNotPaid(),0),' ',17,StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }        
        sb.append("^SL ^C");
        if (message.getStatus().equals("Suspect")) {
            sb.append("^SE    TRANSAKSI SEDANG DIPROSES ^C");
            sb.append("^SE    LAKUKAN CEK STATUS ^C");
        }
        if (advertisementManager.getReceipt() != null) {
            for (Advertisement advertisement : advertisementManager.getReceipt()) {
                sb.append("^SE    ").append(advertisement.getContent()).append(" ^C");
            }
        }
        sb.append("^SL ^C").append("^SL ^C").append("^X");
        boolean printed = printerController.printReceipt(sb.toString(), message.getStan());
    }

    public Resolution cetaku() {
        printReceipt("CETAK ULANG (CU)");
        return new ForwardResolution(EgoWelcome.class);
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
        log.setVoucher(message.getVoucherNumber1());
        log.setPaymentOption(message.getPaymentOption());
        log.setRefNumber(message.getRefNumber());
        return egoManager.saveLog(log);
    }
}
