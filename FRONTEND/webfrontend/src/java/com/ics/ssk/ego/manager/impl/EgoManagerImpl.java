/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.manager.impl;

import com.ics.ssk.ego.dao.EgoDao;
import com.ics.ssk.ego.manager.EgoManager;
import com.ics.ssk.ego.model.EgoMenu;
import com.ics.ssk.ego.model.EgoProduct;
import com.ics.ssk.ego.model.LogEgo;
import java.util.List;

/**
 *
 * @author ndung
 */
public class EgoManagerImpl implements EgoManager {

    private EgoDao egoDao;

    public void setEgoDao(EgoDao egoDao) {
        this.egoDao = egoDao;
    }

    @Override
    public List<EgoMenu> getEgoMenus(String group) {
        return egoDao.getEgoMenus(group);
    }

    @Override
    public boolean saveLog(LogEgo log) {
        return egoDao.saveLog(log);
    }

    @Override
    public EgoProduct getProduct(String id) {
        return egoDao.getProduct(id);        
    }
}
