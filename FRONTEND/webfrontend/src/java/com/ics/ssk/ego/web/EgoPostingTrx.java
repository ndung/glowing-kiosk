/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.web;

import com.ics.ssk.ego.dao.DeviceDao;
import com.ics.ssk.ego.device.controller.SmartPayoutController;
import com.ics.ssk.ego.iso8583.processor.ProcessorKAI;
import com.ics.ssk.ego.model.Jadwal;
import com.ics.ssk.ego.model.MessageUtil;
import com.ics.ssk.ego.model.Penumpang;
import com.ics.ssk.ego.model.SmartPayout;
import java.util.List;
import java.util.Map;
import java.util.TreeMap;
import net.sourceforge.stripes.action.ActionBean;
import net.sourceforge.stripes.action.DefaultHandler;
import net.sourceforge.stripes.action.ForwardResolution;
import net.sourceforge.stripes.action.RedirectResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.action.UrlBinding;
import net.sourceforge.stripes.integration.spring.SpringBean;
import org.apache.log4j.Logger;

/**
 *
 * @author ndung
 */
@UrlBinding("/egopostingtrx.html")
public class EgoPostingTrx extends BaseActionBean {

    Logger logger = Logger.getLogger(EgoPostingTrx.class);
    private Map<String, Object[]> messageTable;
    private Map<String, MessageUtil> sessionTable;
    private SmartPayoutController smartPayoutController;
    private DeviceDao deviceDao;

    @SpringBean("messageTable")
    public void setMessageTable(Map<String, Object[]> messageTable) {
        this.messageTable = messageTable;
    }

    @SpringBean("sessionTable")
    public void setSessionTable(Map<String, MessageUtil> sessionTable) {
        this.sessionTable = sessionTable;
    }

    @SpringBean("smartPayoutController")
    @Override
    public void setSmartPayoutController(SmartPayoutController smartPayoutController) {
        this.smartPayoutController = smartPayoutController;
    }

    @SpringBean("deviceDao")
    public void setDeviceDao(DeviceDao deviceDao) {
        this.deviceDao = deviceDao;
    }
    private MessageUtil message;

    public MessageUtil getMessage() {
        return message;
    }

    public void setMessage(MessageUtil message) {
        this.message = message;
    }
    
    
    public List<Penumpang> getPenumpangs() {
        return (List<Penumpang>) message.getMapsKAI().get(ProcessorKAI.PENUMPANG);
    }

    public String getBook() {
        return (String) message.getMapsKAI().get(ProcessorKAI.CODEBOOK);
    }

    public String getAsal() {
        return (String) message.getMapsKAI().get(ProcessorKAI.ORIGINAL);
    }

    public String getAsalName() {
        return (String) message.getMapsKAI().get(ProcessorKAI.ORIGINAL_NAME);
    }

    public String getTujuan() {
        return (String) message.getMapsKAI().get(ProcessorKAI.DESTINATION);
    }

    public String getTujuanName() {
        return (String) message.getMapsKAI().get(ProcessorKAI.DESTINATION_NAME);
    }

    public String getAnak() {
        return (String) message.getMapsKAI().get(ProcessorKAI.ANAK);
    }

    public String getDewasa() {
        return (String) message.getMapsKAI().get(ProcessorKAI.DEWASA);
    }
    
    public String getInfant() {
        return (String) message.getMapsKAI().get(ProcessorKAI.BAYI);
    }

    public double getPriceAdult() {
        return Integer.parseInt(getDewasa()) * getJadwal().getPriceAdult();
    }

    public double getPriceChild() {
        return Integer.parseInt(getAnak()) * getJadwal().getPriceChild();
    }

    public double getPriceInfant() {
        return Integer.parseInt(getInfant()) * getJadwal().getPriceInfant();
    }

    public double getBiaya() {
        return 7500;
    }

    public double getTotal() {
        return getPriceAdult() + getPriceChild() + getPriceInfant() + getBiaya();
    }

