/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.web;

import com.ics.ssk.ego.converter.NumberTypeConverter;
import com.ics.ssk.ego.device.controller.PrinterController;
import com.ics.ssk.ego.iso8583.PackagerFactory;
import com.ics.ssk.ego.iso8583.SocketClientProvider;
import com.ics.ssk.ego.iso8583.processor.InquiryPaymentPlnProcessor;
import com.ics.ssk.ego.iso8583.processor.PaymentPlnProcessor;
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
import org.jpos.iso.ISOMsg;

@UrlBinding("/egopaymentplnpostpaid.html")
public class EgoPaymentPlnPostpaid extends BaseActionBean {

    public static final String VIEW1 = "/pages/ego/pln_postpaid/inqpayment.jsp";
    public static final String VIEW2 = "/pages/ego/pln_postpaid/infopayment.jsp";
    public static final String VIEW3 = "/pages/ego/pln_postpaid/okpayment.jsp";
    private ParameterManager parameterManager;
    private ResponseCodeManager responseCodeManager;
    private EgoManager egoManager;
    private CounterFactoryManager counterFactoryManager;
    private AdvertisementManager advertisementManager;
    @Validate(required = true, converter = NumberTypeConverter.class, on = "inquiry")
    private String customerId;
    private MessageUtil message;
    private String id;
    private Map<String, MessageUtil> sessionTable;
    private Map<String, Object[]> messageTable;
    private SocketClientProvider client;
    private PrinterController printerController;

