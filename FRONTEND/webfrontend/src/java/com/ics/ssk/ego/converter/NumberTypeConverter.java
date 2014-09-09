package com.ics.ssk.ego.converter;

import java.util.Collection;
import java.util.Locale;
import java.util.regex.Matcher;
import java.util.regex.Pattern;
import net.sourceforge.stripes.validation.LocalizableError;
import net.sourceforge.stripes.validation.TypeConverter;
import net.sourceforge.stripes.validation.ValidationError;

public class NumberTypeConverter implements TypeConverter<String> {

    private static final Pattern pattern1 = Pattern.compile("^\\d+$");
    private static final Pattern pattern2 = Pattern.compile("^[0-9]+$");

    @Override
    public String convert(String input, Class<? extends String> arg1, Collection<ValidationError> errors) {
        String result = input;
        Matcher matcher1 = pattern1.matcher(input);
        Matcher matcher2 = pattern2.matcher(input);
        if (!matcher1.matches() || !matcher2.matches()) {
            errors.add(new LocalizableError("error.invalidNumber", result));
            result = null;
        }
        return result;
    }

    @Override
    public void setLocale(Locale arg0) {
        // TODO Auto-generated method stub
    }
}
