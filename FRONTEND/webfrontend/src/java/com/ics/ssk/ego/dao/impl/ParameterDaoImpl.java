package com.ics.ssk.ego.dao.impl;

import com.ics.ssk.ego.dao.BaseDaoHibernate;
import com.ics.ssk.ego.dao.ParameterDao;
import com.ics.ssk.ego.model.Parameter;

public class ParameterDaoImpl implements ParameterDao {

    private BaseDaoHibernate baseDaoHibernate;

    public void setBaseDaoHibernate(BaseDaoHibernate baseDaoHibernate) {
        this.baseDaoHibernate = baseDaoHibernate;
    }

    @Override
    public Parameter getParameter(String id) {
        return (Parameter) baseDaoHibernate.getObject(Parameter.class, id);
    }
}
