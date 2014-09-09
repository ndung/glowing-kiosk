package com.ics.ssk.ego.manager;

import com.ics.ssk.ego.model.ResponseCode;
import java.util.List;

public interface ResponseCodeManager {

    List<ResponseCode> getRespCodes();

    ResponseCode getRespCode(String id);

    void refreshRespCodeMap();
}
