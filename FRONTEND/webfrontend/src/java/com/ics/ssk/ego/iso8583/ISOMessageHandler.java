package com.ics.ssk.ego.iso8583;

import com.ics.ssk.ego.util.StringUtil;
import org.apache.log4j.Logger;
import org.jpos.iso.ISOMsg;

public class ISOMessageHandler {

    private static Logger logger = Logger.getLogger(ISOMessageHandler.class);

    public static ISOMsg unpackRequest(String message) throws Exception {
        try {
            int size = Integer.parseInt(message.substring(0, 4));
            message = message.substring(4);
            if (size == message.length()) {
                ISOMsg isoMsg = new ISOMsg();
                isoMsg.setPackager(PackagerFactory.getPackager());
                isoMsg.unpack(message.getBytes());
                isoMsg.dump(System.out, " ");
                return isoMsg;
            } else {
                logger.info("Panjang text tidak sesuai");
                return null;
            }
        } catch (Exception e) {
            e.printStackTrace();
            return null;
        }

    }

    public static String packRequest(ISOMsg message) throws Exception {
        message.setPackager(PackagerFactory.getPackager());
        message.dump(System.out, " ");

        String msg = new String(message.pack());
        return StringUtil.addLeadingZeroes(msg.length(), 4) + msg;
    }
}
