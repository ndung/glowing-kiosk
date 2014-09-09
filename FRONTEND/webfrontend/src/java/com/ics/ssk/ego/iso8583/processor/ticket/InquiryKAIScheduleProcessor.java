package com.ics.ssk.ego.iso8583.processor.ticket;

import com.ics.ssk.ego.iso8583.processor.ProcessorKAI;
import com.ics.ssk.ego.util.DateTimeUtil;
import com.ics.ssk.ego.util.StringUtil;
import java.util.Date;
import java.util.Map;
import org.jpos.iso.ISOException;
import org.jpos.iso.ISOMsg;

public class InquiryKAIScheduleProcessor implements ProcessorKAI {

    @Override
    public ISOMsg prepareRequest(Map<String, Object> map) throws ISOException {
        String bit63 = "0000";
        bit63 += "06001";
        bit63 += "0001";

        String bit61 = StringUtil.padding((String) map.get(ProcessorKAI.DEWASA), 2, StringUtil.ALIGN_LEFT);
        bit61 += StringUtil.padding((String) map.get(ProcessorKAI.ANAK), 2, StringUtil.ALIGN_LEFT);
        bit61 += StringUtil.padding((String) map.get(ProcessorKAI.BAYI), 2, StringUtil.ALIGN_LEFT);

        String bit48 = StringUtil.padding((String) map.get(ProcessorKAI.ORIGINAL), 5, StringUtil.ALIGN_LEFT);
        bit48 += StringUtil.padding((String) map.get(ProcessorKAI.DESTINATION), 5, StringUtil.ALIGN_LEFT);
        bit48 += DateTimeUtil.convertDateToStringCustomized((Date) map.get(ProcessorKAI.DATE), DateTimeUtil.YYYYMMDD);

        ISOMsg isoMsg = new ISOMsg();

        isoMsg.setMTI("0100");
        isoMsg.set(3, "360001");
        isoMsg.set(7, DateTimeUtil.convertDateToStringCustomized((Date) map.get(ProcessorKAI.DATE_TRX), DateTimeUtil.YYMMDDHHMMSS).substring(2));
        isoMsg.set(11, StringUtil.addLeadingZeroes((String) map.get(ProcessorKAI.STAN), 6));
        isoMsg.set(12, DateTimeUtil.convertDateToStringCustomized((Date) map.get(ProcessorKAI.DATE_TRX), DateTimeUtil.HHMMSS));
        isoMsg.set(13, DateTimeUtil.convertDateToStringCustomized((Date) map.get(ProcessorKAI.DATE_TRX), DateTimeUtil.YYMMDD).substring(2));
        isoMsg.set(18, "6015");
        isoMsg.set(37, (String) map.get(ProcessorKAI.BANK_REF));
        isoMsg.set(41, StringUtil.padding((String) map.get(ProcessorKAI.KIOSK_ID), 8, StringUtil.ALIGN_LEFT));        
        isoMsg.set(48, bit48);
        isoMsg.set(63, bit63);
        isoMsg.set(61, bit61);

        return isoMsg;
    }
}
