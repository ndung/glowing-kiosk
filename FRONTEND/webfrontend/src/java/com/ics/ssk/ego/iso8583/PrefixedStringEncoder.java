package com.ics.ssk.ego.iso8583;

import java.nio.charset.Charset;
import org.apache.mina.core.buffer.IoBuffer;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolEncoderAdapter;
import org.apache.mina.filter.codec.ProtocolEncoderOutput;

/**
 *
 * @author ICS Team
 *
 */
public class PrefixedStringEncoder extends ProtocolEncoderAdapter{

    protected Charset charset;
    
    public PrefixedStringEncoder(Charset charset) {
        this.charset = charset;
    }

    @Override
    public void encode(IoSession session, Object message, ProtocolEncoderOutput out) throws Exception {

        byte[] messageBytes = ((String) message).getBytes(charset);
        String lengthPrefix = pad(messageBytes.length); // 4 chars        
        IoBuffer buffer = IoBuffer.allocate(4 + messageBytes.length);
        buffer.putString(lengthPrefix, charset.newEncoder());
        buffer.put(messageBytes);
        buffer.flip();

        out.write(buffer);
    }

    private String pad(int length) {
        String paddedLength = String.valueOf(length);
        while (paddedLength.length() < 4) {
            paddedLength = "0" + paddedLength;
        }
        return paddedLength;
    }

}
