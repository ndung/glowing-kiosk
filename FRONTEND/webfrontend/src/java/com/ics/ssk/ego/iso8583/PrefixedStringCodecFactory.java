package com.ics.ssk.ego.iso8583;

import java.nio.charset.Charset;
import org.apache.mina.core.session.IoSession;
import org.apache.mina.filter.codec.ProtocolCodecFactory;
import org.apache.mina.filter.codec.ProtocolDecoder;
import org.apache.mina.filter.codec.ProtocolEncoder;

/**
 * @author ICS Team
 *
 */
public class PrefixedStringCodecFactory implements  ProtocolCodecFactory {

    private final PrefixedStringEncoder encoder;
    private final PrefixedStringDecoder decoder;

    @Override
    public ProtocolDecoder getDecoder(IoSession session) throws Exception {
        return decoder;
    }

    @Override
    public ProtocolEncoder getEncoder(IoSession session) throws Exception {
        return encoder;
    }

    public PrefixedStringCodecFactory() {
        this(Charset.defaultCharset());
    }

    public PrefixedStringCodecFactory(Charset charset) {
        encoder = new PrefixedStringEncoder(charset);
        decoder = new PrefixedStringDecoder(charset);
    }

}
