/*
 * To change this template, choose Tools | Templates
 * and open the template in the editor.
 */
package com.ics.ssk.ego.dao.impl;

import com.ics.ssk.ego.dao.AdvertisementDao;
import com.ics.ssk.ego.dao.BaseDaoHibernate;
import com.ics.ssk.ego.model.Advertisement;
import com.ics.ssk.ego.util.ParameterDao;
import java.util.List;

/**
 *
 * @author ndung
 */
public class AdvertisementDaoImpl implements AdvertisementDao {

    private BaseDaoHibernate baseDaoHibernate;

    public void setBaseDaoHibernate(BaseDaoHibernate baseDaoHibernate) {
        this.baseDaoHibernate = baseDaoHibernate;
    }

    @SuppressWarnings("unchecked")
    @Override
    public List<Advertisement> getHeadline() {
        ParameterDao parameter = new ParameterDao(Advertisement.class);
        parameter.setEquals(Advertisement.TYPE, Advertisement.TYPE_HEADLINE);
        return baseDaoHibernate.getList(parameter);
    }

    @Override
    public List<Advertisement> getReceipt() {
        ParameterDao parameter = new ParameterDao(Advertisement.class);
        parameter.setEquals(Advertisement.TYPE, Advertisement.TYPE_RECEIPT);
        return baseDaoHibernate.getList(parameter);
    }
}