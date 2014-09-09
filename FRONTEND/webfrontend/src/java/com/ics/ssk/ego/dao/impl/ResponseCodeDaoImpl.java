package com.ics.ssk.ego.dao.impl;

import com.ics.ssk.ego.dao.BaseDaoHibernate;
import com.ics.ssk.ego.dao.ResponseCodeDao;
import com.ics.ssk.ego.model.ResponseCode;
import java.util.List;

public class ResponseCodeDaoImpl implements ResponseCodeDao {

    private BaseDaoHibernate baseDaoHibernate;

    public void setBaseDaoHibernate(BaseDaoHibernate baseDaoHibernate) {
        this.baseDaoHibernate = baseDaoHibernate;
    }

    @SuppressWarnings("unchecked")
    @Override
    public List<ResponseCode> getRespCodes() {
        return baseDaoHibernate.getList(ResponseCode.class);
    }

    @Override
    public ResponseCode getRespCode(String id) {
        return (ResponseCode) baseDaoHibernate.getObject(ResponseCode.class, id);
    }
}
