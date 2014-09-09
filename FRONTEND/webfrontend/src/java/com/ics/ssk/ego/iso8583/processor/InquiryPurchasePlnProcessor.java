package com.ics.ssk.ego.iso8583.processor;

import com.ics.ssk.ego.model.MessageUtil;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.StringUtil;
import org.jpos.iso.ISOException;
import org.jpos.iso.ISOMsg;

public class InquiryPurchasePlnProcessor implements Processor {

    @Override
    public ISOMsg prepareRequest(MessageUtil message) throws ISOException {        

        String meterId = "";
        String costumerId = "";

        if (message.getCustomerId().length() == 11) {
            meterId = message.getCustomerId();
            message.setStep(0);            
        }
        if (message.getCustomerId().length() == 12) {
            costumerId = message.getCustomerId();
            message.setStep(1);
        }

        String bit48 = StringUtil.padding(meterId, 11, StringUtil.ALIGN_LEFT);
        bit48 += StringUtil.addLeadingZeroes(costumerId, 12);
        bit48 += message.getStep();

        ISOMsg isoMsg = new ISOMsg();

        isoMsg.setMTI("0100");
        isoMsg.set(3, "461000");
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
        isoMsg.set(63, "0000010020001");

        return isoMsg;
    }
}
