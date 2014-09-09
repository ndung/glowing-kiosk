package com.ics.ssk.ego.iso8583.processor;

import com.ics.ssk.ego.model.MessageUtil;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.StringUtil;
import org.jpos.iso.ISOException;
import org.jpos.iso.ISOMsg;

public class InquiryPurchaseTelcoProcessor implements Processor {

    @Override
    public ISOMsg prepareRequest(MessageUtil message) throws ISOException {
        String bit63 = "0000";
        bit63 += message.getBillerId();
        bit63 += message.getProductId();

        String bit48 = "62";
        bit48 += StringUtil.addLeadingZeroes(message.getPrefix(), 4);
        bit48 += StringUtil.padding(message.getCustomerId(), 10, StringUtil.ALIGN_LEFT);
        bit48 += StringUtil.addLeadingZeroes(message.getAmount(), 12);

        ISOMsg isoMsg = new ISOMsg();

        isoMsg.setMTI("0100");
        isoMsg.set(3, "921000");
        isoMsg.set(7, DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDDHHMMSS).substring(2));
        isoMsg.set(11, StringUtil.addLeadingZeroes(message.getStan(), 6));
        isoMsg.set(12, DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.HHMMSS));
        isoMsg.set(13, DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD).substring(2));
        isoMsg.set(18, "6015");
        isoMsg.set(37, message.getRefNumber());
        isoMsg.set(41, message.getKioskId());
        isoMsg.set(48, bit48);
        isoMsg.set(63, bit63);

        return isoMsg;
    }
}
