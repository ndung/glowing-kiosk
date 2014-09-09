package com.ics.ssk.ego.ext;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import net.sourceforge.stripes.action.ErrorResolution;
import net.sourceforge.stripes.action.Resolution;
import net.sourceforge.stripes.exception.ActionBeanNotFoundException;
import net.sourceforge.stripes.exception.DefaultExceptionHandler;

public class MyExceptionHandler extends DefaultExceptionHandler {

    public Resolution catchActionBeanNotFound(ActionBeanNotFoundException exc, HttpServletRequest req, HttpServletResponse resp) {
        return new ErrorResolution(HttpServletResponse.SC_NOT_FOUND);
    }
}
