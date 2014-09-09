package com.ics.ssk.ego.dao.impl;

import com.ics.ssk.ego.dao.BaseDaoHibernate;
import com.ics.ssk.ego.util.ParameterDao;
import java.io.Serializable;
import java.util.List;
import java.util.Map;
import org.apache.log4j.Logger;
import org.hibernate.criterion.Criterion;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.ProjectionList;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;

public class BaseDaoHibernateImpl extends HibernateDaoSupport implements BaseDaoHibernate {

    Logger log = Logger.getLogger(this.getClass());

    @Override
    public boolean deleteDao(Object dao) {
        try {
            getHibernateTemplate().delete(dao);
            return true;
        } catch (Exception e) {
            log.error(e);
            return false;
        }
    }

    @Override
    public boolean saveDao(Object dao) {
        try {
            getHibernateTemplate().save(dao);
            return true;
        } catch (Exception e) {
            log.error(e);
            return false;
        }
    }

    @Override
    public boolean saveOrUpdate(Object dao) {
        try {
            getHibernateTemplate().saveOrUpdate(dao);
            return true;
        } catch (Exception e) {
            log.error(e);
            return false;
        }
    }

    @Override
    public boolean updateDao(Object dao) {
        try {
            getHibernateTemplate().update(dao);
            return true;
        } catch (Exception e) {
            log.error(e);
            return false;
        }
    }

    @SuppressWarnings("rawtypes")
    @Override
    public List getList(Class clazz) {
        try {
            DetachedCriteria criteria = DetachedCriteria.forClass(clazz);
            return getHibernateTemplate().findByCriteria(criteria);
        } catch (Exception e) {
            log.error(e);
            return null;
        }

    }

    @SuppressWarnings("rawtypes")
    @Override
    public Object getObject(Class clazz, Serializable id) {
        try {
            return getHibernateTemplate().get(clazz, id);
        } catch (Exception e) {
            log.error(e);
            return null;
        }

    }

    @SuppressWarnings("rawtypes")
    @Override
    public List getList(ParameterDao parameter) {
        return (List) getListWithSize(parameter)[1];
    }

    @Override
    public Object[] getListWithSize(ParameterDao parameter) {
        Object[] objects = getList(parameter.getClazz(),
                parameter.getEquals(),
                parameter.getOrs(),
                parameter.getLikes(),
                parameter.getNotEquals(),
                parameter.getIn(),
                parameter.getNotIn(),
                parameter.getBetween(),
                parameter.getGratherThan(),
                parameter.getLessThan(),
                parameter.getGratherEqualsThan(),
                parameter.getLessEqualsThan(),
                parameter.getOrders(),
                parameter.getIsNull(),
                parameter.getIsNotNull(),
                parameter.getOffside(),
                parameter.getMaxRows());

        parameter = null;
        return objects;
    }