    public String getProduct() {
        return "PLNPOST";
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

    public String getCustomerId() {
        return customerId;
    }

    public void setCustomerId(String customerId) {
        this.customerId = customerId;
    }

    @SpringBean("counterFactoryManager")
    public void setCounterFactoryManager(CounterFactoryManager counterFactoryManager) {
        this.counterFactoryManager = counterFactoryManager;
    }

    @SpringBean("egoManager")
    public void setEgoManager(EgoManager egoManager) {
        this.egoManager = egoManager;
    }

    @SpringBean("advertisementManager")
    public void setAdvertisementManager(AdvertisementManager advertisementManager) {
        this.advertisementManager = advertisementManager;
    }

    @SpringBean("responseCodeManager")
    public void setResponseCodeManager(ResponseCodeManager responseCodeManager) {
        this.responseCodeManager = responseCodeManager;
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
    public Resolution back() {
        return new RedirectResolution(EgoWelcome.class);
    }
    
    @DontValidate
    public Resolution cancel() {
        messageTable.put(id, new Object[]{message, null});
        return new RedirectResolution(EgoCashCancel.class).addParameter("id", id);
    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {
        return new ForwardResolution(VIEW1);
    }

    public Resolution inquiry() {
        String kioskId = parameterManager.getParameter("kiosk.id").getValue();
        String kioskLocation = parameterManager.getParameter("kiosk.location").getValue();
        EgoProduct product = egoManager.getProduct("PLNPOST");

        message = new MessageUtil();

        message.setCustomerId(this.getCustomerId());
        message.setBillerId(product.getIndustryCode() + product.getBillerCode());
        message.setDate(new Date());
        message.setAmount(product.getAmount());
        message.setProductName(product.getProductDescription());
        message.setProductId(product.getProductCode());
        message.setProductCode(product.getId());
        message.setKioskId(kioskId);
        message.setKioskLocation(kioskLocation);
        message.setStan(StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6));
        message.setRefNumber(DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD) + message.getStan());

        Processor processor = new InquiryPaymentPlnProcessor();

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
                    String error = respCode.getDescription().toUpperCase();
                    error = error.replaceAll("#DATETIME#", DateTimeUtil.convertDateToStringCustomized(new Date(), "MMM yyyy").toUpperCase());
                    getContext().getMessages().add(new SimpleError(error));
                } else {
                    getContext().getMessages().add(new SimpleError("Unknown Error".toUpperCase()));
                }
            } else {
                String bit48 = isoMsg.getString(48);
                double pinalty = 0;
//                double total = 0;
                String meter1 = "";
                String meter2 = "";
                String bultag = "";
                String bulv1 = "";
                String name = bit48.substring(47, 72).trim();

                message.setAdditional5(bit48.substring(77, 92).trim());
                message.setBit48(bit48);
                message.setSuffix(name);
                message.setVoucherNumber1("");
                message.setJumlahTagihan(Integer.parseInt(bit48.substring(12, 13)));
                message.setAdditional6(StringUtil.addLeadingZeroes(message.getJumlahTagihan(), 2));
                message.setJumlahTunggakan(Integer.parseInt(bit48.substring(13, 15)));
                message.setAdditional2(bit48.substring(15, 47).trim());
                message.setAdditional1(StringUtil.addHiddenValue(name.substring(0, 3), name.length()));
                message.setAdditional3(bit48.substring(92, 96).trim() + "/" + Integer.parseInt(bit48.substring(96, 105).trim()));
                message.setFee(Double.parseDouble(bit48.substring(105, 114)));
                //message.setPinalty(Double.parseDouble(bit48.substring(215,224)));

                if (message.getJumlahTagihan() >= 1) {
                    bultag = DateTimeUtil.convertStringToStringFormaterCustomized(bit48.substring(114, 120) + "01", DateTimeUtil.YYYYMMDD, "MMMyy").toUpperCase();
                    bulv1 = bit48.substring(114, 120);

                    meter1 = StringUtil.addLeadingZeroes(bit48.substring(177, 185), 9);
                    meter2 = StringUtil.addLeadingZeroes(bit48.substring(185, 193), 9);
                    pinalty = pinalty + Double.parseDouble(bit48.substring(168, 177));
//                    total = total + Double.parseDouble(bit48.substring(136, 147));
                }
                if (message.getJumlahTagihan() >= 2) {
                    bultag += "," + DateTimeUtil.convertStringToStringFormaterCustomized(bit48.substring(225, 231) + "01", DateTimeUtil.YYYYMMDD, "MMMyy").toUpperCase();
                    bulv1 += "," + bit48.substring(225, 231);

                    meter2 = StringUtil.addLeadingZeroes(bit48.substring(296, 304), 9);
                    pinalty = pinalty + Double.parseDouble(bit48.substring(279, 288));
//                    total = total + Double.parseDouble(bit48.substring(247, 258));
                }
                if (message.getJumlahTagihan() >= 3) {
                    bultag += "," + DateTimeUtil.convertStringToStringFormaterCustomized(bit48.substring(336, 342) + "01", DateTimeUtil.YYYYMMDD, "MMMyy").toUpperCase();
                    bulv1 += "," + bit48.substring(336, 342);

                    meter2 = StringUtil.addLeadingZeroes(bit48.substring(407, 415), 9);
                    pinalty = pinalty + Double.parseDouble(bit48.substring(390, 399));
//                    total = total + Double.parseDouble(bit48.substring(358, 369));
                }
                if (message.getJumlahTagihan() == 4) {
                    bultag += "," + DateTimeUtil.convertStringToStringFormaterCustomized(bit48.substring(447, 453) + "01", DateTimeUtil.YYYYMMDD, "MMMyy").toUpperCase();
                    bulv1 += "," + bit48.substring(447, 453);

                    meter2 = StringUtil.addLeadingZeroes(bit48.substring(518, 526), 9);
                    pinalty = pinalty + Double.parseDouble(bit48.substring(501, 510));
//                    total = total + Double.parseDouble(bit48.substring(469, 480));
                }
                message.setPinalty(pinalty);
                message.setTahunBulanTag1(bulv1);
                message.setTahunBulanTag2(bultag);
                message.setAdditional4(StringUtil.addLeadingZeroes(Integer.parseInt(meter1), 8) + "-" + StringUtil.addLeadingZeroes(Integer.parseInt(meter2), 8));
                message.setAmount(Double.parseDouble(isoMsg.getString(4)));
                message.setPrice(message.getAmount() + message.getFee());
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
    
    public Resolution cetaku() {
        printReceipt("CETAK ULANG (CU)");        
        return new ForwardResolution(EgoWelcome.class);
    }

    public Resolution exePayment() {
        Processor processor = new PaymentPlnProcessor();

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
                    message.setPesan(isoMsg.getString(62).trim().toUpperCase());
                    message.setVoucherNumber1(isoMsg.getString(48).substring(48, 80).trim());
                    dispenseNote(id, message.getCdMap());
                    printReceipt("CETAK ASLI (CA)");
                } else if (isoMsg.getString(39).equals("50")) {
                    message.setVoucherNumber2("TRANSAKSI SUSPECT");
                    message.setPesan("TRANSAKSI SUSPECT");
                    message.setStatus("Suspect");
                    dispenseNote(id, message.getCdMap());
                    printReceipt("CETAK ASLI (CA)");
                } else {
                    String error = "UNKNOWN ERROR";
                    ResponseCode respCode = responseCodeManager.getRespCode(isoMsg.getString(39));
                    if (respCode != null) {
                        error = respCode.getDescription().toUpperCase();
                    }
                    error = error.replaceAll("#DATETIME#", DateTimeUtil.convertDateToStringCustomized(new Date(), "MMM yyyy").toUpperCase());
                    message.setPesan(error);
                    message.setStatus("Gagal");
                    dispenseNote(id, message.getBaMap());
                }

                message.setStep(0);
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

    private boolean printReceipt(String type){
        StringBuilder sb = new StringBuilder();
        sb.append("^BE    PT. INDO CIPTA GUNA ^C");
        Date date = new Date();
        sb.append("^SL    TANGGAL JAM     : ").append(new SimpleDateFormat("dd/MM/yyyy").format(date)).append(" ")
                .append(new SimpleDateFormat("HH:mm").format(new Date())).append(" ^C");                
        sb.append("^SL    KIOSK ID        : ").append(message.getKioskId()).append(" ^C");
        sb.append("^SL    LOKASI          : ").append(message.getKioskLocation()).append(" ^C");
        sb.append("^SL    NO REF          : ").append(message.getRefNumber()).append(" ^C");
        sb.append("^SL ^C");
        sb.append("^SE    STRUK PEMBAYARAN TAGIHAN LISTRIK ^C");
        sb.append("^SE    ").append(type).append(" ^C");
        sb.append("^SL    IDPEL           : ").append(message.getCustomerId()).append(" ^C");
        sb.append("^SL    NAMA            : ").append(message.getSuffix()).append(" ^C");
        sb.append("^SL    TARIF/DAYA      : ").append(message.getAdditional3()).append("VA").append(" ^C");
        sb.append("^SL    BL/TH           : ").append(message.getTahunBulanTag2()).append(" ^C");
        sb.append("^SL    STAND METER     : ").append(message.getAdditional4()).append(" ^C");
        sb.append("^SL    RP TAG PLN      : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getAmount(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    JPA REF         : ").append(message.getVoucherNumber1()).append(" ^C");
        sb.append("^SE    PLN MENYATAKAN STRUK INI SEBAGAI ^C");
        sb.append("^SE    BUKTI PEMBAYARAN YANG SAH ^C");
        sb.append("^SL    ADMIN BANK      : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getFee(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    TOTAL BAYAR     : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPrice(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SE    TERIMA KASIH ^C");
        if (message.getJumlahTunggakan() > 0) {
            sb.append("^SE    ANDA MASIH MEMILIKI SISA TUNGGAKAN ").append(message.getJumlahTunggakan()).append(" BLN").append(" ^C");
        }        
        sb.append("^SL    PEMBAYARAN=============================^C");
        sb.append("^SL    UANG TUNAI      : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getCurrentCashNote(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        if (message.getChangeAmount() != null && message.getChangeAmount() != 0D) {
            sb.append("^SL    UANG KEMBALI    : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeCashNote(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }
        if (message.getChangeNotPaid() != null && message.getChangeNotPaid() != 0D) {
            sb.append("^SL    TOTAL DONASI    : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeNotPaid(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }
        sb.append("^SL ^C");
        sb.append("^SE    ").append(message.getPesan().toUpperCase()).append(message.getAdditional5()).append(" ^C");

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
        return printerController.printReceipt(sb.toString(), message.getStan());
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
