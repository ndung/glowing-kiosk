package com.ics.ssk.ego.converter;

import java.util.Collection;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import net.sourceforge.stripes.validation.LocalizableError;
import net.sourceforge.stripes.validation.TypeConverter;
import net.sourceforge.stripes.validation.ValidationError;

public class EmailTypeConverter implements TypeConverter<String> {

    private static final Pattern pattern1 = Pattern.compile("^[A-Z0-9._%+-]+@[A-Z0-9.-]+\\.[A-Z]{2,6}$", Pattern.CASE_INSENSITIVE);

    @Override
    public String convert(String input, Class<? extends String> arg1, Collection<ValidationError> errors) {
        String result = input;
        System.out.println("INPUT : " + input);
        Matcher matcher1 = pattern1.matcher(input);
        if (!matcher1.find()) {
            errors.add(new LocalizableError("converter.email.invalidEmail", result));
            result = null;
        }
        return result;
    }

    @Override
    public void setLocale(Locale arg0) {
        // TODO Auto-generated method stub
    }
}
