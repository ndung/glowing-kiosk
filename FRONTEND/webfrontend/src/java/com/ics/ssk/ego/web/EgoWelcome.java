package com.ics.ssk.ego.web;

import com.ics.ssk.ego.manager.EgoManager;
import com.ics.ssk.ego.model.EgoMenu;
import java.util.List;
import net.sourceforge.stripes.action.DefaultHandler;
import net.sourceforge.stripes.action.DontValidate;
import net.sourceforge.stripes.action.ForwardResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.action.UrlBinding;
import net.sourceforge.stripes.integration.spring.SpringBean;

@UrlBinding("/ego.html")
public class EgoWelcome extends BaseActionBean {

    private String VIEW = "/pages/ego/welcome.jsp";
    private EgoManager egoManager;
    private String group;

    public void setGroup(String group) {
        this.group = group;
    }

    public String getGroup() {
        return group;
    }

    public String getBack() {
        String id = null;        
        if (group != null) {
            String[] groups = group.split(",");
            if (groups.length > 1) {
                id = groups[groups.length - 2];
            }
            else{
                id = String.valueOf(Integer.parseInt(group)-1);
                if (id.equals("-1")){
                    return null;
                }else{
//                    if (id.length()<2){
//                        id = "0"+id;
//                    }     
                    return "00";
                }                
            }
        }      
        return id;
    }

    @SpringBean("egoManager")
    public void setEgoManager(EgoManager egoManager) {
        this.egoManager = egoManager;
    }

    public List<EgoMenu> getList() {
        String id = null;
        if (group != null) {
            String[] groups = group.split(",");
            if (groups.length > 0) {
                id = groups[groups.length - 1];
            }
        }
        return egoManager.getEgoMenus(id);
    }

    @DontValidate
    @DefaultHandler
    public Resolution view() {
        return new ForwardResolution(VIEW);
    }
}
