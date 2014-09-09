/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.dao.impl;

import com.ics.ssk.ego.dao.BaseDaoHibernate;
import com.ics.ssk.ego.dao.EgoDao;
import com.ics.ssk.ego.model.EgoMenu;
import com.ics.ssk.ego.model.EgoProduct;
import com.ics.ssk.ego.model.LogEgo;
import com.ics.ssk.ego.util.ParameterDao;
import java.util.List;

/**
 *
 * @author ndung
 */
public class EgoDaoImpl implements EgoDao {

    private BaseDaoHibernate baseDaoHibernate;

    public void setBaseDaoHibernate(BaseDaoHibernate baseDaoHibernate) {
        this.baseDaoHibernate = baseDaoHibernate;
    }

    @Override
    public List<EgoMenu> getEgoMenus(String group) {
        String gr = "00";
        if (group != null) {
            gr = group;
        }
        ParameterDao parameter = new ParameterDao(EgoMenu.class);
        parameter.setEquals(EgoMenu.GROUP, gr);
        parameter.setOrders(ParameterDao.ORDER_ASC, EgoMenu.ID);
        return baseDaoHibernate.getList(parameter);
    }

    @Override
    public boolean saveLog(LogEgo log) {
        return baseDaoHibernate.saveDao(log);
    }

    @Override
    public EgoProduct getProduct(String id) {
        return (EgoProduct) baseDaoHibernate.getObject(EgoProduct.class, id);
    }
}
