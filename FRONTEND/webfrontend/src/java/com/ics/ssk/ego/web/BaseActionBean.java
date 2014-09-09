package com.ics.ssk.ego.web;

import com.ics.ssk.ego.device.controller.SmartPayoutController;
import com.ics.ssk.ego.manager.AdvertisementManager;
import com.ics.ssk.ego.manager.SessionManager;
import com.ics.ssk.ego.model.Advertisement;
import com.ics.ssk.ego.system.UserSecurity;
import com.ics.ssk.ego.util.ConstantaUtil;
import com.ics.ssk.ego.util.PaginatedListImpl;
import java.util.Map;
import net.sourceforge.stripes.action.ActionBean;
import net.sourceforge.stripes.action.ActionBeanContext;
import net.sourceforge.stripes.integration.spring.SpringBean;
import net.sourceforge.stripes.util.CryptoUtil;
import org.apache.log4j.Logger;
import org.displaytag.properties.SortOrderEnum;

public abstract class BaseActionBean extends ConstantaUtil implements ActionBean {
    
    private Logger logger = Logger.getLogger(BaseActionBean.class);

    private BaseActionBeanContext ctx;
    private AdvertisementManager advertisementManager;
    private SmartPayoutController smartPayoutController;
    protected SessionManager sessionManager;
    private String keySession;

    public void setKeySession(String keySession) {
        this.keySession = keySession;
    }

    public String getKeySession() {
        return keySession;
    }

    public String getKey() {
        return CryptoUtil.decrypt(keySession);
    }

    @SpringBean("advertisementManager")
    public void setAdvertisingManager(AdvertisementManager advertisementManager) {
        this.advertisementManager = advertisementManager;
    }

    @SpringBean("smartPayoutController")
    public void setSmartPayoutController(SmartPayoutController smartPayoutController) {
        this.smartPayoutController = smartPayoutController;
    }

    @SpringBean("sessionManager")
    public void setSessionManager(SessionManager sessionManager) {
        this.sessionManager = sessionManager;
    }

    @Override
    public BaseActionBeanContext getContext() {
        return this.ctx;
    }

    @Override
    public void setContext(ActionBeanContext ctx) {
        this.ctx = (BaseActionBeanContext) ctx;
    }

    public String getSessionMenu() {
        String menu = (String) getContext().getSession(LINK_SESSION);
        return menu;
    }

    public String getSessionIklan() {
        String strs = "";
        for (Advertisement advertising : advertisementManager.getHeadline()) {
            String str = advertising.getContent().replace("<", "&lt;").replace(">", "&gt;");
            strs = strs + "<li>" + str + "</li>";
        }
        return strs;
    }

    public UserSecurity getSessionUser() {
        UserSecurity user = (UserSecurity) getContext().getSession(USER_SESSION);
        return user;
    }

    public PaginatedListImpl getExtendedPaginated() {
        PaginatedListImpl paginatedList = new PaginatedListImpl();
        String sortCriterion = null;
        String thePage = null;
        if (ctx.getRequest() != null) {
            sortCriterion = ctx.getRequest().getParameter(PaginatedListImpl.IRequestParameters.SORT);
            paginatedList.setSortDirection(PaginatedListImpl.IRequestParameters.DESC.equals(ctx.getRequest().getParameter(PaginatedListImpl.IRequestParameters.DIRECTION)) ? SortOrderEnum.DESCENDING : SortOrderEnum.ASCENDING);
            thePage = ctx.getRequest().getParameter(PaginatedListImpl.IRequestParameters.PAGE);
        }
        paginatedList.setSortCriterion(sortCriterion);
        if (thePage != null) {
            int index = Integer.parseInt(thePage) - 1;
            paginatedList.setIndex(index);
        } else {
            paginatedList.setIndex(0);
        }
        return paginatedList;
    }

    public void dispenseNote(String id, Map<String, Integer> noteMap) {
        Map<String, Integer> cdMap = noteMap;
        if (cdMap != null && !cdMap.isEmpty()) {
            logger.info("starting smart payout to dispense note...");
            boolean start = smartPayoutController.runSSP(id);
            if (start) {
                for (String key : cdMap.keySet()) {
                    int noteAmount = cdMap.get(key);
                    for (int i = 0; i < noteAmount; i++) {
                        boolean dispensed = smartPayoutController.dispenseNote(id, key);                        
                    }
                }
                smartPayoutController.stopSSP(id);
            }
        }
    }
}
