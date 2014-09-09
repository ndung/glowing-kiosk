package com.ics.ssk.ego.ext;

import java.util.Locale;
import javax.servlet.ServletContext;
import net.sourceforge.stripes.format.DefaultFormatterFactory;
import net.sourceforge.stripes.format.Formatter;
import net.sourceforge.stripes.integration.spring.SpringHelper;

public class SpringFormatterFactory extends DefaultFormatterFactory {

    @SuppressWarnings({"rawtypes", "unchecked"})
    @Override
    public Formatter getInstance(Class<? extends Formatter<?>> clazz, String formatType, String formatPattern, Locale locale) throws Exception {
        Formatter tc = super.getInstance(clazz, formatType, formatPattern, locale);
        ServletContext sc = getConfiguration().getServletContext();

        SpringHelper.injectBeans(tc, sc);

        return tc;
    }
}
