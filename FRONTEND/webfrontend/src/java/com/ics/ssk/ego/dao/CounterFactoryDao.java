package com.ics.ssk.ego.dao;

import com.ics.ssk.ego.model.CounterFactory;

public interface CounterFactoryDao {

    boolean getAddCounter(CounterFactory counterFactory);

    boolean getUpdateCounter(CounterFactory counterFactory);

    CounterFactory getCounterFactory(String id);
}
