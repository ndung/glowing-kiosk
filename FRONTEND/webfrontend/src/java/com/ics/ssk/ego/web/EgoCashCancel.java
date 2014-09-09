package com.ics.ssk.ego.web;

import com.ics.ssk.ego.device.controller.SmartPayoutController;
import com.ics.ssk.ego.model.MessageUtil;
import java.util.Map;
import java.util.concurrent.Executor;
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

@UrlBinding("/egocashcancel.html")
public class EgoCashCancel extends BaseActionBean {
    
    Logger logger = Logger.getLogger(EgoCashCancel.class);

    private String VIEW1 = "/pages/ego/cashcancel.jsp";
    private MessageUtil message;
    private String id;
    private Map<String, Object[]> messageTable;
    private SmartPayoutController smartPayoutController;
    private Executor executor;

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

    @SpringBean("messageTable")
    public void setMessageTable(Map<String, Object[]> messageTable) {
        this.messageTable = messageTable;
    }

    @SpringBean("smartPayoutController")
    @Override
    public void setSmartPayoutController(SmartPayoutController smartPayoutController) {
        this.smartPayoutController = smartPayoutController;
    }

    @SpringBean("cashCancelExecutor")
    public void setExecutor(Executor executor) {
        this.executor = executor;
    }        

    @Before
    @DontValidate
    public void startup() {
        logger.info("cancel trx id:"+id);
        if (id != null) {
            if (messageTable.containsKey(id)) {
                Object[] obj = messageTable.remove(id);
                if (obj != null) {
                    message = (MessageUtil) obj[0];
                }
            }
        }
    }

    @After
    @DontValidate
    public void endup() {
    }

    @DontValidate
    public Resolution back() {
        return new RedirectResolution(EgoWelcome.class);
    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {        
        Map<String, Integer> cdMap = message.getBaMap();
        if (cdMap != null && !cdMap.isEmpty()) {
            executor.execute(new CancelCash(cdMap));
            return new ForwardResolution(VIEW1).addParameter("id", id);
        }        
        else{
            smartPayoutController.stopSSP(id);
            return new RedirectResolution(EgoWelcome.class);
        }
    }
    
    public class CancelCash implements Runnable {

        private Map<String, Integer> cdMap;
        
        public CancelCash(Map<String, Integer> cdMap){
            this.cdMap = cdMap;
        }
        
        @Override
        public void run() {
            logger.info("starting smart payout to cancel note...");   
            boolean sspRun = smartPayoutController.runSSP(id);            
            if (sspRun) {
                for (String key : cdMap.keySet()) {
                    int noteAmount = cdMap.get(key);
                    for (int i = 0; i < noteAmount; i++) {
                        boolean dispensed = smartPayoutController.dispenseNote(id, key);
                        logger.info("dispensed:"+dispensed);
                    }
                }                
            }
            smartPayoutController.stopSSP(id);            
        }
    }
}
