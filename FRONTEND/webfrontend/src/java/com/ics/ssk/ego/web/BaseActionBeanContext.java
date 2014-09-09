package com.ics.ssk.ego.web;

import com.ics.ssk.ego.system.UserSecurity;
import net.sourceforge.stripes.action.ActionBeanContext;

public class BaseActionBeanContext extends ActionBeanContext {

    public void setSession(String key, Object value) {
        getRequest().getSession().setAttribute(key, value);
    }

    public void setTimeSession(int i) {
        getRequest().getSession().setMaxInactiveInterval(i);
    }

    public Object getSession(String key) {
        return getRequest().getSession().getAttribute(key);
    }

    public void removeSession(String key) {
        getRequest().getSession().removeAttribute(key);
    }

    public void destroySession() {
        getRequest().getSession().invalidate();
    }

    public UserSecurity getUserSession() {
        return (UserSecurity) getSession("USER_SESSION");
    }
}
