/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.manager;

import com.ics.ssk.ego.model.EgoMenu;
import com.ics.ssk.ego.model.EgoProduct;
import com.ics.ssk.ego.model.LogEgo;
import java.util.List;

/**
 *
 * @author ndung
 */
public interface EgoManager {

    List<EgoMenu> getEgoMenus(String group);

    boolean saveLog(LogEgo log);
    
    EgoProduct getProduct (String id);
}
