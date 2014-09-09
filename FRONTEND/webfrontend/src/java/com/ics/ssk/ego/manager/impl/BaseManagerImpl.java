package com.ics.ssk.ego.manager.impl;

import com.ics.ssk.ego.dao.BaseDaoHibernate;
import com.ics.ssk.ego.manager.BaseManager;
import com.ics.ssk.ego.util.PaginatedListImpl;
import com.ics.ssk.ego.util.ParameterDao;
import java.util.List;
import org.displaytag.pagination.PaginatedList;
import org.displaytag.properties.SortOrderEnum;

public class BaseManagerImpl implements BaseManager {

    private BaseDaoHibernate baseDaoHibernate;

    public void setBaseDaoHibernate(BaseDaoHibernate baseDaoHibernate) {
        this.baseDaoHibernate = baseDaoHibernate;
    }

    @Override
    public PaginatedList getList(ParameterDao parameter, PaginatedListImpl paginated, String ascOrder, String descOrder) {
        parameter.setOffside(paginated.getPageNumber());
        if (paginated.getSortCriterion() != null) {
            if (paginated.getSortDirection().equals(SortOrderEnum.ASCENDING)) {
                parameter.setOrders(ParameterDao.ORDER_ASC, paginated.getSortCriterion());
            } else if (paginated.getSortDirection().equals(SortOrderEnum.DESCENDING)) {
                parameter.setOrders(ParameterDao.ORDER_DESC, paginated.getSortCriterion());
            }
        } else {
            if (ascOrder != null) {
                parameter.setOrders(ParameterDao.ORDER_ASC, ascOrder);
            }
            if (descOrder != null) {
                parameter.setOrders(ParameterDao.ORDER_DESC, descOrder);
            }
        }
        Object[] objects = baseDaoHibernate.getListWithSize(parameter);
        paginated.setList((List<?>) objects[1]);
        paginated.setTotalNumberOfRows((Integer) objects[0]);
        paginated.setPageSize(parameter.getMaxRows());
        return paginated;
    }

    @Override
    public PaginatedList getList(ParameterDao parameter, PaginatedListImpl paginated) {
        return getList(parameter, paginated, null, null);
    }

    @SuppressWarnings("rawtypes")
    @Override
    public List getList(ParameterDao parameter) {
        return baseDaoHibernate.getList(parameter);
    }
}