    public Jadwal getJadwal() {
        return (Jadwal) message.getMapsKAI().get("PILIH_KAI");
    }
    

    @DefaultHandler
    public Resolution view() {
        String sid = getContext().getRequest().getParameter("id");
        if (messageTable.containsKey(sid)) {
            Object[] obj = messageTable.remove(sid);
            if (obj != null) {
                message = (MessageUtil) obj[0];
                if (message.getPaymentOption().equals("CASH")) {
                    if (message.getCurrentCashNote() > message.getPrice()) {
                        List<SmartPayout> list = deviceDao.getNonZeroSmartPayoutInventory("01");
                        Double changeAmount = message.getCurrentCashNote() - message.getPrice();
                        message.setChangeAmount(changeAmount);
                        Double paidChangeAmount = 0D;
                        Double notPaidChangeAmount = 0D;
                        if (changeAmount != 0) {
                            Map<String, Integer> cdMap = new TreeMap<String, Integer>();
                            for (int i = 0; i < list.size(); i++) {
                                SmartPayout sp = list.get(i);
                                if (cdMap.isEmpty()) {
                                    int n = (int) (changeAmount / sp.getDenom());
                                    if (n > sp.getCurrentPayoutNote()) {
                                        n = sp.getCurrentPayoutNote();
                                    }
                                    if (n > 0) {
                                        cdMap.put(String.valueOf(sp.getDenom()), n);
                                    }
                                } else {
                                    Double ca = changeAmount;
                                    for (String key : cdMap.keySet()) {
                                        ca = ca - (Integer.parseInt(key) * cdMap.get(key));
                                    }
                                    int n = (int) (ca / sp.getDenom());
                                    if (n > sp.getCurrentPayoutNote()) {
                                        n = sp.getCurrentPayoutNote();
                                    }
                                    if (n > 0) {
                                        cdMap.put(String.valueOf(sp.getDenom()), n);
                                    }
                                }
                            }

                            for (String key : cdMap.keySet()) {
                                paidChangeAmount = paidChangeAmount + (Integer.parseInt(key) * cdMap.get(key));
                            }
                            notPaidChangeAmount = changeAmount - paidChangeAmount;
                            message.setCdMap(cdMap);
                            message.setChangeCashNote(paidChangeAmount);
                            message.setChangeNotPaid(notPaidChangeAmount);
                            if (paidChangeAmount == 0) {
                                smartPayoutController.stopSSP(sid);
                            }
                        }
                    } else {
                        smartPayoutController.stopSSP(sid);
                        message.setChangeAmount(0D);
                        message.setChangeCashNote(0D);
                        message.setChangeNotPaid(0D);
                    }
                }
                ActionBean bean = (ActionBean) obj[1];
                if (bean instanceof EgoPurchaseTelco) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(EgoPurchaseTelco.VIEW2).addParameter("id", sid);
                } else if (bean instanceof EgoPurchasePln) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(EgoPurchasePln.VIEW2).addParameter("id", sid);
                } else if (bean instanceof EgoPaymentPlnPostpaid) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(EgoPaymentPlnPostpaid.VIEW2).addParameter("id", sid);
                } else if (bean instanceof EgoPaymentPlnNontaglis) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(EgoPaymentPlnNontaglis.VIEW2).addParameter("id", sid);
                } else if (bean instanceof EgoPaymentTelco) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(EgoPaymentTelco.VIEW2).addParameter("id", sid);
                } else if (bean instanceof EgoPaymentPdam) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(EgoPaymentPdam.VIEW2).addParameter("id", sid);
                } else if (bean instanceof EgoPurchaseGame) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(EgoPurchaseGame.VIEW2).addParameter("id", sid);
                } else if (bean instanceof KAIPayment) {
                    sessionTable.put(sid, message);
                    return new ForwardResolution(KAIPayment.VIEW).addParameter("id", sid);
                }
            }
        }

        return new RedirectResolution(EgoWelcome.class);
    }
}
