package com.ics.ssk.ego.ext;

import java.util.Locale;
import javax.servlet.ServletContext;
import net.sourceforge.stripes.integration.spring.SpringHelper;
import net.sourceforge.stripes.validation.DefaultTypeConverterFactory;
import net.sourceforge.stripes.validation.TypeConverter;

public class SpringTypeConverterFactory extends DefaultTypeConverterFactory {

    @SuppressWarnings("rawtypes")
    @Override
    public TypeConverter getInstance(Class<? extends TypeConverter> clazz, Locale locale) throws Exception {
        TypeConverter tc = super.getInstance(clazz, locale);
        ServletContext sc = getConfiguration().getServletContext();

        SpringHelper.injectBeans(tc, sc);

        return tc;
    }
}
