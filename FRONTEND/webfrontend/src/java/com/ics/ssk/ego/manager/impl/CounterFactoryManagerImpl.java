package com.ics.ssk.ego.manager.impl;

import com.ics.ssk.ego.dao.CounterFactoryDao;
import com.ics.ssk.ego.manager.CounterFactoryManager;
import com.ics.ssk.ego.model.CounterFactory;

public class CounterFactoryManagerImpl implements CounterFactoryManager {

    private CounterFactoryDao counterFactoryDao;

    public void setCounterFactoryDao(CounterFactoryDao counterFactoryDao) {
        this.counterFactoryDao = counterFactoryDao;
    }

    @Override
    public synchronized Integer getPaymentNumber() {
        CounterFactory counter = counterFactoryDao.getCounterFactory(CounterFactory.PAYMENT);
        counter.setValue(counter.getValue() + 1);
        counterFactoryDao.getUpdateCounter(counter);
        return counter.getValue();
    }
}
