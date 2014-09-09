package com.ics.ssk.ego.web;

import com.ics.ssk.ego.device.controller.PrinterController;
import com.ics.ssk.ego.iso8583.PackagerFactory;
import com.ics.ssk.ego.iso8583.SocketClientProvider;
import com.ics.ssk.ego.iso8583.processor.InquiryPaymentKaiProcessor;
import com.ics.ssk.ego.iso8583.processor.PaymentKaiProcessor;
import com.ics.ssk.ego.iso8583.processor.Processor;
import com.ics.ssk.ego.iso8583.processor.ProcessorKAI;
import com.ics.ssk.ego.manager.AdvertisementManager;
import com.ics.ssk.ego.manager.CounterFactoryManager;
import com.ics.ssk.ego.manager.EgoManager;
import com.ics.ssk.ego.manager.ParameterManager;
import com.ics.ssk.ego.manager.ResponseCodeManager;
import com.ics.ssk.ego.model.Advertisement;
import com.ics.ssk.ego.model.EgoProduct;
import com.ics.ssk.ego.model.Jadwal;
import com.ics.ssk.ego.model.LogEgo;
import com.ics.ssk.ego.model.MessageUtil;
import com.ics.ssk.ego.model.Penumpang;
import com.ics.ssk.ego.model.ResponseCode;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.StringUtil;
import java.text.SimpleDateFormat;
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
import org.jpos.iso.ISOMsg;

@UrlBinding("/egokaipayment.html")
public class KAIPayment extends BaseActionBean {

    public static final String VIEW = "/pages/ego/kai/payment.jsp";
    public static final String VIEW_OK = "/pages/ego/kai/okpayment.jsp";
    
    private Jadwal jadwal;
    private Map<String, Object> maps;
    private String anak;
    private String dewasa;
    private String infant;
    private String asal;
    private String asalName;
    private String tujuan;
    private String tujuanName;
    private MessageUtil message;
    private String id;
    private CounterFactoryManager counterFactoryManager;
    private ParameterManager parameterManager;
    private ResponseCodeManager responseCodeManager;
    private EgoManager egoManager;    
    private AdvertisementManager advertisementManager;
    
    private Map<String, MessageUtil> sessionTable;
    private Map<String, Object[]> messageTable;
    private SocketClientProvider client;
    private PrinterController printerController;

    public void setId(String id) {
        this.id = id;
    }

