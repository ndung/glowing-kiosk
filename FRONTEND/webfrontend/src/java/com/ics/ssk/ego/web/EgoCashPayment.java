package com.ics.ssk.ego.web;

import com.ics.ssk.ego.device.controller.SmartPayoutController;
import com.ics.ssk.ego.model.MessageUtil;
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
import net.sourceforge.stripes.integration.spring.SpringBean;
import org.apache.log4j.Logger;

@UrlBinding("/egocashpayment.html")
public class EgoCashPayment extends BaseActionBean {
    
    Logger logger = Logger.getLogger(EgoCashPayment.class);

    private String VIEW1 = "/pages/ego/cashpayment.jsp";
    private String VIEW2 = "/pages/ego/cashcancel.jsp";
    private MessageUtil message;
    private String id;    
    private Map<String, Object[]> messageTable;
    private SmartPayoutController smartPayoutController;       
    private Boolean sspRun;
    
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

    public Boolean getSspRun() {
        return sspRun;
    }

    public void setSspRun(Boolean sspRun) {
        this.sspRun = sspRun;
    }        

    @SpringBean("messageTable")
    public void setMessageTable(Map<String, Object[]> messageTable) {
        this.messageTable = messageTable;
    }

    @SpringBean("smartPayoutController")
    @Override
    public void setSmartPayoutController(SmartPayoutController smartPayoutController) {
        this.smartPayoutController = smartPayoutController;
    }

    @Before
    @DontValidate
    public void startup() {
        System.out.println("id:"+id);
        if (id != null) {
            if (messageTable.containsKey(id)) {
                Object[] obj = messageTable.get(id);
                if (obj != null) {
                    message = (MessageUtil) obj[0];
                    message.setPaymentOption("CASH");
                    logger.info("starting smart payout to accept note...");
                    sspRun = smartPayoutController.runSSP(id);                     
                    if (sspRun){
                        if (message.getLoop() == null){
                            message.setLoop(0);
                            messageTable.put(id, new Object[]{message, obj[1]});
                        }else{
                            if (message.getCurrentCashNote() != null) {
                                if (message.getCurrentCashNote() < message.getPrice()) {
                                    acceptNote();                            
                                    messageTable.put(id, new Object[]{message, obj[1]});
                                }                        
                            }
                            message.setLoop(message.getLoop()+1);
                        }
                    }
                }
            }
        }
    }

    @After
    @DontValidate
    public void endup() {        
    }

    @DontValidate
    public Resolution cancel() {
        return new ForwardResolution(VIEW2).addParameter("id", id);
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

    public void acceptNote() {
        String acceptedNote = smartPayoutController.acceptNote(id);
        logger.info("acceptedNote :"+id+";"+acceptedNote);        
        if (acceptedNote != null) {
            Double currentCashNote = Double.parseDouble(acceptedNote);
            message.setCurrentCashNote(message.getCurrentCashNote() + currentCashNote);
            Map<String, Integer> baMap = (Map<String, Integer>) message.getBaMap();
            if (baMap==null){
                baMap = new TreeMap<String, Integer>();
            }
            if (baMap.get(acceptedNote) != null) {
                int n = baMap.get(acceptedNote);
                baMap.put(acceptedNote, n + 1);
            } else {
                baMap.put(acceptedNote, 1);
            }            
            message.setBaMap(baMap);
        } else {
        }
    }
    
}