    @SuppressWarnings("rawtypes")
    private Object[] getList(Class clazz, Map<String, Object> equals, Map<String, List> ors, Map<String, Object> likes, Map<String, Object> notEquals, Map<String, List> in, Map<String, List> notIn, Map<String, Object[]> between, Map<String, Object> gratherThan, Map<String, Object> lessThan, Map<String, Object> gratherEqualsThan, Map<String, Object> lessEqualsThan, List<String[]> orders, List<String> isNull, List<String> isNotNull, int offside, int maxRows) {

        Object[] objects = new Object[2];
        DetachedCriteria criteria = DetachedCriteria.forClass(clazz);
        DetachedCriteria criteria2 = DetachedCriteria.forClass(clazz);

        if (isNull != null) {
            for (String value : isNull) {
                criteria.add(Restrictions.isNull(value));
                criteria2.add(Restrictions.isNull(value));
            }
        }

        if (isNotNull != null) {
            for (String value : isNotNull) {
                criteria.add(Restrictions.isNotNull(value));
                criteria2.add(Restrictions.isNotNull(value));
            }
        }

        if (equals != null) {
            for (String equal : equals.keySet()) {
                criteria.add(Restrictions.eq(equal, equals.get(equal)));
                criteria2.add(Restrictions.eq(equal, equals.get(equal)));
            }
        }

        if (likes != null) {
            for (String like : likes.keySet()) {
                criteria.add(Restrictions.like(like, likes.get(like)));
                criteria2.add(Restrictions.like(like, likes.get(like)));
            }
        }

        if (notEquals != null) {
            for (String notEqual : notEquals.keySet()) {
                criteria.add(Restrictions.not(Restrictions.eq(notEqual, notEquals.get(notEqual))));
                criteria2.add(Restrictions.not(Restrictions.eq(notEqual, notEquals.get(notEqual))));
            }
        }

        if (in != null && in.size() > 0) {
            for (String inValue : in.keySet()) {
                criteria.add(Restrictions.in(inValue, in.get(inValue)));
                criteria2.add(Restrictions.in(inValue, in.get(inValue)));
            }
        }

        if (notIn != null && notIn.size() > 0) {
            for (String notInValue : notIn.keySet()) {
                criteria.add(Restrictions.not(Restrictions.in(notInValue, notIn.get(notInValue))));
                criteria2.add(Restrictions.not(Restrictions.in(notInValue, notIn.get(notInValue))));
            }
        }

        if (between != null && between.size() > 0) {
            for (String betweenValue : between.keySet()) {
                criteria.add(Restrictions.between(betweenValue, between.get(betweenValue)[0], between.get(betweenValue)[1]));
                criteria2.add(Restrictions.between(betweenValue, between.get(betweenValue)[0], between.get(betweenValue)[1]));
            }
        }

        if (gratherThan != null) {
            for (String gt : gratherThan.keySet()) {
                criteria.add(Restrictions.gt(gt, gratherThan.get(gt)));
                criteria2.add(Restrictions.gt(gt, gratherThan.get(gt)));
            }
        }

        if (lessThan != null) {
            for (String lt : lessThan.keySet()) {
                criteria.add(Restrictions.lt(lt, lessThan.get(lt)));
                criteria2.add(Restrictions.lt(lt, lessThan.get(lt)));
            }
        }

        if (gratherEqualsThan != null) {
            for (String gt : gratherEqualsThan.keySet()) {
                criteria.add(Restrictions.ge(gt, gratherEqualsThan.get(gt)));
                criteria2.add(Restrictions.ge(gt, gratherEqualsThan.get(gt)));
            }
        }

        if (lessEqualsThan != null) {
            for (String lt : lessEqualsThan.keySet()) {
                criteria.add(Restrictions.le(lt, lessEqualsThan.get(lt)));
                criteria2.add(Restrictions.le(lt, lessEqualsThan.get(lt)));
            }
        }

        if (ors != null && ors.size() > 0) {
            for (String orValue : ors.keySet()) {
                Criterion exp1 = null;
                Criterion exp2;
                for (Object object : ors.get(orValue)) {
                    exp2 = Restrictions.eq(orValue, object);
                    if (exp1 == null) {
                        exp1 = exp2;
                    } else {
                        exp1 = Restrictions.or(exp2, exp1);
                    }
                }
                criteria.add(exp1);
                criteria2.add(exp1);
            }
        }

        if (orders != null && orders.size() > 0) {
            for (String[] order : orders) {
                if (order[0].equals(ParameterDao.ORDER_ASC)) {
                    criteria.addOrder(Order.asc(order[1]));
                } else if (order[0].equals(ParameterDao.ORDER_DESC)) {
                    criteria.addOrder(Order.desc(order[1]));
                }
            }
        }

        ProjectionList projectionList = Projections.projectionList();
        projectionList.add(Projections.rowCount());

        criteria2.setProjection(projectionList);

        int size = (Integer) getHibernateTemplate().findByCriteria(criteria2).get(0);
        objects[0] = size;

        if (offside > 0 && maxRows > 0) {
            int start = maxRows * (offside - 1);
            int end = start + maxRows;
            if (start > size) {
                start = size;
            }
            if (end > size) {
                end = size;
            }
            objects[1] = getHibernateTemplate().findByCriteria(criteria, start, maxRows);
        } else {
            objects[1] = getHibernateTemplate().findByCriteria(criteria);
        }

        equals = null;
        ors = null;
        likes = null;
        notEquals = null;
        in = null;
        notIn = null;
        between = null;
        gratherThan = null;
        lessThan = null;
        gratherEqualsThan = null;
        lessEqualsThan = null;
        orders = null;
        isNull = null;
        isNotNull = null;

        return objects;
    }
}
