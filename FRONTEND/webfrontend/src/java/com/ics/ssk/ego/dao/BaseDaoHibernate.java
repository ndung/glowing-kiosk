package com.ics.ssk.ego.dao;

import com.ics.ssk.ego.util.ParameterDao;
import java.io.Serializable;
import java.util.List;

@SuppressWarnings("rawtypes")
public interface BaseDaoHibernate {

    boolean saveDao(Object dao);

    boolean updateDao(Object dao);

    boolean deleteDao(Object dao);

    boolean saveOrUpdate(Object dao);

    List getList(Class clazz);

    Object[] getListWithSize(ParameterDao parameter);

    List getList(ParameterDao parameter);

    Object getObject(Class clazz, Serializable id);
}
