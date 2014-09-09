package com.ics.ssk.ego.iso8583.processor;

import com.ics.ssk.ego.model.MessageUtil;
import org.jpos.iso.ISOException;
import org.jpos.iso.ISOMsg;

public interface Processor {

    public ISOMsg prepareRequest(MessageUtil message) throws ISOException;
}
