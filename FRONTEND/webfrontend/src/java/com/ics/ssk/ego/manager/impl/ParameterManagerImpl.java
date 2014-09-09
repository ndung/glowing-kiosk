package com.ics.ssk.ego.manager.impl;

import com.ics.ssk.ego.dao.ParameterDao;
import com.ics.ssk.ego.manager.ParameterManager;
import com.ics.ssk.ego.model.Parameter;

public class ParameterManagerImpl implements ParameterManager {

    private ParameterDao parameterDao;

    public void setParameterDao(ParameterDao parameterDao) {
        this.parameterDao = parameterDao;
    }

    @Override
    public Parameter getParameter(String id) {
        return parameterDao.getParameter(id);
    }
}
