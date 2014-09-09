package com.ics.ssk.ego.dao;

import com.ics.ssk.ego.model.ResponseCode;
import java.util.List;

public interface ResponseCodeDao {

    List<ResponseCode> getRespCodes();

    ResponseCode getRespCode(String id);
}