    public String getId() {
        return id;
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

    @SuppressWarnings("unchecked")
    public List<Penumpang> getPenumpangs() {
        return (List<Penumpang>) maps.get(ProcessorKAI.PENUMPANG);
    }

    public String getBook() {
        return (String) maps.get(ProcessorKAI.CODEBOOK);
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

    public String getAnak() {
        return anak;
    }

    public void setAnak(String anak) {
        this.anak = anak;
    }

    public String getDewasa() {
        return dewasa;
    }

    public double getPriceAdult() {
        return Integer.parseInt(dewasa) * jadwal.getPriceAdult();
    }

    public double getPriceChild() {
        return Integer.parseInt(anak) * jadwal.getPriceChild();
    }

    public double getPriceInfant() {
        return Integer.parseInt(infant) * jadwal.getPriceInfant();
    }

    public double getBiaya() {
        return 7500;
    }

    public double getTotal() {
        return getPriceAdult() + getPriceChild() + getPriceInfant() + getBiaya();
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
            if (getDewasa() == null) {
                setDewasa((String) maps.get(ProcessorKAI.DEWASA));
            }
            if (getAnak() == null) {
                setAnak((String) maps.get(ProcessorKAI.ANAK));
            }
            if (getInfant() == null) {
                setInfant((String) maps.get(ProcessorKAI.BAYI));
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

        }
        if (id != null) {
            message = sessionTable.remove(id);
        }else{
            message = new MessageUtil();
            message.setPrice(getTotal());
            message.setCurrentCashNote(0D);
            message.setStan(StringUtil.addLeadingZeroes(counterFactoryManager.getPaymentNumber(), 6));
            id = message.getStan();
            sessionTable.put(id, message);
            message.setMapsKAI(maps);
            messageTable.put(id, new Object[]{message, this});
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
        }
        return new ForwardResolution(VIEW);
    }

    public Resolution home() {
        return new RedirectResolution(KAIInquiry.class);
    }

    public Resolution exePayment() {
        String kioskId = parameterManager.getParameter("kiosk.id").getValue();
        String kioskLocation = parameterManager.getParameter("kiosk.location").getValue();
        EgoProduct product = egoManager.getProduct("KAI");        
        message.setCustomerId(getBook());
        message.setBillerId(product.getIndustryCode() + product.getBillerCode());
        message.setBillerName(product.getBillerDescription());
        message.setDate(new Date());
        message.setAmount(product.getAmount());
        message.setProductName(product.getProductDescription());
        message.setProductId(product.getProductCode());
        message.setProductCode("KAI");
        message.setKioskId(kioskId);
        message.setKioskLocation(kioskLocation);
        message.setRefNumber(DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD) + message.getStan());
        try {
            Processor processor = new InquiryPaymentKaiProcessor();
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
                String seat = "";
                message.setBit48(bit48);
                message.setBit63(isoMsg.getString(63));
                message.setAmount(Double.parseDouble(isoMsg.getString(4)));
                message.setFee(Double.parseDouble(bit48.substring(187, 195)));
                message.setJumlahTagihan(Integer.parseInt(bit48.substring(207, 208)));
                for (int i = 0; i < message.getJumlahTagihan(); i++) {
                    try {
                        int penjumlah = 91 * i;
                        if (!bit48.substring(295 + penjumlah, 299 + penjumlah).trim().equals("")) {
                            seat += ", " + bit48.substring(285 + penjumlah, 295 + penjumlah).trim() + "/" + bit48.substring(295 + penjumlah, 299 + penjumlah).trim();
                        }
                    } catch (Exception e) {
                        e.printStackTrace();
                    }

                }
                seat = seat.substring(2);

                message.setAdditional1(bit48.substring(60, 68).trim() + " - " + bit48.substring(68, 88).trim());
                message.setAdditional2(bit48.substring(88, 118).trim() + " - " + bit48.substring(118, 148).trim());
                message.setAdditional3(bit48.substring(208, 238).trim());
                message.setAdditional4(bit48.substring(148, 157).trim() + "/" + bit48.substring(166, 168) + ":" + bit48.substring(168, 170));
                message.setAdditional5(bit48.substring(157, 166).trim() + "/" + bit48.substring(170, 172) + ":" + bit48.substring(172, 174));
                message.setAdditional6(seat);

                String clazz = bit48.substring(174, 175);

                if (clazz.equalsIgnoreCase("A") || clazz.equalsIgnoreCase("I") || clazz.equalsIgnoreCase("H") || clazz.equalsIgnoreCase("G")) {
                    clazz = "Eksekutif";
                } else if (clazz.equalsIgnoreCase("B") || clazz.equalsIgnoreCase("N") || clazz.equalsIgnoreCase("M")) {
                    clazz = "Bisnis";
                } else if (clazz.equalsIgnoreCase("C") || clazz.equalsIgnoreCase("U") || clazz.equalsIgnoreCase("S")) {
                    clazz = "Ekonomi";
                } else if (clazz.equalsIgnoreCase("X")) {
                    clazz = "Eksekutif Promo";
                } else if (clazz.equalsIgnoreCase("Y")) {
                    clazz = "Bisnis Promo";
                } else if (clazz.equalsIgnoreCase("Z")) {
                    clazz = "Ekonomi Promo";
                } else {
                    clazz = "Ekonomi";
                }

                message.setVoucherNumber1(clazz);
                message.setPrice(message.getAmount() + message.getFee());
            
                processor = new PaymentKaiProcessor();
                isoMsg = processor.prepareRequest(message);
                isoMsg.setPackager(PackagerFactory.getPackager());
                sessionTable.put(id, message);
                key = isoMsg.getString(11) + isoMsg.getString(7);
                client.transmit(new String(isoMsg.pack()));
                isoMsg = null;
                timeout = System.currentTimeMillis() + Long.parseLong(parameterManager.getParameter("server.timeout").getValue());
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
                        message.setCustomerId(isoMsg.getString(48).substring(0, 15).trim());
                        message.setStatus("Sukses");                        
                        dispenseNote(id, message.getCdMap());
                        printReceipt("CETAK ASLI (CA)");
                    } else if (isoMsg.getString(39).equals("50")) {
                        message.setCustomerId(isoMsg.getString(48).substring(0, 15).trim());
                        message.setStatus("Suspect");
                        message.setPesan("TRX SUSPECT. SILAHKAN MENGHUBUNGI CUSTOMER SERVICE ANDA");
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

                    return new ForwardResolution(VIEW_OK);
                }
            }
        } catch (Exception e) {
            e.printStackTrace();
        }
        return new ForwardResolution(VIEW).addParameter("id", id);
    }
    
    public Resolution cetaku() {
        printReceipt("CETAK ULANG (CU)");        
        return new ForwardResolution(EgoWelcome.class);
    }
    
    @DontValidate
    public Resolution back() {
        return new RedirectResolution(EgoWelcome.class);
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
        sb.append("^SE    STRUK PEMBAYARAN RESERVASI TIKET KAI ^C");
        sb.append("^SE    ").append(type).append(" ^C");
        sb.append("^SL    KODE BOOKING    : ").append(message.getCustomerId()).append(" ^C");
        sb.append("^SL    NAMA KERETA     : ").append(message.getAdditional1()).append(" ^C");
        sb.append("^SL    BERANGKAT       : ").append(message.getAdditional4()).append(" ^C");
        sb.append("^SL    TIBA            : ").append(message.getAdditional5()).append(" ^C");
        sb.append("^SL    RUTE            : ").append(message.getAdditional2()).append(" ^C");
        sb.append("^SL    NAMA            : ").append(message.getAdditional3()).append(" ^C");
        sb.append("^SL    KRT/KURSI       : ").append(message.getAdditional6()).append(" ^C");        
        sb.append("^SL    JML BAYAR       : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getAmount(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");                
        sb.append("^SL    ADMIN BANK      : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getFee(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SL    TOTAL BAYAR     : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getPrice(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        sb.append("^SE    TERIMA KASIH ^C");        
        sb.append("^SL    PEMBAYARAN=============================^C");
        sb.append("^SL    UANG TUNAI      : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getCurrentCashNote(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        if (message.getChangeAmount() != null && message.getChangeAmount() != 0D) {
            sb.append("^SL    UANG KEMBALI    : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeCashNote(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }
        if (message.getChangeNotPaid() != null && message.getChangeNotPaid() != 0D) {
            sb.append("^SL    TOTAL DONASI    : RP. ").append(StringUtil.pad(StringUtil.numberFormat2Decimal(message.getChangeNotPaid(),0),' ', 17, StringUtil.RIGHT_JUSTIFIED)).append(" ^C");
        }
        sb.append("^SL ^C");
        sb.append("^SE    ").append("STRUK INI HARUS DITUKARKAN DG TIKET DI STASIUN").append(" ^C");
        sb.append("^SE    ").append("ONLINE PALING LAMBAT 1 JAM SEBELUM KEBERANGKATAN").append(" ^C");
        sb.append("^SE    ").append("INFO BOOKING DPT DILIHAT DI www.kereta-api.co.id ATAU CC KAI 121").append(" ^C");
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
        log.setVoucher(message.getVoucherNumber1());
        log.setPaymentOption(message.getPaymentOption());
        log.setRefNumber(message.getRefNumber());
        return egoManager.saveLog(log);
    }        
}
