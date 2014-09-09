package com.ics.ssk.ego.dao.impl;

import com.ics.ssk.ego.dao.BaseDaoHibernate;
import com.ics.ssk.ego.dao.CounterFactoryDao;
import com.ics.ssk.ego.model.CounterFactory;

public class CounterFactoryDaoImpl implements CounterFactoryDao {

    private BaseDaoHibernate baseDaoHibernate;

    public void setBaseDaoHibernate(BaseDaoHibernate baseDaoHibernate) {
        this.baseDaoHibernate = baseDaoHibernate;
    }

    @Override
    public boolean getAddCounter(CounterFactory counterFactory) {
        return baseDaoHibernate.saveDao(counterFactory);
    }

    @Override
    public boolean getUpdateCounter(CounterFactory counterFactory) {
        return baseDaoHibernate.updateDao(counterFactory);
    }

    @Override
    public CounterFactory getCounterFactory(String id) {
        return (CounterFactory) baseDaoHibernate.getObject(CounterFactory.class, id);
    }
}
