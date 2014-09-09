package com.ics.ssk.ego.manager.impl;

import com.ics.ssk.ego.dao.ResponseCodeDao;
import com.ics.ssk.ego.manager.ResponseCodeManager;
import com.ics.ssk.ego.model.ResponseCode;
import com.ics.ssk.ego.util.MapUtil;
import java.util.List;

public class ResponseCodeManagerImpl implements ResponseCodeManager {

    private ResponseCodeDao responseCodeDao;

    public void setResponseCodeDao(ResponseCodeDao responseCodeDao) {
        this.responseCodeDao = responseCodeDao;
    }

    @Override
    public List<ResponseCode> getRespCodes() {
        return responseCodeDao.getRespCodes();
    }

    @Override
    public void refreshRespCodeMap() {
        for (ResponseCode respCode : getRespCodes()) {
            MapUtil.responseCode.put(respCode.getResponseCode(), respCode.getDescription());
        }
    }

    @Override
    public ResponseCode getRespCode(String id) {
        return responseCodeDao.getRespCode(id);
    }
}
