package com.ics.ssk.ego.iso8583.processor;

import com.ics.ssk.ego.model.MessageUtil;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.StringUtil;
import org.jpos.iso.ISOException;
import org.jpos.iso.ISOMsg;

public class PaymentKaiProcessor implements Processor {

    @Override
    public ISOMsg prepareRequest(MessageUtil message) throws ISOException {
        String bit63 = "";
        String bit48 = "";
        if (message.getBit63() != null && !message.getBit63().trim().equals("")) {
            bit63 = message.getBit63();
        } else {
            bit63 = "0000";
            bit63 += "06001";
            bit63 += "0001";
        }

        if (message.getBit48() != null && !message.getBit48().trim().equals("")) {
            bit48 = message.getBit48();
        }

        ISOMsg isoMsg = new ISOMsg();

        isoMsg.setMTI("0200");
        isoMsg.set(3, "860000");
        isoMsg.set(4, StringUtil.addLeadingZeroes(message.getAmount(), 12));
        isoMsg.set(7, DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDDHHMMSS).substring(2));
        isoMsg.set(11, StringUtil.addLeadingZeroes(message.getStan(), 6));
        isoMsg.set(12, DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.HHMMSS));
        isoMsg.set(13, DateTimeUtil.convertDateToStringCustomized(message.getDate(), DateTimeUtil.YYMMDD).substring(2));
        isoMsg.set(18, "6015");
        isoMsg.set(37, message.getRefNumber());
        isoMsg.set(41, message.getKioskId());
        isoMsg.set(43, message.getKioskLocation());
        isoMsg.set(48, bit48);
        isoMsg.set(63, bit63);

        return isoMsg;
    }
}
