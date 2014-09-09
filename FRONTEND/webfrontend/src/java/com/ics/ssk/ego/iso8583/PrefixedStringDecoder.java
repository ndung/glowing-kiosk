package com.ics.ssk.ego.iso8583;

import java.nio.ByteBuffer;
import java.nio.charset.Charset;
import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.CumulativeProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolDecoderOutput;

/**
 *
 * @author ICS Team
 *
 */
public class PrefixedStringDecoder extends CumulativeProtocolDecoder {

    private Charset charset;

    public PrefixedStringDecoder(Charset charset) {
        this.charset = charset;
    }

    @Override
    protected boolean doDecode(IoSession session, IoBuffer in,
            ProtocolDecoderOutput out) throws Exception {

        if (session.getAttribute("length") == null) { // message length has not been pulled out from buffer
            // Failed if there is no sufficient bytes to read prefix
            if (in.remaining() < 4) {
                return false;
            }

            byte[] byteHolder = new byte[4];
            in.get(byteHolder, 0, 4);

            // Keep the length
            session.setAttribute("length", Integer.parseInt(new String(byteHolder)));
        }

        int messageLength = (Integer) session.getAttribute("length");

        // Failed if message length is not sufficient
        if (in.remaining() < messageLength) {
            return false;
        }
        session.removeAttribute("length"); // since there are sufficient bytes to read full message

        byte[] byteHolder = new byte[messageLength];
        in.get(byteHolder, 0, messageLength);

        out.write(charset.decode(ByteBuffer.wrap(byteHolder)).toString());
        return true;
    }

}
